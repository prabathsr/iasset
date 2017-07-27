using System.Xml.Linq;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Domain.Models;

namespace iasset.Weather.Infrastructure.Remote.WebServiceX
{
    public class WeatherWebserviceXClient : IWeatherRemoteApiClient
    {
        private readonly WebServiceXClient _webserviceClient;
        public WeatherWebserviceXClient(WebServiceXClient webserviceClient)
        {
            _webserviceClient = webserviceClient;
        }
        public Country GetWeather(string countryName, string cityName)
        {
            var content = _webserviceClient.GetService()
                    .GetWeather(cityName, countryName);

            var xDoc = XDocument.Parse(content);

            // NOTE: So far I could not find a city with data from this service. Difficult write the code without seeing definitions

            return new Country();
        }
    }
}