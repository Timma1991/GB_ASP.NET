using System.Diagnostics;
using MetricsAgent.Services.Impl;
using Quartz;

namespace MetricsAgent.Job
{
    public class DotnetMetricJob
    {
        private PerformanceCounter _dotnetCounter;
        private IServiceScopeFactory _serviceScopeFactory;

        public DotnetMetricJob(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dotnetCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        }

         
        public Task Execute(IJobExecutionContext context)
        {

            using (IServiceScope serviceScope = _serviceScopeFactory.CreateScope())
            {
                var dotnetMetricsRepository = serviceScope.ServiceProvider.GetService<IDotnetMetricsRepository>();
                try
                {
                    var cpuUsageInPercents = _dotnetCounter.NextValue();
                    var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    Debug.WriteLine($"{time} > {cpuUsageInPercents}");
                    dotnetMetricsRepository.Create(new Models.DotnetMetric
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
