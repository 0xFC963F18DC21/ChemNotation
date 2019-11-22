using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    class Bond : DiagramObject
    {
        // Internal object ID.
        public override ObjectTypeID ObjectID { get; } = ObjectTypeID.Bond;

        public enum BondType
        {
            Single, Double, Triple, Aromatic
        }

        public enum BondStyle
        {
            Plain, Wedged, Dashed
        }

        // Dictionary keys
        private static readonly string[] PropertyKeys =
        {
            "X1", "Y1", "X2", "Y2", "BondType", "Subtype", "Thickness", "Colour", "AnchorID1", "AnchorID2"
        };

        public float X1 { get; private set; }
        public float Y1 { get; private set; }
        public float X2 { get; private set; }
        public float Y2 { get; private set; }
        public float Thickness { get; private set; }
        public SKColor Colour { get; private set; }
        public BondType TypeOfBond { get; private set; }
        public BondStyle BondSubtype { get; private set; }
        public int AnchorID1 { get; private set; }
        public int AnchorID2 { get; private set; }

        public Bond(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0, float thickness = 1.5f, SKColor? colour = null, BondType typeOfBond = BondType.Single, BondStyle subtype = BondStyle.Plain, int anchorID1 = -1, int anchorID2 = -1)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Thickness = thickness;
            Colour = (colour == null) ? DefaultColour : colour.Value;
            TypeOfBond = typeOfBond;
            BondSubtype = subtype;
            AnchorID1 = anchorID1;
            AnchorID2 = anchorID2;
        }

        public override void Draw(Diagram diagram)
        {
            SKCanvas drawSurface = diagram.DiagramSurface.Canvas;

            SKPaint paint = new SKPaint
            {
                Color = Colour,
                IsAntialias = true,
                StrokeWidth = Thickness,
                StrokeCap = SKStrokeCap.Round
            };

            SKPath path = new SKPath();

            double angle = Math.Atan2(Y2 - Y1, X2 - X1);

            if (angle < 0d) angle += Math.PI * 2;

            double a1 = angle + Math.PI / 2;
            double a2 = angle - Math.PI / 2;

            // float singlePoint = 0;

            switch (TypeOfBond)
            {
                case BondType.Single:
                    path.MoveTo(X1, Y1);
                    path.LineTo(X2, Y2);
                    break;
                case BondType.Double:

                    break;
                case BondType.Triple:
                    break;
                case BondType.Aromatic:
                    break;
                default:
                    break;
            }

            drawSurface.DrawPath(path, paint);
        }

        public override void EditInternalParameters(Dictionary<string, object> parameters)
        {
            foreach (string key in parameters.Keys)
            {
                try
                {
                    switch (key)
                    {
                        case "X1":
                            X1 = (float)parameters[key];
                            break;
                        case "Y1":
                            Y1 = (float)parameters[key];
                            break;
                        case "X2":
                            X2 = (float)parameters[key];
                            break;
                        case "Y2":
                            Y2 = (float)parameters[key];
                            break;
                        case "BondType":
                            TypeOfBond = (BondType)parameters[key];
                            break;
                        case "Subtype":
                            BondSubtype = (BondStyle)parameters[key];
                            break;
                        case "Thickness":
                            Thickness = (float)parameters[key];
                            break;
                        case "Colour":
                            Colour = (SKColor)parameters[key];
                            break;
                        case "AnchorID1":
                            AnchorID1 = (int)parameters[key];
                            break;
                        case "AnchorID2":
                            AnchorID2 = (int)parameters[key];
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
                { "X1", X1 },
                { "Y1", Y1 },
                { "X2", X2 },
                { "Y2", Y2 },
                { "BondType", TypeOfBond },
                { "Subtype", BondSubtype },
                { "Thickness", Thickness },
                { "Colour", Colour },
                { "AnchorID1", AnchorID1 },
                { "AnchorID2", AnchorID2 }
            };
        }

        public override bool IsMouseIntersect(Point location)
        {
            throw new NotImplementedException();
        }
    }
}
