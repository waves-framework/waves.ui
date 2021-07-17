using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Base text paint.
    /// </summary>
    public class WavesTextPaint : WavesPaint, IWavesTextPaint
    {
        /// <inheritdoc />
        public IWavesTextStyle TextStyle { get; set; } = new WavesTextStyle();
    }
}
