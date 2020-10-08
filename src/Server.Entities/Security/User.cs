using Microsoft.AspNetCore.Identity;

namespace Server.Entities.Security
{
    public class User : IdentityUser  
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
