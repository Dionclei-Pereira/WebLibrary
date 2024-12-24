using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebLibrary.Data;
using WebLibrary.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connection = "server=localhost;userid=root;password=12345678;database=blognow";
builder.Services.AddDbContext<LibraryContext>(options => options.UseNpgsql());
builder.Services.AddIdentity<LibaryUser, IdentityRole>(options => {

}).AddEntityFrameworkStores<LibraryContext>().AddDefaultTokenProviders();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
