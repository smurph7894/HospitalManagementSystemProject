using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemClient
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
        public int VitalId { get; set; }
        public int PatientId { get; set; }

        [StringLength(50)]
        public string VitalType { get; set; } // e.g., 'HeartRate', 'BloodPressure'

        [StringLength(50)]
        public string Value { get; set; } // e.g., '72 bpm', '120/80 mmHg'

        [StringLength(20)]
        public string Unit { get; set; } // e.g., 'bpm', 'mmHg'
        public DateTime RecordedAt { get; set; } = DateTime.Now;
        public int RecordedBy { get; set; } // ID of the staff member who recorded the vital
    }
}