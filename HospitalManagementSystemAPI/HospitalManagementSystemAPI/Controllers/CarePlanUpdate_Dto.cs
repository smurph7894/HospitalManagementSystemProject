using HospitalManagementSystemAPI.Models;

namespace HospitalManagementSystemAPI.Controllers
{
    public class CarePlanUpdate_Dto
    {
        public int? AppointmentId { get; set; }
        public string? Notes { get; set; }
    }
}