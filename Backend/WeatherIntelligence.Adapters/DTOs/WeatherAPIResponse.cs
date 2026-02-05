namespace WeatherIntelligence.Adapters.DTOs
{
    // reponse json de weatherAPI
    public class WeatherAPIResponse
    {
        public LocationData Location {get; set;} = new ();
        public CurrentData Current { get; set; } = new();
    }

    // information sur  la loca 
    public class LocationData
    {
        public string Name {get; set; }= string.Empty;
        public string Country {get;set;}= string.Empty;
    }
    public class CurrentData
    {
        public double Temp_c { get; set; } // Température en Celsius
        public int Humidity { get; set; } // Humidité en %
        public ConditionData Condition { get; set; } = new(); // Description
        public double Wind_kph { get; set; } // Vent en km/h
    }

    public class ConditionData
    {
        public string Text {get; set;}= string.Empty;
    }
    
}