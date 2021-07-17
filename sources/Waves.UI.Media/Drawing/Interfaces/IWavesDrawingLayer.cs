using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Interface of drawing object.
    /// </summary>
    public interface IMtlDrawingLayer : IMtlDrawingObject
    {
        /// <summary>
        /// Gets objects of layer.
        /// </summary>
        IList<IMtlDrawingObject> DrawingObjects { get; }

        /// <summary>
        /// Adds object to layer.
        /// </summary>
        /// <param name="obj">Drawing object.</param>
        void AddObject(IMtlDrawingObject obj);

        /// <summary>
        /// Removes object from layer.
        /// </summary>
        /// <param name="obj">Drawing object.</param>
        void RemoveObject(IMtlDrawingObject obj);
    }
}
