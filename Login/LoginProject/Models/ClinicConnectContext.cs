using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClinicConnect.Models;

public partial class ClinicConnectContext : DbContext
{
    public ClinicConnectContext()
    {
    }

    public ClinicConnectContext(DbContextOptions<ClinicConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentType> AppointmentTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Practitioner> Practitioners { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost; uid=root; pwd=10RelationalDatabasesAreVeryUseful!; database=clinic_connect");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointment");

            entity.HasIndex(e => e.AppointmentTypeId, "appointment_type_appointment_fk");

            entity.HasIndex(e => e.PatientId, "patient_appointment_fk");

            entity.HasIndex(e => e.PractitionerId, "practitioner_appointment_fk");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentTypeId).HasColumnName("appointment_type_id");
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .HasColumnName("date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PractitionerId).HasColumnName("practitioner_id");

            entity.HasOne(d => d.AppointmentType).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AppointmentTypeId)
                .HasConstraintName("appointment_type_appointment_fk");

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentsNavigation)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("patient_appointment_fk");

            entity.HasOne(d => d.Practitioner).WithMany(p => p.AppointmentsNavigation)
                .HasForeignKey(d => d.PractitionerId)
                .HasConstraintName("practitioner_appointment_fk");
        });

        modelBuilder.Entity<AppointmentType>(entity =>
        {
            entity.HasKey(e => e.AppointmentTypeId).HasName("PRIMARY");

            entity.ToTable("appointment_type");

            entity.Property(e => e.AppointmentTypeId).HasColumnName("appointment_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("patient");

            entity.HasIndex(e => e.UserId, "users_patient_fk");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Appointments)
                .HasColumnType("text")
                .HasColumnName("appointments");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Patients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("users_patient_fk");
        });

        modelBuilder.Entity<Practitioner>(entity =>
        {
            entity.HasKey(e => e.PractitionerId).HasName("PRIMARY");

            entity.ToTable("practitioner");

            entity.HasIndex(e => e.UserId, "users_practitioner_fk");

            entity.Property(e => e.PractitionerId).HasColumnName("practitioner_id");
            entity.Property(e => e.Appointments)
                .HasColumnType("text")
                .HasColumnName("appointments");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Practitioners)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("users_practitioner_fk");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.Postcode).HasColumnName("postcode");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasColumnName("role");
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .HasColumnName("state");
            entity.Property(e => e.StreetAddress)
                .HasMaxLength(255)
                .HasColumnName("street_address");
            entity.Property(e => e.Suburb)
                .HasMaxLength(255)
                .HasColumnName("suburb");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("user_roles");

            entity.HasIndex(e => e.RoleId, "user_roles_roles_fk");

            entity.HasIndex(e => e.UserId, "user_roles_users_fk");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("user_roles_roles_fk");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_roles_users_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
