using System.Diagnostics;
using MetricsAgent.Services.Impl;
using Quartz;

namespace MetricsAgent.Job
{
    public class RamMEtricJob
    { 
    private PerformanceCounter _ramCounter;
    private IServiceScopeFactory _serviceScopeFactory;

        public RamMEtricJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
            _ramCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

    }


    public Task Execute(IJobExecutionContext context)
    {

        using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
        {
            var ramMetricsRepository = serviceScope.ServiceProvider.GetService<RamMetricRepository>();
            try
            {
                var cpuUsageInPercents = _ramCounter.NextValue();
                var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                Debug.WriteLine($"{time} > {cpuUsageInPercents}");
                ramMetricsRepository.Create(new Models.RamMertic
                {
                    Value = (int)cpuUsageInPercents,
                    Time = (long)time.TotalSeconds
                });
            }
            catch (Exception ex)
            {

            }
        }

        return Task.CompletedTask;
    }
}
}
