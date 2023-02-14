using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class RatingSyllablesGenerator : AbstractSyllableGenerator
    {
        Dictionary<string, int> Syllables = new Dictionary<string, int>();

        public override string GenerateSyllable()
        {
            string syllable = Syllables.Keys.ElementAt(random.Next(0, Settings.MaxRating - 1)).ToUpper();
            if (syllable == prevSyllable)
            {
                return (GenerateSyllable());
            }
            else
            {
                prevSyllable = syllable;
                return syllable;
            }
        }

        public RatingSyllablesGenerator(Settings settings) : base(settings)
        {
            string json = File.ReadAllText(settings.FileName, Encoding.UTF8);
            var sylls = JsonConvert.DeserializeObject(json);
            foreach (JObject item in (sylls as JArray))
            {
                Syllables.Add(Convert.ToString(item.GetValue("name")), Convert.ToInt32(item.GetValue("value")));
            }
        }
    }
}