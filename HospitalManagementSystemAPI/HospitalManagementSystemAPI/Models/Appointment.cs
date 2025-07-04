﻿using System.ComponentModel.DataAnnotations;

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
        [Key]
        public int AppointmentId { get; set; }

        [Required]
        public int PatientId { get; set; } // Foreign key to Patient

        public int? StaffId { get; set; } // Foreign key to Staff

        [Required]
        public DateTime ScheduledAt { get; set; }

        public int DurationMinutes { get; set; } = 30; // Default duration is 30 minutes

        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}