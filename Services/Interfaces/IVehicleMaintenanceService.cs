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
        Task<bool> AddVehicleMaintenanceAsync(VehicleMaintenance maintenance);
        Vehicle GetByRegNo(string regNo);
        Task<bool> IsVehicleMaintenanceExist(int id);
        Task<bool> UpdateVehicleMaintenanceAsync(VehicleMaintenance maintenance);
        Task<bool> DeleteVehicleMaintenanceAsync(string id);
    }
}