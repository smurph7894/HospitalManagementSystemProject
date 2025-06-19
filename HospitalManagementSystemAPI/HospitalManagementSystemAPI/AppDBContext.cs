using HospitalManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<ReportsHistory> ReportsHistory { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Vitals> Vitals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Admissions
            modelBuilder.Entity<Admission>()
                .Property(e => e.AdmittedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Admission>()
                .Property(e => e.DischargedAt).HasConversion(
                    v => v,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

            modelBuilder.Entity<Admission>()
                .Property(e => e.DischargeBy).HasConversion(
                   v => v,
                   v => !v.HasValue ? (int?)null : v.Value);

            //Appointments - duration default handled in Appointment class
            modelBuilder.Entity<Appointment>()
                .Property(e => e.ScheduledAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Appointment>()
                .Property(e => e.CreatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Appointment>()
                .Property(e => e.UpdatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //Beds
            modelBuilder.Entity<Bed>()
                .Property(e => e.CreatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}



