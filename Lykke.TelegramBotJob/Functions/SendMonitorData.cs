using System;
using System.Threading.Tasks;
using Common;
using Core.Infrastructure;
using Lykke.JobTriggers.Triggers.Attributes;

namespace Lykke.TelegramBotJob.Functions
{
    public class SendMonitorData
    {
        private readonly IServiceMonitoringRepository _serviceMonitoringRepository;

        public SendMonitorData(IServiceMonitoringRepository serviceMonitoringRepository)
        {
            _serviceMonitoringRepository = serviceMonitoringRepository;
        }

        [TimerTrigger("00:00:30")]
        public async Task SendMonitorRecord()
        {
            await
                _serviceMonitoringRepository.UpdateOrCreate(MonitoringRecord.Create("TelegramBot", DateTime.UtcNow,
                    ReflectionUtils.GetAssemblyVersion()));
        }
    }
}
