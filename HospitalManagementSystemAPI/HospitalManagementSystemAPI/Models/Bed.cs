using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public enum BedStatus
    {
        Available,
        Occupied,
        Maintenance

    }
    public class Bed
    {
        [Key]
        public int BedId { get; set; }

        [Required]
        [StringLength(100)]
        public string Ward { get; set; }

        [Required]
        [StringLength(50)]
        public string BedNumber { get; set; }

        [Required]
        public BedStatus Status { get; set; } 
    }
}
