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
        // Debug logger
        private static readonly ErrorLogger Log = new ErrorLogger(typeof(DrawingForm));

        // Current displayed diagram.
        public Diagram CurrentDiagram { get; private set; }

        // Current selected tool.
        private DiagramAction SelectedTool { get; set; }

        // Current diagram object
        public DiagramObject CurrentObject { get; set; } = null;

        public string MessageBoxText {
            set
            {
                if (value != StatusTextBox.Text)
                {
                    Log.LogMessageInfo("STATUS MESSAGE TEXT BOX UPDATED:\r\n\t" + value);
                    StatusTextBox.Text = value;
                }
            }
        }

        public DrawingForm()
        {
            InitializeComponent();

            Log.LogMessageInfo("New diagram created.");
            MessageBoxText = "Ready.";
            SelectedTool = DiagramAction.Select;

            CurrentDiagram = new Diagram(DiagramView.Width, DiagramView.Height);
            UpdateScreen();
        }

        /// <summary>
        /// Updates the diagram view on the screen.
        /// <param name="clear">Clear the screen before redrawing?</param>
        /// </summary>
        public void UpdateScreen(bool clear)
        {
            try
            {
                SKSurface surf = CurrentDiagram.UpdateView(clear);
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

                if (clear) MessageBoxText = "Diagram view cleared and redrawn.";
            }
            catch (Exception e)
            {
                Log.LogMessageError("Uncaught exception.");
                ErrorLogger.ShowErrorMessageBox(e);
            }
        }


        /// <summary>
        /// Updates the diagram view on the screen without clearing first.
        /// </summary>
        public void UpdateScreen()
        {
            UpdateScreen(false);
        }

        private void DiagramView_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            Point clickLocation = args.Location;

            // SKPaint style = new SKPaint();
            // style.Color = SKColors.Black;

            // int testSquareWidth = 8;

            // CurrentDiagram.DiagramSurface.Canvas.DrawRect(
            //     clickLocation.X - (testSquareWidth / 2),
            //     clickLocation.Y - (testSquareWidth / 2),
            //     testSquareWidth, testSquareWidth,
            //     style);

            try
            {
                // CurrentDiagram.AddDiagramObject(new Atom("C", clickLocation.X, clickLocation.Y));
                switch (SelectedTool)
                {
                    case DiagramAction.Select:
                        break;
                    case DiagramAction.PlaceAtom:
                        var par = CurrentObject.GetInternalParameters();

                        par["X"] = (float)clickLocation.X;
                        par["Y"] = (float)clickLocation.Y;

                        Atom a = new Atom();
                        a.EditInternalParameters(par);

                        CurrentDiagram.AddDiagramObject(a);
                        break;
                    case DiagramAction.PlaceAtomCarbon:
                        CurrentDiagram.AddDiagramObject(new Atom("C", clickLocation.X, clickLocation.Y));
                        break;
                    case DiagramAction.PlaceAtomNitrogen:
                        CurrentDiagram.AddDiagramObject(new Atom("N", clickLocation.X, clickLocation.Y, new SKColor(0, 128, 0)));
                        break;
                    case DiagramAction.PlaceAtomOxygen:
                        CurrentDiagram.AddDiagramObject(new Atom("O", clickLocation.X, clickLocation.Y, new SKColor(255, 0, 0)));
                        break;
                    case DiagramAction.PlaceAtomHydrogen:
                        CurrentDiagram.AddDiagramObject(new Atom("H", clickLocation.X, clickLocation.Y, new SKColor(128, 128, 128)));
                        break;
                    case DiagramAction.PlaceSingleBond:
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Single);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        return;
                    case DiagramAction.PlaceDoubleBond:
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Double);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        return;
                    case DiagramAction.PlaceTripleBond:
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Triple);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        return;
                    case DiagramAction.PlaceAromaticBond:
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Aromatic);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        return;
                    case DiagramAction.PlaceBondEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Bond)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            CurrentObject.EditInternalParameters(parameters);

                            CurrentDiagram.AddDiagramObject(CurrentObject);
                            ToolGroup.Enabled = true;

                            switch (((Bond)CurrentObject).TypeOfBond)
                            {
                                case Bond.BondType.Single:
                                    SelectedTool = DiagramAction.PlaceSingleBond;
                                    break;
                                case Bond.BondType.Double:
                                    SelectedTool = DiagramAction.PlaceDoubleBond;
                                    break;
                                case Bond.BondType.Triple:
                                    SelectedTool = DiagramAction.PlaceTripleBond;
                                    break;
                                case Bond.BondType.Aromatic:
                                    SelectedTool = DiagramAction.PlaceAromaticBond;
                                    break;
                            }
                            // CurrentObject = null;
                        }
                        break;
                    case DiagramAction.PlaceLineStart:
                        break;
                    case DiagramAction.PlaceLineEnd:
                        break;
                    case DiagramAction.PlaceCurveStart:
                        break;
                    case DiagramAction.PlaceCurveEnd:
                        break;
                    case DiagramAction.PlaceStraightArrowStart:
                        break;
                    case DiagramAction.PlaceStraightArrowEnd:
                        break;
                    case DiagramAction.PlaceCurvedArrowStart:
                        break;
                    case DiagramAction.PlaceCurvedArrowPoint:
                        break;
                    case DiagramAction.ChargeIncrease:
                        break;
                    case DiagramAction.ChargeDecrease:
                        break;
                    case DiagramAction.PlaceTextGeneric:
                        break;
                    case DiagramAction.PlaceTextLabel:
                        break;
                    case DiagramAction.PlaceParentheses:
                        break;
                    case DiagramAction.PlaceBrackets:
                        break;
                    case DiagramAction.PlaceBraces:
                        break;
                    case DiagramAction.PlaceMiscellaneous:
                        break;
                    case DiagramAction.EditObjectHandle:
                        break;
                    default:
                        break;
                }

                UpdateScreen(true);
            } catch (Exception err)
            {
                ErrorLogger.ShowErrorMessageBox(err);
            }
        }

        /// <summary>
        /// Enum for currently-selected action.
        /// </summary>
        public enum DiagramAction
        {
            Select,
            PlaceAtom, PlaceAtomCarbon, PlaceAtomNitrogen, PlaceAtomOxygen, PlaceAtomHydrogen, 
            PlaceSingleBond, PlaceDoubleBond, PlaceTripleBond, PlaceAromaticBond, PlaceBondEnd,
            PlaceLineStart, PlaceLineEnd,
            PlaceCurveStart, PlaceCurveEnd,
            PlaceStraightArrowStart, PlaceStraightArrowEnd,
            PlaceCurvedArrowStart, PlaceCurvedArrowPoint,
            ChargeIncrease, ChargeDecrease,
            PlaceTextGeneric, PlaceTextLabel,
            PlaceParentheses, PlaceBrackets, PlaceBraces, PlaceMiscellaneous,
            EditObjectHandle
        }

        private void newDiagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentDiagram.ResetDiagram();
            UpdateScreen(true);
            MessageBoxText = "Diagram reset and all objects destroyed.";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorLogger.LogMessage("Application exited.", typeof(DrawingForm));
            Application.Exit();
        }

        // TODO: fix tab ordering on the buttons

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.Select;
            MessageBoxText = "Select tool selected.";
        }

        private void ButtonAtom_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAtom;
            MessageBoxText = "Atom tool selected.";

            AtomSelectionForm sForm = new AtomSelectionForm();
            sForm.ShowDialog();

            var par = CurrentObject.GetInternalParameters();
            MessageBoxText = par["Symbol"] + " selected.";
        }

        private void ButtonAtomCarbon_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAtomCarbon;
            MessageBoxText = "Carbon tool selected.";
        }

        private void ButtonAtomNitrogen_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAtomNitrogen;
            MessageBoxText = "Nitrogen tool selected.";
        }

        private void ButtonOxygen_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAtomOxygen;
            MessageBoxText = "Oxygen tool selected.";
        }

        private void ButtonAtomHydrogen_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAtomHydrogen;
            MessageBoxText = "Hydrogen tool selected.";
        }

        private void ButtonBondSingle_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceSingleBond;
            MessageBoxText = "Single Bond tool selected.";
        }

        private void ButtonBondDouble_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceDoubleBond;
            MessageBoxText = "Double Bond tool selected.";
        }

        private void ButtonBondTriple_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceTripleBond;
            MessageBoxText = "Triple Bond tool selected.";
        }

        private void ButtonBondAromatic_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceAromaticBond;
            MessageBoxText = "Aromatic Bond tool selected.";
        }

        private void ButtonLine_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceLineStart;
            MessageBoxText = "Line tool selected.";
        }

        private void ButtonCurve_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceCurveStart;
            MessageBoxText = "Curve tool selected.";
        }

        private void ButtonArrowStraight_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceStraightArrowStart;
            MessageBoxText = "Straight Arrow tool selected.";
        }

        private void ButtonArrowCurved_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceCurvedArrowStart;
            MessageBoxText = "Curved Arrow tool selected.";
        }

        private void ButtonChargePositive_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.ChargeIncrease;
            MessageBoxText = "Positive Charge tool selected.";
        }

        private void ButtonChargeNegative_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.ChargeDecrease;
            MessageBoxText = "Negative Charge tool selected.";
        }

        private void ButtonParentheses_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceParentheses;
            MessageBoxText = "Parentheses tool selected.";
        }

        private void ButtonBrackets_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceBrackets;
            MessageBoxText = "Brackets tool selected.";
        }

        private void ButtonBraces_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceBraces;
            MessageBoxText = "Braces tool selected.";
        }

        private void ButtonObject_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceMiscellaneous;
            MessageBoxText = "Miscellaneous tool selected.";

            // TODO: make window for selection of misc items
        }
    }
}
