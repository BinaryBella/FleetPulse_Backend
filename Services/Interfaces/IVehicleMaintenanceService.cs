using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceService
    {
        Task<IEnumerable<VehicleMaintenance>> GetAllVehicleMaintenancesAsync();
        Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int id);
        Task<bool> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance);
        Vehicle? GetByRegNo(string regNo);
        Task<bool> UpdateVehicleMaintenanceAsync(string id, VehicleMaintenance maintenance);
        Task<bool> DeleteVehicleMaintenanceAsync(string id);
    }
}