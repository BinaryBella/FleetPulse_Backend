using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceService
    {
        Task<List<VehicleMaintenanceDTO>> GetAllVehicleMaintenancesAsync();
        Task<VehicleMaintenance> GetVehicleMaintenanceByIdAsync(int MaintenanceId);
        Task<VehicleMaintenance> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance); 
        Task<Vehicle> GetByRegNoAsync(string regNo);
        Task<bool> IsVehicleMaintenanceExistAsync(int id);
        Task<bool> UpdateVehicleMaintenanceAsync(VehicleMaintenance maintenance);
        Task<bool> DeleteVehicleMaintenanceAsync(int id);
    }
}