using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    /// <summary>
    /// The common parent class for all visual objects on the diagram.
    /// </summary>
    public abstract class DiagramObject
    {
        // Debug logger
        protected static readonly ErrorLogger Log = new ErrorLogger(typeof(DiagramObject));

        // Default object colour
        protected static readonly SKColor DefaultColour = new SKColor(0, 0, 0, 255);

        // With an abstract class, the IDs can use an internal enum type.
        public enum ObjectTypeID
        {
            Base, Atom, Bond, Line, Text
        }

        // These properties replace the getter methods in the interface
        public virtual ObjectTypeID ObjectID
        {
            get
            {
                return ObjectTypeID.Base;
            }
        }
        public int DiagramID { get; set; }

        /// <summary>
        /// Draws the current object to a diagram.
        /// </summary>
        /// <param name="diagram">Reference of the diagram to draw on.</param>
        public abstract void Draw(Diagram diagram);

        /// <summary>
        /// This method allows the passing of values into the values into the diagram object.
        /// It allows the editing of already-placed objects.
        /// </summary>
        /// <param name="parameters">The object's internal parameters.</param>
        public abstract void EditInternalParameters(Dictionary<string, object> parameters);

        /// <summary>
        /// Gets the current object's parameters.
        /// </summary>
        /// <returns>Dictionary containing the current instance's parameters.</returns>
        public abstract Dictionary<string, object> GetInternalParameters();

        /// <summary>
        /// Tells the current control if the mouse is intersecting the object's click area.
        /// </summary>
        /// <returns><code>true</code> if it intersects. <code>false</code> otherwise.</returns>
        public abstract bool IsMouseIntersect(Point location);

        /// <summary>
        /// A <code>DiagramObject</code> factory method.
        /// </summary>
        /// <param name="type">Object type to create</param>
        /// <returns>Initialised <code>DiagramObject</code> instance. Must be edited after before adding to diagram.</returns>
        public DiagramObject CreateNewObjectInstance(ObjectTypeID type)
        {
            switch (type) {
                case ObjectTypeID.Atom:
                    return new Atom();
                case ObjectTypeID.Bond:
                    return new Bond();
                case ObjectTypeID.Line:
                    throw new NotImplementedException("Line type not implemented yet.");
                case ObjectTypeID.Text:
                    throw new NotImplementedException("Text type not implemented yet.");
                default:
                    throw new ArgumentException("Invalid DiagramObject type.");
            }
        }
    }
}
