using WeatherIntelligence.Core.Interfaces;
using WeatherIntelligence.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurer HttpClient
builder.Services.AddHttpClient();

// Lire les clés API depuis appsettings.json (SÉCURISÉ !)
var openWeatherKey = builder.Configuration["WeatherApiKeys:OpenWeather"] 
    ?? throw new Exception("OpenWeather API key manquante dans appsettings.json");
var weatherApiKey = builder.Configuration["WeatherApiKeys:WeatherAPI"] 
    ?? throw new Exception("WeatherAPI API key manquante dans appsettings.json");
var weatherstackKey = builder.Configuration["WeatherApiKeys:Weatherstack"] 
    ?? throw new Exception("Weatherstack API key manquante dans appsettings.json");

// Enregistrer les 3 Adapters
builder.Services.AddScoped<IWeatherService>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
    return new OpenWeatherAdapter(httpClient, openWeatherKey);
});

builder.Services.AddScoped<IWeatherService>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
    return new WeatherAPIAdapter(httpClient, weatherApiKey);
});

builder.Services.AddScoped<IWeatherService>(sp =>
{
    var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient();
    return new WeatherstackAdapter(httpClient, weatherstackKey);
});

// Configurer CORS (pour le frontend React)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();