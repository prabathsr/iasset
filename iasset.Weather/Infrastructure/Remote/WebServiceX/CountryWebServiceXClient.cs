using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using iasset.Weather.Domain.Contracts;

namespace iasset.Weather.Infrastructure.Remote.WebServiceX
{
    public class CountryWebserviceXClient : ICountryRemoteApiClient
    {
        private readonly WebServiceXClient _webserviceClient;

        public CountryWebserviceXClient(WebServiceXClient webserviceClient)
        {
            _webserviceClient = webserviceClient;
        }

        public IEnumerable<string> GetCities(string countryName)
        {
            var content = _webserviceClient.GetService()
                .GetCitiesByCountry(countryName);

            var xDoc = XDocument.Parse(content);
            return xDoc.Descendants()
                .Elements("Table")
                .Select(e => e.Element("City").Value);
        }
    }
}