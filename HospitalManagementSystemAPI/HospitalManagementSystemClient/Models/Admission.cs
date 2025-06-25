using System.ComponentModel.DataAnnotations;
using System;

namespace HospitalManagementSystemClient
{
    public class Admission
    {
        public int AdmissionId { get; set; }
        public int PatientId { get; set; } // ID of the patient being admitted
        public int BedId { get; set; } // ID of the bed assigned to the patient
        public DateTime AdmittedAt { get; set; } = DateTime.Now;
        public int AdmitBy { get; set; } // ID of the staff member who admitted the patien
        public int? DischargeBy { get; set; }
        public DateTime? DischargedAt { get; set; }
    }
}