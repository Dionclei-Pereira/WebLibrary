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
builder.Services.AddTransient<ITokenService, TokenServiceHMAC>();
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
using (var scope = app.Services.CreateScope()) {
    var seed = scope.ServiceProvider.GetRequiredService<SeedDB>();
    await seed.Seed();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<FilterMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
