using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemAPI.Models;


namespace HospitalManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetAll()
        {
            return await _context.InventoryItems.ToListAsync();
        }

        // POST: api/inventory
        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] InventoryItem item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;

            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // PUT: api/inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] InventoryItem updatedItem)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            item.Name = updatedItem.Name;
            item.Description = updatedItem.Description;
            item.QuantityInStock = updatedItem.QuantityInStock;
            item.UnitOfMeasure = updatedItem.UnitOfMeasure;
            item.ReorderLevel = updatedItem.ReorderLevel;
            item.Location = updatedItem.Location;
            item.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE: api/inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
