namespace ChemNotation
{
    partial class AtomSelectionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AtomList = new System.Windows.Forms.ListBox();
            this.Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // AtomList
            // 
            this.AtomList.FormattingEnabled = true;
            this.AtomList.Location = new System.Drawing.Point(12, 27);
            this.AtomList.Name = "AtomList";
            this.AtomList.Size = new System.Drawing.Size(216, 121);
            this.AtomList.TabIndex = 0;
            this.AtomList.SelectedIndexChanged += new System.EventHandler(this.AtomList_SelectedIndexChanged);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(12, 9);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(78, 13);
            this.Label.TabIndex = 1;
            this.Label.Text = "Select Element";
            // 
            // AtomSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 160);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.AtomList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AtomSelectionForm";
            this.Text = "Atom Selection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox AtomList;
        private System.Windows.Forms.Label Label;
    }
}