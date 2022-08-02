using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using VirtualShop.IdentityServer.Configuration;
using VirtualShop.IdentityServer.Data;
using VirtualShop.IdentityServer.Models.SeedDatabase.Contracts;

namespace VirtualShop.IdentityServer.Models.SeedDatabase
{
    public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task InitializeSeedRoles()
        {
            // Create admin role if it doesn't exist.
            if (!(await roleManager.RoleExistsAsync(IdentityConfiguration.Admin)))
                await roleManager.CreateAsync(
                    new IdentityRole { Name = IdentityConfiguration.Admin, NormalizedName = IdentityConfiguration.Admin.ToUpper() });

            // Create client role if it doesn't exist.
            if (!(await roleManager.RoleExistsAsync(IdentityConfiguration.Client)))
                await roleManager.CreateAsync(
                    new IdentityRole { Name = IdentityConfiguration.Client, NormalizedName = IdentityConfiguration.Client.ToUpper() });
        }

        public async Task InitializeSeedUsers()
        {
            if (await userManager.FindByEmailAsync("admin@virtualshop.com") is null)
            {
                var admin = new ApplicationUser
                {
                    NormalizedEmail = "ADMIN",
                    UserName = "admin",
                    Email = "admin@virtualshop.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "User",
                    LastName = "Admin",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if ((await userManager.CreateAsync(admin, "Change$2022")).Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin);
                    await userManager.AddClaimsAsync(admin, new Claim[] {
                        new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                    });
                }
            }

            if (await userManager.FindByEmailAsync("client@virtualshop.com") is null)
            {
                var client = new ApplicationUser
                {
                    NormalizedEmail = "CLIENT",
                    UserName = "client",
                    Email = "client@virtualshop.com",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 12345-6789",
                    FirstName = "User",
                    LastName = "Client",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                if ((await userManager.CreateAsync(client, "Change$2022")).Succeeded)
                {
                    await userManager.AddToRoleAsync(client, IdentityConfiguration.Client);
                    await userManager.AddClaimsAsync(client, new Claim[] {
                        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, client.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, client.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    });
                }
            }
        }
    }
}