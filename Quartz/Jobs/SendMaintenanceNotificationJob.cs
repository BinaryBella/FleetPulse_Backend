using Quartz;
using System.Threading.Tasks;
using FleetPulse_BackEndDevelopment.Services.Interfaces;

namespace FleetPulse_BackEndDevelopment.Quartz.Jobs
{

    public class SendMaintenanceNotificationJob : IJob
    {
        private readonly IPushNotificationService _pushNotificationService;

        public SendMaintenanceNotificationJob(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _pushNotificationService.SendMaintenanceNotificationAsync();
        }
    }
}