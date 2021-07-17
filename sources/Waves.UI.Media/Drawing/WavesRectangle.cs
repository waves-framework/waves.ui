using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Rectangle.
    /// </summary>
    public class MtlRectangle : MtlShapeDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Rectangle";

        /// <summary>
        ///     Gets or sets corner radius.
        /// </summary>
        public float CornerRadius { get; set; }

        /// <inheritdoc />
        public override void Draw(IMtlDrawingElement e)
        {
            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
