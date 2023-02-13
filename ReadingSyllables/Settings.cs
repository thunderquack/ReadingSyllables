using Newtonsoft.Json;

namespace ReadingSyllables
{
    [Serializable]
    internal class Settings
    {
        public const string SETTINGS_FILE_NAME = "settings.json";

        [JsonProperty("mode")]
        public ApplicationMode Mode { get; set; }

        [JsonProperty("file_name")]
        public string FileName { get; set; }

        [JsonProperty("max_rating")]
        public int MaxRating { get; set; } = 10;

        public static Settings Load()
        {
            return JsonConvert.DeserializeObject(File.ReadAllText(SETTINGS_FILE_NAME), typeof(Settings)) as Settings;
        }

        public void Save()
        {
            File.WriteAllText(SETTINGS_FILE_NAME, JsonConvert.SerializeObject(this));
        }
    }
}