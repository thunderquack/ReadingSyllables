using ReadingSyllables.Models;

namespace ReadingSyllables.Statistics
{
    internal class StatisticsCalculator
    {
        private SyllablesContext syllablesContext;
        public StatisticsCalculator(SyllablesContext syllablesContext)
        {
            this.syllablesContext = syllablesContext;
        }

        public void ShowStatisticsForm()
        {
            var topTenSyllables = syllablesContext.Syllables.OrderByDescending(x => x.Show).Take(10).ToList();
            string topTen = "";
            foreach (var syllable in topTenSyllables)
            {
                topTen += $"{syllable.Name}: {syllable.Show}, {syllable.NextShow.ToLocalTime()}{Environment.NewLine}";
            }
            MessageBox.Show(topTen, "Статистика");
        }
        //a
        //b
    }
}
