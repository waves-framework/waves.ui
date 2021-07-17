using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Primitive drawing object.
    /// </summary>
    public abstract class MtlShapeDrawingObject : MtlDrawingObject, IMtlShapeDrawingObject
    {
        /// <inheritdoc />
        public float Height { get; set; } = 0;

        /// <inheritdoc />
        public float Width { get; set; } = 0;

        /// <inheritdoc />
        public MtlPoint Location { get; set; } = new MtlPoint(0, 0);

        /// <inheritdoc />
        public abstract override string Name { get; set; }

        /// <inheritdoc />
        public abstract override void Draw(IMtlDrawingElement e);
    }
}
