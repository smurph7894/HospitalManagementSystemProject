using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystemAPI.Models;

public class InventoryItem
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public int QuantityInStock { get; set; } = 0;

    [StringLength(50)]
    public string UnitOfMeasure { get; set; }

    [Required]
    public int ReorderLevel { get; set; } = 0;

    [Required]
    public bool isMedicine { get; set; } = false; //BIT of  0-False, 1-True in SQL

    [Required]
    public int TotalHospitalUsage { get; set; } = 0;

    [StringLength(100)]
    public string Location { get; set; }
    public bool IsMedication { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
}