using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace ChemNotation.DiagramObjects
{
    public class Diagram : IDisposable
    {
        // Debug logger
        private static readonly ErrorLogger Log = new ErrorLogger(typeof(Diagram));

        // Image size, created during runtime.
        private SKImageInfo? Metadata;

        // Drawing surface. Used to draw the objects to the screen.
        private SKSurface _DiagramSurface;
        public SKSurface DiagramSurface
        {
            get
            {
                if (_DiagramSurface == null)
                {
                    // Initialises a blank white canvas.
                    _DiagramSurface = SKSurface.Create(Metadata.Value);
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
        public Diagram(int width, int height)
        {
            Metadata = new SKImageInfo(width, height);
            Log.LogMessageGeneral("New diagram created.");
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
            if (clearScreen)
            {
                Log.LogMessageGeneral("Diagram cleared pre-update.");
                DiagramSurface.Canvas.Clear(SKColors.White);
            }

            IEnumerable<DiagramObject> bonds =
                from obj in DiagramObjects
                where obj.ObjectID == DiagramObject.ObjectTypeID.Bond
                select obj;

            IEnumerable<DiagramObject> lines =
                from obj in DiagramObjects
                where obj.ObjectID == DiagramObject.ObjectTypeID.Line
                select obj;

            IEnumerable<DiagramObject> atoms =
                from obj in DiagramObjects
                where obj.ObjectID == DiagramObject.ObjectTypeID.Atom
                select obj;

            IEnumerable<DiagramObject> texts =
                from obj in DiagramObjects
                where obj.ObjectID == DiagramObject.ObjectTypeID.Text
                select obj;

            IEnumerable<DiagramObject>[] enumerables = { bonds, lines, atoms, texts };

            foreach (IEnumerable<DiagramObject> enumerable in enumerables)
            {
                // Draw each object from the list.
                foreach (DiagramObject obj in enumerable)
                {
                    obj.Draw(this);
                }
            }

            return DiagramSurface;
        }

        /// <summary>
        /// Clears all the drawn objects in the diagram.
        /// </summary>
        public void ResetDiagram()
        {
            Log.LogMessageInfo("Diagram reset.");
            Dispose();
        }

        /// <summary>
        /// Adds an object instance to the internal object list.
        /// </summary>
        /// <param name="obj"><code>DiagramObject</code> instance</param>
        public void AddDiagramObject(DiagramObject obj)
        {
            Log.LogMessageDebug($"Object placed: {obj}");
            if (ErrorLogger.AllowLogging)
            {
                var parameters = obj.GetInternalParameters();
                foreach (string key in parameters.Keys)
                {
                    Log.LogMessageDebug($"{key}: {parameters[key]}");
                }
            }

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

        /// <summary>
        /// Gets the object that the mouse is intersecting.
        /// </summary>
        /// <param name="selectedObjectID">Object currently selected</param>
        /// <param name="mouseLocation">Current location of mouse</param>
        /// <returns></returns>
        public int FindMouseIntersect(int? selectedObjectID, Point mouseLocation)
        {
            int SID = -1;
            if (selectedObjectID != null) SID = selectedObjectID.Value;

            foreach (DiagramObject item in DiagramObjects)
            {
                try
                {
                    if (item.IsMouseIntersect(mouseLocation) && item.DiagramID != SID) return item.DiagramID;
                } catch (NotImplementedException e)
                {
                    // Mouse checking code not implemented on object. This is a serious issue.
                    ErrorLogger.ShowErrorMessageBox(e);
                    continue;
                }
            }

            return -1;
        }

        // Get newest free object ID.
        private int _NextFreeID = 0;
        public int NextFreeID()
        {
            return _NextFreeID++;
        }

        public void Dispose()
        {
            try
            {
                for (int i = 0; i < DiagramObjects.Count; i++)
                {
                    DiagramObjects[i] = null;
                }

                DiagramObjects.Clear();
                DiagramObjects.TrimExcess();

                _DiagramSurface.Dispose();
                _DiagramSurface = null;
            } catch (NullReferenceException e)
            {
                Log.LogMessageError("Nullref caught.", e);
            } catch (Exception e)
            {
                ErrorLogger.ShowErrorMessageBox(e);
            }
        }

        ~Diagram()
        {
            Dispose();
            Log.LogMessageInfo("Diagram instance destroyed.");
        }
    }
}
