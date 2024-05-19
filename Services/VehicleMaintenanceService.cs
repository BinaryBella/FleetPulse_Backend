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
            return await _context.VehicleMaintenances
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
                    VehicleId = vm.VehicleId,
                    VehicleRegistrationNo = vm.Vehicle.VehicleRegistrationNo,
                    VehicleMaintenanceTypeId = vm.VehicleMaintenanceTypeId,
                    TypeName = vm.VehicleMaintenanceType.TypeName,
                    Status = vm.Status,
                }).ToListAsync();
        }



        public async Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int MaintenanceId)
        {
            return await _context.VehicleMaintenances.FindAsync(MaintenanceId);
        }

        public async Task<VehicleMaintenance> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance)
        {
            _context.VehicleMaintenances.Add(maintenance);
            await _context.SaveChangesAsync();
            return maintenance;
        }

        public async Task<Vehicle> GetByRegNoAsync(string regNo)
        {
            return await _context.Vehicles.FirstOrDefaultAsync(c => c.VehicleRegistrationNo == regNo);
        }

        public async Task<bool> IsVehicleMaintenanceExistAsync(int id)
        {
            return await _context.VehicleMaintenances.AnyAsync(x => x.MaintenanceId == id);
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
            existingMaintenance.VehicleMaintenanceTypeId = maintenance.VehicleMaintenanceTypeId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleMaintenanceAsync(int id)
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
