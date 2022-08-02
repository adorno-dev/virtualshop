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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "VirtualShop.Products.API", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Bearer",
        Scheme = "oauth2",
        BearerFormat = "JWT",
        Description = @"JWT authorization header using the bearer scheme. Enter 'Bearer'[space] <token>'",
        In = ParameterLocation.Header
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services
       .AddAuthentication("Bearer")
       .AddJwtBearer("Bearer", options => {
            options.Authority = builder.Configuration.GetSection("VirtualShop.IdentityServer:ApplicationUrl").Value;
            options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
