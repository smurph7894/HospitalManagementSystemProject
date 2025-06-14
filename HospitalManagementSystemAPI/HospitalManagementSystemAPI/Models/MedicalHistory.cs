using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;

namespace HospitalManagementSystemAPI.Models
{
    public class MedicalHistory
    {
        public int HistoryId { get; set; }

        [Required]
        public int PatientId { get; set; }

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