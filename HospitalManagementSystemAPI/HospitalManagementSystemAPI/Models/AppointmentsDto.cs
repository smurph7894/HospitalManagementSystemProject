using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class AppointmentsDto
    {
        public int AppointmentId { get; set; }
        public int? PatientId { get; set; } 
        public int? StaffId { get; set; }
        public DateTime? ScheduledAt { get; set; }
        public int? DurationMinutes { get; set; } = 30; 
        public string? Reason { get; set; }
        public Status? Status { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
