using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [StringLength(24)]
        public string UserRef { get; set; } //unsure if should be int and have ref to User table

        [Required]
        [StringLength(50)]
        public string StaffType { get; set; }

        [StringLength(50)]
        public string Specialization{ get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public DateTime HireDate { get; set; }

        [StringLength(20, MinimumLength = 10)] // Added MinimumLength property  
        public string Phone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}