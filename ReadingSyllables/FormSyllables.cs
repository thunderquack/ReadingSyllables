using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ReadingSyllables.Models;
using ReadingSyllables.SyllablesGenerator;
using System.Text;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private bool sizeWasChanged = true;

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


        internal SyllablesContext SyllablesContext
        {
            get
            {
                return Program.host.Services.GetRequiredService<SyllablesContext>();
            }
        }

        public FormSyllables()
        {
            InitializeComponent();
            settings = Settings.Load();
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
                    syllablesGenerator = new CardsSyllablesGenerator(settings);
                    syllable = syllablesGenerator.GenerateSyllable();
                    break;
            }
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
            // Button Presses

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
                    context.SaveChanges();
                }
                // Average
                if (e.KeyCode == Keys.F9)
                {
                    var s = context.Syllables.FirstOrDefault(x => x.Name == shownSyllable);
                    s.Show++;
                    s.NextShow = RepeatingRule.GetNextRepeat(s.Show);
                    context.SaveChanges();
                }
                // Good
                if (e.KeyCode == Keys.F10)
                {
                }
            }

            nextSyllable = syllablesGenerator.GenerateSyllable();
            labelSyllable.Text = syllable;
            syllable = nextSyllable;
            ShowSettingsInTitle();
            Graphics g = Graphics.FromHwndInternal(this.Handle);

            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            int height = this.Height - titleHeight;

            SizeF sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);

            labelSyllable.Size = new Size(this.Width, this.Height);

            if (sizeWasChanged)
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, height / 4, labelSyllable.Font.Style);
                sizeWasChanged = false;
            }

            while (sz.Width < this.Width)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Height < this.Height - titleHeight)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Width > this.Width)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

            while (sz.Height > this.Height - titleHeight)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }
        }

        private void FormSyllables_Resize(object sender, EventArgs e)
        {
            sizeWasChanged = true;
        }
    }
}