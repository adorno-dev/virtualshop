using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VirtualShop.IdentityServer.Data;

namespace VirtualShop.IdentityServer.Services
{
    public class ProfileAppService : IProfileService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory;

        public ProfileAppService
        (
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(id);
            var userClaims = await userClaimsPrincipalFactory.CreateAsync(user);

            var claims = userClaims.Claims.ToList();
            claims.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));

            if (userManager.SupportsUserRole)
            {
                var roles = await userManager.GetRolesAsync(user);

                foreach (string role in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, role));

                    if (roleManager.SupportsRoleClaims)
                    {
                        IdentityRole identityRole = await roleManager.FindByNameAsync(role);

                        if (identityRole != null)
                            claims.AddRange(await roleManager.GetClaimsAsync(identityRole));
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userid = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(userid);

            context.IsActive = user is not null;
        }
    }
}
