using IoTCassandraAPI.Domain.DTO;
using IoTCassandraAPI.Domain.Entity;
using IoTCassandraAPI.Domain.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace IoTCassandraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LightnessController : ControllerBase
    {
        private readonly IDataManipulationService _dataManipulation;
        private readonly string _tableName;

        public LightnessController(IDataManipulationService dataManipulation)
        {
            _dataManipulation = dataManipulation;
            _tableName = "lightness";
        }

        [HttpPost("{greenhouse}")]
        public IActionResult Register([FromBody] IoTDataRegister<double> data, [FromRoute] string greenhouse)
        {
            try
            {
                IoTData<double> register = _dataManipulation.Register(data.Data, greenhouse, _tableName);
                var location = Url.Action(nameof(Register), new { greenhouse = register.Greenhouse, id = register.Id }) ?? $"/{register.Greenhouse}/{register.Id}";

                return Created(location, data);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet("{greenhouse}/{id}")]
        public IActionResult Register([FromRoute] string greenhouse, [FromRoute] string id)
        {
            try
            {
                IoTData<double> data = _dataManipulation.FindById<double>(_tableName, id, greenhouse);

                return Ok(data);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
