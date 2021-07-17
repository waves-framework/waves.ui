using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    /// Pixel.
    /// </summary>
    public class WavesPixel : WavesDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Pixel";

        /// <summary>
        /// Gets or sets point.
        /// </summary>
        public WavesPoint Point { get; set; }

        /// <inheritdoc />
        public override void Draw(IWavesDrawingElement e)
        {
            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
