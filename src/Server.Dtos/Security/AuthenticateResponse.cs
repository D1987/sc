using Server.Entities.Security;

namespace Server.Dtos.Security
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
        }
    }
}
