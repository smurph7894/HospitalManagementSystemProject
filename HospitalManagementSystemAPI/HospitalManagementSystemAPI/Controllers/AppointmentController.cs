using HospitalManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using System.Data;

namespace HospitalManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/appointment/patient/{patientId} - gets all appointments for a specific patient
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult> GetPatientAppointments(int patientId)
        {
            try
            {
               List<Appointment> appointments = await _context.Appointments
                    .Where(a => a.PatientId == patientId)
                    .OrderByDescending(a => a.ScheduledAt)
                    .ToListAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving appointments: {ex.Message}");
            }
        }
        // GET: api/appointment/staff/{staffId} - gets all appointments for a specific staff member
        [HttpGet("staff/{staffId}")]
        public async Task<ActionResult> GetStaffAppointments(int staffId)
        {
            try
            {
                var appointments = await _context.Appointments
                    .Where(a => a.StaffId == staffId)
                    .OrderByDescending(a => a.ScheduledAt)
                    .ToListAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving appointments: {ex.Message}");
            }
        }
        // GET: api/appointment/{appointmentId} - gets a specific appointment by ID
        [HttpGet("{appointmentId}")]
        public async Task<ActionResult> GetAppointmentById(int appointmentId)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(appointmentId);
                if (appointment == null)
                {
                    return NotFound("Appointment not found.");
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving appointment: {ex.Message}");
            }
        }

        //GET: api/appointment/{appointmentId}/careplans/careplanupdates - gets care plans and updates for a specific appointment
        [HttpGet("{appointmentId}/careplans/careplanupdates")]
        public async Task<ActionResult> GetCarePlansAndUpdatesForAppointment(int appointmentId)
        {
            var _connectionString = "Server=LITTLE_JUICY\\SQLEXPRESS;Database=HospitalManagementDB;Trusted_Connection=True;Encrypt=False;";
            try
            {
                var result = new
                {
                    CarePlans = new List<CarePlan>(),
                    CarePlanUpdates = new List<CarePlanUpdates>()
                };

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // Get Care Plans that have updates for the specific appointment
                    // This joins CarePlans with CarePlanUpdates to find care plans with matching appointment updates
                    var carePlansQuery = @"
                        SELECT DISTINCT cp.CarePlanId, cp.PatientId, cp.Condition, cp.Description, 
                               cp.DiagnosisDate, cp.DateResolved, cp.CreatedAt
                        FROM CarePlans cp
                        INNER JOIN CarePlanUpdates cpu ON cp.CarePlanId = cpu.CarePlanId
                        WHERE cpu.AppointmentId = @AppointmentId";

                    var carePlans = new List<CarePlan>();
                    var carePlanIds = new List<int>();

                    using (var carePlansCommand = new SqlCommand(carePlansQuery, connection))
                    {
                        carePlansCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);

                        using (var reader = await carePlansCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var carePlan = new CarePlan
                                {
                                    CarePlanId = reader.GetInt32("CarePlanId"),
                                    PatientId = reader.GetInt32("PatientId"),
                                    Condition = reader.GetString("Condition"),
                                    Description = reader.IsDBNull("Description") ? null : reader.GetString("Description"),
                                    DiagnosisDate = reader.IsDBNull("DiagnosisDate") ? null : reader.GetDateTime("DiagnosisDate"),
                                    DateResolved = reader.IsDBNull("DateResolved") ? null : reader.GetDateTime("DateResolved"),
                                    CreatedAt = reader.GetDateTime("CreatedAt")
                                };
                                carePlans.Add(carePlan);
                                carePlanIds.Add(carePlan.CarePlanId);
                            }
                        }
                    }

                    // Get the Care Plan Updates for each CarePlan individually
                    foreach (var carePlan in carePlans)
                    {
                        var carePlanUpdatesQuery = @"
                            SELECT cpu.CarePlanUpdateId, cpu.AppointmentId, cpu.Notes
                            FROM CarePlanUpdates cpu
                            WHERE cpu.AppointmentId = @AppointmentId 
                            AND cpu.CarePlanId = @CarePlanId";

                        using (var updatesCommand = new SqlCommand(carePlanUpdatesQuery, connection))
                        {
                            updatesCommand.Parameters.AddWithValue("@AppointmentId", appointmentId);
                            updatesCommand.Parameters.AddWithValue("@CarePlanId", carePlan.CarePlanId);

                            using (var reader = await updatesCommand.ExecuteReaderAsync())
                            {
                                var carePlanUpdatesForThisPlan = new List<CarePlanUpdates>();

                                while (await reader.ReadAsync())
                                {
                                    var update = new CarePlanUpdates
                                    {
                                        CarePlanUpdateId = reader.GetInt32("CarePlanUpdateId"),
                                        AppointmentId = reader.GetInt32("AppointmentId"),
                                        Notes = reader.GetString("Notes")
                                    };
                                    carePlanUpdatesForThisPlan.Add(update);
                                }

                                carePlan.CarePlanUpdates = carePlanUpdatesForThisPlan;
                            }
                        }
                    }

                    // Collect all updates for the response
                    var allCarePlanUpdates = carePlans.SelectMany(cp => cp.CarePlanUpdates).ToList();

                    result = new { CarePlans = carePlans, CarePlanUpdates = allCarePlanUpdates };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plans: {ex.Message}");
            }
        }

        // POST: api/appointment - creates a new appointment
        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            try
            {
                if (appointment == null || appointment.PatientId <= 0 || appointment.StaffId <= 0)
                {
                    return BadRequest("Invalid appointment data.");
                }
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating appointment: {ex.Message}");
            }
        }

        // PUT: api/appointment/{appointmentId} - updates an existing appointment
        [HttpPut("{appointmentId}")]
        public async Task<ActionResult> UpdateAppointment(int appointmentId, [FromBody] AppointmentsDto updatedAppointment)
        {
            try
            {
                if (updatedAppointment == null || updatedAppointment.AppointmentId != appointmentId)
                {
                    return BadRequest("Invalid appointment data.");
                }
                var existingAppointment = await _context.Appointments.FindAsync(appointmentId);
                if (existingAppointment == null)
                {
                    return NotFound("Appointment not found.");
                }

                if (updatedAppointment.PatientId.HasValue)
                {
                    existingAppointment.PatientId = updatedAppointment.PatientId.Value;
                }
                if (updatedAppointment.StaffId.HasValue)
                {
                    existingAppointment.StaffId = updatedAppointment.StaffId.Value;
                }
                if (updatedAppointment.ScheduledAt.HasValue)
                {
                    existingAppointment.ScheduledAt = updatedAppointment.ScheduledAt.Value;
                }
                if (updatedAppointment.DurationMinutes.HasValue)
                {
                    existingAppointment.DurationMinutes = updatedAppointment.DurationMinutes.Value;
                }
                if (!string.IsNullOrEmpty(updatedAppointment.Reason))
                {
                    existingAppointment.Reason = updatedAppointment.Reason;
                }
                if (updatedAppointment.Status.HasValue)
                {
                    existingAppointment.Status = updatedAppointment.Status.Value;
                }
                existingAppointment.UpdatedAt = DateTime.UtcNow; // Update the timestamp

                _context.Appointments.Update(existingAppointment);
                await _context.SaveChangesAsync();
                return Ok(existingAppointment);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating appointment: {ex.Message}");
            }
        }

        // DELETE: api/appointment/{appointmentId} - deletes an appointment
        [HttpDelete("{appointmentId}")]
        public async Task<ActionResult> DeleteAppointment(int appointmentId)
        {
            try
            {
                var appointment = await _context.Appointments.FindAsync(appointmentId);
                if (appointment == null)
                {
                    return NotFound("Appointment not found.");
                }
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
                return Ok("Appointment deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting appointment: {ex.Message}");
            }
        }
    }
}
