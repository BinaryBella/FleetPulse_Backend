using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceTypeService : IVehicleMaintenanceTypeService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceTypeService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMaintenanceType>> GetAllVehicleMaintenanceTypesAsync()
        {
            return await _context.VehicleMaintenanceType.ToListAsync();
        }

        public async Task<VehicleMaintenanceType> GetVehicleMaintenanceTypeByIdAsync(int id)
        {
            return await _context.VehicleMaintenanceType.FindAsync(id);
        }

        public async Task<VehicleMaintenanceType> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType)
        {
            _context.VehicleMaintenanceType.Add(maintenanceType);
            await _context.SaveChangesAsync();
            return maintenanceType;
        }

        public async Task<bool> UpdateVehicleMaintenanceTypeAsync(int id, VehicleMaintenanceType maintenanceType)
        {
            var existingType = await _context.VehicleMaintenanceType.FindAsync(id);
            if (existingType == null)
                return false;

            existingType.TypeName = maintenanceType.TypeName;
            existingType.Status = maintenanceType.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleMaintenanceTypeAsync(int id)
        {
            var maintenanceType = await _context.VehicleMaintenanceType.FindAsync(id);
            if (maintenanceType == null)
                return false;

            _context.VehicleMaintenanceType.Remove(maintenanceType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
