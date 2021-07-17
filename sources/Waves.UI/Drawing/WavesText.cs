using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Text.
    /// </summary>
    public class WavesText : WavesDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Text";

        /// <summary>
        ///     Gets or sets text style.
        /// </summary>
        public IWavesTextStyle Style { get; set; }

        /// <summary>
        ///     Gets or sets text value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets text location.
        /// </summary>
        public WavesPoint Location { get; set; } = new WavesPoint(0, 0);

        /// <inheritdoc />
        public override void Draw(IWavesDrawingElement e)
        {
            if (Style == null)
            {
                return;
            }

            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
