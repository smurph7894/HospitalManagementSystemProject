using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class ReportsHistory
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        [StringLength(100)]
        public string ReportType { get; set; }  // e.g., 'Lab Report', 'Radiology Report', etc.

        [StringLength(int.MaxValue)]
        public string? Parameters { get; set; }  // JSON or XML string containing report parameters

        [Required]
        public DateTime GeneratedAt { get; set; } = DateTime.Now;

        [Required]
        public int GeneratedBy { get; set; }  // ID of the staff member who generated the report

        [StringLength(500)]
        public string? FilePath { get; set; }  // Path to the generated report file, if applicable
    }
}