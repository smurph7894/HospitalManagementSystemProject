using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemClient.Models
{
    public  class Patient
    {
        public int PatientId { get; set; }

        public string PatientOrgId { get; set; } // Id connected to MongoDB for login

        [Required]
        [StringLength(100, MinimumLength = 2)] // Added MinimumLength property  
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)] // Added MinimumLength property  
        public string LastName { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)] // Added MinimumLength property
        public char Gender { get; set; }

        [StringLength(20, MinimumLength = 10)] // Added MinimumLength property  
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(200)]
        public string EmergencyContactName { get; set; }

        [StringLength(20, MinimumLength = 10)] // Added MinimumLength property  
        public string EmergencyContactPhone { get; set; }

        [StringLength(200)]
        public string InsuranceProvider { get; set; }

        [StringLength(100)]
        public string InsurancePolicyNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
