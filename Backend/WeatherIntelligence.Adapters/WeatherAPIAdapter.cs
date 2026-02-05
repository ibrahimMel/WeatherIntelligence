using System.Text.Json;
using WeatherIntelligence.Core.Interfaces;
using WeatherIntelligence.Core.Models;
using WeatherIntelligence.Adapters.DTOs;
using System.Net.Mail;

namespace WeatherIntelligence.Adapters
{
    public class WeatherAPIAdapter  : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey ;
        private const string BaseUrl ="https://api.weatherapi.com/v1/current.json";
        public string AdapterName => "WeatherAPI";
        public WeatherAPIAdapter (HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey=apiKey;
        }
        public async Task<WeatherData> GetWeatherAsync(string city)
        {
            //construire l url complet 
            var url = $"{BaseUrl}?Key={_apiKey}&q={city}&aqi=no";
            // key = cle q= ville aqi = no ( j ai pas importaer la qualiter de l air )

            var response = await _httpClient.GetAsync(url); // appel hhtp get

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erreur WeatherApi:{response.StatusCode}");
            }
            var json =await response.Content.ReadAsStringAsync(); //lire le fichier json

            //deserraliser json en objet adapter 

            var weatherApiData = JsonSerializer.Deserialize<WeatherAPIResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            if ( weatherApiData == null)
            {
                throw new Exception (" Impossible de deserialiser la reponse ");
            }
            // transformation de dormat weatherAPI -> format unifier 
            return new WeatherData
            {
                City = weatherApiData.Location.Name, // Nom ville
                Country = weatherApiData.Location.Country, // Pays
                Temperature = weatherApiData.Current.Temp_c, // Température
                Humidity = weatherApiData.Current.Humidity, // Humidité
                Description = weatherApiData.Current.Condition.Text, // Description
                WindSpeed = weatherApiData.Current.Wind_kph, // Vent déjà en km/h
                Source = AdapterName, // Source = "WeatherAPI"
                RetrievedAt = DateTime.UtcNow // Date/heure actuelle
            };


        }
    }
}