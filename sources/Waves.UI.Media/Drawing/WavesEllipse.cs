using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Ellipse.
    /// </summary>
    public class MtlEllipse : MtlShapeDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Ellipse";

        /// <summary>
        ///     Gets or sets ellipse radius.
        /// </summary>
        public float Radius { get; set; }

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
