using System.Text.Json.Serialization;

namespace ReadingSyllables
{
    [Serializable]
    internal class Settings
    {
        [JsonPropertyName("mode")]
        public ApplicationMode Mode { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        [JsonPropertyName("max_rating")]
        public int MaxRating { get; set; } = 10;
    }
}