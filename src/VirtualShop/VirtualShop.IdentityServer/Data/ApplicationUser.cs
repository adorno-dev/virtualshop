using Microsoft.AspNetCore.Identity;

namespace VirtualShop.IdentityServer.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
    }
}