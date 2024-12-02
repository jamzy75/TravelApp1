using Newtonsoft.Json;
using RestSharp;

namespace TravelApp1
{
   

    public class NewsService
    {
        public static async Task<NewsResponse> GetNewsAsync(string location)
        {
            var client = new RestClient("https://newsapi.org/v2/everything");
            var request = new RestRequest();

            request.AddParameter("q", location); // Search for news based on the location
            request.AddParameter("from", "2024-11-02"); // Filter articles by date
            request.AddParameter("sortBy", "publishedAt"); // Sort by publication date
            request.AddParameter("language", "en"); // Set language to English
            request.AddParameter("apiKey", "7075bb13dae04ad79e3a21d3263800e0"); //  NewsAPI key

            var response = await client.GetAsync(request);
            if (!string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<NewsResponse>(response.Content);
            }

            return null;
        }
    }

}
