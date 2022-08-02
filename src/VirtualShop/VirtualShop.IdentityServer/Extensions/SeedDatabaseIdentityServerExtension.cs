using VirtualShop.IdentityServer.Models.SeedDatabase.Contracts;

namespace VirtualShop.IdentityServer.Extensions
{
    public static class SeedDatabaseIdentityServerExtension
    {
        public static void SeedDatabaseIdentityServer(this IApplicationBuilder builder)
        {
            var seedInitialContent = async Task () => {
                using (var scope = builder.ApplicationServices.CreateScope())
                {
                    var provider = scope.ServiceProvider.GetService<IDatabaseSeedInitializer>();
                    await provider.InitializeSeedRoles();
                    await provider.InitializeSeedUsers();
                }
            };

            seedInitialContent();
        }
    }
}