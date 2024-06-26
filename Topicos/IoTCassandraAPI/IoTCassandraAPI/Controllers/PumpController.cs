﻿using Domain.Interface.API.Service;
using IoTCassandraAPI.Domain.DTO;
using IoTCassandraAPI.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace IoTCassandraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PumpController : ControllerBase
    {
        private readonly IDataManipulationService _dataManipulation;
        private readonly string _tableName;

        public PumpController(IDataManipulationService dataManipulation)
        {
            _dataManipulation = dataManipulation;
            _tableName = "pump";
        }

        [HttpPost("{greenhouse}")]
        public IActionResult Register([FromBody] IoTDataRegister<bool> data, [FromRoute] string greenhouse)
        {
            try
            {
                IoTData<bool> register = _dataManipulation.Register(data.Data, greenhouse, _tableName);
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
                IoTData<bool> data = _dataManipulation.FindById<bool>(_tableName, id, greenhouse);

                return Ok(data);
            }
            catch (Exception)
            {
                return Problem();
            }
        }
    }
}
