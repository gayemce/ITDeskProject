using ITDeskServer.Context;
using ITDeskServer.Middleware;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Connection bilgisi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer("Data Source=DESKTOP-I7G56NT\\SQLEXPRESS;Initial Catalog=YMYP_ITDeskDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

//Identity kütüphanesinin DbContext ile baðlý olduðunu bildirmek için:
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 6;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 2;
    opt.Lockout.AllowedForNewUsers = true;

}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ExtensionsMiddleware.AutoMigration(app);

ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
