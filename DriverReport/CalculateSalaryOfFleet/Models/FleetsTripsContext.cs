using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CalculateSalaryOfFleet.Models
{
    public partial class FleetsTripsContext : DbContext
    {
        public FleetsTripsContext()
        {
        }

        public FleetsTripsContext(DbContextOptions<FleetsTripsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DeliveryCustomers> DeliveryCustomers { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Excels> Excels { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Trucks> Trucks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MSI\\SQL_EXPRESS;Database=FleetsTrips;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryCustomers>(entity =>
            {
                entity.HasKey(e => e.DeliveryCustCode);

                entity.Property(e => e.DeliveryCustCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DeliveryAddress).HasMaxLength(200);

                entity.Property(e => e.ServiceLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.HasKey(e => e.DriverIcno);

                entity.Property(e => e.DriverIcno)
                    .HasColumnName("DriverICNo")
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DriverName).HasMaxLength(100);

                entity.Property(e => e.DriverPhone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TruckId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Truck)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.TruckId)
                    .HasConstraintName("FK_Drivers_Trucks");
            });

            modelBuilder.Entity<Excels>(entity =>
            {
                entity.HasKey(e => e.ExcelCode);

                entity.Property(e => e.ExcelFileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExcelUploadedDate).HasColumnType("date");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobNo);

                entity.Property(e => e.JobNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DriverIcno)
                    .HasColumnName("DriverICNo")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DriverIcnoNavigation)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.DriverIcno)
                    .HasConstraintName("FK_Jobs_Drivers1");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.AtdcompleteDate)
                    .HasColumnName("ATDCompleteDate")
                    .HasColumnType("date");

                entity.Property(e => e.DeliveryCustCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TranportAgent)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeliveryCustCodeNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryCustCode)
                    .HasConstraintName("FK_Orders_DeliveryCustomers");

                entity.HasOne(d => d.JobNoNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.JobNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Jobs");
            });

            modelBuilder.Entity<Trucks>(entity =>
            {
                entity.HasKey(e => e.TruckId);

                entity.Property(e => e.TruckId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.TruckType).HasMaxLength(30);
            });
        }
    }
}
