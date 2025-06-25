using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarePlanController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CarePlanController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/careplan/patient/{patientId} - gets all care plans for a specific patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult> GetCarePatientsPlans(int patientId)
        {
            try
            {
                List<CarePlan> patientCarePlans = await _context.CarePlans
                .Where(cp => cp.PatientId == patientId)
                .OrderByDescending(cp => cp.DiagnosisDate)
                .ToListAsync();

                return Ok(patientCarePlans);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plans: {ex.Message}");
            }

        }

        // POST: api/careplan - creates a new care plan for a patient
        [HttpPost]
        public async Task<ActionResult> CreateCarePlan([FromBody] CarePlan carePlan)
        {
            try
            {
                if (carePlan == null || carePlan.PatientId <= 0 || string.IsNullOrEmpty(carePlan.Condition))
                {
                    return BadRequest("Invalid care plan data.");
                }

                // Check if the patient exists
                var patientExists = await _context.Patients.AnyAsync(p => p.PatientId == carePlan.PatientId);
                if (!patientExists)
                {
                    return NotFound("Patient not found.");
                }

                // Add the care plan to the database
                _context.CarePlans.Add(carePlan);
                await _context.SaveChangesAsync();
                
                return Ok(carePlan);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating care plan: {ex.Message}");
            }

        }

        // PUT: api/careplan/{carePlanId} - updates an existing care plan
        [HttpPut("{carePlanId}")]
        public async Task<ActionResult> UpdateCarePlan(int carePlanId, [FromBody] CarePlanUpdateDto carePlan)
        {
            try
            {
                if (carePlanId <= 0 )
                {
                    return BadRequest("Invalid care plan update data.");
                }

                var existingCarePlan = await _context.CarePlans.FindAsync(carePlanId);

                if (existingCarePlan == null)
                {
                    return NotFound("Care plan not found.");
                }

                // Update the care plan properties
                if ( carePlan.Description != null)
                {
                    existingCarePlan.Description = carePlan.Description;
                }
                if(carePlan.Condition != null)
                {
                    existingCarePlan.Condition = carePlan.Condition;
                }
                if ( carePlan.DateResolved != null)
                {
                    existingCarePlan.DateResolved = carePlan.DateResolved;
                }
                if( carePlan.DiagnosisDate != null)
                {
                    existingCarePlan.DiagnosisDate = carePlan.DiagnosisDate;
                }

                //if carePlanUpdate - handle
                if (carePlan.CarePlanUpdate != null || carePlan.CarePlanUpdate.AppointmentId > 0 || !string.IsNullOrEmpty(carePlan.CarePlanUpdate.Notes))
                {
                    // Check if the appointment exists
                    var appointmentExists = await _context.Appointments.AnyAsync(a => a.AppointmentId == carePlan.CarePlanUpdate.AppointmentId);
                        if (!appointmentExists)
                        {
                            return NotFound("Appointment not found.");
                        }

                    CarePlanUpdates carePlanUpdate = new CarePlanUpdates
                    {
                        AppointmentId = carePlan.CarePlanUpdate.AppointmentId,
                        Notes = carePlan.CarePlanUpdate.Notes,
                    };

                    // check if creating carePlanUpdate instance was successful
                    if (carePlanUpdate == null)
                    {
                        return BadRequest("Care plan update was not possible.");
                    }

                    // Add the care plan updates
                    existingCarePlan.CarePlanUpdates.Add(carePlanUpdate);
                }
                else if (carePlan.CarePlanUpdate.AppointmentId <= 0 || string.IsNullOrEmpty(carePlan.CarePlanUpdate.Notes))
                {
                    return BadRequest("Invalid care plan update data.");
                }

                // Update the care plan in the database
                _context.CarePlans.Update(existingCarePlan);
                await _context.SaveChangesAsync();
                return Ok(existingCarePlan);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating care plan: {ex.Message}");
            }
        }

        // DELETE: api/careplan/{id} - deletes a care plan
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCarePlan(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid care plan ID.");
                }

                var carePlan = await _context.CarePlans.FindAsync(id);

                if (carePlan == null)
                {
                    return NotFound("Care plan not found.");
                }

                // Remove the care plan from the database
                _context.CarePlans.Remove(carePlan);
                await _context.SaveChangesAsync();
                return Ok($"Deletion of CarePlan {id} Successful");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting care plan: {ex.Message}");
            }
        }
    }
}
