using System;
using Google.Protobuf.WellKnownTypes;

namespace openpos.Shared
{
    public partial class WeatherForecast
    {
        public DateTime DateTime
        {
            get => Date.ToDateTime();
            set { Date = Timestamp.FromDateTime(value.ToUniversalTime()); }
        }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        
    }
}