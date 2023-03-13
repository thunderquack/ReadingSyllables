namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardWordsGenerator : AbstractGenerator
    {
        public override string NextSyllable()
        {
            var list = Context.Syllables.OrderBy(x => x.Id).Where(x => x.Show >= Size).ToList();
            var words = Context.Words.Where(x => x.Syllables.All(x => list.Contains(x))).ToList();
            if (words.Count < 2)
            {
                return "НЕДОСТАТОЧНО";
            }
            var idx = random.Next(words.Count());
            if (words.ElementAt(idx).Name == prevSyllable)
            {
                return NextSyllable();
            }
            words.ElementAt(idx).ShowCounter++;
            prevSyllable = words.ElementAt(idx).Name;
            return words.ElementAt(idx).Name.ToUpper();
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
