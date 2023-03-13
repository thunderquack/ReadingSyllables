namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardSyllablesGenerator : AbstractGenerator, ICardGenerator
    {
        protected override string GenerateSyllable()
        {
            var list = Context.Syllables.OrderBy(x => x.Id).Where(x => x.NextShow < DateTime.UtcNow).Take(Size).ToList();
            var idx = random.Next(list.Count());
            if (list.ElementAt(idx).Name == previousSyllable)
            {
                return GenerateSyllable();
            }
            list.ElementAt(idx).ShowCounter++;
            Context.SaveChanges();
            return list.ElementAt(idx).Name;
        }

        public override string GetShortSettings()
        {
            return $"Card Syllables - Size: {Size}";
        }

        public void DoBad()
        {
            throw new NotImplementedException();
        }

        public void DoAverage()
        {
            throw new NotImplementedException();
        }

        public void DoGood()
        {
            throw new NotImplementedException();
        }

        public CardSyllablesGenerator(Settings settings) : base(settings)
        {
        }
    }
}