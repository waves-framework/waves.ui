using Waves.UI.Drawing;
using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Extensions
{
    /// <summary>
    /// Drawing extensions.
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Gets paint from drawing object.
        /// </summary>
        /// <param name="obj">Drawing object.</param>
        /// <returns>Returns paint.</returns>
        public static IWavesPaint GetPaint(this WavesDrawingObject obj)
        {
            return new WavesPaint
            {
                Fill = obj.Fill,
                Stroke = obj.Stroke,
                IsAntialiased = obj.IsAntialiased,
                Opacity = obj.Opacity,
                StrokeThickness = obj.StrokeThickness,
            };
        }

        /// <summary>
        /// Gets paint from text.
        /// </summary>
        /// <param name="obj">Text.</param>
        /// <returns>Returns paint.</returns>
        public static IWavesTextPaint GetPaint(this WavesText obj)
        {
            return new WavesTextPaint
            {
                Fill = obj.Fill,
                IsAntialiased = obj.IsAntialiased,
                Opacity = obj.Opacity,
                TextStyle = obj.Style,
            };
        }
    }
}
