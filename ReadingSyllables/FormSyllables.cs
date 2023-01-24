namespace ReadingSyllables
{
    public partial class FormSyllables : Form
    {
        public FormSyllables()
        {
            InitializeComponent();
        }

        private void FormSyllables_KeyPress(object sender, KeyPressEventArgs e)
        {
            labelSyllable.Text = "Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю Съешь ещё этих мягких французских булок да выпей чаю";

            while (labelSyllable.Width > ((sender as Form) != null ? (sender as Form).Width : 800))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size - 0.5f, labelSyllable.Font.Style);
            }

            while (labelSyllable.Width < ((sender as Form) != null ? (sender as Form).Width - 200 : 0))
            {
                labelSyllable.Font = new Font(labelSyllable.Font.FontFamily, labelSyllable.Font.Size + 0.5f, labelSyllable.Font.Style);
            }

        }

        private void tlPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}