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
            var words = Context.Words
                .Where(w => w.Syllables.All(s => list.Contains(s) && w.NextShow < DateTime.UtcNow))
                .ToList();
            words = words.OrderBy(x => x.GetSyllablesCount()).ToList();
            if (words.Count < 3)
            {
                throw new NotEnoughWordsException();
            }
            int minimumSyllables = words[0].GetSyllablesCount();
            var chosenWords = words.Where(x => x.GetSyllablesCount() <= minimumSyllables);
            if (chosenWords.Count() < 3)
            {
                chosenWords = words.Where(x => x.GetSyllablesCount() <= minimumSyllables + 1);
            }
            var idx = random.Next(chosenWords.Count());
            var chosenWord = words[idx];
            chosenWord.ShowCounter++;
            Context.SaveChanges();
            construction = futureConstruction;
            futureConstruction = chosenWord.GetConstruction();
            return chosenWord.Name;
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