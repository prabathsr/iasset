using System;
using System.Collections.Generic;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Domain.Models;
using iasset.Weather.Logger;

namespace iasset.Weather.Infrastructure.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IApiLogger<WeatherService> _logger;
        private readonly IEnumerable<IWeatherRemoteApiClient> _weatherServiceClients;

        public WeatherService(
            IApiLogger<WeatherService> logger,
            IEnumerable<IWeatherRemoteApiClient> weatherServiceClients)
        {
            _logger = logger;
            _weatherServiceClients = weatherServiceClients;
        }

        public Country GetWeather(string countryName, string cityName)
        {
            return GetWeather(_logger, countryName, cityName);
        }

        private Country GetWeather(IApiLogger<WeatherService> logger, string countryName, string cityName)
        {
            /* Work through available weather services until we find data. 
             * Similar approach to factory pattern, however, intention in here to provide more flexibility to 
             * add new clients later, through DI, without modifying the existing code
             * */
            foreach (var weatherService in _weatherServiceClients)
                try
                {
                    var weather = weatherService.GetWeather(countryName, cityName);
                    if (weather != null)
                        return weather;
                }
                catch (Exception exp)
                {
                    logger.Error("WeatherService failed at GetWeather", exp);
                }

            return null;
        }
    }
}