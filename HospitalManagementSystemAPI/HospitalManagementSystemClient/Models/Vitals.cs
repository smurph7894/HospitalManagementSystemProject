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
        public DateTime RecordedAt { get; set; }
        public int RecordedBy { get; set; }
        public string Unit { get; set; }
        public string Value { get; set; }
        public string VitalType { get; set; }
    }
}