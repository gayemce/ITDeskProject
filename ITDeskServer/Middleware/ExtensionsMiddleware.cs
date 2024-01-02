using ITDeskServer.Context;
using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITDeskServer.Middleware;

//newlenmeden doğrudan çağrılacakları için static classlardır.
public static class ExtensionsMiddleware
{
    public static void AutoMigration(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }

    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            //userManager User ile alakalı CRUD işlemleri dahil bir çok işlemi içinde barındıran identity kütüphanesinden gelen bir service
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new()
                {
                    Email = "admin@admin.com",
                    UserName = "admin",
                    FirstName = "IT",
                    LastName = "Admin",
                    EmailConfirmed = true,
                }, "Password12*").Wait();
            }
        }
    }

    public static void CreateUsers(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var userInfos = new List<(string email, string userName, string firstName, string lastName, string password)>
        {
            ("dkaya@gmail.com", "dkaya", "Deniz", "Kaya", "Password12*"),
            ("sakca@gmail.com", "semra_akca", "Semra", "Akca", "Password12*"),
            ("cgocmen@gmail.com", "cgocmen", "Cemre", "Göcmen", "Password12*"),
            ("ayilmaz@gmail.com", "ayilmaz", "Ali", "Yılmaz", "Password12*"),
            ("akaraca@gmail.com", "akaracalı", "Ayşe", "Karacalı", "Password12*"),
            ("aozturk@gmail.com", "aozturk", "Ahmet", "Öztürk", "Password12*"),
        };

            foreach (var userInfo in userInfos)
            {
                var user = new AppUser
                {
                    Email = userInfo.email,
                    UserName = userInfo.userName,
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName,
                    EmailConfirmed = true,
                };

                userManager.CreateAsync(user, userInfo.password).Wait();
            }
        }
    }


    public static void CreateRoles(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                }).Wait();
            }
        }
    }

    public static void CreateUserRole(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();


            AppUser? user = userManager.Users.FirstOrDefault(p => p.Email == "admin@admin.com");
            if(user is not null)
            {
                AppRole? role = roleManager.Roles.FirstOrDefault(p => p.Name == "Admin");
                if(role is not null)
                {
                    bool userRoleExist = context.AppUserRoles.Any(p=> p.RoleId == role.Id && p.UserId == user.Id);
                    if (!userRoleExist)
                    {
                        AppUserRole appUserRole = new()
                        {
                            RoleId = role.Id,
                            UserId = user.Id,
                        };

                        context.AppUserRoles.Add(appUserRole);
                        context.SaveChanges();
                    }
                }
            }

        }
    }
}
