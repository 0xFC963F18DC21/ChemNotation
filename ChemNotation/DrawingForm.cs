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

        // Selected diagram object
        public DiagramObject SelectedObject { get; set; } = null;

        public string MessageBoxText {
            get
            {
                return StatusTextBox.Text;
            }
            set
            {
                if (value != StatusTextBox.Text)
                {
                    Log.LogMessageInfo("STATUS MESSAGE TEXT BOX UPDATED:\r\n\t" + value);
                    StatusTextBox.Text = value;
                }
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
            PlaceCurveStart, PlaceCurveControlPoint, PlaceCurveEnd,
            PlaceStraightArrowStart, PlaceStraightArrowEnd,
            PlaceCurvedArrowStart, PlaceCurvedArrowControlPoint, PlaceCurvedArrowEnd,
            ChargeIncrease, ChargeDecrease,
            PlaceSharpLineStart, PlaceSharpLineControlPoint, PlaceSharpArrowStart, PlaceSharpArrowControlPoint, PlaceText
        }

        public DrawingForm()
        {
            InitializeComponent();

            Log.LogMessageInfo("New diagram created.");
            MessageBoxText = "Ready.";
            SelectedTool = DiagramAction.Select;

            CurrentDiagram = new Diagram(DiagramView.Width, DiagramView.Height);
            UpdateScreen(true);
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

                if (clear && !MessageBoxText.Contains(" Diagram view cleared and redrawn.")) MessageBoxText += " Diagram view cleared and redrawn.";
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

        // When clicking on the diagram.
        private void DiagramView_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            Point clickLocation = args.Location;

            // Grid snap for atoms and texts.
            if (GridSettingForm.Settings.GridWidth > 0 && GridSettingForm.Settings.GridHeight > 0 &&
                (new DiagramAction[] {
                    DiagramAction.PlaceAtom, DiagramAction.PlaceText, DiagramAction.PlaceAtomCarbon, DiagramAction.PlaceAtomHydrogen, DiagramAction.PlaceAtomNitrogen, DiagramAction.PlaceAtomOxygen
                }.Contains(SelectedTool)))
            {
                // Get grid settings from settings object.
                int w = GridSettingForm.Settings.GridWidth, h = GridSettingForm.Settings.GridHeight;
                int xo = GridSettingForm.Settings.GridXOffset, yo = GridSettingForm.Settings.GridYOffset;
                int ux = clickLocation.X - xo, uy = clickLocation.Y - yo;

                // Find closest grid intersections.
                int[][] pairs =
                {
                    new int[] { ux - (ux % w), uy - (uy % h) },
                    new int[] { ux + (w - (ux % w)), uy - (uy % h) },
                    new int[] { ux - (ux % w), uy + (h - (uy % h)) },
                    new int[] { ux + (w - (ux % w)), uy + (h - (uy % h)) }
                };

                // Get distances to intersections.
                double[] distances = new double[pairs.Length];
                for (int i = 0; i < pairs.Length; i++)
                {
                    int dx, dy;
                    dx = pairs[i][0] - ux;
                    dy = pairs[i][1] - uy;

                    distances[i] = Math.Sqrt(dx * dx + dy * dy);  // via Pythagoras' Theorem.
                }

                // Get minimum distance and index of minimum distance.
                int mindex;
                double minstance;
                mindex = 0;
                minstance = distances[0];
                for (int i = 1; i < distances.Length; i++)
                {
                    if (distances[i] < minstance)
                    {
                        minstance = distances[i];
                        mindex = i;
                    }
                }

                // Move selection to placement.
                clickLocation.X = pairs[mindex][0] + xo;
                clickLocation.Y = pairs[mindex][1] + yo;
            }

            // Finding nearby objects for object anchoring.
            List<DiagramObject> objects = new List<DiagramObject>();
            List<DiagramObject> atoms = new List<DiagramObject>();
            List<DiagramObject> texts = new List<DiagramObject>();
            foreach (DiagramObject obj in CurrentDiagram.Objects) if (obj.IsMouseIntersect(clickLocation)) objects.Add(obj);
            foreach (DiagramObject obj in objects) if (obj.ObjectID == DiagramObject.ObjectTypeID.Atom) atoms.Add(obj);
            foreach (DiagramObject obj in objects) if (obj.ObjectID == DiagramObject.ObjectTypeID.Text) texts.Add(obj);
            int anchor = -1;  // For object anchoring.

            try
            {
                // CurrentDiagram.AddDiagramObject(new Atom("C", clickLocation.X, clickLocation.Y));
                switch (SelectedTool)
                {
                    case DiagramAction.Select:
                        foreach (DiagramObject obj in objects)
                        {
                            if (args.Button == MouseButtons.Right)
                            {
                                CurrentDiagram.RemoveDiagramObject(obj.DiagramID);
                                MessageBoxText = $"Object of ID {obj.DiagramID} deleted.";
                                break;
                            }
                            else
                            {
                                if (SelectedObject != obj)
                                {
                                    SelectedObject = obj;

                                    EditingForm eForm = new EditingForm(obj);
                                    eForm.Show();

                                    break;
                                }
                            }
                        }
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
                        if (atoms.Count > 0) anchor = atoms[0].DiagramID;
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Single, Bond.BondStyle.Plain, anchor);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        break;
                    case DiagramAction.PlaceDoubleBond:
                        if (atoms.Count > 0) anchor = atoms[0].DiagramID;
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Double, Bond.BondStyle.Plain, anchor);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        break;
                    case DiagramAction.PlaceTripleBond:
                        if (atoms.Count > 0) anchor = atoms[0].DiagramID;
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Triple, Bond.BondStyle.Plain, anchor);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        break;
                    case DiagramAction.PlaceAromaticBond:
                        if (atoms.Count > 0) anchor = atoms[0].DiagramID;
                        CurrentObject = new Bond(clickLocation.X, clickLocation.Y, 0, 0, 1.5f, null, Bond.BondType.Aromatic, Bond.BondStyle.Plain, anchor);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceBondEnd;
                        break;
                    case DiagramAction.PlaceBondEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Bond)
                        {
                            int ach = ((Bond)CurrentObject).AnchorID1;
                            if (atoms.Count > 0)
                            {
                                foreach (var atom in atoms)
                                {
                                    if (atom.DiagramID != ach)
                                    {
                                        anchor = atom.DiagramID;
                                        break;
                                    }
                                    else
                                    {
                                        anchor = -1;
                                    }
                                }
                            }

                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            parameters["AnchorID2"] = anchor;
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
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Line);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceLineEnd;
                        break;
                    case DiagramAction.PlaceLineEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            CurrentObject.EditInternalParameters(parameters);

                            CurrentDiagram.AddDiagramObject(CurrentObject);
                            ToolGroup.Enabled = true;

                            SelectedTool = DiagramAction.PlaceLineStart;
                            // CurrentObject = null;
                        }
                        break;
                    case DiagramAction.PlaceCurveStart:
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Curve, Line.LineStyle.Plain);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceCurveControlPoint;
                        break;
                    case DiagramAction.PlaceCurveControlPoint:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            List<float[]> cpts = (List<float[]>)parameters["ControlPoints"];

                            cpts.Add(new float[] { clickLocation.X, clickLocation.Y });

                            CurrentObject.EditInternalParameters(parameters);

                            if (cpts.Count == 2) SelectedTool = DiagramAction.PlaceCurveEnd;
                        }
                        break;
                    case DiagramAction.PlaceCurveEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            CurrentObject.EditInternalParameters(parameters);

                            CurrentDiagram.AddDiagramObject(CurrentObject);
                            ToolGroup.Enabled = true;

                            SelectedTool = DiagramAction.PlaceCurveStart;
                            // CurrentObject = null;
                        }
                        break;
                    case DiagramAction.PlaceStraightArrowStart:
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Line, Line.LineStyle.Plain, Line.EndType.Plain, Line.EndType.Arrow);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceStraightArrowEnd;
                        break;
                    case DiagramAction.PlaceStraightArrowEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            CurrentObject.EditInternalParameters(parameters);

                            CurrentDiagram.AddDiagramObject(CurrentObject);
                            ToolGroup.Enabled = true;

                            SelectedTool = DiagramAction.PlaceStraightArrowStart;
                            // CurrentObject = null;
                        }
                        break;
                    case DiagramAction.PlaceCurvedArrowStart:
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Curve, Line.LineStyle.Plain, Line.EndType.Plain, Line.EndType.Arrow);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceCurvedArrowControlPoint;
                        break;
                    case DiagramAction.PlaceCurvedArrowControlPoint:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            List<float[]> cpts = (List<float[]>)parameters["ControlPoints"];

                            cpts.Add(new float[] { clickLocation.X, clickLocation.Y });

                            CurrentObject.EditInternalParameters(parameters);

                            if(cpts.Count == 2) SelectedTool = DiagramAction.PlaceCurvedArrowEnd;
                        }
                        break;
                    case DiagramAction.PlaceCurvedArrowEnd:
                        if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                        {
                            var parameters = CurrentObject.GetInternalParameters();
                            parameters["X2"] = (float)clickLocation.X;
                            parameters["Y2"] = (float)clickLocation.Y;
                            CurrentObject.EditInternalParameters(parameters);

                            CurrentDiagram.AddDiagramObject(CurrentObject);
                            ToolGroup.Enabled = true;

                            SelectedTool = DiagramAction.PlaceCurvedArrowStart;
                            // CurrentObject = null;
                        }
                        break;
                    case DiagramAction.ChargeIncrease:
                        foreach (DiagramObject obj in objects)
                        {
                            if (obj.ObjectID == DiagramObject.ObjectTypeID.Atom)
                            {
                                var p = ((Atom)obj).GetInternalParameters();
                                p["Charge"] = (int)p["Charge"] + 1;
                                obj.EditInternalParameters(p);
                                break;
                            }
                        }
                        break;
                    case DiagramAction.ChargeDecrease:
                        foreach (DiagramObject obj in objects)
                        {
                            if (obj.ObjectID == DiagramObject.ObjectTypeID.Atom)
                            {
                                var p = ((Atom)obj).GetInternalParameters();
                                p["Charge"] = (int)p["Charge"] - 1;
                                obj.EditInternalParameters(p);
                                break;
                            }
                        }
                        break;
                    case DiagramAction.PlaceSharpLineStart:
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Sharp, Line.LineStyle.Plain);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceSharpLineControlPoint;
                        break;
                    case DiagramAction.PlaceSharpLineControlPoint:
                        if (args.Button == MouseButtons.Right)
                        {
                            if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                            {
                                var parameters = CurrentObject.GetInternalParameters();
                                parameters["X2"] = (float)clickLocation.X;
                                parameters["Y2"] = (float)clickLocation.Y;
                                CurrentObject.EditInternalParameters(parameters);

                                CurrentDiagram.AddDiagramObject(CurrentObject);
                                ToolGroup.Enabled = true;
                            }

                            SelectedTool = DiagramAction.PlaceSharpLineStart;
                        }
                        else
                        {
                            if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                            {
                                var parameters = CurrentObject.GetInternalParameters();
                                List<float[]> cpts = (List<float[]>)parameters["ControlPoints"];

                                cpts.Add(new float[] { clickLocation.X, clickLocation.Y });

                                CurrentObject.EditInternalParameters(parameters);
                            }
                        }
                        break;
                    case DiagramAction.PlaceSharpArrowStart:
                        CurrentObject = new Line(clickLocation.X, clickLocation.Y, 0, 0, null, 1.5f, null, Line.LineType.Sharp, Line.LineStyle.Plain, Line.EndType.Plain, Line.EndType.Arrow);
                        ToolGroup.Enabled = false;
                        SelectedTool = DiagramAction.PlaceSharpArrowControlPoint;
                        break;
                    case DiagramAction.PlaceSharpArrowControlPoint:
                        if (args.Button == MouseButtons.Right)
                        {
                            if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                            {
                                var parameters = CurrentObject.GetInternalParameters();
                                parameters["X2"] = (float)clickLocation.X;
                                parameters["Y2"] = (float)clickLocation.Y;
                                CurrentObject.EditInternalParameters(parameters);

                                CurrentDiagram.AddDiagramObject(CurrentObject);
                                ToolGroup.Enabled = true;
                            }

                            SelectedTool = DiagramAction.PlaceSharpArrowStart;
                        }
                        else
                        {
                            if (CurrentObject.ObjectID == DiagramObject.ObjectTypeID.Line)
                            {
                                var parameters = CurrentObject.GetInternalParameters();
                                List<float[]> cpts = (List<float[]>)parameters["ControlPoints"];

                                cpts.Add(new float[] { clickLocation.X, clickLocation.Y });

                                CurrentObject.EditInternalParameters(parameters);
                            }
                        }
                        break;
                    case DiagramAction.PlaceText:
                        CurrentDiagram.AddDiagramObject(new Text("Edit object to edit text.", clickLocation.X, clickLocation.Y));
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;

            // Disable visible grid
            GridSettingForm.Settings = new GridSettingForm.ProgramSettings(0, 0, 0, 0);

            // Create file saving dialogue window.
            SaveFileDialog dialog = new SaveFileDialog();

            // Default settings.
            dialog.AddExtension = true;
            dialog.FileName = $"SAVEFILE_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff")}";
            dialog.DefaultExt = "png";

            dialog.Filter = "PNG Files (*.png)|All Files (*.*)";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult res = dialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                try
                {
                    // Only save if dialogue was confirmed. Otherwise do not.
                    byte[] img = CurrentDiagram.DiagramSurface.Snapshot().Encode().ToArray();
                    using (Stream stream = dialog.OpenFile())
                    {
                        stream.Write(img, 0, img.Length);
                    }

                    MessageBoxText = $"File successfully saved as '{dialog.FileName}'.";
                }
                catch (Exception exc)
                {
                    Log.LogMessageError($"File save at '{dialog.FileName}' failed.", exc);
                }
            }

            dialog.Dispose();
            dialog = null;

            // Reset grid snap.
            using (StreamReader reader = File.OpenText(@"Resource/Settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                GridSettingForm.Settings = (GridSettingForm.ProgramSettings)serializer.Deserialize(reader, typeof(GridSettingForm.ProgramSettings));
            }

            Enabled = true;
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
            SelectedTool = DiagramAction.PlaceSharpLineStart;
            MessageBoxText = "Sharp Line tool selected.";
        }

        private void ButtonBrackets_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceSharpArrowStart;
            MessageBoxText = "Sharp Arrow tool selected.";
        }

        private void ButtonBraces_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void ButtonObject_Click(object sender, EventArgs e)
        {
            SelectedTool = DiagramAction.PlaceText;
            MessageBoxText = "Text tool selected.";
        }

        private void queryFormulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Enumerating all of the present atoms.
            IEnumerable<DiagramObject> atoms =
                from obj in CurrentDiagram.Objects
                where obj.ObjectID == DiagramObject.ObjectTypeID.Atom
                select obj;

            if (atoms.Count() < 1)
            {
                MessageBoxText = "There are no atoms on the diagram.";
                return;
            }

            Dictionary<string, int> symbolValues = new Dictionary<string, int>();
            foreach (DiagramObject atom in atoms)
            {
                if (!(atom is Atom)) continue;

                Atom atm = (Atom)atom;
                string symbol = atm.Symbol;

                if (!symbolValues.Keys.Contains(symbol)) symbolValues.Add(symbol, 1);
                else
                {
                    ++symbolValues[symbol];
                }
            }

            // Forming the query.
            StringBuilder query = new StringBuilder();
            List<string> sortedKeys = symbolValues.Keys.ToList();
            sortedKeys.Sort();

            foreach (string key in sortedKeys)
            {
                query.Append(key);
                if (symbolValues[key] > 1) query.Append(symbolValues[key]);
            }

            QuerySourceForm src = new QuerySourceForm();
            src.ChooseSource(query.ToString());

            MessageBoxText = $"Query window opened for {query}";

            src.Dispose();
            src = null;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AForm.Show();
            MessageBoxText = "About / Help window opened.";
        }

        private void gridSnapSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enabled = false;
            GridSettingForm form = new GridSettingForm();
            form.ShowDialog();

            form.Dispose();
            form = null;
            Enabled = true;
        }
    }
}
