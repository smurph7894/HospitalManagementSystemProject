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

        // GET: api/careplan/{carePlanId}/careplanupdates - gets all care plan updates for a specific care plan
        [HttpGet("{carePlanId}/careplanupdates")]
        public ActionResult GetCarePlanUpdates(int carePlanId, CarePlanUpdate_UpdateDto carePlanUpdate)
        {
            try
            {
                _context.CarePlanUpdates.Add(new CarePlanUpdates()
                {
                    AppointmentId = carePlanUpdate.AppointmentId,
                    CarePlanId = carePlanId,
                    Notes = carePlanUpdate.Notes
                });
                return Ok("Care plan added.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plan updates: {ex.Message}");
            }
        }

        // GET: api/careplan/{carePlanId}/careplanupdates - gets all care plan updates for a specific care plan
        [HttpPost("{carePlanId}/careplanupdates")]
        public async Task<ActionResult> PostCarePlanUpdates(int carePlanId)
        {
            try
            {
                var carePlanUpdates = await _context.CarePlanUpdates
                    .Where(cp => cp.CarePlanId == carePlanId)
                    .ToListAsync();
                if (carePlanUpdates == null)
                {
                    return NotFound("Care plan not found.");
                }
                return Ok(carePlanUpdates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plan updates: {ex.Message}");
            }
        }

        [HttpGet("patient/{patientId}/careplanupdates")]
        public async Task<ActionResult> GetAllCarePlanUpdatesByPatientId(int patientId)
        {
            try
            {
                var updates = await _context.CarePlanUpdates
                    .Where(cpu => _context.CarePlans
                        .Any(cp => cp.CarePlanId == cpu.CarePlanId && cp.PatientId == patientId))
                    .ToListAsync();

                return Ok(updates);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plan updates: {ex.Message}");
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
                if (carePlan.CarePlanUpdate != null)
                {
                    if (carePlan.CarePlanUpdate.AppointmentId <= 0 || string.IsNullOrEmpty(carePlan.CarePlanUpdate.Notes))
                    {
                        return BadRequest("Invalid care plan update data.");
                    }
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

        // PUT: api/careplan/{carePlanId} - updates an existing care plan
        [HttpPut("{carePlanId}/doctor")]
        public async Task<ActionResult> UpdateCarePlanDoctor(int carePlanId, [FromBody] CarePlanUpdateDto carePlan)
        {
            try
            {
                if (carePlanId <= 0)
                {
                    return BadRequest("Invalid care plan update data.");
                }

                var existingCarePlan = await _context.CarePlans.FindAsync(carePlanId);

                if (existingCarePlan == null)
                {
                    return NotFound("Care plan not found.");
                }

                // Update the care plan properties
                if (carePlan.Description != null)
                {
                    existingCarePlan.Description = carePlan.Description;
                }
                if (carePlan.Condition != null)
                {
                    existingCarePlan.Condition = carePlan.Condition;
                }
                if (carePlan.DateResolved != null)
                {
                    existingCarePlan.DateResolved = carePlan.DateResolved;
                }
                if (carePlan.DiagnosisDate != null)
                {
                    existingCarePlan.DiagnosisDate = carePlan.DiagnosisDate;
                }

                //if carePlanUpdate - handle
                if (carePlan.CarePlanUpdate != null)
                {
                    if (carePlan.CarePlanUpdate.AppointmentId <= 0 || string.IsNullOrEmpty(carePlan.CarePlanUpdate.Notes))
                    {
                        return BadRequest("Invalid care plan update data.");
                    }
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

        //PUT: api/careplan/careplanupdate/{id}
        [HttpPut("careplanupdates/{id}")]
        public async Task<ActionResult> UpdateCarePlanUpdate(int id, [FromBody] CarePlanUpdate_Dto carePlanUpdate)
        {
            try
            {
                var existingCarePlanUpdate = await _context.CarePlanUpdates.FindAsync(id);

                if (existingCarePlanUpdate == null)
                {
                    return NotFound("Care plan update not found.");
                }

                // Update the properties of the existing care plan update
                if (carePlanUpdate.AppointmentId.HasValue)
                {
                    existingCarePlanUpdate.AppointmentId = carePlanUpdate.AppointmentId.Value;
                }

                if (!string.IsNullOrEmpty(carePlanUpdate.Notes))
                {
                    existingCarePlanUpdate.Notes = carePlanUpdate.Notes;
                }

                // Save changes to the database
                _context.CarePlanUpdates.Update(existingCarePlanUpdate);
                await _context.SaveChangesAsync();

                return Ok(existingCarePlanUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating care plan update: {ex.Message}");
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
 
        //DELETE: api/careplan/careplanupdate/{id}
        [HttpDelete("carePlanUpdate/{id}")]
        public async Task<ActionResult> DeleteCarePlanUpdates(int id)
        {
            try
            {
                var carePlanUpdate = await _context.CarePlanUpdates.FindAsync(id);
                if (carePlanUpdate == null)
                {
                    return NotFound("CarePlanUpdate not found");
                }

                // Correctly reference the instance of carePlanUpdate instead of the type  
                _context.CarePlanUpdates.Remove(carePlanUpdate);
                await _context.SaveChangesAsync();

                return Ok($"CarePlanUpdate {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting CarePlanUpdate: {ex.Message}");
            }
        }
    }
}
