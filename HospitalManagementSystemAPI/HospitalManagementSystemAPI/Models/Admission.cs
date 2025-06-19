using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class Admission
    {
        public int AdmissionId { get; set; }

        [Required]
        public int PatientId { get; set; } // ID of the patient being admitted

        [Required]
        public int BedId { get; set; } // ID of the bed assigned to the patient

        public DateTime AdmittedAt { get; set; } = DateTime.Now;

        [Required]
        public int AdmitBy { get; set; } // ID of the staff member who admitted the patient

        public int? DischargeBy { get; set; }

        public DateTime? DischargedAt { get; set; }
    }
}