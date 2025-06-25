using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class CarePlanUpdates
    {
        [Key]
        public int CarePlanUpdateId { get; set; }

        [Required]
        public int AppointmentId { get; set; } // Foreign key to Appointment  

        [Required]
        public string Notes { get; set; } // Notes or updates regarding the care plan  

        public int CarePlanId { get; set; } // Foreign key to CarePlan
    }
}
