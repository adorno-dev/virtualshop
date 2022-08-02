using Microsoft.AspNetCore.Authentication;
using VirtualShop.Web.Services;
using VirtualShop.Web.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("Products.API", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetSection("ServiceUri:Products.API").Value);
})
.ConfigurePrimaryHttpMessageHandler(() => {
    var handler = new HttpClientHandler();
    if (builder.Environment.IsDevelopment())
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
    return handler;
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services
       .AddAuthentication(options => {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc"; })
       .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
       .AddOpenIdConnect("oidc", options => {
            options.Authority = builder.Configuration.GetSection("ServiceUri:IdentityServer").Value;
            options.ClientSecret = builder.Configuration.GetSection("Client:Secret").Value;
            options.ClientId = "VirtualShop";
            options.GetClaimsFromUserInfoEndpoint = true;
            options.ResponseType = "code";
            options.ClaimActions.MapJsonKey("role", "role", "role");
            options.ClaimActions.MapJsonKey("sub", "sub", "sub");
            options.TokenValidationParameters.NameClaimType = "name";
            options.TokenValidationParameters.RoleClaimType = "role";
            options.Scope.Add("VirtualShop");
            options.SaveTokens = true;
       });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
