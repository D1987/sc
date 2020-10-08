using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Dal;
using Server.Dtos.Security;
using Server.Entities.Security;
using Server.Services.Helpers;
using Server.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private ServerContext _context;

        public UserService(IOptions<AppSettings> appSettings, ServerContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public async Task<ActionResult<AuthenticateResponse>> Authenticate(AuthenticateRequest model)
        {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                return null;

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == model.UserName);

            // return null if user not found
            if (user == null) return null;

            // check if password is correct
            if (!VerifyPasswordHash(user, user.PasswordHash, model.Password))
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private static bool VerifyPasswordHash(User user, string hashedPassword, string confirmPassword)
        {
            var passwordHasher = new PasswordHasher<User>(null);
            bool verified = false;
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, confirmPassword);
            if (result == PasswordVerificationResult.Success) verified = true;
            else if (result == PasswordVerificationResult.SuccessRehashNeeded) verified = true;
            else if (result == PasswordVerificationResult.Failed) verified = false;

            return verified;
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
