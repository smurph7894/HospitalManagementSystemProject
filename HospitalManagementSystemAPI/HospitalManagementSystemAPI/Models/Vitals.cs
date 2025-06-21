using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public enum VitalType
    {
        HeartRate,
        BloodPressure,
        Temperature,
        RespiratoryRate,
        OxygenSaturation,
        BloodGlucose
    }

    public class Vitals
    {
        [Key]
        public int VitalId { get; set; }
        [Required]
        public int PatientId { get; set; }
        [Required]
        [StringLength(50)]
        public VitalType VitalType { get; set; } 

        [Required]
        [StringLength(50)]
        public string Value { get; set; }  // e.g., '72 bpm', '120/80 mmHg'

        [Required]
        [StringLength(20)]
        public string Unit { get; set; }  // e.g., 'bpm', 'mmHg'

        [Required]
        public DateTime RecordedAt { get; set; } = DateTime.Now;

        [Required]
        public int RecordedBy { get; set; }  // ID of the staff member who recorded the vital

    }
}