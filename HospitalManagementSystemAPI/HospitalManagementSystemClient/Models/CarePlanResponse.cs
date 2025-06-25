using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemClient.Models
{
    public class CarePlanResponse
    {
        public List<CarePlan> CarePlans { get; set; }
        public List<CarePlanUpdates> CarePlanUpdates { get; set; }
    }
}
