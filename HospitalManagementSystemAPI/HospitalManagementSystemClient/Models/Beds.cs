using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystemClient.Models
{
    public enum BedStatus
    {
        Available,
        Occupied,
        Maintenance
    }
    public class Bed
    {
        public int BedId { get; set; }
        [StringLength(100)]
        public string Ward { get; set; }
        [StringLength(50)]
        public string BedNumber { get; set; }
        public BedStatus Status { get; set; }
    }
}
