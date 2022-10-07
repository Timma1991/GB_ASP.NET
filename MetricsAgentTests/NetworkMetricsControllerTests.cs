using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetricsAgent.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgentTests
{
    public class NetworkMetricsControllerTests
    {
        private NetworkMetricsController _networkMetricsController ;
        public NetworkMetricsControllerTests()
        {
            _networkMetricsController = new NetworkMetricsController();
        }

        [Fact]
        public void GetDotnetMetrics_ReturnOk()
        {
            TimeSpan fromTime = TimeSpan.FromSeconds(0);
            TimeSpan toTime = TimeSpan.FromSeconds(100);
            var result = _networkMetricsController.GetNEtworkMetrics(fromTime, toTime);
            Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
