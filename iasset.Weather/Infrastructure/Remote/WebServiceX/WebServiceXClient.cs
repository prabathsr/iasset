using System.Configuration;
using iasset.Weather.GlobalWeatherService;

namespace iasset.Weather.Infrastructure.Remote.WebServiceX
{
    public class WebServiceXClient
    {
        private GlobalWeather _globalWeather;
        public GlobalWeather GetService()
        {
            if (this._globalWeather == null)
            {
                _globalWeather = new GlobalWeather();
                _globalWeather.Url = ConfigurationManager.AppSettings["WebServiceXUrl"];
            }

            return _globalWeather;
        }
    }
}