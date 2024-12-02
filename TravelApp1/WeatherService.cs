
using RestSharp;
using Newtonsoft.Json;
namespace WebFrameCA2
{

    public class WeatherService
    {
        public WeatherService(RestClient @object)
        {
        }

        public static async Task<WeatherResponse> GetWeatherAsync(string location)
        {
            var client = new RestClient("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline");
            var request = new RestRequest($"{location}?unitGroup=metric&key=4E35CV696YCCP9JPY7MXTGJER&contentType=json");

            var response = await client.GetAsync(request);
            if (!string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<WeatherResponse>(response.Content);
            }

            return null;
        }
    }

}