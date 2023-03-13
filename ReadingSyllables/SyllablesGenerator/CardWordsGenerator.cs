using ReadingSyllables.Exceptions;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardWordsGenerator : AbstractGenerator
    {
        protected override string Generate()
        {
            var list = Context.Syllables.OrderBy(x => x.Id).Where(x => x.Show >= Size).ToList();
            var words = Context.Words.Where(x => x.Syllables.All(x => list.Contains(x))).ToList();
            if (words.Count < 2)
            {
                throw new NotEnoughWordsException();
            }
            var idx = random.Next(words.Count());
            words.ElementAt(idx).ShowCounter++;
            Context.SaveChanges();
            return words.ElementAt(idx).Name;
        }

        public override string GetShortSettings()
        {
            return $"Card Syllables - Size: {Size}";
        }

        public CardWordsGenerator(Settings settings) : base(settings)
        {
        }
    }
}