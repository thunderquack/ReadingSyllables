﻿namespace ReadingSyllables
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
            this.labelSyllable.Location = new System.Drawing.Point(0, 0);
            this.labelSyllable.Name = "labelSyllable";
            this.labelSyllable.Size = new System.Drawing.Size(41, 15);
            this.labelSyllable.TabIndex = 0;
            this.labelSyllable.Text = "Слог";
            this.labelSyllable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FormSyllables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelSyllable);
            this.Name = "FormSyllables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Слоги";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSyllables_KeyDown);
            this.Resize += new System.EventHandler(this.FormSyllables_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Label labelSyllable;
    }
}