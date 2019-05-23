using System;

namespace WeatherService.Models.Incoming
{
    public class WeatherInfo
    {
        public int LocationId { get; set; }
        public int Temperature { get; set; }
        public DateTime DateTemperatureMeasured { get; set; }
        public int Humidity { get; set; }
        public DateTime DateHumidityMeasured { get; set; }

    }
}
