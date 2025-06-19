using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystemAPI.Models
{
    public enum TransactionType
    {
        Received,
        Dispensed,
        Adjustment
    }

    public class InventoryTransaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required] 
        public int ItemId { get; set; } // item ID from InventoryItem

        [Required]
        public int ChangeQuantity { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public int PerformedBy { get; set; } // ID of the staff member who performed the transaction

        [Required]
        public DateTime TransactionDateTime { get; set; } = DateTime.Now;

        [StringLength(500, ErrorMessage = "Remarks cannot exceed 500 characters.")]
        public string Remarks { get; set; }
    }
}
