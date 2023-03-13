﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class RatingSyllablesGenerator : AbstractGenerator
    {
        private Dictionary<string, int> Syllables = new Dictionary<string, int>();

        protected override string GenerateSyllable()
        {
            return Syllables.Keys.ElementAt(random.Next(0, Size)).ToUpper();
        }

        public override string GetShortSettings()
        {
            return $"Rating - Maximum rating: {Size}";
        }

        public int GetLength()
        {
            return Syllables.Count;
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