using System.Text.Json;
using WeatherIntelligence.Core.Interfaces;
using WeatherIntelligence.Core.Models;
using WeatherIntelligence.Adapters.DTOs;

namespace WeatherIntelligence.Adapters
{
// Adapter pour l'API Weatherstack
    public class WeatherstackAdapter : IWeatherService
    {
        private readonly HttpClient _httpClient; // Client HTTP
        private readonly string _apiKey; // Clé API Weatherstack
        private const string BaseUrl = "http://api.weatherstack.com/current"; // URL de base (HTTP pas HTTPS)

        public string AdapterName => "Weatherstack"; // Nom de l'adapter

        public WeatherstackAdapter(HttpClient httpClient, string apiKey) // Constructeur
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }

        public async Task<WeatherData> GetWeatherAsync(string city) // Méthode principale
        {
            // Construire l'URL avec paramètres
            var url = $"{BaseUrl}?access_key={_apiKey}&query={city}";
            // access_key=clé, query=ville

            var response = await _httpClient.GetAsync(url); // Appel HTTP GET

            if (!response.IsSuccessStatusCode) // Si erreur
            {
                throw new HttpRequestException($"Erreur Weatherstack: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync(); // Lire JSON

            // Désérialiser JSON → objet C#
            var weatherstackData = JsonSerializer.Deserialize<WeatherstackResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (weatherstackData == null) // Si échec désérialisation
            {
                throw new Exception("Impossible de désérialiser la réponse Weatherstack");
            }

            // TRANSFORMATION : Format Weatherstack → Format unifié
            return new WeatherData
            {
                City = weatherstackData.Location.Name, // Nom ville
                Country = weatherstackData.Location.Country, // Pays
                Temperature = weatherstackData.Current.Temperature, // Température
                Humidity = weatherstackData.Current.Humidity, // Humidité
                Description = weatherstackData.Current.Weather_descriptions.FirstOrDefault() ?? "N/A", // Première description
                WindSpeed = weatherstackData.Current.Wind_speed, // Vent en km/h
                Source = AdapterName, // Source = "Weatherstack"
                RetrievedAt = DateTime.UtcNow // Date/heure actuelle
            };
        }
    }
    
}
