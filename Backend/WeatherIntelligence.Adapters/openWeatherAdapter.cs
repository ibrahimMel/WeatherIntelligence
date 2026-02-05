using System.Text.Json;
using WeatherIntelligence.Core.Interfaces;
using WeatherIntelligence.Core.Models;
using WeatherIntelligence.Adapters.DTOs;  // ← CORRIGÉ : Adapters avec S
using System.Security;
using System.Diagnostics.CodeAnalysis;

namespace WeatherIntelligence.Adapters
{
    public class OpenWeatherAdapter : IWeatherService
    {
        private readonly HttpClient _httpClient; // pour l appel de l API
        private readonly string _apiKey; // cle api
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    

    public string AdapterName => "OpenWeather"; //nom de l adapter
    public OpenWeatherAdapter (HttpClient httpClient, string apiKey)//construteur 
        {
            //construire l url complet 
                _httpClient = httpClient; // Injection du HttpClient
                _apiKey = apiKey; // Stockage de la clé API
        }
            public async Task<WeatherData> GetWeatherAsync (string city) // Méthode principale
        {
            // ceation de l url complet
            var url = $"{BaseUrl}?q={city}&appid={_apiKey}&units=metric&lang=fr";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erreur OpenWeather: {response.StatusCode}");

            }
            var json = await response.Content.ReadAsStringAsync(); // lire le json

            // convertir json en objet c#
            var openWeatherData = JsonSerializer.Deserialize<OpenWeatherResponse>(json,new JsonSerializerOptions{ PropertyNameCaseInsensitive =true}); // ignore casse (temp = temp)
            if (openWeatherData==null) // si la deserialisation echoue
            {
                throw new Exception ("impossible de deseraliser la reponse open wheather ");
            }

            // la transfomation en format open wheather a fomat weatherData
return new WeatherData
{
     City = openWeatherData.Name, // Nom ville depuis OpenWeather
            Country = openWeatherData.Sys.Country, // Code pays depuis OpenWeather
            Temperature = openWeatherData.Main.Temp, // Température en Celsius
            Humidity = openWeatherData.Main.Humidity, // Humidité en %
            Description = openWeatherData.Weather.FirstOrDefault()?.Description ?? "N/A", // Première description ou "N/A"
            WindSpeed = openWeatherData.Wind.Speed * 3.6, // Conversion m/s → km/h
            Source = AdapterName, // Source = "OpenWeather"
            RetrievedAt = DateTime.UtcNow // Date/heure actuelle UTC
};
        }
        }}
