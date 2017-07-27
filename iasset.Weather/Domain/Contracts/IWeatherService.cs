using iasset.Weather.Domain.Models;

namespace iasset.Weather.Domain.Contracts
{
    public interface IWeatherService
    {
        Country GetWeather(string countryName, string cityName);
    }
}