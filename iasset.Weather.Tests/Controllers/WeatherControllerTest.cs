using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Castle.Components.DictionaryAdapter.Xml;
using iasset.Weather.Controllers;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Domain.Models;
using iasset.Weather.Infrastructure.Services;
using iasset.Weather.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace iasset.Weather.Tests.Controllers
{
    [TestClass]
    public class WeatherControllerTest
    {
        private Mock<CountryService> _countryServiceMock;
        private Mock<WeatherService> _weatherServiceMock;
        private Mock<ApiLogger<WeatherController>> _loggerMock;

        #region Initilizer for tests
        [TestInitialize]
        public void InitializerForTests()
        {
            #region Datasets
            var cities = new List<string>
            {
                "Sydney",
                "Melbourne",
                "Brisbane",
                "Perth"
            };

            var countryWeatherInfo = new Country
            {
                Name = "Australia",
                City = new City
                {
                    Name = "Sydney",
                    SkyConditions = "Clear Sky"
                }
            };
            #endregion

            // Country remote service Mock
            var countryRemoteServiceMock = new Mock<ICountryRemoteApiClient>();
            countryRemoteServiceMock.Setup(
                s => s.GetCities(It.IsAny<string>())
            ).Returns(cities);

            // Weather remote service Mock
            var weatherRemoteServiceMock = new Mock<IWeatherRemoteApiClient>();
            weatherRemoteServiceMock.Setup(
                s => s.GetWeather(It.IsAny<string>(), It.IsAny<string>())
            ).Returns(countryWeatherInfo);

            // Country Service Mock
            _countryServiceMock = new Mock<CountryService>(
                new Mock<IApiLogger<CountryService>>().Object,
                countryRemoteServiceMock.Object
            );

            // Weather Service Mock
            _loggerMock = new Mock<ApiLogger<WeatherController>>();
            _weatherServiceMock = new Mock<WeatherService>(
                new Mock<IApiLogger<WeatherService>>().Object,
                new List<IWeatherRemoteApiClient> { weatherRemoteServiceMock.Object }
            );
        }
        #endregion

        [TestMethod]
        public void GetLocations()
        {
            var controller = new WeatherController(
                _loggerMock.Object, 
                _countryServiceMock.Object,
                _weatherServiceMock.Object
            );

            // Retrieve results
            var actionResult = controller.GetLocations("Australia") as OkNegotiatedContentResult<IEnumerable<string>>;
            
            // Check for Ok http status
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<string>>));
            // Check for cities count
            Assert.AreEqual(actionResult?.Content.Count(), 4);
        }

        [TestMethod]
        public void GetWeather()
        {
            var controller = new WeatherController(
                _loggerMock.Object,
                _countryServiceMock.Object,
                _weatherServiceMock.Object
            );

            // Retrieve results
            var countryName = "Australia";
            var cityName = "Sydney";
            var actionResult = controller.GetWeather(countryName, cityName)
                as OkNegotiatedContentResult<Country>;

            // Check for Ok http status
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Country>));
            // Check for country name
            Assert.AreEqual(actionResult?.Content.Name , countryName);
            // Check for city name
            Assert.AreEqual(actionResult?.Content.City.Name, cityName);
            // Check for weather information (Sky Condition)
            Assert.AreEqual(actionResult?.Content.City.SkyConditions, "Clear Sky");
        }
    }
}