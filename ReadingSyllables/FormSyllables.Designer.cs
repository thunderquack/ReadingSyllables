namespace ReadingSyllables
{
    partial class FormSyllables
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rbText = new RichTextBox();
            SuspendLayout();
            // 
            // rbText
            // 
            rbText.BackColor = SystemColors.Control;
            rbText.BorderStyle = BorderStyle.None;
            rbText.Dock = DockStyle.Fill;
            rbText.Enabled = false;
            rbText.Location = new Point(0, 0);
            rbText.Margin = new Padding(0);
            rbText.Name = "rbText";
            rbText.ReadOnly = true;
            rbText.Size = new Size(1143, 750);
            rbText.TabIndex = 0;
            rbText.Text = "СЛОГ";
            rbText.TextChanged += rbText_TextChanged;
            // 
            // FormSyllables
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1143, 750);
            Controls.Add(rbText);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormSyllables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Слоги";
            Load += FormSyllables_Load;
            KeyDown += FormSyllables_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rbText;
    }
}