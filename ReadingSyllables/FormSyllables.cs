using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadingSyllables.Models;
using ReadingSyllables.Services;
using ReadingSyllables.Statistics;
using ReadingSyllables.SyllablesGenerator;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private string syllable = "";
        private string nextSyllable = "";
        private Settings settings;
        private AbstractSyllableGenerator syllablesGenerator;

        private SyllablesContext context
        {
            get
            {
                return Program.host.Services.GetRequiredService<SyllablesContext>();
            }
        }

        private TitleService title
        {
            get
            {
                return Program.host.Services.GetRequiredService<TitleService>();
            }
        }

        public FormSyllables()
        {
            InitializeComponent();
            settings = Settings.Load();
            title.SetRequiredForm(this);
            switch (settings.Mode)
            {
                case ApplicationMode.Random:
                default:
                    syllablesGenerator = new RandomSyllablesGenerator(settings);
                    (syllablesGenerator as RandomSyllablesGenerator).SetLength(2);
                    syllable = syllablesGenerator.GenerateSyllable();
                    break;

                case ApplicationMode.Rating:
                    syllablesGenerator = new RatingSyllablesGenerator(settings);
                    syllable = syllablesGenerator.GenerateSyllable();
                    break;

                case ApplicationMode.Cards:
                    ImportCards();
                    ImportWords();
                    syllablesGenerator = new CardsSyllablesGenerator(settings);
                    syllable = syllablesGenerator.GenerateSyllable();
                    break;
            }
        }

        private void ImportWords()
        {
            string json = File.ReadAllText(settings.WordsList, Encoding.UTF8);
            var words = JsonConvert.DeserializeObject(json);
            Dictionary<string, List<string>> loadedWords = new Dictionary<string, List<string>>();
            foreach (JToken word in (words as JObject).Children().ToList())
            {
                string key = word.Path.ToLower();
                List<string> values = new List<string>();
                foreach (var val in word.Values().ToList())
                {
                    values.Add(val.ToString().ToLower());
                }
                loadedWords.Add(key, values);
            }
            var dbWordsList = context.Words.ToList();
            foreach (var word in dbWordsList)
            {
                if (!loadedWords.ContainsKey(word.Name))
                {
                    context.Words.Remove(word);
                }
            }
            context.SaveChanges();
            dbWordsList = context.Words.ToList();
            foreach (var word in loadedWords)
            {
                Word? dbWord = dbWordsList.FirstOrDefault(x => x.Name == word.Key);
                if (dbWord == null)
                {
                    dbWord = new Word()
                    {
                        Name = word.Key,
                    };
                    context.Words.Add(dbWord);
                };
                var lSyllables = context.Syllables.Where(x => word.Value.Contains(x.Name)).ToHashSet();
                dbWord.Syllables = lSyllables;
                foreach (var syllable in lSyllables)
                {
                    if (syllable.Words == null)
                    {
                        syllable.Words = new();
                    }
                    syllable.Words.Add(dbWord);
                }
            }
            context.SaveChanges();
        }

        private void ImportCards()
        {
            string json = File.ReadAllText(settings.FileName, Encoding.UTF8);
            var sylls = JsonConvert.DeserializeObject(json);
            foreach (JObject item in (sylls as JArray))
            {
                int rating = Convert.ToInt32(item.GetValue("value"));
                string name = Convert.ToString(item.GetValue("name"));
                var dbSyll = context.Syllables.FirstOrDefault(x => x.Name == name);
                if (dbSyll == null)
                {
                    Syllable s = new()
                    {
                        Rating = rating,
                        Name = name,
                    };
                    context.Syllables.Add(s);
                }
            }
            context.SaveChanges();
        }

        private void ShowSettingsInTitle()
        {
            Text = $"{syllable} - {syllablesGenerator.GetShortSettings()}";
        }

        private void FormSyllables_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            // Button Presses

            if (e.KeyCode == Keys.F5)
            {
                Program.host.Services.GetRequiredService<StatisticsCalculator>().ShowStatisticsForm();
                return;
            }

            if (e.KeyCode == Keys.F11)
            {
                switch (settings.Mode)
                {
                    case ApplicationMode.Random:
                    default:
                        return;

                    case ApplicationMode.Rating:
                        if (syllablesGenerator.Settings.MaxRating > 2)
                        {
                            syllablesGenerator.Settings.MaxRating--;
                            ShowSettingsInTitle();
                        }
                        return;
                }
            }

            if (e.KeyCode == Keys.F12)
            {
                switch (settings.Mode)
                {
                    case ApplicationMode.Random:
                    default:
                        return;

                    case ApplicationMode.Rating:
                        if (syllablesGenerator.Settings.MaxRating < (syllablesGenerator as RatingSyllablesGenerator).GetLength() - 1)
                        {
                            syllablesGenerator.Settings.MaxRating++;
                            ShowSettingsInTitle();
                        }
                        return;
                }
            }

            if (settings.Mode == ApplicationMode.Cards)
            {
                string shownSyllable = labelSyllable.Text.ToLower();

                // Bad
                if (e.KeyCode == Keys.F8)
                {
                    var s = context.Syllables.FirstOrDefault(x => x.Name == shownSyllable);
                    s.Show = 0;
                    s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
                    _ = title.SetTitle($"Bad - {s.NextShow}");
                    context.SaveChanges();
                }

                // Average
                if (e.KeyCode == Keys.F9)
                {
                    var s = context.Syllables.FirstOrDefault(x => x.Name == shownSyllable);
                    s.Show++;
                    s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
                    _ = title.SetTitle($"Average - {s.NextShow}");
                    context.SaveChanges();
                }

                // Good
                if (e.KeyCode == Keys.F10)
                {
                    var s = context.Syllables.FirstOrDefault(x => x.Name == shownSyllable);
                    s.Show++;
                    s.NextShow = RepeatingRule.GetNextRepeat(++s.Show);
                    _ = title.SetTitle($"Good - {s.NextShow}");
                    context.SaveChanges();
                }
            }

            nextSyllable = syllablesGenerator.GenerateSyllable();
            labelSyllable.Text = syllable;
            syllable = nextSyllable;
            ShowSettingsInTitle();
            ResizeLabel();
        }

        private void ResizeLabel()
        {
            Graphics graphics = labelSyllable.CreateGraphics();
            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            int height = this.Height - titleHeight;
            Font font = new Font(labelSyllable.Font.FontFamily, height, labelSyllable.Font.Style);
            int minFontSize = 8;
            SizeF size = graphics.MeasureString(labelSyllable.Text, font);
            float fontSize = font.Size;
            while (size.Width > labelSyllable.Width - 100 || size.Height > labelSyllable.Height)
            {
                fontSize--;
                if (fontSize < minFontSize)
                {
                    fontSize = minFontSize;
                    font = new Font(font.FontFamily, fontSize, font.Style);
                    break;
                }
                font = new Font(font.FontFamily, fontSize, font.Style);
                size = graphics.MeasureString(labelSyllable.Text, font);
            }
            labelSyllable.Font = font;
            graphics.Dispose();
        }

        internal void SetTitle(string title)
        {
            Text = title;
        }
    }
}