using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    public class Diagram
    {
        // Image size
        private static readonly SKImageInfo Metadata = new SKImageInfo(640, 480);

        // Drawing surface. Used to draw the objects to the screen.
        private SKSurface _DiagramSurface;
        public SKSurface DiagramSurface
        {
            get
            {
                if (_DiagramSurface == null)
                {
                    // Initialises a blank white canvas.
                    _DiagramSurface = SKSurface.Create(Metadata);
                    _DiagramSurface.Canvas.Clear(SKColors.White);
                }

                return _DiagramSurface;
            }
        }

        // List of object instances inside the diagram.
        protected List<DiagramObject> DiagramObjects { get; set; }

        /// <summary>
        /// Creates a new blank diagram.
        /// </summary>
        public Diagram()
        {
            DiagramObjects = new List<DiagramObject>();
        }

        /// <summary>
        /// Redraws the screen without clearing.
        /// </summary>
        /// <returns>The resulting <code>SKSurface</code></returns>
        public SKSurface UpdateView()
        {
            return UpdateView(false);
        }

        /// <summary>
        /// Redraws the screen.
        /// </summary>
        /// <param name="clearScreen">Should the code clear the screen before drawing?</param>
        /// <returns>The resulting <code>SKSurface</code></returns>
        public SKSurface UpdateView(bool clearScreen)
        {
            if (clearScreen) DiagramSurface.Canvas.Clear(SKColors.White);

            foreach (DiagramObject obj in DiagramObjects)
            {
                // Draw each object from the list.
                obj.Draw(this);
            }

            return DiagramSurface;
        }

        /// <summary>
        /// Adds an object instance to the internal object list.
        /// </summary>
        /// <param name="obj"><code>DiagramObject</code> instance</param>
        public void AddDiagramObject(DiagramObject obj)
        {
            DiagramObjects.Add(obj);
        }

        /// <summary>
        /// Removes an object with the specified ID.
        /// </summary>
        /// <param name="id">ID number of object in diagram.</param>
        public void RemoveDiagramObject(int id)
        {
            for (int i = DiagramObjects.Count - 1; i >= 0; i--)
            {
                if (DiagramObjects[i].DiagramID == id)
                {
                    // Removes object with ID match and trims list to reduce memory consumption.
                    DiagramObjects.RemoveAt(i);
                    DiagramObjects.TrimExcess();
                    break;
                }
            }
        }

        ~Diagram()
        {
            _DiagramSurface.Dispose();
        }
    }
}
