using System.Collections.Generic;
using System.Linq;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Logger;

namespace iasset.Weather.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRemoteApiClient _countryServiceClient;
        private readonly IApiLogger<CountryService> _logger;

        public CountryService(
            IApiLogger<CountryService> logger,
            ICountryRemoteApiClient countryServiceClient)
        {
            _logger = logger;
            _countryServiceClient = countryServiceClient;
        }

        public IEnumerable<string> GetCities(string countryName)
        {
            return _countryServiceClient
                .GetCities(countryName)
                .OrderBy(ct => ct);
        }
    }
}