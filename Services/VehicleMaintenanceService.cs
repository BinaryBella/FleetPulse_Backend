using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceService : IVehicleMaintenanceService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleMaintenanceDTO>> GetAllVehicleMaintenancesAsync()
        {
            var maintenances = await _context.VehicleMaintenances
                .Include(vm => vm.Vehicle)
                .Include(vm => vm.VehicleMaintenanceType)
                .Select(vm => new VehicleMaintenanceDTO
                {
                    MaintenanceId = vm.MaintenanceId,
                    MaintenanceDate = vm.MaintenanceDate,
                    Cost = vm.Cost,
                    PartsReplaced = vm.PartsReplaced,
                    ServiceProvider = vm.ServiceProvider,
                    SpecialNotes = vm.SpecialNotes,
                    VehicleRegistrationNo = vm.Vehicle.VehicleRegistrationNo,
                    VehicleMaintenanceTypeName = vm.VehicleMaintenanceType.TypeName,
                    Status = vm.Status
                }).ToListAsync();
    
            return maintenances;
        }

        public async Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int MaintenanceId)
        {
            return await _context.VehicleMaintenances.FindAsync(MaintenanceId);
        }

        public async Task<bool> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance)
        {
            _context.VehicleMaintenances.Add(maintenance);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public Vehicle? GetByRegNo(string regNo)
        {
            return _context.Vehicles.FirstOrDefault(c => c.VehicleRegistrationNo == regNo);
        }

        public Task<bool> IsVehicleMaintenanceExist(int id)
        {
            return Task.FromResult(_context.VehicleMaintenances.Any(x => x.MaintenanceId == id));
        }

        public async Task<bool> UpdateVehicleMaintenanceAsync(VehicleMaintenance maintenance)
        {
            var existingMaintenance = await _context.VehicleMaintenances.FindAsync(maintenance.MaintenanceId);
            if (existingMaintenance == null)
                return false;

            existingMaintenance.MaintenanceDate = maintenance.MaintenanceDate;
            existingMaintenance.Cost = maintenance.Cost;
            existingMaintenance.PartsReplaced = maintenance.PartsReplaced;
            existingMaintenance.ServiceProvider = maintenance.ServiceProvider;
            existingMaintenance.SpecialNotes = maintenance.SpecialNotes;
            existingMaintenance.Status = maintenance.Status;
            existingMaintenance.VehicleMaintenanceType = maintenance.VehicleMaintenanceType;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleMaintenanceAsync(string id)
        {
            var maintenance = await _context.VehicleMaintenances.FindAsync(id);
            if (maintenance == null)
                return false;

            _context.VehicleMaintenances.Remove(maintenance);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
