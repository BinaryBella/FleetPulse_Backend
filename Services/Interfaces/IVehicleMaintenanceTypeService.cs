using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceTypeService
    {
        Task<IEnumerable<VehicleMaintenanceType>> GetAllVehicleMaintenanceTypesAsync();
        Task<VehicleMaintenanceType> GetVehicleMaintenanceTypeByIdAsync(int id);
        Task<VehicleMaintenanceType> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType);
        Task<bool> UpdateVehicleMaintenanceTypeAsync(int id, VehicleMaintenanceType maintenanceType);
        Task<bool> DeleteVehicleMaintenanceTypeAsync(int id);
    }
}