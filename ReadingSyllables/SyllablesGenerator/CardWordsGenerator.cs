using ReadingSyllables.Exceptions;

namespace ReadingSyllables.SyllablesGenerator
{
    internal class CardWordsGenerator : AbstractGenerator, ICardGenerator, IHasConstruction
    {
        private string futureConstruction = string.Empty;
        private string construction = string.Empty;

        protected override string Generate()
        {
            var list = Context.Syllables.OrderBy(x => x.Id).Where(x => x.Show >= Size).ToList();
            var words = Context.Words.Where(x => x.Syllables.All(x => list.Contains(x) && x.NextShow < DateTime.UtcNow)).ToList();
            if (words.Count < 3)
            {
                throw new NotEnoughWordsException();
            }
            var idx = random.Next(words.Count());
            words.ElementAt(idx).ShowCounter++;
            Context.SaveChanges();
            construction = futureConstruction;
            futureConstruction = words.ElementAt(idx).GetConstruction();           
            return words.ElementAt(idx).Name;
        }

        public override string GetShortSettings()
        {
            return $"Card Words - Size: {Size}";
        }

        public string DoBad()
        {
            var s = Context.Words.First(x => x.Name == currentPiece);
            s.Show = 0;
            s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
            Save();
            return $"Bad - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public string DoAverage()
        {
            var s = Context.Words.First(x => x.Name == currentPiece);
            s.Show++;
            s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
            Save();
            return $"Average - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public string DoGood()
        {
            var s = Context.Words.First(x => x.Name == currentPiece);
            s.Show++;
            s.NextShow = RepeatingRule.GetNextRepeat(++s.Show);
            Save();
            return $"Good - {currentPiece} - {s.NextShow.ToLocalTime()}";
        }

        public string GetConstruction()
        {
            return construction;
        }

        public CardWordsGenerator(Settings settings) : base(settings)
        {
        }
    }
}