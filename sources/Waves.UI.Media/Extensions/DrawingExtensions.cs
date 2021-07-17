using MTL.UI.Media.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Extensions
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
        public static IMtlPaint GetPaint(this MtlDrawingObject obj)
        {
            return new MtlPaint
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
        public static IMtlTextPaint GetPaint(this MtlText obj)
        {
            return new MtlTextPaint
            {
                Fill = obj.Fill,
                IsAntialiased = obj.IsAntialiased,
                Opacity = obj.Opacity,
                TextStyle = obj.Style,
            };
        }
    }
}
