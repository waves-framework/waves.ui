﻿using PropertyChanged;
using Waves.Core.Base;
using Waves.UI.Drawing.Base.Interfaces;

namespace Waves.UI.Drawing.Base
{
    /// <summary>
    ///     Primitive drawing object.
    /// </summary>
    public abstract class ShapeDrawingObject : DrawingObject, IShapeDrawingObject
    {
        /// <inheritdoc />
        public float Height { get; set; } = 0;

        /// <inheritdoc />
        public float Width { get; set; } = 0;

        /// <inheritdoc />
        public Point Location { get; set; } = new Point(0, 0);

        /// <inheritdoc />
        [SuppressPropertyChangedWarnings]
        public abstract override string Name { get; set; }

        /// <inheritdoc />
        public abstract override void Draw(IDrawingElement e);
    }
}