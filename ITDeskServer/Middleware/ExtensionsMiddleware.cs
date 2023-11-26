﻿using ITDeskServer.Context;
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
                    Email = "test@test.com",
                    UserName = "test",
                    Name = "Gaye",
                    LastName = "Tekin"
                }, "Password12*").Wait();
            }
        }
    }
}