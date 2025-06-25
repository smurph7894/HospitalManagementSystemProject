using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StaffController(AppDbContext context)
        {
            _context = context;
        }

        // Get: api/staff/{userRef} - gets staff by userId from login
        [HttpGet("{userRef}")]
        public async Task<ActionResult> GetStaffById(int userRef)
        {
            try
            {
                var staff = await _context.Staff
                    .Where(s => s.UserRef == userRef.ToString())
                    .FirstOrDefaultAsync();
                if (staff == null)
                {
                    return NotFound($"Staff with ID {userRef} not found.");
                }
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving staff: {ex.Message}");
            }
        }

        // GET: api/staff
        [HttpGet]
        public async Task<ActionResult> GetAllStaff()
        {
            try
            {
                var staffList = await _context.Staff.ToListAsync();
                return Ok(staffList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving staff: {ex.Message}");
            }
        }

        // POST: api/staff
        [HttpPost]
        public async Task<ActionResult> CreateStaff([FromBody] Staff staff)
        {
            try
            {
                if (staff == null || string.IsNullOrEmpty(staff.Name) || staff.UserRef == null || staff.StaffType == null || staff.DepartmentId >= 0)
                {
                    return BadRequest("Invalid staff data.");
                }
                _context.Staff.Add(staff);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetStaffById), new { id = staff.StaffId }, staff);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating staff: {ex.Message}");
            }
        }

        // PUT: api/staff/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStaff(int id, [FromBody] Staff staff)
        {
            try
            {
                if (id != staff.StaffId || staff == null)
                {
                    return BadRequest("Staff ID mismatch or invalid data.");
                }
                var existingStaff = await _context.Staff.FindAsync(id);
                if (existingStaff == null)
                {
                    return NotFound($"Staff with ID {id} not found.");
                }

                // Update the existing staff properties
                if(staff.Name != null)
                {
                    existingStaff.Name = staff.Name;
                }
                if (staff.StaffType != null)
                {
                    existingStaff.StaffType = staff.StaffType;
                }
                if (staff.Specialization != null)
                {
                    existingStaff.Specialization = staff.Specialization;
                }
                if (staff.DepartmentId >= 0)
                {
                    existingStaff.DepartmentId = staff.DepartmentId;
                }
                if (staff.Phone != null)
                {
                    existingStaff.Phone = staff.Phone;
                }
                if (staff.Email != null)
                {
                    existingStaff.Email = staff.Email;
                }
                existingStaff.UpdatedAt = DateTime.Now; // Update the timestamp


                _context.Staff.Update(existingStaff);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating staff: {ex.Message}");
            }
        }

        // DELETE: api/staff/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStaff(int id)
        {
            try
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                {
                    return NotFound($"Staff with ID {id} not found.");
                }
                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting staff: {ex.Message}");
            }

        }
    }
}
