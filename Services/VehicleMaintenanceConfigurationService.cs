using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                Status = vehicleMaintenanceConfigurationDto.Status,
                LastMaintenanceDate = DateTime.UtcNow 
            };

            _context.VehicleMaintenanceConfigurations.Add(vehicleMaintenanceConfiguration);
            await _context.SaveChangesAsync();

            return vehicleMaintenanceConfiguration;
        }

        public async Task<List<VehicleMaintenanceConfiguration>> GetDueMaintenanceTasksAsync()
        {
            var allConfigurations = await _context.VehicleMaintenanceConfigurations.ToListAsync();

            var dueConfigurations = allConfigurations
                .Where(config => CalculateNextMaintenanceDate(config.LastMaintenanceDate, config.Duration) <= DateTime.UtcNow)
                .ToList();

            return dueConfigurations ?? new List<VehicleMaintenanceConfiguration>();
        }

        private DateTime CalculateNextMaintenanceDate(DateTime lastMaintenanceDate, string duration)
        {
            var durationParts = duration.Split(' ');
            var durationValue = int.Parse(durationParts[0]);
            var durationType = durationParts[1];

            return durationType switch
            {
                "days" => lastMaintenanceDate.AddDays(durationValue),
                "months" => lastMaintenanceDate.AddMonths(durationValue),
                "years" => lastMaintenanceDate.AddYears(durationValue),
                _ => lastMaintenanceDate
            };
        }
    }
}
