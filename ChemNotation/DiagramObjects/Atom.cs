using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    class Atom : DiagramObject
    {
        /// <summary>
        /// Type ID. Used for casting.
        /// </summary>
        public override ObjectTypeID ObjectID { get; } = ObjectTypeID.Atom;

        private static readonly string[] PropertyKeys =
        {
            "X", "Y", "Symbol", "FontFamily", "FontSize", "Colour", "Charge"
        };

        public float X { get; private set; }
        public float Y { get; private set; }
        public string Symbol { get; private set; }
        public string FontFamily { get; private set; }
        public float FontSize { get; private set; }
        public SKColor Colour { get; private set; }
        public int Charge { get; private set; }

        public Atom() : this("C", 0f, 0f, "Arial", 12, null, 0) { }

        /// <summary>
        /// Creates a new atom object. Default parameters used if arguments omitted.
        /// </summary>
        /// <param name="symbol">Symbol, default <code>C</code>C/param>
        /// <param name="x">Pixel x-coordinate of object</param>
        /// <param name="y">Pixel y-coordinate of object</param>
        /// <param name="fontFamily">Font family of text</param>
        /// <param name="fontSize">Font size of text</param>
        /// <param name="colour">Font colour</param>
        public Atom(string symbol = "C", float x = 0, float y = 0, string fontFamily = "Arial", float fontSize = 12, SKColor? colour = null, int charge = 0)
        {
            DiagramID = Program.DForm.CurrentDiagram.NextFreeID();
            X = x;
            Y = y;
            Symbol = symbol;
            FontFamily = fontFamily;
            FontSize = fontSize;
            Colour = (colour == null ? DefaultColour : colour.Value);
            Charge = charge;
        }

        public override void Draw(Diagram diagram)
        {
            SKPaint paint = new SKPaint
            {
                Color = Colour,
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(FontFamily),
                TextAlign = SKTextAlign.Center,
                TextSize = FontSize
            };

            diagram.DiagramSurface.Canvas.DrawText(Symbol, X, Y + FontSize / 2, paint);
        }

        public override void EditInternalParameters(Dictionary<string, object> parameters)
        {
            foreach (string key in parameters.Keys)
            {
                try
                {
                    // If dictionary has a key, attempt to set respective value.
                    switch (key)
                    {
                        case "X":
                            X = (float)parameters[key];
                            break;
                        case "Y":
                            Y = (float)parameters[key];
                            break;
                        case "Symbol":
                            Symbol = (string)parameters[key];
                            break;
                        case "FontFamily":
                            FontFamily = (string)parameters[key];
                            break;
                        case "FontSize":
                            FontSize = (float)parameters[key];
                            break;
                        case "Colour":
                            Colour = (SKColor)parameters[key];
                            break;
                        case "Charge":
                            Charge = (int)parameters[key];
                            break;
                        default:
                            break;
                    }
                }
                catch (InvalidCastException e)
                {
                    // Dictionary has invalid data type. Error needs to be addressed.
                    ErrorLogger.ShowErrorMessageBox(e);
                    continue;
                }
                catch (Exception e)
                {
                    // Dictionary has an invalid key. Error can be left alone.
                    Log.LogMessageError("Miscellaneous error:", e);
                    continue;
                }
            }
        }

        public override Dictionary<string, object> GetInternalParameters()
        {
            return new Dictionary<string, object>
            {
                { "X", X },
                { "Y", Y },
                { "Symbol", Symbol },
                { "FontFamily", FontFamily },
                { "FontSize", FontSize },
                { "Colour", Colour },
                { "Charge", Charge }
            };
        }

        public override bool IsMouseIntersect(Point location)
        {
            return Math.Sqrt((location.X - X) * (location.X - X) + (location.Y - Y) * (location.Y - Y)) < (FontSize / 2);
        }
    }
}
