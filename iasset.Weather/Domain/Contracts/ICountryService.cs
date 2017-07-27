using System.Collections.Generic;

namespace iasset.Weather.Domain.Contracts
{
    public interface ICountryService
    {
        IEnumerable<string> GetCities(string countryName);
    }
}