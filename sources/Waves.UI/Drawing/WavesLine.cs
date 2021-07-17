using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Line.
    /// </summary>
    public class WavesLine : WavesDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Line";

        /// <summary>
        ///     Gets or sets first point.
        /// </summary>
        public WavesPoint Point1 { get; set; }

        /// <summary>
        ///     Gets or sets second point.
        /// </summary>
        public WavesPoint Point2 { get; set; }

        /// <summary>
        ///     Gets or sets dash pattern.
        /// </summary>
        public float[] DashPattern { get; set; } = { 0, 0, 0, 0 };

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
