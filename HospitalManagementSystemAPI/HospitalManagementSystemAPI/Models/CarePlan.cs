using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class CarePlan
    {
        [Key]
        public int CarePlanId { get; set; }

        [Required]
        public int PatientId { get; set; } // Foreign key to Patient  

        [Required]
        [StringLength(200)]
        public string Condition { get; set; }

        public string? Description { get; set; }

        public List<CarePlanUpdates> CarePlanUpdates { get; set; }

        [Required]
        public DateTime DiagnosisDate { get; set; } = DateTime.Now;

        public DateTime? DateResolved { get; set; }

        //overloaded constructors for CarePlan class for creating care plans with different parameters
        public CarePlan(int patientId, string condiition, DateTime diagnosisDate)
        {
            PatientId = patientId;
            Condition = condiition;
            DiagnosisDate = diagnosisDate;
            CarePlanUpdates = new List<CarePlanUpdates>(); // Initialize as an empty list  
        }

        public CarePlan(int patientId, string condiition, string description, DateTime diagnosisDate, DateTime dateResolved)
        {
            PatientId = patientId;
            Condition = condiition;
            Description = description;
            DiagnosisDate = diagnosisDate;
            DateResolved = dateResolved;
            CarePlanUpdates = new List<CarePlanUpdates>(); // Initialize as an empty list  
        }
    }
}