using FleetPulse_BackEndDevelopment.Services;
using Quartz;
using FleetPulse_BackEndDevelopment.Services.Interfaces;

namespace FleetPulse_BackEndDevelopment.Quartz.Jobs
{
    public class SendMaintenanceNotificationJob : IJob
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IVehicleMaintenanceConfigurationService _vehicleMaintenanceConfigurationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SendMaintenanceNotificationJob> _logger;
        private static readonly Dictionary<int, bool> _notifiedTasks = new Dictionary<int, bool>();

        public SendMaintenanceNotificationJob(
            IPushNotificationService pushNotificationService,
            IVehicleMaintenanceConfigurationService vehicleMaintenanceConfigurationService,
            IConfiguration configuration,
            ILogger<SendMaintenanceNotificationJob> logger)
        {
            _pushNotificationService = pushNotificationService ?? throw new ArgumentNullException(nameof(pushNotificationService));
            _vehicleMaintenanceConfigurationService = vehicleMaintenanceConfigurationService ?? throw new ArgumentNullException(nameof(vehicleMaintenanceConfigurationService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var dueTasks = await _vehicleMaintenanceConfigurationService.GetDueMaintenanceTasksAsync();

                if (dueTasks.Count == 0)
                {
                    _logger.LogInformation("No maintenance tasks are due.");
                    return;
                }

                var deviceTokens = _configuration.GetSection("DeviceTokens").Get<List<string>>();

                foreach (var task in dueTasks)
                {
                    // Check for invalid or default vehicle registration number
                    if (string.IsNullOrWhiteSpace(task.VehicleRegistrationNo) || task.VehicleRegistrationNo == "0")
                    {
                        _logger.LogWarning($"Skipping notification for vehicle with invalid registration number: {task.VehicleRegistrationNo}");
                        continue;
                    }

                    // Check if the task has already been notified
                    if (_notifiedTasks.ContainsKey(task.Id))
                    {
                        _logger.LogInformation($"Task for vehicle {task.VehicleRegistrationNo} already notified.");
                        continue;
                    }

                    var message = $"Vehicle {task.VehicleRegistrationNo} requires maintenance for {task.TypeName}.";

                    foreach (var token in deviceTokens)
                    {
                        await _pushNotificationService.SendNotificationAsync(token, "Maintenance Due", message, 0);
                        _logger.LogInformation($"Notification sent for vehicle {task.VehicleRegistrationNo}: {message}");
                    }

                    // Mark the task as notified
                    _notifiedTasks[task.Id] = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending maintenance notifications.");
                throw;
            }
        }
    }
}
