using System;
using System.Collections.Generic;
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
        // With an abstract class, the IDs can use an internal enum type.
        public enum ObjectTypeID
        {
            Atom, Bond, Line, Text
        }

        // These properties replace the getter methods in the interface
        public ObjectTypeID ObjectID { get; protected set; }
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
        public abstract  bool IsMouseIntersect();
    }
}
