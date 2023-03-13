namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardSyllablesGenerator : AbstractGenerator, ICardGenerator
    {
        protected override string Generate()
        {
            var list = Context.Syllables.OrderBy(x => x.Id).Where(x => x.NextShow < DateTime.UtcNow).Take(Size).ToList();
            var idx = random.Next(list.Count());
            if (list.ElementAt(idx).Name == previousPiece)
            {
                return Generate();
            }
            list.ElementAt(idx).ShowCounter++;
            Context.SaveChanges();
            return list.ElementAt(idx).Name;
        }

        public override string GetShortSettings()
        {
            return $"Card Syllables - Size: {Size}";
        }

        public string DoBad()
        {
            var s = Context.Syllables.First(x => x.Name == currentPiece);
            s.Show = 0;
            s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
            Save();
            return $"Bad - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public string DoAverage()
        {
            var s = Context.Syllables.First(x => x.Name == currentPiece);
            s.Show++;
            s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
            Save();
            return $"Average - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public string DoGood()
        {
            var s = Context.Syllables.First(x => x.Name == currentPiece);
            s.Show++;
            s.NextShow = RepeatingRule.GetNextRepeat(++s.Show);
            Save();
            return $"Good - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public CardSyllablesGenerator(Settings settings) : base(settings)
        {
        }
    }
}