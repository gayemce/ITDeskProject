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
builder.Services.AddIdentityCore<AppUser>(opt =>
{
    opt.Password.RequiredLength = 6;

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ExtensionsMiddleware.AutoMigration(app);

ExtensionsMiddleware.CreateFirstUser(app);

app.Run();
