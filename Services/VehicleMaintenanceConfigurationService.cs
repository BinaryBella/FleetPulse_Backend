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
        private readonly ILogger<VehicleMaintenanceConfigurationService> _logger; // Define ILogger

        public VehicleMaintenanceConfigurationService(FleetPulseDbContext context, ILogger<VehicleMaintenanceConfigurationService> logger)
        {
            _context = context;
            _logger = logger; // Inject ILogger instance
        }

        public async Task<VehicleMaintenanceConfiguration> AddVehicleMaintenanceConfigurationAsync(
            VehicleMaintenanceConfigurationDTO vehicleMaintenanceConfigurationDto)
        {
            try
            {
                var vehicleMaintenanceConfiguration = new VehicleMaintenanceConfiguration
                {
                    VehicleId = vehicleMaintenanceConfigurationDto.VehicleId,
                    VehicleRegistrationNo = vehicleMaintenanceConfigurationDto.VehicleRegistrationNo,
                    VehicleMaintenanceTypeId = vehicleMaintenanceConfigurationDto.VehicleMaintenanceTypeId,
                    Duration = vehicleMaintenanceConfigurationDto.Duration,
                    Status = vehicleMaintenanceConfigurationDto.Status,
                    LastMaintenanceDate = DateTime.UtcNow
                };

                _context.VehicleMaintenanceConfigurations.Add(vehicleMaintenanceConfiguration);
                await _context.SaveChangesAsync();

                return vehicleMaintenanceConfiguration;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding vehicle maintenance configuration.");
                throw; // Rethrow the exception to propagate it up
            }
        }

        public async Task<List<VehicleMaintenanceConfiguration>> GetDueMaintenanceTasksAsync()
        {
            try
            {
                var allConfigurations = await _context.VehicleMaintenanceConfigurations.ToListAsync();

                var dueConfigurations = allConfigurations
                    .Where(config => CalculateNextMaintenanceDate(config.LastMaintenanceDate, config.Duration) <=
                                     DateTime.UtcNow)
                    .ToList();

                return dueConfigurations ?? new List<VehicleMaintenanceConfiguration>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving due maintenance tasks.");
                throw; // Rethrow the exception to propagate it up
            }
        }

        private DateTime CalculateNextMaintenanceDate(DateTime lastMaintenanceDate, string duration)
        {
            try
            {
                var durationParts = duration.Split(' ');

                if (durationParts.Length != 2)
                {
                    throw new FormatException("Invalid duration format. Expected '<value> <unit>' (e.g., '30 days').");
                }

                var durationValue = int.Parse(durationParts[0]);
                var durationType = durationParts[1].Trim().ToLower(); // Convert to lowercase for case insensitivity

                switch (durationType)
                {
                    case "days":
                        return lastMaintenanceDate.AddDays(durationValue);
                    case "months":
                        return lastMaintenanceDate.AddMonths(durationValue);
                    case "years":
                        return lastMaintenanceDate.AddYears(durationValue);
                    case "seconds":
                        return lastMaintenanceDate.AddSeconds(durationValue);
                    default:
                        throw new FormatException(
                            $"Unsupported duration type: {durationType}. Supported types: days, months, years, seconds.");
                }
            }
            catch (FormatException ex)
            {
                // Log the exception with detailed information
                _logger.LogError(ex, $"Invalid duration format or type: {duration}");
                throw; // Rethrow the exception to propagate it up
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                _logger.LogError(ex, $"Error processing duration: {duration}");
                throw; // Rethrow the exception to propagate it up
            }
        }
    }
}
