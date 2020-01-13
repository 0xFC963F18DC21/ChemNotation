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
            Plain, Arrow
        }

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

        public Line(float x1 = 0, float y1 = 0, float x2 = 0, float y2 = 0, List<float[]> controlPoints = null, float thickness = 4f, SKColor? colour = null, LineType typeOfLine = LineType.Line, LineStyle styleOfLine = LineStyle.Plain, EndType headType = EndType.Plain, EndType tailType = EndType.Plain, int anchorID1 = -1, int anchorID2 = -1)
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
            throw new NotImplementedException();
        }

        public override void EditInternalParameters(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, object> GetInternalParameters()
        {
            throw new NotImplementedException();
        }

        public override bool IsMouseIntersect(Point location)
        {
            throw new NotImplementedException();
        }
    }
}
