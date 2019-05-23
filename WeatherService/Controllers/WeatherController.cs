using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherService.Models.Incoming;
using WeatherService.Models.Outgoing;

namespace WeatherService.Controllers
{
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult> Get(int locationId)
        {
            var httpClient = _httpClientFactory.CreateClient("TemperatureService");
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync($"temperature/{locationId}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                int temperature = await httpResponseMessage.Content.ReadAsAsync<int>();
                return Ok(temperature);
            }

            return StatusCode((int)httpResponseMessage.StatusCode, "The temperature service returned an error.");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] WeatherInfo weatherModel) 
        {
            var httpClient = _httpClientFactory.CreateClient("TemperatureService");

            var temperatureInfo = new TemperatureInfo
            {
                LocationId = weatherModel.LocationId,
                Temperature = weatherModel.Temperature,
                DateMeasured = weatherModel.DateTemperatureMeasured
            };

            string temperatureJson = JsonConvert.SerializeObject(temperatureInfo);
            HttpContent httpContent = new StringContent(temperatureJson, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync("temperature", httpContent);

            return StatusCode((int) httpResponseMessage.StatusCode);
        }

        [HttpDelete("{locationId}")]
        public async Task<ActionResult> Delete(int locationId)
        {
            var httpClient = _httpClientFactory.CreateClient("TemperatureService");

            HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync($"temperature/{locationId}");

            return StatusCode((int)httpResponseMessage.StatusCode);
        }
    }
}
