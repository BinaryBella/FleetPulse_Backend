using FleetPulse_BackEndDevelopment.Models.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class HelperReportService
    {
        // Method to fetch helper reports asynchronously
        public async Task<List<HelperReportModel>> GetHelperReportsAsync()
        {
            // Implement logic to fetch helper reports from database or any other data source
            // For now, let's return an empty list
            return new List<HelperReportModel>();
        }
    }
}
