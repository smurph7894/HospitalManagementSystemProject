using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemClient.Models
{
    public class CarePlan
    {
        public int CarePlanId { get; set; }
        public int PatientId { get; set; } // Foreign key to Patient  
        [StringLength(200)]
        public string Condition { get; set; }
        public string Description { get; set; }
        public List<CarePlanUpdates> CarePlanUpdates { get; set; }
        public DateTime? DiagnosisDate { get; set; }
        public DateTime? DateResolved { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public CarePlan()
        {
            CarePlanUpdates = new List<CarePlanUpdates>();
        }
    }
}