using LoginProject.Areas.Identity.Data;
using LoginProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace LoginProject.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    //public DbSet<ApplicationUser> applicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }

    public DbSet<LoginProject.Models.Aspnetusers> Aspnetusers { get; set; } = default!;

}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.Title).HasMaxLength(255);
        builder.Property(u => u.Address).HasMaxLength(255);
        builder.Property(u => u.Suburb).HasMaxLength(255);
        builder.Property(u => u.State).HasMaxLength(255);
        builder.Property(u => u.Postcode).HasMaxLength(4);
        builder.Property(u => u.Dob);
        builder.Property(u => u.Staff);
    }
}