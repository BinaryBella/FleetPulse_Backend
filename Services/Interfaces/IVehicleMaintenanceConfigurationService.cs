using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IVehicleMaintenanceConfigurationService
{
    Task<VehicleMaintenanceConfiguration> AddVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO vehicleMaintenanceConfigurationDto);
    Task<List<VehicleMaintenanceConfiguration>> GetDueMaintenanceTasksAsync();
}