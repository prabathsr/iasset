using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Domain.Models;
using Newtonsoft.Json;

namespace iasset.Weather.Infrastructure.Remote.OpenWeather
{
    public class OpenWeatherApiClient : IWeatherRemoteApiClient
    {
        private HttpClient _httpClient;

        public string AppId {
            get { return ConfigurationManager.AppSettings["OpenWeatherMapApiAppKey"]; }
        }

        public HttpClient GetHttpClient()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["OpenWeatherMapApiUrl"]);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            return _httpClient;
        }

        public Country GetWeather(string countryName, string cityName)
        {
            var client = GetHttpClient();
            var response = client.GetAsync($"data/2.5/weather?q={cityName}&APPID={AppId}").Result;
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<WeatherDto>(content, new JsonSerializerSettings {MissingMemberHandling = MissingMemberHandling.Ignore});

            return new Country()
            {
                Name = countryName,
                City =  new City
                {
                    Name = cityName,
                    Time = data.Time,
                    WindSpeed =  data.Wind.Speed,
                    WindDirection = data.Wind.Deg,
                    Visibility =  data.Visibility,
                    SkyConditions =  data.Weather.FirstOrDefault()?.SkyCondition,
                    Temperature = data.Main.Temp - 273, // Kelvin to C
                    DewPoint =  0,
                    Humidity = data.Main.Humidity,
                    Pressure = data.Main.Pressure
                }
            };
        }

        #region LocalDto
        class WeatherDto
        {
            public class WindInfo
            {
                public decimal Speed { get; set; }
                public decimal Deg { get; set; }
            }

            public class WeatherInfo
            {
                [JsonProperty("description")]
                public string SkyCondition { get; set; }
            }

            public class MainInfo
            {
                public decimal Temp { get; set; }
                public int Humidity { get; set; }
                public decimal Pressure { get; set; }
            }
            [JsonProperty("dt")]
            public long Time { get; set; }
            public WindInfo Wind { get; set; }
            public decimal Visibility { get; set; }
            public IEnumerable<WeatherInfo> Weather { get; set; }
            public MainInfo Main { get; set; }
        }
        #endregion
    }
}