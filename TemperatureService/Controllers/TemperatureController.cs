using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TemperatureService.Models;

namespace TemperatureService.Controllers
{
    [Route("[controller]")]
    public class TemperatureController : ControllerBase
    {
        static int _getCounter = 0;
        static int _postCounter = 0;
        static int _deleteCounter = 0;
        static readonly Random randomTemperature = new Random();

        [HttpGet("{locationId}")]
        public ActionResult Get(int locationId)
        {
            _getCounter++;

            if (_getCounter % 4 == 0) // only one out of four requests will succeed
            {
                return Ok(randomTemperature.Next(0, 120));
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when getting the temperature.");
        }

        [HttpPost]
        public ActionResult Post([FromBody] TemperatureInfo temperatureInfo)
        {
            _postCounter++;

            if (_postCounter % 4 == 0)
            {
                // save the data                
                return Created("", null);
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when saving the temperature info.");
        }

        [HttpDelete("{locationId}")]
        public ActionResult Delete(int locationId)
        {
            _deleteCounter++;

            if (_deleteCounter % 4 == 0)
            {
                // delete the data                
                return Ok();
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong when deleting the temperature info.");
        }
    }
}
