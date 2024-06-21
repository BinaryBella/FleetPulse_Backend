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
                    _logger.LogInformation("No maintenance tasks are due kk.");
                    return;
                }

                var deviceTokens = _configuration.GetSection("FCMDeviceTokens").Get<List<string>>();

                foreach (var task in dueTasks)
                {
                    var message = $"Vehicle {task.VehicleId} requires maintenance for {task.TypeName}.";

                    foreach (var token in deviceTokens)
                    {
                        await _pushNotificationService.SendNotificationAsync(token, "Maintenance Due", message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending maintenance notifications.");
                throw; // Rethrow the exception to propagate it up
            }
        }
    }
}
