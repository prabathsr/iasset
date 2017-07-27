using System.Collections.Generic;

namespace iasset.Weather.Domain.Contracts
{
    public interface ICountryRemoteApiClient
    {
        IEnumerable<string> GetCities(string countryName);
    }
}