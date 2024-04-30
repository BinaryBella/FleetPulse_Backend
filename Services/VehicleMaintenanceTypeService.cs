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
        
        public bool DoesVehicleMaintenanceTypeExists(string vehicleMaintenanceType)
        {
            var maintenanceType = _context.VehicleMaintenanceType.FirstOrDefault(x => x.TypeName == vehicleMaintenanceType);
            return maintenanceType != null;
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
        
        public async Task<bool> DeactivateVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType)
        {
            _context.Entry(maintenanceType).State = EntityState.Detached;
            
            maintenanceType.Status = false;
            
            var result = _context.VehicleMaintenanceType.Update(maintenanceType);
            
            await _context.SaveChangesAsync();
            
            if (result.State == EntityState.Modified)
            {
                return true;
            }
            return false;       
        }
    }
}
