using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    class Text : DiagramObject
    {
        // Internal object ID.
        public override ObjectTypeID ObjectID { get; } = ObjectTypeID.Text;

        public enum TextAlignment
        {
            TopLeft = 0, TopCentre = 1, TopRight = 2,
            MiddleLeft = 3, MiddleCentre = 4, MiddleRight = 5,
            BottomLeft = 6, BottomCentre = 7, BottomRight = 8
        }

        public float X { get; private set; }
        public float Y { get; private set; }

        private string _Content;
        public string Content
        {
            get
            {
                return _Content;
            }
            private set
            {
                _Content = value;

                string[] CleanContent = _Content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                int Max = 0;
                foreach (string s in CleanContent) if (s.Length > Max) Max = s.Length;

                TextWidth = Max * FontSize;
                TextHeight = CleanContent.Length * FontSize;
            }
        }

        public string FontFamily { get; private set; }
        public bool Bold { get; private set; }
        public bool Italic { get; private set; }

        private float _FontSize;
        public float FontSize
        {
            get
            {
                return _FontSize;
            }
            private set
            {
                _FontSize = value;
                Content = _Content == null ? "" : _Content;
            }
        }

        public SKColor Colour { get; private set; }
        public TextAlignment Alignment { get; private set; }

        private float TextWidth { get; set; } = 0;
        private float TextHeight { get; set; } = 0;

        private static readonly string[] PropertyKeys =
        {
            "X", "Y", "Content", "FontFamily", "Bold", "Italic", "FontSize", "Colour", "Alignment"
        };

        public Text() : this("Insert Text") { }
        
        /// <summary>
        /// Creates a text object.
        /// </summary>
        /// <param name="content">Text contents of object.</param>
        /// <param name="x">X-position</param>
        /// <param name="y">Y-position</param>
        /// <param name="colour">Font colour</param>
        /// <param name="fontFamily">Font family</param>
        /// <param name="bold">Use bold style?</param>
        /// <param name="italic">Use italic style?</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="alignment">Text alignment</param>
        public Text(string content = "Insert Text", float x = 0, float y = 0, SKColor? colour = null, string fontFamily = "Arial",
            bool bold = false, bool italic = false, float fontSize = 16, TextAlignment alignment = TextAlignment.TopLeft)
        {
            DiagramID = Program.DForm.CurrentDiagram.NextFreeID();
            X = x;
            Y = y;
            FontFamily = fontFamily;
            Bold = bold;
            Italic = italic;
            FontSize = fontSize;
            Colour = (colour == null ? DefaultColour : colour.Value);
            Alignment = alignment;
            Content = content;
        }

        public override void Draw(Diagram diagram)
        {
            // Select text alignment.
            int aVal = (int)Alignment;

            SKTextAlign alignment = SKTextAlign.Left;
            if (aVal % 3 == 2) alignment = SKTextAlign.Right;
            if (aVal % 3 == 1) alignment = SKTextAlign.Center;

            // Paint and style.
            SKFontStyle style = new SKFontStyle(
                Bold ? SKFontStyleWeight.Bold : SKFontStyleWeight.Normal,
                SKFontStyleWidth.Normal,
                Italic ? SKFontStyleSlant.Italic : SKFontStyleSlant.Upright
                );

            SKPaint paint = new SKPaint
            {
                Color = Colour,
                IsAntialias = true,
                Typeface = SKTypeface.FromFamilyName(FontFamily, style),
                TextAlign = alignment,
                TextSize = FontSize
            };

            TextWidth *= (paint.FontMetrics.MaxCharacterWidth / FontSize);  // Compensation of font width.

            // Vertical alignment compensation.
            string[] lines = Content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            float comp = FontSize / 2;
            if (aVal < 3) comp = FontSize;
            else if (aVal > 5) comp = 0;

            for (int i = 0; i < lines.Length; i++) {
                diagram.DiagramSurface.Canvas.DrawText(lines[i], X, Y + comp + (i * FontSize), paint);
            }
            paint.Dispose();
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
                        case "Content":
                            Content = (string)parameters[key];
                            break;
                        case "FontFamily":
                            FontFamily = (string)parameters[key];
                            break;
                        case "Bold":
                            Bold = (int)parameters[key] > 0;
                            break;
                        case "Italic":
                            Italic = (int)parameters[key] > 0;
                            break;
                        case "FontSize":
                            FontSize = (float)parameters[key];
                            break;
                        case "Colour":
                            Colour = (SKColor)parameters[key];
                            break;
                        case "Alignment":
                            Alignment = (TextAlignment)parameters[key];
                            break;
                        default:
                            continue;
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

        public override Dictionary<string, object> GetEditableParameters()
        {
            try
            {
                return new Dictionary<string, object>
                {
                    { "X", X },
                    { "Y", Y },
                    { "Content", Content },
                    { "FontFamily", FontFamily },
                    { "Bold", Bold ? 1 : 0 },
                    { "Italic", Italic ? 1 : 0 },
                    { "FontSize", FontSize },
                    { "Red", Colour.Red },
                    { "Green", Colour.Green },
                    { "Blue", Colour.Blue },
                    { "Alpha", Colour.Alpha },
                    { "Alignment", (int)Alignment }
                };
            }
            catch (Exception e)
            {
                // Uncaught misc. error. Please log.
                ErrorLogger.ShowErrorMessageBox(e);
                return null;
            }
        }

        public override Dictionary<string, object> GetInternalParameters()
        {
            try
            {
                return new Dictionary<string, object>
                {
                    { "X", X },
                    { "Y", Y },
                    { "Content", Content },
                    { "FontFamily", FontFamily },
                    { "Bold", Bold ? 1 : 0 },
                    { "Italic", Italic ? 1 : 0 },
                    { "FontSize", FontSize },
                    { "Colour", Colour },
                    { "Alignment", Alignment }
                };
            }
            catch (Exception e)
            {
                // Uncaught misc. error. Please log.
                ErrorLogger.ShowErrorMessageBox(e);
                return null;
            }
        }

        public override bool IsMouseIntersect(Point location)
        {
            float offsetX = 0, offsetY = 0;
            bool result = false;

            if (Alignment.ToString().Contains("Left")) offsetX = 0;
            else if (Alignment.ToString().Contains("Centre")) offsetX = TextWidth / 2;
            else if (Alignment.ToString().Contains("Right")) offsetX = TextWidth;

            if (Alignment.ToString().Contains("Top")) offsetY = 0;
            else if (Alignment.ToString().Contains("Middle")) offsetY = TextHeight / 2;
            else if (Alignment.ToString().Contains("Bottom")) offsetY = TextHeight;

            result = (
                (location.X >= X - offsetX && location.X <= (X - offsetX) + TextWidth) &&
                (location.Y >= Y - offsetY && location.Y <= (Y - offsetY) + TextHeight)
                );
            return result;
        }

        public override void ReplaceInternalParameters(Dictionary<string, object> parameters)
        {
            if (parameters.Keys.Contains("Red") && parameters.Keys.Contains("Green") && parameters.Keys.Contains("Blue") && parameters.Keys.Contains("Alpha"))
            {
                if (parameters.Keys.Contains("Colour"))
                {
                    parameters["Colour"] = new SKColor(
                        (byte)parameters["Red"],
                        (byte)parameters["Green"],
                        (byte)parameters["Blue"],
                        (byte)parameters["Alpha"]
                        );
                }
                else
                {
                    parameters.Add("Colour", new SKColor(
                        (byte)parameters["Red"],
                        (byte)parameters["Green"],
                        (byte)parameters["Blue"],
                        (byte)parameters["Alpha"]
                        ));
                }
            }

            if (parameters.Keys.Contains("Alignment")) parameters["Alignment"] = (TextAlignment)((int)parameters["Alignment"]);

            EditInternalParameters(parameters);
        }
    }
}
