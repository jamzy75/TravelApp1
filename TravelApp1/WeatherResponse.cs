using Newtonsoft.Json;

namespace TravelApp1
{
    public class WeatherResponse
    {
        [JsonProperty("resolvedAddress")]
        public string ResolvedAddress { get; set; }

        [JsonProperty("currentConditions")]
        public CurrentConditions CurrentConditions { get; set; }

        [JsonProperty("days")]
        public List<Day> Days { get; set; }
    }

    public class CurrentConditions
    {
        [JsonProperty("temp")]
        public float Temp { get; set; }

        [JsonProperty("conditions")]
        public string Conditions { get; set; }

        [JsonProperty("humidity")]
        public float Humidity { get; set; }

        [JsonProperty("windspeed")]
        public float WindSpeed { get; set; }
    }

    public class Day
    {
        [JsonProperty("datetime")]
        public string DateTime { get; set; }

        [JsonProperty("tempmax")]
        public float TempMax { get; set; }

        [JsonProperty("tempmin")]
        public float TempMin { get; set; }

        [JsonProperty("conditions")]
        public string Conditions { get; set; }
    }

}
