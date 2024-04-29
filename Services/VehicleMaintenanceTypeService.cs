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

        public async Task<IEnumerable<VehicleMaintenanceType?>> GetAllVehicleMaintenanceTypesAsync()
        {
            return await _context.VehicleMaintenanceType.ToListAsync();
        }

        public async Task<VehicleMaintenanceType?> GetVehicleMaintenanceTypeByIdAsync(int id)
        {
            return await _context.VehicleMaintenanceType.FindAsync(id);
        }

        public async Task<VehicleMaintenanceType?> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType? maintenanceType)
        {
            _context.VehicleMaintenanceType.Add(maintenanceType);
            await _context.SaveChangesAsync();
            return maintenanceType;
        }

        public async Task<bool> UpdateVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType)
        {
            _context.Entry(maintenanceType).State = EntityState.Detached;
            var result = _context.VehicleMaintenanceType.Update(maintenanceType);
            result.State = EntityState.Detached;
            await _context.SaveChangesAsync();

            if (result.State == EntityState.Modified)
            {
                return true;
            }

            return false;
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