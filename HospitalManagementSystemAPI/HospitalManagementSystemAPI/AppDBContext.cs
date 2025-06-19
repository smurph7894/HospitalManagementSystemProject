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
            //defaults are handled in the models, so we don't need to set them here

            //Admissions
            modelBuilder.Entity<Admission>()
                .Property(e => e.AdmittedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //can be null, but if it has a value, it should be UTC
            modelBuilder.Entity<Admission>()
                .Property(e => e.DischargedAt).HasConversion(
                    v => v,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);

            modelBuilder.Entity<Admission>()
                .Property(e => e.DischargeBy).HasConversion(
                   v => v,
                   v => !v.HasValue ? (int?)null : v.Value);

            //Appointments
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

            //Beds - none needed
            //Departments - none needed

            //InventoryItems
            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.CreatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<InventoryItem>()
                .Property(e => e.UpdatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //InventoryTransactions
            modelBuilder.Entity<InventoryTransaction>()
                .Property(e => e.TransactionDate).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //MedicalHistories
            modelBuilder.Entity<MedicalHistory>()
                .Property(e => e.DateRecorded).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //Patients
            modelBuilder.Entity<Patient>()
                .Property(e => e.DOB).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Patient>()
                .Property(e => e.CreatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Patient>()
                .Property(e => e.UpdatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //ReportsHistory
            modelBuilder.Entity<ReportsHistory>()
                .Property(e => e.GeneratedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //Staffs
            modelBuilder.Entity<Staff>()
                .Property(e => e.HireDate).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Staff>()
                .Property(e => e.CreatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            modelBuilder.Entity<Staff>()
                .Property(e => e.UpdatedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            //Vitals
            modelBuilder.Entity<Vitals>()
                .Property(e => e.RecordedAt).HasConversion(
                    v => v,
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}



