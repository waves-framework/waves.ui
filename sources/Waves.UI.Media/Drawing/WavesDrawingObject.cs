using System;
using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Base abstract class for drawing objects.
    /// </summary>
    public abstract class MtlDrawingObject : IMtlDrawingObject
    {
        /// <inheritdoc />
        public abstract string Name { get; set; }

        /// <inheritdoc />
        public bool IsAntialiased { get; set; } = true;

        /// <inheritdoc />
        public bool IsVisible { get; set; } = true;

        /// <inheritdoc />
        public float Opacity { get; set; } = 1.0f;

        /// <inheritdoc />
        public float StrokeThickness { get; set; } = 1;

        /// <inheritdoc />
        public Color Fill { get; set; } = Color.Black;

        /// <inheritdoc />
        public Color Stroke { get; set; } = Color.Gray;

        /// <inheritdoc />
        public abstract void Draw(IMtlDrawingElement e);
    }
}
