using System;
using Waves.Core.Base;
using Waves.UI.Drawing.Base.Interfaces;

namespace Waves.UI.Drawing.Base
{
    /// <summary>
    ///     Base abstract class for drawing objects.
    /// </summary>
    public abstract class DrawingObject : WavesObject, IDrawingObject
    {
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public abstract override string Name { get; set; }

        /// <inheritdoc />
        public bool IsAntialiased { get; set; } = true;

        /// <inheritdoc />
        public bool IsVisible { get; set; } = true;

        /// <inheritdoc />
        public float Opacity { get; set; } = 1.0f;

        /// <inheritdoc />
        public float StrokeThickness { get; set; } = 1;

        /// <inheritdoc />
        public WavesColor Fill { get; set; } = WavesColor.Black;

        /// <inheritdoc />
        public WavesColor Stroke { get; set; } = WavesColor.Gray;

        /// <inheritdoc />
        public abstract void Draw(IDrawingElement e);
    }
}