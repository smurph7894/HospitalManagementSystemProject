using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VitalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/vitals/patient/{patientId}
        [HttpGet("/patient/{patientId}")]
        public async Task<ActionResult> GetVitalsByPatientId(int patientId)
        {
            try
            {
                var vitals = await _context.Vitals
                    .Where(v => v.PatientId == patientId)
                    .OrderByDescending(v => v.RecordedAt)
                    .ToListAsync();

                // Check if any vitals were found
                if (vitals == null || !vitals.Any())
                {
                    return NotFound("No vitals found for the specified patient.");
                }

                return Ok(vitals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving vitals: {ex.Message}");
            }
        }

        // POST: api/vitals
        [HttpPost]
        public async Task<ActionResult> CreateVital([FromBody] Vitals vital)
        {
            try
            {
                if (vital == null || vital.PatientId <= 0 || string.IsNullOrEmpty(vital.Value))
                {
                    return BadRequest("Invalid vital data.");
                }
                // Check if the patient exists
                var patientExists = await _context.Patients.AnyAsync(p => p.PatientId == vital.PatientId);

                if (!patientExists)
                {
                    return NotFound("Patient not found.");
                }

                // Add the vital to the database
                _context.Vitals.Add(vital);
                await _context.SaveChangesAsync();

                return Ok(vital);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating vital: {ex.Message}");
            }
        }
    }
}
