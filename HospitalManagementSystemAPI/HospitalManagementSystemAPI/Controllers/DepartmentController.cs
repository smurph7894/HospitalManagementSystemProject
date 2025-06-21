using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/department
        [HttpGet]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                var departments = await _context.Departments.ToListAsync();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving departments: {ex.Message}");
            }
        }

        // GET: api/department/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving department: {ex.Message}");
            }
        }

        // POST: api/department
        [HttpPost]
        public async Task<ActionResult> CreateDepartment([FromBody] Department department)
        {
            try
            {
                if (department == null || string.IsNullOrEmpty(department.Name))
                {
                    return BadRequest("Invalid department data.");
                }
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentId }, department);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating department: {ex.Message}");
            }
        }

        // PUT: api/department/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] Department department)
        {
            try
            {
                if (id != department.DepartmentId || department == null || string.IsNullOrEmpty(department.Name))
                {
                    return BadRequest("Invalid department data.");
                }
                var existingDepartment = await _context.Departments.FindAsync(id);
                if (existingDepartment == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }

                if (department.Name != existingDepartment.Name)
                {
                    existingDepartment.Name = department.Name;
                }
                if (department.Description != null)
                {
                    existingDepartment.Description = department.Description;
                }

                _context.Departments.Update(existingDepartment);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating department: {ex.Message}");
            }
        }

        // DELETE: api/department/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var department = await _context.Departments.FindAsync(id);
                if (department == null)
                {
                    return NotFound($"Department with ID {id} not found.");
                }
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting department: {ex.Message}");
            }
        }
    }
}
