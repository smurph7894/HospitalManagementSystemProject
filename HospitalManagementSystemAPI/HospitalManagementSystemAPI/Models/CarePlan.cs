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

        public DateTime? DiagnosisDate { get; set; }

        public DateTime? DateResolved { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //overloaded constructors for CarePlan class for creating care plans with different parameters
        public CarePlan(int patientId, string condiition)
        {
            PatientId = patientId;
            Condition = condiition;
            CreatedAt = DateTime.Now; // Default to current date if not provided
            CarePlanUpdates = new List<CarePlanUpdates>(); // Initialize as an empty list  
        }

        public CarePlan(int patientId, string condiition, string description, DateTime diagnosisDate, DateTime dateResolved)
        {
            PatientId = patientId;
            Condition = condiition;
            Description = description;
            DiagnosisDate = diagnosisDate;
            DateResolved = dateResolved;
            CreatedAt = DateTime.Now; // Default to current date if not provided
            CarePlanUpdates = new List<CarePlanUpdates>(); // Initialize as an empty list  
        }
    }
}