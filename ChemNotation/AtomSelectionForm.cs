using ChemNotation.DiagramObjects;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemNotation
{
    public partial class AtomSelectionForm : Form
    {
        public AtomSelectionForm()
        {
            ControlBox = false;
            InitializeComponent();

            foreach (JSONData.DataBlock block in JSONData.Data)
            {
                if (block.Name.Contains("DUMMY")) continue;
                AtomList.Items.Add($"{block.Name}, {block.Symbol}");
            }
        }

        private void AtomList_SelectedIndexChanged(object sender, EventArgs e)
        {
            JSONData.DataBlock? block = JSONData.QueryName((AtomList.SelectedItem.ToString().Split(new string[] { ", " }, StringSplitOptions.None)[0]));
            if (block.HasValue)
            {
                JSONData.DataBlock b = block.Value;

                SKColor c = new SKColor((byte)b.Red, (byte)b.Green, (byte)b.Blue, 255);

                Atom selected = new Atom(b.Symbol, 0, 0, c);
                Program.DForm.CurrentObject = selected;
            }

            Close();
        }
    }
}
