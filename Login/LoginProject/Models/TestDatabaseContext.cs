using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginProject.Models;

public partial class TestDatabaseContext : DbContext
{
    public TestDatabaseContext()
    {
    }

    public TestDatabaseContext(DbContextOptions<TestDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aspnetrole> Aspnetroles { get; set; }

    public virtual DbSet<Aspnetuserrole> Aspnetuserroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost; uid=root; pwd=10RelationalDatabasesAreVeryUseful!; database=test_database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aspnetrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("aspnetroles");

            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<Aspnetuserrole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PRIMARY");

            entity.ToTable("aspnetuserroles");

            entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Aspnetuserroles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
