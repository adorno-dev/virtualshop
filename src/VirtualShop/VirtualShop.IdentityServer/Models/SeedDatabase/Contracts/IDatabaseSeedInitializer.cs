namespace VirtualShop.IdentityServer.Models.SeedDatabase.Contracts
{
    public interface IDatabaseSeedInitializer
    {
        Task InitializeSeedRoles();
        Task InitializeSeedUsers();
    }
}