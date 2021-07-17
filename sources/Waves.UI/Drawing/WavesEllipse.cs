using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Ellipse.
    /// </summary>
    public class WavesEllipse : WavesShapeDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Ellipse";

        /// <summary>
        ///     Gets or sets ellipse radius.
        /// </summary>
        public float Radius { get; set; }

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
