using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public class StaffDto
    {
        public int StaffId { get; set; }
        public string? Name { get; set; }

        public string? StaffType { get; set; }

        public string? Specialization { get; set; }

        public int? DepartmentId { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
