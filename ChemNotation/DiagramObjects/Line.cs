using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemNotation.DiagramObjects
{
    class Line : DiagramObject
    {
        // Internal object ID.
        public override ObjectTypeID ObjectID { get; } = ObjectTypeID.Line;

        public enum LineType
        {
            Line, Sharp, Curve
        }

        public enum LineStyle
        {
            Plain, Dashed
        }

        public enum EndType
        {
            Plain, Arrow, Half_Arrow
        }

        private double ArrowOffset { get; } = Math.PI / 9;

        public float X1 { get; private set; }
        public float Y1 { get; private set; }
        public float X2 { get; private set; }
        public float Y2 { get; private set; }
        public List<float[]> ControlPoints { get; private set; }
        public float Thickness { get; private set; }
        public SKColor Colour { get; private set; }
        public LineType TypeOfLine { get; private set; }
        public LineStyle StyleOfLine { get; private set; }
        public EndType HeadType { get; private set; }
        public EndType TailType { get; private set; }
        public int AnchorID1 { get; private set; }
        public int AnchorID2 { get; private set; }

        // Dictionary keys
        private static readonly string[] PropertyKeys =
        {
            "X1", "Y1", "X2", "Y2", "ControlPoints", "LineType", "LineStyle", "Thickness", "Colour", "HeadType", "TailType", "AnchorID1", "AnchorID2"
        };

        public Line(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0, List<float[]> controlPoints = null, float thickness = 1.5f,
            SKColor? colour = null, LineType typeOfLine = LineType.Line, LineStyle styleOfLine = LineStyle.Plain, EndType headType = EndType.Plain,
            EndType tailType = EndType.Plain, int anchorID1 = -1, int anchorID2 = -1)
        {
            DiagramID = Program.DForm.CurrentDiagram.NextFreeID();
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
            Thickness = thickness;
            Colour = (colour == null) ? DefaultColour : colour.Value;
            ControlPoints = (controlPoints == null) ? new List<float[]>() : controlPoints;
            TypeOfLine = typeOfLine;
            StyleOfLine = styleOfLine;
            HeadType = headType;
            TailType = tailType;
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

            if (StyleOfLine == LineStyle.Dashed)
            {
                paint.PathEffect = SKPathEffect.CreateDash(new float[] { 9f, 3f }, 0);
            }

            SKPath path = new SKPath();

            if (TypeOfLine == LineType.Line)
            {
                path.MoveTo(X1, Y1);
                path.LineTo(X2, Y2);
                diagram.DiagramSurface.Canvas.DrawPath(path, paint);
            }
            else if (TypeOfLine == LineType.Sharp)
            {
                path.MoveTo(X1, Y1);

                foreach (float[] coordPair in ControlPoints)
                {
                    path.LineTo(coordPair[0], coordPair[1]);
                }

                path.LineTo(X2, Y2);
                diagram.DiagramSurface.Canvas.DrawPath(path, paint);
            }
            else if (TypeOfLine == LineType.Curve)
            {
                List<float[]> cpts = new List<float[]>();

                path.MoveTo(X1, Y1);
                path.CubicTo(ControlPoints[0][0], ControlPoints[0][1], ControlPoints[1][0], ControlPoints[1][1], X2, Y2);
                diagram.DiagramSurface.Canvas.DrawPath(path, paint);
            }
            else
            {
                Log.LogMessageError("An invalid line type has somehow been assigned: " + TypeOfLine);
            }

            if (HeadType == EndType.Arrow || TailType == EndType.Arrow || HeadType == EndType.Half_Arrow || TailType == EndType.Half_Arrow)
            {
                double tailAngle = Math.Atan2(Y1 - Y2, X1 - X2);
                if (TypeOfLine != LineType.Line) tailAngle = Math.Atan2(ControlPoints[ControlPoints.Count - 1][1] - Y2, ControlPoints[ControlPoints.Count - 1][0] - X2);

                double headAngle = Math.Atan2(Y2 - Y1, X2 - X1);
                if (TypeOfLine != LineType.Line)
                {
                    try
                    {
                        headAngle = Math.Atan2(ControlPoints[0][1] - Y1, ControlPoints[0][0] - X1);
                    }
                    catch
                    {
                        // Nothing.
                    }
                }

                if (HeadType == EndType.Arrow || HeadType == EndType.Half_Arrow)
                {
                    SKPath p2 = new SKPath(), p3 = new SKPath();

                    double ha1, ha2;
                    ha1 = headAngle + ArrowOffset;
                    ha2 = headAngle - ArrowOffset;

                    float dPointX1 = X1 + (float)(16 * Math.Cos(ha1));
                    float dPointY1 = Y1 + (float)(16 * Math.Sin(ha1));

                    float dPointX2 = X1 + (float)(16 * Math.Cos(ha2));
                    float dPointY2 = Y1 + (float)(16 * Math.Sin(ha2));

                    p2.MoveTo(X1, Y1);
                    p2.LineTo(dPointX1, dPointY1);

                    p3.MoveTo(X1, Y1);
                    p3.LineTo(dPointX2, dPointY2);

                    diagram.DiagramSurface.Canvas.DrawPath(p2, paint);
                    if (HeadType == EndType.Arrow) diagram.DiagramSurface.Canvas.DrawPath(p3, paint);
                }

                if (TailType == EndType.Arrow || TailType == EndType.Half_Arrow)
                {
                    SKPath p2 = new SKPath(), p3 = new SKPath();

                    double ta1, ta2;
                    ta1 = tailAngle + ArrowOffset;
                    ta2 = tailAngle - ArrowOffset;

                    float dPointX1 = X2 + (float)(16 * Math.Cos(ta1));
                    float dPointY1 = Y2 + (float)(16 * Math.Sin(ta1));
                                                  
                    float dPointX2 = X2 + (float)(16 * Math.Cos(ta2));
                    float dPointY2 = Y2 + (float)(16 * Math.Sin(ta2));

                    p2.MoveTo(X2, Y2);
                    p2.LineTo(dPointX1, dPointY1);

                    p3.MoveTo(X2, Y2);
                    p3.LineTo(dPointX2, dPointY2);

                    diagram.DiagramSurface.Canvas.DrawPath(p2, paint);
                    if (TailType == EndType.Arrow) diagram.DiagramSurface.Canvas.DrawPath(p3, paint);
                }
            }
            paint.Dispose();
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
                        case "ControlPoints":
                            ControlPoints = (List<float[]>)parameters[key];
                            break;
                        case "LineType":
                            TypeOfLine = (LineType)parameters[key];
                            break;
                        case "LineStyle":
                            StyleOfLine = (LineStyle)parameters[key];
                            break;
                        case "Thickness":
                            Thickness = (float)parameters[key];
                            break;
                        case "Colour":
                            Colour = (SKColor)parameters[key];
                            break;
                        case "HeadType":
                            HeadType = (EndType)parameters[key];
                            break;
                        case "TailType":
                            TailType = (EndType)parameters[key];
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
                return new Dictionary<string, object>()
                {
                    { "X1", X1 },
                    { "Y1", Y1 },
                    { "X2", X2 },
                    { "Y2", Y2 },
                    { "ControlPoints", ControlPoints },
                    { "LineType", TypeOfLine },
                    { "LineStyle", StyleOfLine },
                    { "Thickness", Thickness },
                    { "Colour", Colour },
                    { "HeadType", HeadType },
                    { "TailType", TailType },
                    { "AnchorID1", AnchorID1 },
                    { "AnchorID2", AnchorID2 }
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
            float dX, dY;
            dX = location.X - X1;
            dY = location.Y - Y1;

            float dX2, dY2;
            dX2 = location.X - X2;
            dY2 = location.Y - Y2;

            double distance = Math.Min(Math.Sqrt(dX * dX + dY * dY), Math.Sqrt(dX2 * dX2 + dY2 * dY2));
            return distance <= 8;
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

            if (parameters.Keys.Contains("ControlPointX1"))
            {
                List<float[]> points = new List<float[]>();
                int i = 0;
                while (true)
                {
                    try
                    {
                        points.Add(new float[] { (float)parameters[$"ControlPointX{i}"], (float)parameters[$"ControlPointY{i}"] });
                        ++i;
                    }
                    catch
                    {
                        break;
                    }
                }

                parameters.Add("ControlPoints", points);
            }

            EditInternalParameters(parameters);
        }

        public override Dictionary<string, object> GetEditableParameters()
        {
            try
            {
                var dict = new Dictionary<string, object>()
                {
                    { "X1", X1 },
                    { "Y1", Y1 },
                    { "X2", X2 },
                    { "Y2", Y2 },
                    { "LineType", (int)TypeOfLine },
                    { "LineStyle", (int)StyleOfLine },
                    { "Thickness", Thickness },
                    { "Red", Colour.Red },
                    { "Green", Colour.Green },
                    { "Blue", Colour.Blue },
                    { "Alpha", Colour.Alpha },
                    { "HeadType", (int)HeadType },
                    { "TailType", (int)TailType },
                    { "AnchorID1", AnchorID1 },
                    { "AnchorID2", AnchorID2 }
                };

                if (ControlPoints != null)
                {
                    for (int i = 0; i < ControlPoints.Count; i++)
                    {
                        for (int j = 0; j < ControlPoints[i].Length; j++)
                        {
                            string key = $"ControlPoint{(j == 0 ? 'X' : 'Y')}{i}";
                            dict.Add(key, ControlPoints[i][j]);
                        }
                    }
                }

                return dict;
            }
            catch (Exception e)
            {
                // Uncaught misc. error. Please log.
                ErrorLogger.ShowErrorMessageBox(e);
                return null;
            }
        }
    }
}
