using System.Security.Claims;
using System.Text.Json.Serialization;
using FurnitureShop.DAL;
using FurnitureShop.DAL.Interfaces;
using FurnitureShop.DAL.Repository;
using FurnitureShop.Service.Implementations;
using FurnitureShop.Service.Interfaces;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication().AddCookie(option =>
{
    option.Cookie.Name = "user";
    option.Cookie.SecurePolicy = CookieSecurePolicy.None;
    option.Events.OnValidatePrincipal = async context => { };
});

builder.Services.AddAuthorization(option =>
{
    const string defaultPolicy = "defaultPolicy";
    option.AddPolicy(defaultPolicy, policyBuilder => { policyBuilder.RequireClaim(ClaimTypes.NameIdentifier); });
    option.DefaultPolicy = option.GetPolicy(defaultPolicy)!;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDatabaseContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IParameterRepository, ParameterRepository>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IParameterService, ParameterService>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IBannerService, BannerService>();

var app = builder.Build();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:5173");
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowCredentials();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image")),
    RequestPath = "/image"
});

using var scope = app.Services.CreateScope();

using var context = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

// context.Database.EnsureDeleted();
// context.Database.EnsureCreated();

app.Run();