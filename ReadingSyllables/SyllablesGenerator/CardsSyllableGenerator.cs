using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardsSyllablesGenerator : AbstractSyllableGenerator
    {
        public int Size { get; set; } = 10;
        public SyllablesContext Context
        {
            get
            {
                return Program.host.Services.GetRequiredService<SyllablesContext>();   
            }
        }

        public override string GenerateSyllable()
        {
            var list = Context.Syllables.OrderBy(x=>x.Id).Where(x => x.NextShow < DateTime.UtcNow).Take(Size).ToList();
            var idx = random.Next(list.Count());
            if (list.ElementAt(idx).Name == prevSyllable)
            {
                return GenerateSyllable();
            }
            list.ElementAt(idx).ShowCounter++;
            prevSyllable = list.ElementAt(idx).Name;
            return list.ElementAt(idx).Name.ToUpper();
        }

        public override string GetShortSettings()
        {
            return $"Card - Size: {Size}";
        }

        public CardsSyllablesGenerator(Settings settings) : base(settings) { }

    }
}
