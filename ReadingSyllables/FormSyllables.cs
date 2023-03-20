using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReadingSyllables.Exceptions;
using ReadingSyllables.Models;
using ReadingSyllables.Services;
using ReadingSyllables.Statistics;
using ReadingSyllables.SyllablesGenerator;
using System.Text;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        public string CurrentPiece
        {
            get
            {
                return piecesGenerator.GetCurrentPiece();
            }
        }
        public string NextPiece
        {
            get
            {
                return piecesGenerator.GetNextPiece();
            }
        }
        private Settings settings;
        private AbstractGenerator piecesGenerator;
        private string construction = string.Empty;
        private int currentSyllable = 0;

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
                    piecesGenerator = new RandomSyllablesGenerator(settings);
                    piecesGenerator.Size = 2;
                    break;

                case ApplicationMode.Rating:
                    piecesGenerator = new RatingSyllablesGenerator(settings);
                    break;

                case ApplicationMode.CardSyllables:
                    ImportCards();
                    piecesGenerator = new CardSyllablesGenerator(settings);
                    break;

                case ApplicationMode.CardWords:
                    ImportCards();
                    // TODO:
                    // ImportWords();
                    piecesGenerator = new CardWordsGenerator(settings);
                    break;
            }
            rbText.Text = CurrentPiece.ToUpper();
            if (piecesGenerator is IHasConstruction)
            {
                construction = ((IHasConstruction)piecesGenerator).GetConstruction().ToUpper();
            }
            ResizeLabel();
            ShowSettingsInTitle();
        }

        private void ImportWords()
        {
            string json = File.ReadAllText(settings.WordsList, Encoding.UTF8);

            var wordsDict = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);
            Dictionary<string, List<string>> loadedWords = new Dictionary<string, List<string>>();
            Dictionary<string, string> constructions = new Dictionary<string, string>();
            foreach (KeyValuePair<string, JObject> word in wordsDict)
            {
                string key = word.Key.ToLower();
                List<string> values = new List<string>();
                foreach (var syllable in word.Value["syllables"])
                {
                    values.Add(syllable.ToString());
                }
                loadedWords.Add(key, values);
                constructions.Add(key, word.Value["split_word"].ToString()); // ugly but fast
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
                dbWord.Construction = constructions[word.Key];
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
            Text = $"{piecesGenerator.GetNextPiece()} - {piecesGenerator.GetShortSettings()}";
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
                        if (piecesGenerator.Settings.Size > 2)
                        {
                            piecesGenerator.Settings.Size--;
                            ShowSettingsInTitle();
                        }
                        return;

                    default:
                        piecesGenerator.Size--;
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
                        if (piecesGenerator.Settings.Size < (piecesGenerator as RatingSyllablesGenerator).GetLength() - 1)
                        {
                            piecesGenerator.Settings.Size++;
                            ShowSettingsInTitle();
                        }
                        return;

                    default:
                        piecesGenerator.Size++;
                        ShowSettingsInTitle();
                        break;
                }
            }

            if (settings.Mode == ApplicationMode.CardSyllables || settings.Mode == ApplicationMode.CardWords)
            {
                Keys[] keyCodes = { Keys.F7, Keys.F8, Keys.F9, Keys.F10 };

                if (keyCodes.Contains(e.KeyCode))
                {
                    keyProcessed = true;
                    var cardGenerator = piecesGenerator as ICardGenerator;
                    if (cardGenerator == null)
                    {
                        return;
                    }
                    // Bad
                    if (e.KeyCode == Keys.F8)
                    {
                        _ = title.SetTitle(cardGenerator.DoBad());
                    }

                    // Average
                    if (e.KeyCode == Keys.F9)
                    {
                        _ = title.SetTitle(cardGenerator.DoAverage());
                    }

                    // Good
                    if (e.KeyCode == Keys.F10)
                    {
                        _ = title.SetTitle(cardGenerator.DoGood());
                    }
                    context.SaveChanges();
                }
                if (e.KeyCode == Keys.Right)
                {
                    Random r = new();
                    int i = r.Next(rbText.Text.Length);
                    rbText.Select(0, rbText.Text.Length);
                    rbText.SelectionColor = Color.DarkBlue;
                    rbText.Select(0, i);
                    rbText.SelectionColor = Color.Green;
                }
            }

            if (keyProcessed)
            {
                try
                {
                    rbText.Text = piecesGenerator.GetCurrentPieceAndGenerateNext().ToUpper();
                    if (piecesGenerator is IHasConstruction)
                    {
                        construction = ((IHasConstruction)piecesGenerator).GetConstruction().ToUpper();
                    }
                }
                catch (NotEnoughWordsException ex)
                {
                    MessageBox.Show("Не получается сгенерировать слова," + Environment.NewLine +
                        "Возможно следует вернуться к слогам",
                        caption: "Ошибка",
                        icon: MessageBoxIcon.Stop,
                        buttons: MessageBoxButtons.OK);
                    Environment.Exit(-1);
                }
                ShowSettingsInTitle();
                ResizeLabel();
            }
        }

        private void ResizeLabel()
        {
            Graphics graphics = rbText.CreateGraphics();
            Rectangle screenRectangle = RectangleToScreen(ClientRectangle);
            int titleHeight = screenRectangle.Top - Top;
            int height = Height - titleHeight;
            Font font = new Font(rbText.Font.FontFamily, height, rbText.Font.Style);
            int minFontSize = 8;
            SizeF size = graphics.MeasureString(rbText.Text, font);
            float fontSize = font.Size;
            while (size.Width > rbText.Width - 100 || size.Height > rbText.Height)
            {
                fontSize--;
                if (fontSize < minFontSize)
                {
                    fontSize = minFontSize;
                    font = new Font(font.FontFamily, fontSize, font.Style);
                    break;
                }
                font = new Font(font.FontFamily, fontSize, font.Style);
                size = graphics.MeasureString(rbText.Text, font);
            }
            rbText.Font = font;
            graphics.Dispose();
        }

        internal void SetTitle(string title)
        {
            Text = title;
        }

        private void rbText_TextChanged(object sender, EventArgs e)
        {
            rbText.SelectAll();
            rbText.SelectionAlignment = HorizontalAlignment.Center;
            rbText.DeselectAll();
        }

        private void FormSyllables_Load(object sender, EventArgs e)
        {
            rbText.SelectAll();
            rbText.ForeColor = Color.DarkBlue;
            rbText.DeselectAll();
        }
    }
}