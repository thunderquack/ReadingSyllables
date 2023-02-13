using ReadingSyllables.SyllablesGenerator;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private bool sizeWasChanged = true;

        private string syllable = "";
        private string nextSyllable = "";
        private Settings settings;
        private AbstractSyllableGenerator syllablesGenerator;

        public FormSyllables()
        {
            InitializeComponent();
            settings = Settings.Load();
            if (settings.Mode == ApplicationMode.Random)
            {
                syllablesGenerator = new RandomSyllablesGenerator(settings);
                (syllablesGenerator as RandomSyllablesGenerator).SetLength(2);
                syllable = syllablesGenerator.GenerateSyllable();
            }
        }

        private void FormSyllables_KeyPress(object sender, KeyPressEventArgs e)
        {
            nextSyllable = syllablesGenerator.GenerateSyllable();
            labelSyllable.Text = syllable;
            Text = nextSyllable;
            syllable = nextSyllable;
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