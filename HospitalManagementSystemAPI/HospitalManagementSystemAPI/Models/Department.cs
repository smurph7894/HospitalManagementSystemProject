using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)] // Added MinimumLength property  
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}