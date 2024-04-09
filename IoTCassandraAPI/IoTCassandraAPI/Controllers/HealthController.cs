using Microsoft.AspNetCore.Mvc;

namespace IoTCassandraAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public IActionResult IsAlive()
        {
            try
            {
                return Ok();
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
