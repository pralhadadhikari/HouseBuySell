using HouseBuySell.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseBuySell.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

       
        builder.Entity<ApplicationUser>()
            .Property(e => e.Address)
            .HasMaxLength(50);

        builder.Entity<ApplicationUser>()
            .Property(e => e.IsActive)
            .HasDefaultValue(true);

        builder.Entity<ApplicationUser>()
            .Property(e => e.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");


        builder.Entity<IdentityRole>()
            .ToTable("Roles")
            .Property(p => p.Id)
            .HasColumnName("RoleId");
    }
}
