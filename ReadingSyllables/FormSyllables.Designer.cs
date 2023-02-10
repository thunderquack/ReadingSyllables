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
            this.labelSyllable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelSyllable
            // 
            this.labelSyllable.AutoSize = true;
            this.labelSyllable.Location = new System.Drawing.Point(0, 0);
            this.labelSyllable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSyllable.Name = "labelSyllable";
            this.labelSyllable.Size = new System.Drawing.Size(59, 25);
            this.labelSyllable.TabIndex = 0;
            this.labelSyllable.Text = "label1";
            // 
            // FormSyllables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 750);
            this.Controls.Add(this.labelSyllable);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormSyllables";
            this.Text = "Слоги";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormSyllables_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormSyllables_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label labelSyllable;
    }
}