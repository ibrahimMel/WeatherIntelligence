using WeatherIntelligence.Core.Models;
namespace WeatherIntelligence.Core.Interfaces
{
// interface commune pour tout les adapters
public interface IWeatherService
{
    //recupere les donnee meteo pour une ville 
    Task<WeatherData> GetWeatherAsync (string city);
    string AdapterName {get;}
}
}
