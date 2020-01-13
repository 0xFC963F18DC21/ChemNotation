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

        public Bond() : this(0, 0) { }

        public Bond(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0, float thickness = 4f, SKColor? colour = null, BondType typeOfBond = BondType.Single, BondStyle subtype = BondStyle.Plain, int anchorID1 = -1, int anchorID2 = -1)
        {
            DiagramID = Program.DForm.CurrentDiagram.NextFreeID();
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
            SKPaint paint = new SKPaint
            {
                Color = Colour,
                IsAntialias = true,
                StrokeWidth = Thickness,
                StrokeCap = SKStrokeCap.Round,
                Style = SKPaintStyle.Stroke
            };

            SKPath path = new SKPath();

            double angle = Math.Atan2(Y2 - Y1, X2 - X1);

            //if (angle < 0d) angle += Math.PI * 2;

            double a1 = angle + (Math.PI / 2);
            double a2 = angle - (Math.PI / 2);

            float dPointX1 = X1 + (float)(3 * Math.Cos(a1));
            float dPointY1 = Y1 + (float)(3 * Math.Sin(a1));
            float dPointX2 = X2 + (float)(3 * Math.Cos(a1));
            float dPointY2 = Y2 + (float)(3 * Math.Sin(a1));

            float dPointX3 = X1 + (float)(3 * Math.Cos(a2));
            float dPointY3 = Y1 + (float)(3 * Math.Sin(a2));
            float dPointX4 = X2 + (float)(3 * Math.Cos(a2));
            float dPointY4 = Y2 + (float)(3 * Math.Sin(a2));

            if (TypeOfBond != BondType.Single) Log.LogMessageGeneral($"({X1}, {Y1}) | ({X2}, {Y2}) || ({dPointX1}, {dPointY1}) | ({dPointX2}, {dPointY2}) | ({dPointX3}, {dPointY3}) | ({dPointX4}, {dPointY4})");

            if (BondSubtype == BondStyle.Wedged || BondSubtype == BondStyle.Dashed)
            {
                SKPoint[] points = new SKPoint[]
                {
                    new SKPoint() { X = X1, Y = Y1 },
                    new SKPoint() { X = dPointX3, Y = dPointY3 },
                    new SKPoint() { X = dPointX4, Y = dPointY4 }
                };

                switch (BondSubtype)
                {
                    case BondStyle.Wedged:
                        paint.Style = SKPaintStyle.StrokeAndFill;
                        break;
                    case BondStyle.Dashed:
                        paint.Style = SKPaintStyle.StrokeAndFill;
                        paint.PathEffect = SKPathEffect.CreateDash(new float[] { 9f, 3f }, 0);
                        break;
                    default:
                        break;
                }

                diagram.DiagramSurface.Canvas.DrawPoints(SKPointMode.Polygon, points, paint);
            }
            else
            {
                switch (TypeOfBond)
                {
                    case BondType.Single:
                        path.MoveTo(X1, Y1);
                        path.LineTo(X2, Y2);
                        break;
                    case BondType.Double:
                        path.MoveTo(dPointX1, dPointY1);
                        path.LineTo(dPointX2, dPointY2);

                        SKPath path2 = new SKPath();
                        path2.MoveTo(dPointX3, dPointY3);
                        path2.LineTo(dPointX4, dPointY4);

                        diagram.DiagramSurface.Canvas.DrawPath(path2, paint);
                        break;
                    case BondType.Triple:
                        path.MoveTo(X1, Y1);
                        path.LineTo(X2, Y2);

                        SKPath p2 = new SKPath(), p3 = new SKPath();
                        p2.MoveTo(dPointX1, dPointY1);
                        p2.LineTo(dPointX2, dPointY2);

                        p3.MoveTo(dPointX3, dPointY3);
                        p3.LineTo(dPointX4, dPointY4);

                        diagram.DiagramSurface.Canvas.DrawPath(p2, paint);
                        diagram.DiagramSurface.Canvas.DrawPath(p3, paint);
                        break;
                    case BondType.Aromatic:
                        SKPaint paintDash = new SKPaint
                        {
                            Color = Colour,
                            IsAntialias = true,
                            StrokeWidth = Thickness,
                            StrokeCap = SKStrokeCap.Round,
                            PathEffect = SKPathEffect.CreateDash(new float[] { 9f, 3f }, 0),
                            Style = SKPaintStyle.Stroke
                        };

                        SKPath pth2 = new SKPath();
                        pth2.MoveTo(dPointX3, dPointY3);
                        pth2.LineTo(dPointX4, dPointY4);

                        diagram.DiagramSurface.Canvas.DrawPath(pth2, paintDash);

                        path.MoveTo(dPointX1, dPointY1);
                        path.LineTo(dPointX2, dPointY2);
                        break;
                    default:
                        break;
                }

                //path.Close();
                diagram.DiagramSurface.Canvas.DrawPath(path, paint);
            }
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
            try
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
            catch (Exception e)
            {
                // If some error occurs, please log.
                ErrorLogger.ShowErrorMessageBox(e);
                return null;
            }
        }

        public override bool IsMouseIntersect(Point location)
        {
            throw new NotImplementedException();
        }
    }
}
