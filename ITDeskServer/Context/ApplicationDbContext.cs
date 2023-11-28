using ITDeskServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITDeskServer.Context;

public sealed class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
  public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.UserId);
        builder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);
        builder.Entity<IdentityUserClaim<Guid>>().HasKey(x => x.Id);

        builder.Ignore<IdentityRoleClaim<Guid>>();
        //builder.Ignore<IdentityUserClaim<Guid>>(); SignIn Manager kullanılacak
        //builder.Ignore<IdentityUserLogin<Guid>>(); SignIn Manager kullanılacak
        //builder.Ignore<IdentityUserToken<Guid>>(); SignIn Manager kullanılacak
        builder.Ignore<IdentityUserRole<Guid>>();
    }
}
