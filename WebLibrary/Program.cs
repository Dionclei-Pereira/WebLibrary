using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebLibrary.Data;
using WebLibrary.Entities;
using WebLibrary.Services;
using WebLibrary.Services.Interfaces;
using WebLibrary.Services.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connection = "server=localhost;userid=root;password=12345678;database=library";
builder.Services.AddDbContext<LibraryContext>(options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = null;
}).AddEntityFrameworkStores<LibraryContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ITokenService, TokenServiceHMAC>();
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SeedDB>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    
    c.AddSecurityDefinition("Bearer", new() {
        Description = "Insert: {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Logging.AddConsole();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope()) {
    var seed = scope.ServiceProvider.GetRequiredService<SeedDB>();
    await seed.Seed();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin")) {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var userAdmin = await userManager.FindByEmailAsync("dionclei2@gmail.com");
    if (userAdmin != null && !(await userManager.GetRolesAsync(userAdmin)).Contains("Admin")) {
        await userManager.AddToRoleAsync(userAdmin, "Admin");
    }
}

app.UseMiddleware<FilterMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
