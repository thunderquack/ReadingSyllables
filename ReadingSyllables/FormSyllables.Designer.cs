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
            labelSyllable = new Label();
            SuspendLayout();
            // 
            // labelSyllable
            // 
            labelSyllable.Dock = DockStyle.Fill;
            labelSyllable.Location = new Point(0, 0);
            labelSyllable.Margin = new Padding(0);
            labelSyllable.Name = "labelSyllable";
            labelSyllable.Size = new Size(1143, 750);
            labelSyllable.TabIndex = 0;
            labelSyllable.Text = "СЛОГ";
            labelSyllable.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormSyllables
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1143, 750);
            Controls.Add(labelSyllable);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FormSyllables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Слоги";
            KeyDown += FormSyllables_KeyDown;
            ResumeLayout(false);
        }

        #endregion

        private Label labelSyllable;
    }
}