using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class AdmissionDto
    {
        public int AdmissionId { get; set; }
        public int? BedId { get; set; } 
        public int? AdmitBy { get; set; }
        public int? DischargeBy { get; set; }
        public DateTime? DischargedAt { get; set; }
    }
}
