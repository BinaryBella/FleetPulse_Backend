using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using System.Threading.Tasks;

namespace FleetPulse_BackEndDevelopment.Services
{
    public class VehicleMaintenanceConfigurationService : IVehicleMaintenanceConfigurationService
    {
        private readonly FleetPulseDbContext _context;

        public VehicleMaintenanceConfigurationService(FleetPulseDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleMaintenanceConfiguration> AddVehicleMaintenanceConfigurationAsync(VehicleMaintenanceConfigurationDTO vehicleMaintenanceConfigurationDto)
        {
            var vehicleMaintenanceConfiguration = new VehicleMaintenanceConfiguration
            {
                VehicleId = vehicleMaintenanceConfigurationDto.VehicleId,
                VehicleMaintenanceTypeId = vehicleMaintenanceConfigurationDto.VehicleMaintenanceTypeId,
                Duration = vehicleMaintenanceConfigurationDto.Duration,
                Status = vehicleMaintenanceConfigurationDto.Status
            };

            _context.VehicleMaintenanceConfigurations.Add(vehicleMaintenanceConfiguration);
            await _context.SaveChangesAsync();

            return vehicleMaintenanceConfiguration;
        }
    }
}