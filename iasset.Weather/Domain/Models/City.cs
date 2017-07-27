using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace iasset.Weather.Domain.Models
{
    public class City
    {
        public string Name { get; set; }
        public long Time { get; set; }
        public decimal WindSpeed { get; set; }
        public decimal WindDirection { get; set; }
        public decimal Visibility { get; set; }
        public string SkyConditions { get; set; }
        public decimal Temperature { get; set; }
        public decimal DewPoint { get; set; }
        public int Humidity { get; set; }
        public decimal Pressure { get; set; }
    }
}