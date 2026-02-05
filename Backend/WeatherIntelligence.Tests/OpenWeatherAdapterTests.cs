using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherIntelligence.Adapters;
using Xunit;
using FluentAssertions;

namespace WeatherIntelligence.Tests
{
    public class OpenWeatherAdapterTests
    {
        [Fact] // Indique que c'est un test
        public async Task GetWeatherAsync_ValidCity_ReturnCorrectData()
        {
            // ARRANGE - Préparation
            
            // Créer une fausse réponse JSON (comme si OpenWeather répondait)
            var fakeJsonResponse = @"{
                ""main"": {
                    ""temp"": 22.5,
                    ""humidity"": 65
                },
                ""weather"": [{
                    ""description"": ""clear sky""
                }],
                ""wind"": {
                    ""speed"": 5.5
                },
                ""name"": ""Rabat"",
                ""sys"": {
                    ""country"": ""MA""
                }
            }";

            // Créer un Mock de HttpMessageHandler (simulation de l'appel HTTP)
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>( 
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage 
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(fakeJsonResponse)
                });

            // Créer un HttpClient avec le mock
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);

            // Créer l'adapter avec le HttpClient mocké
            var adapter = new OpenWeatherAdapter(httpClient, "fake-api-key");

            // ACT - Exécution de l'action à tester
            var result = await adapter.GetWeatherAsync("Rabat");

            // ASSERT - Vérification des résultats
            result.Should().NotBeNull(); // Le résultat ne doit pas être null
            result.City.Should().Be("Rabat"); // La ville doit être "Rabat"
            result.Country.Should().Be("MA"); // Le pays doit être "MA"
            result.Temperature.Should().Be(22.5); // Température correcte
            result.Humidity.Should().Be(65); // Humidité correcte
            result.Description.Should().Be("clear sky"); // Description correcte
            result.WindSpeed.Should().BeApproximately(19.8, 0.1); // 5.5 * 3.6 = 19.8 km/h
            result.Source.Should().Be("OpenWeather"); // Source correcte
        }
    }
}