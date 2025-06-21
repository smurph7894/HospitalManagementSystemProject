using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BedController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/bed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bed>>> GetBeds()
        {
            try
            {
                var beds = await _context.Beds.ToListAsync();
                return Ok(beds);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving beds: {ex.Message}");
            }
        }

        // GET: api/bed/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Bed>> GetBed(int id)
        {
            try
            {
                var bed = await _context.Beds.FindAsync(id);
                if (bed == null)
                {
                    return NotFound($"Bed with ID {id} not found.");
                }
                return Ok(bed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving bed: {ex.Message}");
            }
        }

        // POST: api/bed
        [HttpPost]
        public async Task<ActionResult<Bed>> CreateBed([FromBody] Bed bed)
        {
            try
            {
                if (bed == null || string.IsNullOrEmpty(bed.Ward) || string.IsNullOrEmpty(bed.BedNumber))
                {
                    return BadRequest("Invalid bed data.");
                }
                _context.Beds.Add(bed);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBed), new { id = bed.BedId }, bed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating bed: {ex.Message}");
            }
        }

        // PUT: api/bed/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBed(int id, BedStatus? Status, string Ward)
        {
            try
            {
                var existingBed = await _context.Beds.FindAsync(id);
                if (existingBed == null)
                {
                    return NotFound($"Bed with ID {id} not found.");
                }

                // Update only the fields that are provided
                if (Status.HasValue)
                {
                    existingBed.Status = Status.Value;
                }
                if (!string.IsNullOrEmpty(Ward))
                {
                    existingBed.Ward = Ward;
                }

                _context.Entry(existingBed).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating bed: {ex.Message}");
            }
        }

        // DELETE: api/bed/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBed(int id)
        {
            try
            {
                var bed = await _context.Beds.FindAsync(id);
                if (bed == null)
                {
                    return NotFound($"Bed with ID {id} not found.");
                }
                _context.Beds.Remove(bed);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting bed: {ex.Message}");
            }
        }
    }
}
