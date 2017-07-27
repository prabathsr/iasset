using System;

namespace iasset.Weather.Logger
{
    public interface IApiLogger<T>
        where T : class
    {
        void Error(string message, Exception ex);
    }
}