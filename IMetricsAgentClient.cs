using MetricsManager.Models.Requests;

namespace MetricsManager.Services.Client.impl
{
    public interface IMetricsAgentClient
    {
        CpuMetricsResponse GetCpuMetrics(CpuMetricsRequest request);
    }
}
