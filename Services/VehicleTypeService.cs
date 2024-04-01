using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleTypeService
    {
        private readonly FleetPulseDbContext _context;
        public VehicleTypeService(FleetPulseDbContext context)
        {
            _context = context;
        }
        
    }
}
