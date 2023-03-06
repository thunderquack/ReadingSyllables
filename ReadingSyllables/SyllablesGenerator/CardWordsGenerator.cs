namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardWordsGenerator : AbstractGenerator
    {
        public override string GenerateSyllable()
        {
            var list = Context.Words.OrderBy(x => x.Id).Where(x => x.NextShow < DateTime.UtcNow && x.Show >= Size).Take(10).ToList();
            if (list.Count<2)
            {
                return "НЕДОСТАТОЧНО";
            }
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
            return $"Card Syllables - Size: {Size}";
        }
        public CardWordsGenerator(Settings settings) : base(settings)
        {
        }
    }
}
