using HospitalManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //public DbSet<Admission> Admissions { get; set; }
        //public DbSet<Appointment> Appointment { get; set; }
        //public DbSet<Bed> Beds { get; set; }
        //public DbSet<Department> Departments { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
    //    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    //    public DbSet<MedicalHistory> MedicalHistories { get; set; }
    //    public DbSet<Patient> Patients { get; set; }
    //    public DbSet<ReportsHistory> ReportsHistory { get; set; }
    //    public DbSet<Staff> Staffs { get; set; }
    //    public DbSet<Vitals> Vitals { get; set; }
    }
}



