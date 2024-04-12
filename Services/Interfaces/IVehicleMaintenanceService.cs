using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceService
    {
        Task<IEnumerable<VehicleMaintenance>> GetAllVehicleMaintenancesAsync();
        Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(string id);
        Task<VehicleMaintenance> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance);
        Task<bool> UpdateVehicleMaintenanceAsync(string id, VehicleMaintenance maintenance);
        Task<bool> DeleteVehicleMaintenanceAsync(string id);
    }
}