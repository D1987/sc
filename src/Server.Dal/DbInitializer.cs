using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Entities.Security;
using System.Linq;

namespace Server.Dal
{
    public static class DbInitializer
    {
        public static void Initialize(ServerContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        public static void Seed(ServerContext context)
        {
            SeedUsers(context, "serveradmin@talrace.com", "Tor21log!");
        }

        private static void SeedUsers(ServerContext context, string email, string userPassword)
        {
            var user = context.Set<User>().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                var aplicationUser = new User
                {
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpperInvariant(),
                    NormalizedEmail = email.ToUpperInvariant(),
                    SecurityStamp = email.ToString(),
                };

                var passwordHasher = new PasswordHasher<User>(null);

                aplicationUser.PasswordHash = passwordHasher.HashPassword(aplicationUser, userPassword);
                
                context.Add(aplicationUser);

                context.SaveChanges();
            }
        }
    }
}
