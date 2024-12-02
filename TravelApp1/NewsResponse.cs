using Newtonsoft.Json;

namespace TravelApp1
{
    public class NewsResponse
    {
        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }
    }

    public class Article
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }
    }

}
