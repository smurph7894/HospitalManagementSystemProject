using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class CarePlanUpdateDto
    {
        public int CarePlanId { get; set; }

        public string Condition { get; set; }

        public string? Description { get; set; }

        public DateTime? DiagnosisDate { get; set; }

        public DateTime? DateResolved { get; set; }

        public CarePlanUpdate_UpdateDto? CarePlanUpdate { get; set; }
    }

    public class CarePlanUpdate_UpdateDto
    {
        public int AppointmentId { get; set; } // Foreign key to Appointment  
        public string Notes { get; set; } // Notes or updates regarding the care plan  
    }
}
