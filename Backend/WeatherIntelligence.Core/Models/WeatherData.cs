namespace WeatherIntelligence.Core.Models
{
    //model unifier des donnee meto 
    public class WeatherData
    {
        public string City { get; set; } =string.Empty;
        public string Country {get ; set;} = string.Empty;
        public double Temperature {get ; set;} // en celsius
        public int Humidity {get; set;} //en %
        public string Description {get; set;} =string.Empty;
        public double WindSpeed { get; set; }
        public string Source { get; set; } = string.Empty;
        public DateTime RetrievedAt {get; set;}= DateTime.UtcNow;
    }

}