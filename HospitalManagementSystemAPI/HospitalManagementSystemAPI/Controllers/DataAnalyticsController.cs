using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagementSystemAPI.Controllers
{
    
    
    [ApiController]
    [Route("api/[controller]")]
    public class DataAnalyticsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataAnalyticsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/dataanalytics/medication-usage
        [HttpGet("medication-usage")]
        public async Task<IActionResult> GetMedicationUsageSummary()
        {
            // Query only items marked as medications
            var medicationItems = await _context.InventoryItems
                .Where(i => i.IsMedication)
                .Select(i => new
                {
                    i.Name,
                    i.QuantityInStock,
                    i.ReorderLevel,
                    i.TotalHospitalUsage,
                    NeedsRestock = i.QuantityInStock <= i.ReorderLevel
                })
                .ToListAsync();

            return Ok(medicationItems);
        }

        //public IActionResult GetMedicationUsage()
        //{
        //    var usage = _context.InventoryTransactions
        //        .Where(t => t.TransactionType == TransactionType.Dispensed)
        //        .GroupBy(t => t.ItemId)
        //        .Select(g => new
        //        {
        //            ItemId = g.Key,
        //            TotalDispensed = g.Sum(x => x.ChangeQuantity)
        //        })
        //        .Join(_context.InventoryItems,
        //                g => g.ItemId,
        //                i => i.ItemId,
        //                (g, i) => new
        //                {
        //                    i.Name,
        //                    g.TotalDispensed
        //                });

        //    return Ok(usage.ToList()); 
        //}

        // GET: api/dataanalytics/patient-visits
        [HttpGet("patient-visits")]
        public async Task<IActionResult> GetPatientVisits()
        {
            // Placeholder structure 
            return Ok(new { Message = "Patient visit analytics coming soon." });
        }

        // GET: api/dataanalytics/common-ailments
        [HttpGet("common-ailments")]
        public async Task<IActionResult> GetCommonAilments()
        {
            // Placeholder structure 
            return Ok(new { Message = "Common ailments analytics coming soon." });
        }
    }
    
}
