using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardsSyllablesGenerator : AbstractSyllableGenerator
    {
        public int Size { get; set; } = 10;
        private Random random = new Random();
        public SyllablesContext Context
        {
            get
            {
                return Program.host.Services.GetRequiredService<SyllablesContext>();   
            }
        }

        public override string GenerateSyllable()
        {
            var list = Context.Syllables.Where(x => x.NextShow < DateTime.UtcNow).Take(Size).ToList();
            var idx = random.Next(list.Count());
            return list.ElementAt(idx).Name;
        }

        public override string GetShortSettings()
        {
            return $"Card - Size: {Size}";
        }

        public CardsSyllablesGenerator(Settings settings) : base(settings) { }

    }
}
