using System;
using System.Collections.Generic;
using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Base abstract class for drawing objects.
    /// </summary>
    public class MtlDrawingLayer : MtlDrawingObject, IMtlDrawingLayer
    {
        /// <inheritdoc />
        public override string Name { get; set; }

        /// <inheritdoc />
        public IList<IMtlDrawingObject> DrawingObjects { get; } = new List<IMtlDrawingObject>();

        /// <inheritdoc />
        public void AddObject(IMtlDrawingObject obj)
        {
            DrawingObjects.Add(obj);
        }

        /// <inheritdoc />
        public void RemoveObject(IMtlDrawingObject obj)
        {
            DrawingObjects.Remove(obj);
        }

        /// <inheritdoc />
        public override void Draw(IMtlDrawingElement e)
        {
            if (DrawingObjects == null)
            {
                return;
            }

            foreach (var obj in DrawingObjects)
            {
                obj.Draw(e);
            }
        }
    }
}
