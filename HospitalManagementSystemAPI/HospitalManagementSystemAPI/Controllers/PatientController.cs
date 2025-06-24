using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

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

        //GET: api/patient/userId/{patientOrgId} - gets a specific patient by Organization ID
        [HttpGet("userId/{patientOrgId}")]
        public async Task<ActionResult> GetPatientByOrgId(string patientOrgId)
        {
            try
            {
                if (string.IsNullOrEmpty(patientOrgId))
                {
                    return BadRequest("Patient Organization ID cannot be empty.");
                }
                var patient = await _context.Patients
                    .Where(p => p.PatientOrgId == patientOrgId)
                    .FirstOrDefaultAsync();
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

        // GET: api/patient/{category}/{searchInput} - gets patients based on search input and by category if added  
        [HttpGet("search/{category}/{searchInput}")]
        public async Task<ActionResult> SearchPatients(string searchInput, string category)
        {
            try
            {
                if (string.IsNullOrEmpty(searchInput))
                {
                    return BadRequest("Search input cannot be empty.");
                }

                bool isInt = int.TryParse(searchInput, out int parsedId);
                bool isDate = DateTime.TryParse(searchInput, out DateTime parsedDate);
                bool isChar = char.TryParse(searchInput, out char parsedGender);

                List<Patient> patients;

                if (category == "category" && searchInput != null)
                {
                    patients = await _context.Patients
                        .Where(p =>
                            // string searches  
                            p.FirstName.Contains(searchInput) || p.LastName.Contains(searchInput) || p.Phone.Contains(searchInput) ||
                            p.Email.Contains(searchInput) || p.Address.Contains(searchInput) || p.EmergencyContactName.Contains(searchInput) ||
                            p.EmergencyContactPhone.Contains(searchInput) || p.InsuranceProvider.Contains(searchInput) || p.InsurancePolicyNumber.Contains(searchInput) ||
                            // non-string searches  
                            (isInt && p.PatientId == parsedId) || (isDate && p.DOB.Date == parsedDate) || (isChar && p.Gender == parsedGender)
                        )
                        .ToListAsync();
                    if (!patients.Any())
                    {
                        return NotFound("No patients found matching the search criteria.");
                    }
                    return Ok(patients);
                }
                else if (category != "category")
                {
                    if (isInt)
                    {
                        patients = await _context.Patients
                            .Where(p =>
                                p.PatientId == parsedId
                            )
                            .ToListAsync();
                    }
                    else if (isDate)
                    {
                        patients = await _context.Patients
                            .Where(p =>
                                p.DOB.Date == parsedDate.Date
                            )
                            .ToListAsync();
                    }
                    else if (isChar)
                    {
                        patients = await _context.Patients
                            .Where(p =>
                                p.Gender == parsedGender
                            )
                            .ToListAsync();
                    }
                    else
                    {
                        patients = await _context.Patients
                            .Where(p => StringMatches(p, searchInput, category))
                            .ToListAsync();
                    }
                    if (!patients.Any())
                    {
                        return NotFound("No patients found matching the search criteria.");
                    }
                    return Ok(patients);
                }

                // Return BadRequest if category is invalid  
                return BadRequest("Invalid category.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error searching patients: {ex.Message}");
            }
        }

        private static bool StringMatches(Patient patient, string searchInput, string category)
        {
            var propertyInfo = patient.GetType().GetProperty(category);
            var prop = propertyInfo.GetValue(patient);
            return ((string)prop).Contains(searchInput);
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

        // DELETE: api/patient/{patientId} - deletes a patient using PatientId
        [HttpDelete("{patientId}")]
        public async Task<ActionResult> DeletePatient(int patientId)
        {
            string connectionString = "Server=LITTLE_JUICY\\SQLEXPRESS;Database=HospitalManagementDB;Trusted_Connection=True;Encrypt=False;";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        //delete careplans 
                        string deleteCarePlanQuery = @"
                        DELETE FROM CarePlans WHERE PatientId = @PatientId";

                        using (SqlCommand cmd = new SqlCommand(deleteCarePlanQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PatientId", patientId);
                            await cmd.ExecuteNonQueryAsync();
                        }

                        //delete patient and cascading data
                        string deletePatientQuery = @"
                        DELETE FROM Patients WHERE PatientId = @PatientId";

                        using (SqlCommand cmd = new SqlCommand(deletePatientQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@PatientId", patientId);
                            int rowsAffected = await cmd.ExecuteNonQueryAsync();

                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return NotFound("Patient not found.");
                            }
                        }
                        transaction.Commit();
                        return Ok("Patient deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting patient: {ex.Message}");
                    }
                }
            }
        }
    }
}
