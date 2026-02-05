namespace WeatherIntelligence.Adapters.DTOs
{
    public class WeatherstackResponse
    {
        public LocationInfo Location {get ;set;}
        public CurrentWeather Current { get; set; } = new();
    }

    // information sur location 
    public class LocationInfo
    {
        public string Name {get; set;} = string.Empty;
        public string Country {get; set;} = string.Empty;
    }

    // donnee meteo actuelles 
    public class CurrentWeather
    {
         public int Temperature { get; set; } // Température en Celsius
        public int Humidity { get; set; } // Humidité en %
        public List<string> Weather_descriptions { get; set; } = new(); // Descriptions
        public int Wind_speed { get; set; } // Vent en km/h
    }
}