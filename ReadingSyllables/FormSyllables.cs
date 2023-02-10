using System.Reflection.Metadata;

namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        private string prevSyllable = "";
        public FormSyllables()
        {
            InitializeComponent();
        }

        private void FormSyllables_KeyPress(object sender, KeyPressEventArgs e)
        {
            prevSyllable = SyllablesGenerator.GenerateSyllable(2, prevSyllable);
            labelSyllable.Text = prevSyllable;
            this.Refresh();
            //Graphics.MeasureString()

        }

        private void FormSyllables_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            SizeF sz = e.Graphics.MeasureString(labelSyllable.Text, labelSyllable.Font);
            int width = (sender as Form).Width - 100;
            int height = (sender as Form).Height - 100;
            labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, height/4, labelSyllable.Font.Style);


            while (sz.Width < (sender as Form).Width - 100)
            {
                sz = e.Graphics.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Height < (sender as Form).Width - 100)
            {
                sz = e.Graphics.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (sz.Width > (sender as Form).Width)
            {
                sz = e.Graphics.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

            while (sz.Height > (sender as Form).Width)
            {
                sz = e.Graphics.MeasureString(labelSyllable.Text, labelSyllable.Font);
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

        }
    }
}