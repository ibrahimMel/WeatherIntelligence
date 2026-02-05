namespace WeatherIntelligence.Adapters.DTOs
{
    public class OpenWeatherResponse
    {
        public MainData Main {get; set;} = new(); // temp + humidite
        public List<WeatherInfo> Weather {get; set;}= new();// description meteo
        public WindData Wind {get; set;} = new(); //vitesse du vent
        public string Name {get; set;}=  string.Empty;
        public SysData Sys {get; set;}= new();

    }

    //temp humidite
    public class MainData
    {
        public double Temp{get; set;}
        public int Humidity{get; set;}
    }
    // Description de la météo
public class WeatherInfo
{
    public string Description { get; set; } = string.Empty; // Ex: "clear sky"
}

// Informations sur le vent
public class WindData
{
    public double Speed { get; set; } // Vitesse en m/s
}

// Informations système
public class SysData
{
    public string Country { get; set; } = string.Empty; // Code pays
}
}