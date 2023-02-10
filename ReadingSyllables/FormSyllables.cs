using System.Reflection.Metadata;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private bool sizeWasChanged = true;

        private string prevSyllable = "";
        public FormSyllables()
        {
            InitializeComponent();
        }

        private void FormSyllables_KeyPress(object sender, KeyPressEventArgs e)
        {
            prevSyllable = SyllablesGenerator.GenerateSyllable(2, prevSyllable);
            labelSyllable.Text = prevSyllable;
            Graphics g = Graphics.FromHwndInternal(this.Handle);
            var form = sender as Form;

            SizeF sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
            int width = form.Width - 100;
            int height = form.Height - 100;

            if (sizeWasChanged)
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, height / 4, labelSyllable.Font.Style);
                sizeWasChanged= false;
            }
            labelSyllable.Size = new Size(form.Width, form.Height);

            while (sz.Width < (sender as Form).Width - 100)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Height < (sender as Form).Height - 100)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Width > (sender as Form).Width)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

            while (sz.Height > (sender as Form).Height)
            {
                sz = g.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

        }

        private void FormSyllables_Resize(object sender, EventArgs e)
        {
            sizeWasChanged= true;
        }
    }
}