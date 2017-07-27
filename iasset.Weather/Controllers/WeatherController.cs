using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Logger;

namespace iasset.Weather.Controllers
{
    [RoutePrefix("api")]
    public class WeatherController : ApiController
    {
        private readonly IWeatherService _weatherService;
        private readonly ICountryService _countryService;
        private readonly IApiLogger<WeatherController> _logger;

        public WeatherController(
            IApiLogger<WeatherController> logger, 
            ICountryService countryService,
            IWeatherService weatherService)
        {
            _logger = logger;
            _countryService = countryService;
            _weatherService = weatherService;
        }

        [Route("{country}/cities")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        [ResponseType(typeof(BadRequestResult))]
        public IHttpActionResult GetLocations([FromUri] string country)
        {
            try
            {
                return Ok(_countryService.GetCities(country));
            }
            catch (Exception exp)
            {
                _logger.Error("WeatherController failed at GetLocations", exp);
                return BadRequest();
            }
        }

        [Route("{country}/{city}/weather")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<string>))]
        [ResponseType(typeof(NotFoundResult))]
        [ResponseType(typeof(BadRequestResult))]
        public IHttpActionResult GetWeather([FromUri] string country, [FromUri] string city)
        {
            try
            {
                var weather = _weatherService.GetWeather(country, city);
                if (weather == null)
                    return NotFound();

                return Ok(weather);
            }
            catch (Exception exp)
            {
                _logger.Error("WeatherController failed at GetWeather", exp);
                return BadRequest();
            }
        }
    }
}