using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemClient.Models
{
    public class CarePlanUpdates
    {
        public int CarePlanUpdateId { get; set; }
        public int AppointmentId { get; set; } // Foreign key to Appointment  
        public string Notes { get; set; } // Notes or updates regarding the care plan  
    }
}
