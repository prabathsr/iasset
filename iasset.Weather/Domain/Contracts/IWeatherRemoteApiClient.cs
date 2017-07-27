using iasset.Weather.Domain.Models;

namespace iasset.Weather.Domain.Contracts
{
    public interface IWeatherRemoteApiClient
    {
        Country GetWeather(string countryName, string cityName);
    }
}