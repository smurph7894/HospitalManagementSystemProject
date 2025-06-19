namespace HospitalManagementSystemAPI.Models
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public string UnitOfMeasure { get; set; }
        public int ReorderLevel { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}