namespace ChemNotation
{
    partial class EditingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditingForm));
            this.Header = new System.Windows.Forms.Label();
            this.PropertyList = new System.Windows.Forms.ListBox();
            this.PropertyValue = new System.Windows.Forms.TextBox();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Location = new System.Drawing.Point(9, 9);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(117, 13);
            this.Header.TabIndex = 0;
            this.Header.Text = "[please put in the thing]";
            // 
            // PropertyList
            // 
            this.PropertyList.FormattingEnabled = true;
            this.PropertyList.Location = new System.Drawing.Point(12, 25);
            this.PropertyList.Name = "PropertyList";
            this.PropertyList.Size = new System.Drawing.Size(296, 173);
            this.PropertyList.TabIndex = 1;
            this.PropertyList.SelectedIndexChanged += new System.EventHandler(this.PropertyList_SelectedIndexChanged);
            // 
            // PropertyValue
            // 
            this.PropertyValue.Location = new System.Drawing.Point(12, 208);
            this.PropertyValue.Multiline = true;
            this.PropertyValue.Name = "PropertyValue";
            this.PropertyValue.Size = new System.Drawing.Size(215, 50);
            this.PropertyValue.TabIndex = 2;
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(233, 208);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 50);
            this.ConfirmButton.TabIndex = 3;
            this.ConfirmButton.Text = "OK";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // EditingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 270);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.PropertyValue);
            this.Controls.Add(this.PropertyList);
            this.Controls.Add(this.Header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(336, 308);
            this.MinimumSize = new System.Drawing.Size(336, 308);
            this.Name = "EditingForm";
            this.Text = "Object Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditingForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Header;
        private System.Windows.Forms.ListBox PropertyList;
        private System.Windows.Forms.TextBox PropertyValue;
        private System.Windows.Forms.Button ConfirmButton;
    }
}