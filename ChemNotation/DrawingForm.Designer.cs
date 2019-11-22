namespace ChemNotation
{
    partial class DrawingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawingForm));
            this.DiagramView = new System.Windows.Forms.PictureBox();
            this.ButtonGroup = new System.Windows.Forms.GroupBox();
            this.ButtonAtomHydrogen = new System.Windows.Forms.Button();
            this.ButtonCurve = new System.Windows.Forms.Button();
            this.ButtonObject = new System.Windows.Forms.Button();
            this.ButtonBraces = new System.Windows.Forms.Button();
            this.ButtonBrackets = new System.Windows.Forms.Button();
            this.ButtonParentheses = new System.Windows.Forms.Button();
            this.ButtonChargeNegative = new System.Windows.Forms.Button();
            this.ButtonChargePositive = new System.Windows.Forms.Button();
            this.ButtonOxygen = new System.Windows.Forms.Button();
            this.ButtonAtom = new System.Windows.Forms.Button();
            this.ButtonArrowCurved = new System.Windows.Forms.Button();
            this.ButtonAtomNitrogen = new System.Windows.Forms.Button();
            this.ButtonArrowStraight = new System.Windows.Forms.Button();
            this.ButtonLine = new System.Windows.Forms.Button();
            this.ButtonAtomCarbon = new System.Windows.Forms.Button();
            this.ButtonBondAromatic = new System.Windows.Forms.Button();
            this.ButtonBondTriple = new System.Windows.Forms.Button();
            this.ButtonBondDouble = new System.Windows.Forms.Button();
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.ButtonBondSingle = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusTextBox = new System.Windows.Forms.TextBox();
            this.DrawingFormToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.newDiagramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.DiagramView)).BeginInit();
            this.ButtonGroup.SuspendLayout();
            this.DrawingFormToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DiagramView
            // 
            this.DiagramView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DiagramView.Location = new System.Drawing.Point(158, 31);
            this.DiagramView.Name = "DiagramView";
            this.DiagramView.Size = new System.Drawing.Size(800, 600);
            this.DiagramView.TabIndex = 0;
            this.DiagramView.TabStop = false;
            this.DiagramView.Click += new System.EventHandler(this.DiagramView_Click);
            // 
            // ButtonGroup
            // 
            this.ButtonGroup.Controls.Add(this.ButtonAtomHydrogen);
            this.ButtonGroup.Controls.Add(this.ButtonCurve);
            this.ButtonGroup.Controls.Add(this.ButtonObject);
            this.ButtonGroup.Controls.Add(this.ButtonBraces);
            this.ButtonGroup.Controls.Add(this.ButtonBrackets);
            this.ButtonGroup.Controls.Add(this.ButtonParentheses);
            this.ButtonGroup.Controls.Add(this.ButtonChargeNegative);
            this.ButtonGroup.Controls.Add(this.ButtonChargePositive);
            this.ButtonGroup.Controls.Add(this.ButtonOxygen);
            this.ButtonGroup.Controls.Add(this.ButtonAtom);
            this.ButtonGroup.Controls.Add(this.ButtonArrowCurved);
            this.ButtonGroup.Controls.Add(this.ButtonAtomNitrogen);
            this.ButtonGroup.Controls.Add(this.ButtonArrowStraight);
            this.ButtonGroup.Controls.Add(this.ButtonLine);
            this.ButtonGroup.Controls.Add(this.ButtonAtomCarbon);
            this.ButtonGroup.Controls.Add(this.ButtonBondAromatic);
            this.ButtonGroup.Controls.Add(this.ButtonBondTriple);
            this.ButtonGroup.Controls.Add(this.ButtonBondDouble);
            this.ButtonGroup.Controls.Add(this.ButtonSelect);
            this.ButtonGroup.Controls.Add(this.ButtonBondSingle);
            this.ButtonGroup.Location = new System.Drawing.Point(12, 31);
            this.ButtonGroup.Name = "ButtonGroup";
            this.ButtonGroup.Size = new System.Drawing.Size(130, 600);
            this.ButtonGroup.TabIndex = 1;
            this.ButtonGroup.TabStop = false;
            this.ButtonGroup.Text = "Tools";
            // 
            // ButtonAtomHydrogen
            // 
            this.ButtonAtomHydrogen.Location = new System.Drawing.Point(6, 162);
            this.ButtonAtomHydrogen.Name = "ButtonAtomHydrogen";
            this.ButtonAtomHydrogen.Size = new System.Drawing.Size(118, 23);
            this.ButtonAtomHydrogen.TabIndex = 5;
            this.ButtonAtomHydrogen.Text = "Hydrogen";
            this.ButtonAtomHydrogen.UseVisualStyleBackColor = true;
            this.ButtonAtomHydrogen.Click += new System.EventHandler(this.ButtonAtomHydrogen_Click);
            // 
            // ButtonCurve
            // 
            this.ButtonCurve.Location = new System.Drawing.Point(6, 336);
            this.ButtonCurve.Name = "ButtonCurve";
            this.ButtonCurve.Size = new System.Drawing.Size(118, 23);
            this.ButtonCurve.TabIndex = 11;
            this.ButtonCurve.Text = "Curve";
            this.ButtonCurve.UseVisualStyleBackColor = true;
            this.ButtonCurve.Click += new System.EventHandler(this.ButtonCurve_Click);
            // 
            // ButtonObject
            // 
            this.ButtonObject.Location = new System.Drawing.Point(6, 568);
            this.ButtonObject.Name = "ButtonObject";
            this.ButtonObject.Size = new System.Drawing.Size(118, 23);
            this.ButtonObject.TabIndex = 19;
            this.ButtonObject.Text = "Miscellaneous";
            this.ButtonObject.UseVisualStyleBackColor = true;
            this.ButtonObject.Click += new System.EventHandler(this.ButtonObject_Click);
            // 
            // ButtonBraces
            // 
            this.ButtonBraces.Location = new System.Drawing.Point(6, 539);
            this.ButtonBraces.Name = "ButtonBraces";
            this.ButtonBraces.Size = new System.Drawing.Size(118, 23);
            this.ButtonBraces.TabIndex = 18;
            this.ButtonBraces.Text = "{ } Braces";
            this.ButtonBraces.UseVisualStyleBackColor = true;
            this.ButtonBraces.Click += new System.EventHandler(this.ButtonBraces_Click);
            // 
            // ButtonBrackets
            // 
            this.ButtonBrackets.Location = new System.Drawing.Point(6, 510);
            this.ButtonBrackets.Name = "ButtonBrackets";
            this.ButtonBrackets.Size = new System.Drawing.Size(118, 23);
            this.ButtonBrackets.TabIndex = 17;
            this.ButtonBrackets.Text = "[ ] Brackets";
            this.ButtonBrackets.UseVisualStyleBackColor = true;
            this.ButtonBrackets.Click += new System.EventHandler(this.ButtonBrackets_Click);
            // 
            // ButtonParentheses
            // 
            this.ButtonParentheses.Location = new System.Drawing.Point(6, 481);
            this.ButtonParentheses.Name = "ButtonParentheses";
            this.ButtonParentheses.Size = new System.Drawing.Size(118, 23);
            this.ButtonParentheses.TabIndex = 16;
            this.ButtonParentheses.Text = "( ) Parentheses";
            this.ButtonParentheses.UseVisualStyleBackColor = true;
            this.ButtonParentheses.Click += new System.EventHandler(this.ButtonParentheses_Click);
            // 
            // ButtonChargeNegative
            // 
            this.ButtonChargeNegative.Location = new System.Drawing.Point(6, 452);
            this.ButtonChargeNegative.Name = "ButtonChargeNegative";
            this.ButtonChargeNegative.Size = new System.Drawing.Size(118, 23);
            this.ButtonChargeNegative.TabIndex = 15;
            this.ButtonChargeNegative.Text = "Negative Charge";
            this.ButtonChargeNegative.UseVisualStyleBackColor = true;
            this.ButtonChargeNegative.Click += new System.EventHandler(this.ButtonChargeNegative_Click);
            // 
            // ButtonChargePositive
            // 
            this.ButtonChargePositive.Location = new System.Drawing.Point(6, 423);
            this.ButtonChargePositive.Name = "ButtonChargePositive";
            this.ButtonChargePositive.Size = new System.Drawing.Size(118, 23);
            this.ButtonChargePositive.TabIndex = 14;
            this.ButtonChargePositive.Text = "Positive Charge";
            this.ButtonChargePositive.UseVisualStyleBackColor = true;
            this.ButtonChargePositive.Click += new System.EventHandler(this.ButtonChargePositive_Click);
            // 
            // ButtonOxygen
            // 
            this.ButtonOxygen.Location = new System.Drawing.Point(6, 133);
            this.ButtonOxygen.Name = "ButtonOxygen";
            this.ButtonOxygen.Size = new System.Drawing.Size(118, 23);
            this.ButtonOxygen.TabIndex = 4;
            this.ButtonOxygen.Text = "Oxygen";
            this.ButtonOxygen.UseVisualStyleBackColor = true;
            this.ButtonOxygen.Click += new System.EventHandler(this.ButtonOxygen_Click);
            // 
            // ButtonAtom
            // 
            this.ButtonAtom.Location = new System.Drawing.Point(6, 46);
            this.ButtonAtom.Name = "ButtonAtom";
            this.ButtonAtom.Size = new System.Drawing.Size(118, 23);
            this.ButtonAtom.TabIndex = 1;
            this.ButtonAtom.Text = "Atom";
            this.ButtonAtom.UseVisualStyleBackColor = true;
            this.ButtonAtom.Click += new System.EventHandler(this.ButtonAtom_Click);
            // 
            // ButtonArrowCurved
            // 
            this.ButtonArrowCurved.Location = new System.Drawing.Point(6, 394);
            this.ButtonArrowCurved.Name = "ButtonArrowCurved";
            this.ButtonArrowCurved.Size = new System.Drawing.Size(118, 23);
            this.ButtonArrowCurved.TabIndex = 13;
            this.ButtonArrowCurved.Text = "Curved Arrow";
            this.ButtonArrowCurved.UseVisualStyleBackColor = true;
            this.ButtonArrowCurved.Click += new System.EventHandler(this.ButtonArrowCurved_Click);
            // 
            // ButtonAtomNitrogen
            // 
            this.ButtonAtomNitrogen.Location = new System.Drawing.Point(6, 104);
            this.ButtonAtomNitrogen.Name = "ButtonAtomNitrogen";
            this.ButtonAtomNitrogen.Size = new System.Drawing.Size(118, 23);
            this.ButtonAtomNitrogen.TabIndex = 3;
            this.ButtonAtomNitrogen.Text = "Nitrogen";
            this.ButtonAtomNitrogen.UseVisualStyleBackColor = true;
            this.ButtonAtomNitrogen.Click += new System.EventHandler(this.ButtonAtomNitrogen_Click);
            // 
            // ButtonArrowStraight
            // 
            this.ButtonArrowStraight.Location = new System.Drawing.Point(6, 365);
            this.ButtonArrowStraight.Name = "ButtonArrowStraight";
            this.ButtonArrowStraight.Size = new System.Drawing.Size(118, 23);
            this.ButtonArrowStraight.TabIndex = 12;
            this.ButtonArrowStraight.Text = "Straight Arrow";
            this.ButtonArrowStraight.UseVisualStyleBackColor = true;
            this.ButtonArrowStraight.Click += new System.EventHandler(this.ButtonArrowStraight_Click);
            // 
            // ButtonLine
            // 
            this.ButtonLine.Location = new System.Drawing.Point(6, 307);
            this.ButtonLine.Name = "ButtonLine";
            this.ButtonLine.Size = new System.Drawing.Size(118, 23);
            this.ButtonLine.TabIndex = 10;
            this.ButtonLine.Text = "Line";
            this.ButtonLine.UseVisualStyleBackColor = true;
            this.ButtonLine.Click += new System.EventHandler(this.ButtonLine_Click);
            // 
            // ButtonAtomCarbon
            // 
            this.ButtonAtomCarbon.Location = new System.Drawing.Point(6, 75);
            this.ButtonAtomCarbon.Name = "ButtonAtomCarbon";
            this.ButtonAtomCarbon.Size = new System.Drawing.Size(118, 23);
            this.ButtonAtomCarbon.TabIndex = 2;
            this.ButtonAtomCarbon.Text = "Carbon";
            this.ButtonAtomCarbon.UseVisualStyleBackColor = true;
            this.ButtonAtomCarbon.Click += new System.EventHandler(this.ButtonAtomCarbon_Click);
            // 
            // ButtonBondAromatic
            // 
            this.ButtonBondAromatic.Location = new System.Drawing.Point(6, 278);
            this.ButtonBondAromatic.Name = "ButtonBondAromatic";
            this.ButtonBondAromatic.Size = new System.Drawing.Size(118, 23);
            this.ButtonBondAromatic.TabIndex = 9;
            this.ButtonBondAromatic.Text = "Aromatic Bond";
            this.ButtonBondAromatic.UseVisualStyleBackColor = true;
            this.ButtonBondAromatic.Click += new System.EventHandler(this.ButtonBondAromatic_Click);
            // 
            // ButtonBondTriple
            // 
            this.ButtonBondTriple.Location = new System.Drawing.Point(6, 249);
            this.ButtonBondTriple.Name = "ButtonBondTriple";
            this.ButtonBondTriple.Size = new System.Drawing.Size(118, 23);
            this.ButtonBondTriple.TabIndex = 8;
            this.ButtonBondTriple.Text = "Triple Bond";
            this.ButtonBondTriple.UseVisualStyleBackColor = true;
            this.ButtonBondTriple.Click += new System.EventHandler(this.ButtonBondTriple_Click);
            // 
            // ButtonBondDouble
            // 
            this.ButtonBondDouble.Location = new System.Drawing.Point(6, 220);
            this.ButtonBondDouble.Name = "ButtonBondDouble";
            this.ButtonBondDouble.Size = new System.Drawing.Size(118, 23);
            this.ButtonBondDouble.TabIndex = 7;
            this.ButtonBondDouble.Text = "Double Bond";
            this.ButtonBondDouble.UseVisualStyleBackColor = true;
            this.ButtonBondDouble.Click += new System.EventHandler(this.ButtonBondDouble_Click);
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.Location = new System.Drawing.Point(6, 17);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(118, 23);
            this.ButtonSelect.TabIndex = 0;
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.UseVisualStyleBackColor = true;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonBondSingle
            // 
            this.ButtonBondSingle.Location = new System.Drawing.Point(6, 191);
            this.ButtonBondSingle.Name = "ButtonBondSingle";
            this.ButtonBondSingle.Size = new System.Drawing.Size(118, 23);
            this.ButtonBondSingle.TabIndex = 6;
            this.ButtonBondSingle.Text = "Single Bond";
            this.ButtonBondSingle.UseVisualStyleBackColor = true;
            this.ButtonBondSingle.Click += new System.EventHandler(this.ButtonBondSingle_Click);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(9, 648);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(37, 13);
            this.StatusLabel.TabIndex = 2;
            this.StatusLabel.Text = "Status";
            // 
            // StatusTextBox
            // 
            this.StatusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.StatusTextBox.Enabled = false;
            this.StatusTextBox.Location = new System.Drawing.Point(52, 645);
            this.StatusTextBox.Name = "StatusTextBox";
            this.StatusTextBox.Size = new System.Drawing.Size(906, 20);
            this.StatusTextBox.TabIndex = 3;
            // 
            // DrawingFormToolStrip
            // 
            this.DrawingFormToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.DrawingFormToolStrip.Location = new System.Drawing.Point(0, 0);
            this.DrawingFormToolStrip.Name = "DrawingFormToolStrip";
            this.DrawingFormToolStrip.Size = new System.Drawing.Size(970, 25);
            this.DrawingFormToolStrip.TabIndex = 4;
            this.DrawingFormToolStrip.Text = "Toolbar";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDiagramToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // newDiagramToolStripMenuItem
            // 
            this.newDiagramToolStripMenuItem.Name = "newDiagramToolStripMenuItem";
            this.newDiagramToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newDiagramToolStripMenuItem.Text = "New Diagram";
            this.newDiagramToolStripMenuItem.Click += new System.EventHandler(this.newDiagramToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // DrawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 670);
            this.Controls.Add(this.DrawingFormToolStrip);
            this.Controls.Add(this.StatusTextBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.ButtonGroup);
            this.Controls.Add(this.DiagramView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DrawingForm";
            this.Text = "ChemNotation - Drawing Window";
            ((System.ComponentModel.ISupportInitialize)(this.DiagramView)).EndInit();
            this.ButtonGroup.ResumeLayout(false);
            this.DrawingFormToolStrip.ResumeLayout(false);
            this.DrawingFormToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox ButtonGroup;
        private System.Windows.Forms.Button ButtonAtomNitrogen;
        private System.Windows.Forms.Button ButtonAtomCarbon;
        private System.Windows.Forms.Button ButtonOxygen;
        private System.Windows.Forms.Button ButtonCurve;
        private System.Windows.Forms.Button ButtonArrowCurved;
        private System.Windows.Forms.Button ButtonArrowStraight;
        private System.Windows.Forms.Button ButtonLine;
        private System.Windows.Forms.Button ButtonBondAromatic;
        private System.Windows.Forms.Button ButtonBondTriple;
        private System.Windows.Forms.Button ButtonBondDouble;
        private System.Windows.Forms.Button ButtonSelect;
        private System.Windows.Forms.Button ButtonBondSingle;
        private System.Windows.Forms.Button ButtonAtom;
        private System.Windows.Forms.Button ButtonAtomHydrogen;
        private System.Windows.Forms.Button ButtonChargeNegative;
        private System.Windows.Forms.Button ButtonChargePositive;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.TextBox StatusTextBox;
        private System.Windows.Forms.ToolStrip DrawingFormToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem newDiagramToolStripMenuItem;
        public System.Windows.Forms.PictureBox DiagramView;
        private System.Windows.Forms.Button ButtonObject;
        private System.Windows.Forms.Button ButtonBraces;
        private System.Windows.Forms.Button ButtonBrackets;
        private System.Windows.Forms.Button ButtonParentheses;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}