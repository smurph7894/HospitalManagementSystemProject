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

        //GET: api/appointment/{appointment.AppointmentId}/careplan/careplanupdates - get care plans and updates for a specific appointment
        [HttpGet("{appointmentId}/careplan/careplanupdates")]
        public async Task<ActionResult> GetCarePlansAndUpdatesForAppointment(int appointmentId)
        {
            try
            {
                var carePlanUpdates = await _context.CarePlanUpdates
                    .Where(cpu => cpu.AppointmentId == appointmentId)
                    .ToListAsync();
                if (carePlanUpdates == null || !carePlanUpdates.Any())
                {
                    return NotFound("No care plan updates found for this appointment.");
                }
                var carePlanIds = carePlanUpdates.Select(cpu => cpu.CarePlanId).Distinct().ToList();
                var carePlans = await _context.CarePlans
                    .Where(cp => carePlanIds.Contains(cp.CarePlanId))
                    .ToListAsync();
                return Ok(new { CarePlans = carePlans, CarePlanUpdates = carePlanUpdates });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving care plans and updates: {ex.Message}");
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
