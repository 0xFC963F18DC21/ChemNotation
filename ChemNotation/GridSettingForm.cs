using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace ChemNotation
{
    public partial class GridSettingForm : Form
    {
        private static ProgramSettings _Settings;
        public static ProgramSettings Settings
        {
            get
            {
                if (_Settings == null)
                {
                    // Get settings from file.
                    using (StreamReader reader = File.OpenText(@"Resource/Settings.json"))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        _Settings = (ProgramSettings)serializer.Deserialize(reader, typeof(ProgramSettings));
                    }
                }

                if (_Settings == null) _Settings = new ProgramSettings(0, 0, 0, 0);
                return _Settings;
            }
            set
            {
                _Settings = value;
            }
        }

        public GridSettingForm()
        {
            InitializeComponent();
            ControlBox = false;

            TextBoxWidth.Text = Settings.GridWidth.ToString();
            TextBoxHeight.Text = Settings.GridHeight.ToString();
            TextBoxXOffset.Text = Settings.GridXOffset.ToString();
            TextBoxYOffset.Text = Settings.GridYOffset.ToString();

            Program.DForm.UpdateScreen(true);
        }

        public class ProgramSettings
        {
            public int GridWidth { get; set; }
            public int GridHeight { get; set; }
            public int GridXOffset { get; set; }
            public int GridYOffset { get; set; }

            public ProgramSettings(int gridWidth, int gridHeight, int gridXOffset, int gridYOffset)
            {
                GridWidth = gridWidth;
                GridHeight = gridHeight;
                GridXOffset = gridXOffset;
                GridYOffset = gridYOffset;
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            // Closes without saving.
            Close();
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            // Confirm settings.
            int w = 0, h = 0, xo = 0, yo = 0;
            int.TryParse(TextBoxWidth.Text, out w);
            int.TryParse(TextBoxHeight.Text, out h);
            int.TryParse(TextBoxXOffset.Text, out xo);
            int.TryParse(TextBoxYOffset.Text, out yo);

            Settings.GridWidth = w;
            Settings.GridHeight = h;
            Settings.GridXOffset = xo;
            Settings.GridYOffset = yo;

            // TODO: Save to settings.
            JsonSerializer serializer = new JsonSerializer();
            using (TextWriter writer = new StreamWriter(@"Resource/Settings.json"))
            {
                serializer.Serialize(writer, Settings);
            }

            // If invalid, the text in the box will be reset to zero.
            TextBoxWidth.Text = w.ToString();
            TextBoxHeight.Text = h.ToString();
            Program.DForm.UpdateScreen(true);

            Close();
        }
    }
}
