using FleetPulse_BackEndDevelopment.Models;

public interface IVehicleMaintenanceService
{
    Task<IEnumerable<VehicleMaintenance>> GetAllVehicleMaintenancesAsync();
    Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int id);
    Task<bool> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance);
    Vehicle? GetByRegNo(string regNo);
    Task<bool> UpdateVehicleMaintenanceAsync(VehicleMaintenance maintenance);
    Task<bool> DeleteVehicleMaintenanceAsync(string id);
    Task<bool> IsVehicleMaintenanceExist(int id);
}