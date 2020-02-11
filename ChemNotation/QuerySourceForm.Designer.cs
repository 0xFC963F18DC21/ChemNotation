namespace ChemNotation
{
    partial class QuerySourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuerySourceForm));
            this.ButtonChemSpider = new System.Windows.Forms.Button();
            this.ButtonPubChem = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonChemSpider
            // 
            this.ButtonChemSpider.Location = new System.Drawing.Point(12, 85);
            this.ButtonChemSpider.Name = "ButtonChemSpider";
            this.ButtonChemSpider.Size = new System.Drawing.Size(296, 23);
            this.ButtonChemSpider.TabIndex = 0;
            this.ButtonChemSpider.Text = "ChemSpider";
            this.ButtonChemSpider.UseVisualStyleBackColor = true;
            this.ButtonChemSpider.Click += new System.EventHandler(this.ButtonChemSpider_Click);
            // 
            // ButtonPubChem
            // 
            this.ButtonPubChem.Location = new System.Drawing.Point(12, 56);
            this.ButtonPubChem.Name = "ButtonPubChem";
            this.ButtonPubChem.Size = new System.Drawing.Size(296, 23);
            this.ButtonPubChem.TabIndex = 0;
            this.ButtonPubChem.Text = "PubChem";
            this.ButtonPubChem.UseVisualStyleBackColor = true;
            this.ButtonPubChem.Click += new System.EventHandler(this.ButtonPubChem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please choose a chemical database website to query\r\n\r\n";
            // 
            // QuerySourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 120);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonPubChem);
            this.Controls.Add(this.ButtonChemSpider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QuerySourceForm";
            this.Text = "Choose Source";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonChemSpider;
        private System.Windows.Forms.Button ButtonPubChem;
        private System.Windows.Forms.Label label1;
    }
}