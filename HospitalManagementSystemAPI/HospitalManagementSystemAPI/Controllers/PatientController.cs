using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/patient/{patientId} - gets a specific patient by ID
        [HttpGet("{patientId}")]
        public async Task<ActionResult> GetPatientById(int patientId)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(patientId);
                if (patient == null)
                {
                    return NotFound("Patient not found.");
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving patient: {ex.Message}");
            }
        }

        // Get: api/patient - gets all patients
        [HttpGet("all")]
        public async Task<ActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _context.Patients.ToListAsync();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving patients: {ex.Message}");
            }
        }

        // POST: api/patient - creates a new patient
        [HttpPost]
        public async Task<ActionResult> CreatePatient([FromBody] Patient patient)
        {
            try
            {
                if (patient == null || string.IsNullOrEmpty(patient.FirstName) || string.IsNullOrEmpty(patient.LastName))
                {
                    return BadRequest("Invalid patient data.");
                }
                // Add the patient to the database
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetPatientById), new { patientId = patient.PatientId }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating patient: {ex.Message}");
            }
        }

        // PUT: api/patient/{patientId} - updates an existing patient
        [HttpPut("{patientId}")]
        public async Task<ActionResult> UpdatePatient(int patientId, [FromBody] PatientUpdateDto patient)
        {
            try
            {
                if (patient == null || patient.PatientId != patientId || string.IsNullOrEmpty(patient.FirstName) || string.IsNullOrEmpty(patient.LastName))
                {
                    return BadRequest("Invalid patient data.");
                }
                var existingPatient = await _context.Patients.FindAsync(patientId);
                if (existingPatient == null)
                {
                    return NotFound("Patient not found.");
                }
                // Update the patient details
                if(patient.FirstName != null)
                {
                    existingPatient.FirstName = patient.FirstName;
                }
                if (patient.LastName != null)
                {
                    existingPatient.LastName = patient.LastName;
                }
                if (patient.DOB != default(DateTime))
                {
                    existingPatient.DOB = (DateTime)patient.DOB;
                }
                if (patient.Gender != null)
                {
                    existingPatient.Gender = (Char)patient.Gender;
                }
                if (patient.Phone != null)
                {
                    existingPatient.Phone = patient.Phone;
                }
                if (patient.Email != null)
                {
                    existingPatient.Email = patient.Email;
                }
                if (patient.Address != null)
                {
                    existingPatient.Address = patient.Address;
                }
                if (patient.EmergencyContactName != null)
                {
                    existingPatient.EmergencyContactName = patient.EmergencyContactName;
                }
                if (patient.EmergencyContactPhone != null)
                {
                    existingPatient.EmergencyContactPhone = patient.EmergencyContactPhone;
                }
                if (patient.InsuranceProvider != null)
                {
                    existingPatient.InsuranceProvider = patient.InsuranceProvider;
                }
                if (patient.InsurancePolicyNumber != null)
                {
                    existingPatient.InsurancePolicyNumber = patient.InsurancePolicyNumber;
                }

                _context.Patients.Update(existingPatient);
                await _context.SaveChangesAsync();
                return Ok(existingPatient);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating patient: {ex.Message}");
            }
        }

        // DELETE: api/patient/{patientId} - deletes a patient
        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatient(int patientId)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(patientId);
                if (patient == null)
                {
                    return NotFound("Patient not found.");
                }
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting patient: {ex.Message}");
            }
        }
    }
}
