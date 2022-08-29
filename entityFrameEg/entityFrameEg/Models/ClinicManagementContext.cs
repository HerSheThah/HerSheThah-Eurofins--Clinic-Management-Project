using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace entityFrameEg.Models
{
    public partial class ClinicManagementContext : DbContext
    {
        public ClinicManagementContext()
        {
        }

        public ClinicManagementContext(DbContextOptions<ClinicManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<DoctorDetail> DoctorDetails { get; set; } = null!;
        public virtual DbSet<PatientDetail> PatientDetails { get; set; } = null!;
        public virtual DbSet<StaffDetail> StaffDetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=ClinicManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("appointments");

                entity.Property(e => e.Appointmentid)
                    .ValueGeneratedNever()
                    .HasColumnName("appointmentid");

                entity.Property(e => e.AppointmentTimeFrom)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("appointmentTimeFrom");

                entity.Property(e => e.AppointmentTimeTo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("appointmentTimeTo");

                entity.Property(e => e.Doctor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("doctor");

                entity.Property(e => e.PatientId).HasColumnName("patientId");

                entity.Property(e => e.SpecializationId).HasColumnName("specializationId");

                entity.Property(e => e.VisitDate)
                    .HasColumnType("datetime")
                    .HasColumnName("visitDate");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__appointme__patie__5DCAEF64");

                entity.HasOne(d => d.Specialization)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.SpecializationId)
                    .HasConstraintName("FK__appointme__speci__5EBF139D");
            });

            modelBuilder.Entity<DoctorDetail>(entity =>
            {
                entity.HasKey(e => e.SpecializationId)
                    .HasName("PK__doctorDe__7E8C9BC7A32CB558");

                entity.ToTable("doctorDetails");

                entity.Property(e => e.SpecializationId)
                    .ValueGeneratedNever()
                    .HasColumnName("specializationID");

                entity.Property(e => e.DoctorId).HasColumnName("doctorId");

                entity.Property(e => e.EndTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("endTime");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sex");

                entity.Property(e => e.Specialization)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("specialization");

                entity.Property(e => e.StartTime)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("startTime");
            });

            modelBuilder.Entity<PatientDetail>(entity =>
            {
                entity.HasKey(e => e.PatientId)
                    .HasName("PK__patientD__A17005ECFC944E11");

                entity.ToTable("patientDetails");

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("patientId");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Dateofbirth)
                    .HasColumnType("date")
                    .HasColumnName("dateofbirth");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Sex)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("sex");
            });

            modelBuilder.Entity<StaffDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("staffDetails");

                entity.HasIndex(e => e.Username, "UQ__staffDet__536C85E4E5C9EDF4")
                    .IsUnique();

                entity.Property(e => e.Firstname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
