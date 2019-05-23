using System;

namespace TemperatureService.Models
{
    public class TemperatureInfo
    {
        public int LocationId { get; set; }
        public int Temperature { get; set; }
        public DateTime DateMeasured { get; set; }
    }
}
