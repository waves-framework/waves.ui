using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    /// Pixel.
    /// </summary>
    public class MtlPixel : MtlDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Pixel";

        /// <summary>
        /// Gets or sets point.
        /// </summary>
        public MtlPoint Point { get; set; }

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
