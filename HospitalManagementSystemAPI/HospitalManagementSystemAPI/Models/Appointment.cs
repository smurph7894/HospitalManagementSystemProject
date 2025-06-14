using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public enum Status
    {
        Scheduled,
        InProgress,
        Completed,
        Cancelled,
        NoShow
    }
    public class Appointment
    {
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; } // Foreign key to Patient

        [Required]
        public int StaffId { get; set; } // Foreign key to Staff

        [Required]
        public DateTime ScheduledAt { get; set; }

        public int DurationMinutes { get; set; } = 30; // Default duration is 30 minutes

        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        [StringLength(50)]
        public Status Status { get; set; }

        [Required]
        public int CreatedBy { get; set; } // ID of the staff member who created the appointment

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}