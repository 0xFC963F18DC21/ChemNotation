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
using ChemNotation.DiagramObjects;
using SkiaSharp;
using SkiaSharp.Views;

namespace ChemNotation
{
    public partial class DrawingForm : Form
    {
        public Diagram CurrentDiagram { get; private set; }

        public DrawingForm()
        {
            CurrentDiagram = new Diagram();

            InitializeComponent();

            UpdateScreen();
        }

        /// <summary>
        /// Updates the diagram view on the screen.
        /// </summary>
        public void UpdateScreen()
        {
            SKSurface surf = CurrentDiagram.UpdateView();
            using (SKImage screen = surf.Snapshot())
            using (SKData data = screen.Encode(SKEncodedImageFormat.Png, 100))
            using (Stream stream = data.AsStream())
            {
                // The above using statement chain converts the SKSurface into a stream,
                // which can be used to make a Windows Forms-compatible Bitmap.
                Bitmap img = new Bitmap(stream, false);
                if (DiagramView.Image != null) DiagramView.Image.Dispose();
                DiagramView.Image = img;
            }
        }

        private void DiagramView_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            Point clickLocation = args.Location;

            SKPaint style = new SKPaint();
            style.Color = SKColors.Black;

            int testSquareWidth = 8;

            CurrentDiagram.DiagramSurface.Canvas.DrawRect(
                clickLocation.X - (testSquareWidth / 2),
                clickLocation.Y - (testSquareWidth / 2),
                testSquareWidth, testSquareWidth,
                style);
            UpdateScreen();
        }
    }
}
