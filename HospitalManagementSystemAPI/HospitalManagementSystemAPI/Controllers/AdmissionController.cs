using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdmissionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AdmissionController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/admission/patient/{patientId} - gets all admissions for a specific patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult> GetPatientAdmissions(int patientId)
        {
            try
            {
                var admissions = await _context.Admissions
                    .Where(a => a.PatientId == patientId)
                    .OrderByDescending(a => a.AdmittedAt)
                    .ToListAsync();
                return Ok(admissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving admissions: {ex.Message}");
            }
        }

        // GET: api/admission/staff/{staffId} - gets all admissions handled by a specific staff member
        [HttpGet("staff/{staffId}")]
        public async Task<ActionResult> GetStaffAdmissions(int staffId)
        {
            try
            {
                var admissions = await _context.Admissions
                    .Where(a => a.AdmitBy == staffId || a.DischargeBy == staffId)
                    .OrderByDescending(a => a.AdmittedAt)
                    .ToListAsync();
                return Ok(admissions);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving admissions: {ex.Message}");
            }
        }

        // GET: api/admission/{admissionId} - gets a specific admission by ID
        [HttpGet("{admissionId}")]
        public async Task<ActionResult> GetAdmissionById(int admissionId)
        {
            try
            {
                var admission = await _context.Admissions.FindAsync(admissionId);
                if (admission == null)
                {
                    return NotFound($"Admission with ID {admissionId} not found.");
                }
                return Ok(admission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving admission: {ex.Message}");
            }
        }

        // GET: api/admission/bed/{bedId} - gets bed by an admission
        [HttpGet("bed/{bedId}")]
        public async Task<ActionResult> GetAdmissionsByBed(int bedId)
        {
            try
            {
                var admissionBed = await _context.Admissions
                    .FirstOrDefaultAsync(a => a.BedId == bedId);
                return Ok(admissionBed);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving admissions: {ex.Message}");
            }
        }

        // POST: api/admission - creates a new admission
        [HttpPost]
        public async Task<ActionResult> CreateAdmission([FromBody] Admission admission)
        {
            if (admission == null)
            {
                return BadRequest("Admission data is null.");
            }
            try
            {
                _context.Admissions.Add(admission);
                await _context.SaveChangesAsync();
                return Ok(admission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating admission: {ex.Message}");
            }
        }

        // PUT: api/admission/{admissionId} - updates an existing admission
        [HttpPut("{admissionId}")]
        public async Task<ActionResult> UpdateAdmission(int admissionId, [FromBody] AdmissionDto admissionDto)
        {
            if (admissionDto == null)
            {
                return BadRequest("Admission data is null.");
            }
            try
            {
                var admission = await _context.Admissions.FindAsync(admissionId);
                if (admission == null)
                {
                    return NotFound($"Admission with ID {admissionId} not found.");
                }

                // Update properties
                if (admissionDto.BedId.HasValue)
                {
                    admission.BedId = admissionDto.BedId.Value;
                }
                if (admissionDto.AdmitBy.HasValue)
                {
                    admission.AdmitBy = admissionDto.AdmitBy.Value;
                }
                if (admissionDto.DischargeBy.HasValue)
                {
                    admission.DischargeBy = admissionDto.DischargeBy.Value;
                }
                if (admissionDto.DischargedAt.HasValue)
                {
                    admission.DischargedAt = admissionDto.DischargedAt.Value;
                }

                _context.Admissions.Update(admission);
                await _context.SaveChangesAsync();
                return Ok(admission);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating admission: {ex.Message}");
            }
        }

        // DELETE: api/admission/{admissionId} - deletes an admission
        [HttpDelete("{admissionId}")]
        public async Task<ActionResult> DeleteAdmission(int admissionId)
        {
            try
            {
                var admission = await _context.Admissions.FindAsync(admissionId);
                if (admission == null)
                {
                    return NotFound($"Admission with ID {admissionId} not found.");
                }
                _context.Admissions.Remove(admission);
                await _context.SaveChangesAsync();
                return Ok($"Admission with ID {admissionId} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting admission: {ex.Message}");
            }
        }
    }
}
