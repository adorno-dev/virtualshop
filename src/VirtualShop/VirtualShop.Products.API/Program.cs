using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VirtualShop.Products.API.Context;
using VirtualShop.Products.API.Repositories;
using VirtualShop.Products.API.Repositories.Contracts;
using VirtualShop.Products.API.Services;
using VirtualShop.Products.API.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services
       .AddControllers()
       .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtualShop.Products.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"'Bearer' [space] seu token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
         {
            new OpenApiSecurityScheme
            {
               Reference = new OpenApiReference
               {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
               },
               Scheme = "oauth2",
               Name = "Bearer",
               In= ParameterLocation.Header
            },
            new List<string> ()
         }
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddCors(options => 
    options.AddPolicy("CorsPolicy", builder => 
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin()));

builder.Services
       .AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options => {
            options.Authority = builder.Configuration.GetSection("VirtualShop.IdentityServer:ApplicationUrl").Value;
            options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
            // Development only!!!
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                options.BackchannelHttpHandler = handler;
            }
       });

builder.Services
       .AddAuthorization(options => {
            options.AddPolicy("ApiScope", policy => {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "VirtualShop");
            });
       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
