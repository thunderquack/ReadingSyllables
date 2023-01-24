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

            while (labelSyllable.Width > ((sender as Form) != null ? (sender as Form).Width : 800))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

            while (labelSyllable.Height > ((sender as Form) != null ? (sender as Form).Height : 800))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 1f, labelSyllable.Font.Style);
            }

            while (labelSyllable.Width < ((sender as Form) != null ? (sender as Form).Width - 300 : 0))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }

            while (labelSyllable.Height < ((sender as Form) != null ? (sender as Form).Height - 300 : 0))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 1f, labelSyllable.Font.Style);
            }
        }

        private void tlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}