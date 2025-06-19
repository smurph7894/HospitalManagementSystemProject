using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.SignalR;



namespace HospitalManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<InventoryHub> _hubContext;

        public InventoryController(AppDbContext context, IHubContext<InventoryHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // GET: api/inventory
        [HttpGet("all")]
        public async Task<ActionResult>GetInventory()
        {
            var inventory = await _context.InventoryItems.ToListAsync();
            return Ok(inventory);
        }

        // POST: api/inventory
        [HttpPost("add")]
        public async Task<IActionResult> AddInventory([FromBody] InventoryItem item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            _context.InventoryItems.Add(item);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", item);
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
            await _hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", item);
            return Ok(item);
        }

        // DELETE: api/inventory/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.InventoryItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveInventoryDeletion", item);

            return Ok();
        }
    }
}
