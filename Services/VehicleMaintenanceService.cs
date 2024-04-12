using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceService : IVehicleMaintenanceService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleMaintenance>> GetAllVehicleMaintenancesAsync()
        {
            return await _context.VehicleMaintenance.ToListAsync();
        }

        public async Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(string id)
        {
            return await _context.VehicleMaintenance.FindAsync(id);
        }

        public async Task<VehicleMaintenance> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance)
        {
            _context.VehicleMaintenance.Add(maintenance);
            await _context.SaveChangesAsync();
            return maintenance;
        }

        public async Task<bool> UpdateVehicleMaintenanceAsync(string id, VehicleMaintenance maintenance)
        {
            var existingMaintenance = await _context.VehicleMaintenance.FindAsync(id);
            if (existingMaintenance == null)
                return false;

            existingMaintenance.MaintenanceDate = maintenance.MaintenanceDate;
            existingMaintenance.MaintenanceStatus = maintenance.MaintenanceStatus;
            existingMaintenance.Description = maintenance.Description;
            existingMaintenance.Cost = maintenance.Cost;
            existingMaintenance.PartsReplaced = maintenance.PartsReplaced;
            existingMaintenance.ServiceProvider = maintenance.ServiceProvider;
            existingMaintenance.SpecialNotes = maintenance.SpecialNotes;
            existingMaintenance.Status = maintenance.Status;
            existingMaintenance.TypeName = maintenance.TypeName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleMaintenanceAsync(string id)
        {
            var maintenance = await _context.VehicleMaintenance.FindAsync(id);
            if (maintenance == null)
                return false;

            _context.VehicleMaintenance.Remove(maintenance);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
