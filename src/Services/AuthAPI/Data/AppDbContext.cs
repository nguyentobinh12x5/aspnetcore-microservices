using System;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Seed roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
        );
    }


}
