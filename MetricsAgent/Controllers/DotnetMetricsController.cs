using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrix/dotnet")]
    [ApiController]
    public class DotnetMetricsController : ControllerBase
    {
        [HttpGet("from/{fromTime}/to/{toTime}")]
        
        public IActionResult GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}
