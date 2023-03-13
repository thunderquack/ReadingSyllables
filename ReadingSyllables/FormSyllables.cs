using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadingSyllables.Models;
using ReadingSyllables.Services;
using ReadingSyllables.Statistics;
using ReadingSyllables.SyllablesGenerator;
using System.Text;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private string syllable = "";
        private string nextSyllable = "";
        private Settings settings;
        private AbstractGenerator syllablesGenerator;

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
                    syllablesGenerator.Size = 2;
                    break;

                case ApplicationMode.Rating:
                    syllablesGenerator = new RatingSyllablesGenerator(settings);
                    break;

                case ApplicationMode.CardSyllables:
                    ImportCards();
                    syllablesGenerator = new CardSyllablesGenerator(settings);
                    break;

                case ApplicationMode.CardWords:
                    ImportCards();
                    ImportWords();
                    syllablesGenerator = new CardWordsGenerator(settings);
                    break;
            }
            syllablesGenerator.GetCurrentSyllableAndGenerateNext();
            syllable = syllablesGenerator.GetCurrentSyllable();
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
                if (!dbWord.Syllables.Equals(lSyllables))
                {
                    dbWord.Syllables = lSyllables;
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
            bool keyProcessed = false;

            // Button Presses

            if (e.KeyCode == Keys.F5)
            {
                Program.host.Services.GetRequiredService<StatisticsCalculator>().ShowStatisticsForm();
                return;
            }

            if (e.KeyCode == Keys.F11)
            {
                keyProcessed = true;
                switch (settings.Mode)
                {
                    case ApplicationMode.Random:
                        return;

                    case ApplicationMode.Rating:
                        if (syllablesGenerator.Settings.Size > 2)
                        {
                            syllablesGenerator.Settings.Size--;
                            ShowSettingsInTitle();
                        }
                        return;

                    default:
                        syllablesGenerator.Size--;
                        ShowSettingsInTitle();
                        break;
                }
            }

            if (e.KeyCode == Keys.F12)
            {
                keyProcessed = true;
                switch (settings.Mode)
                {
                    case ApplicationMode.Random:
                        return;

                    case ApplicationMode.Rating:
                        if (syllablesGenerator.Settings.Size < (syllablesGenerator as RatingSyllablesGenerator).GetLength() - 1)
                        {
                            syllablesGenerator.Settings.Size++;
                            ShowSettingsInTitle();
                        }
                        return;

                    default:
                        syllablesGenerator.Size++;
                        ShowSettingsInTitle();
                        break;
                }
            }

            if (settings.Mode == ApplicationMode.CardSyllables)
            {
                Keys[] keyCodes = { Keys.F7, Keys.F8, Keys.F9, Keys.F10 };

                if (keyCodes.Contains(e.KeyCode))
                {
                    string shownSyllable = labelSyllable.Text.ToLower();
                    keyProcessed = true;
                    var s = context.Syllables.FirstOrDefault(x => x.Name == shownSyllable);

                    // Bad
                    if (e.KeyCode == Keys.F8)
                    {
                        s.Show = 0;
                        s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
                        _ = title.SetTitle($"Bad - {s.NextShow.ToLocalTime()}");
                    }

                    // Average
                    if (e.KeyCode == Keys.F9)
                    {
                        s.Show++;
                        s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
                        _ = title.SetTitle($"Average - {s.NextShow.ToLocalTime()}");
                    }

                    // Good
                    if (e.KeyCode == Keys.F10)
                    {
                        s.Show++;
                        s.NextShow = RepeatingRule.GetNextRepeat(++s.Show);
                        _ = title.SetTitle($"Good - {s.NextShow.ToLocalTime()}");
                    }
                    context.SaveChanges();
                }
            }

            if (keyProcessed)
            {
                nextSyllable = syllablesGenerator.GetCurrentSyllableAndGenerateNext();
                labelSyllable.Text = syllable.ToUpper();
                syllable = nextSyllable;
                ShowSettingsInTitle();
                ResizeLabel();
            }
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