using System.Net;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualShop.IdentityServer.Configuration;
using VirtualShop.IdentityServer.Data;
using VirtualShop.IdentityServer.Extensions;
using VirtualShop.IdentityServer.Models.SeedDatabase;
using VirtualShop.IdentityServer.Models.SeedDatabase.Contracts;
using VirtualShop.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services
       .AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<AppDbContext>()
       .AddDefaultTokenProviders();

var identityServerBuilder = builder.Services
       .AddIdentityServer(options =>
       {
           options.Events.RaiseErrorEvents = true;
           options.Events.RaiseInformationEvents = true;
           options.Events.RaiseFailureEvents = true;
           options.Events.RaiseSuccessEvents = true;
           options.EmitStaticAudienceClaim = true;
       })
       .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
       .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
       .AddInMemoryClients(IdentityConfiguration.Clients)
       .AddAspNetIdentity<ApplicationUser>();
       identityServerBuilder.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDatabaseSeedInitializer, DatabaseIdentityServerInitializer>();
builder.Services.AddScoped<IProfileService, ProfileAppService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.SeedDatabaseIdentityServer();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();