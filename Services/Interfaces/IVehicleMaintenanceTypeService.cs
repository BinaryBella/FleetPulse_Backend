using FleetPulse_BackEndDevelopment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces
{
    public interface IVehicleMaintenanceTypeService
    {
        Task<IEnumerable<VehicleMaintenanceType?>> GetAllVehicleMaintenanceTypesAsync();
        Task<VehicleMaintenanceType?> GetVehicleMaintenanceTypeByIdAsync(int id);
        Task<bool> IsVehicleTypeExist(int id);
        bool DoesVehicleMaintenanceTypeExists(string vehicleMaintenanceType);
        Task<VehicleMaintenanceType?> AddVehicleMaintenanceTypeAsync(VehicleMaintenanceType? maintenanceType);
        Task<bool> UpdateVehicleMaintenanceTypeAsync(VehicleMaintenanceType maintenanceType);
        Task DeactivateMaintenanceTypeAsync(int maintenanceTypeId);
        Task ActivateVehicleMaintenanceTypeAsync(int id);
    }
}