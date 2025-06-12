using System.Text.Json.Serialization;

namespace ReviewApp.Models
{
    public class Game
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonPropertyName("platforms")]
        public string? Platforms { get; set; }
        [JsonPropertyName("genre")]
        public string? Genre { get; set; }
        [JsonPropertyName("developer")]
        public string? Developer { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
