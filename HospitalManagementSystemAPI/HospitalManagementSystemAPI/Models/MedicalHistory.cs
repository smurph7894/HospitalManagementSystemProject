using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace HospitalManagementSystemAPI.Models
{
    public class MedicalHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int PatientId { get; set; } // Foreign key to Patient

        [Required]
        [StringLength(200)] 
        public string Condition { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DateRecorded { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string DocumentPath { get; set; }
    }
}