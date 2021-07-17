using System.Drawing;

namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Interface of drawing object.
    /// </summary>
    public interface IMtlDrawingObject
    {
        /// <summary>
        ///     Gets whether drawing object is antialiased.
        /// </summary>
        bool IsAntialiased { get; set; }

        /// <summary>
        ///     Gets whether drawing object is visible.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets drawing object name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets object's opacity.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        ///     Gets or sets stroke thickness.
        /// </summary>
        float StrokeThickness { get; set; }

        /// <summary>
        ///     Gets or sets fill.
        /// </summary>
        Color Fill { get; set; }

        /// <summary>
        ///     Gets or sets stroke.
        /// </summary>
        Color Stroke { get; set; }

        /// <summary>
        /// Draw object in current canvas.
        /// </summary>
        /// <param name="element">Drawing element.</param>
        void Draw(IMtlDrawingElement element);
    }
}
