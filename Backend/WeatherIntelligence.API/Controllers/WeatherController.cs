using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using WeatherIntelligence.Core.Interfaces;
using WeatherIntelligence.Core.Models;

namespace WeatherIntelligence.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IEnumerable<IWeatherService> _weatherServices;
        public WeatherController(IEnumerable<IWeatherService> weatherservices)
        {
            _weatherServices = weatherservices;
        }
        //recupere les methodes de tous les adapter 
        [HttpGet("{city}")]
         public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                var results = new List<WeatherData>();

                // appler tout les adapters
                foreach ( var service in _weatherServices)
                {
                    try
                    {
                        var data = await service.GetWeatherAsync(city);
                        results.Add(data);

                    }
                    catch ( Exception ex)
                    {
                        // si il echoue on va continuer avec les autre 
                        Console.WriteLine($"Erreur avec {service.AdapterName}:{ex.Message}");
                    }
                } 
                if (results.Count == 0)
                {
                    return NotFound($"Aucune donnee touver pour {city}");
                }
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode (500,$"Erreur serveur:{ex.Message}");
            }
        }
        //recupere la meteo d un seule adappter specifique 
        [HttpGet("source/{adapterName}/{city}")]
        public async Task<IActionResult> GetWeatherBySource(string adapterName, string city)
        {
            try
            {
                var service = _weatherServices.FirstOrDefault(s=>
                s.AdapterName.Equals(adapterName, StringComparison.OrdinalIgnoreCase));
                if (service == null)
                {
                    return NotFound($"Adapter'{adapterName}'non trouve");
                }
                var data = await service.GetWeatherAsync(city);
                return Ok(data);
            }
            catch (HttpRequestException ex)
            {
                return NotFound($"Ville non trouvée:{ex.Message}");
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"Erreur de serveur: {ex.Message}");
            }
        }
        //lister tout les adapters dispo
        [HttpGet("adapters")]
        public IActionResult GetAdapers()
        {
            var adapters= _weatherServices.Select(s => new
            {
                name =s.AdapterName
            });
            return Ok(adapters);
        }
    }
}