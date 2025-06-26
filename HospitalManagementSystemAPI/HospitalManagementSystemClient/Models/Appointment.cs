using System.ComponentModel.DataAnnotations;
using System;

namespace HospitalManagementSystemClient
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
        public int PatientId { get; set; } // Foreign key to Patient
        public int? StaffId { get; set; } // Foreign key to Staff
        public DateTime ScheduledAt { get; set; }
        public int DurationMinutes { get; set; } = 30; // Default duration is 30 minutes
        public string Reason { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}