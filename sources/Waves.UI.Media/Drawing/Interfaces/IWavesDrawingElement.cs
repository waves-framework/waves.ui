using System.Collections.Generic;
using MTL.UI.Presentation.Interfaces;

namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Interface for drawing element.
    /// </summary>
    public interface IMtlDrawingElement : IMtlView
    {
        /// <summary>
        /// Gets name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets drawing elements.
        /// </summary>
        IEnumerable<IMtlDrawingObject> DrawingObjects { get; set; }

        /// <summary>
        ///  Updates view.
        /// </summary>
        /// <param name="element">Drawing element.</param>
        void Update(object element);

        /// <summary>
        /// Draws pixel.
        /// </summary>
        /// <param name="pixel">Pixel.</param>
        void Draw(MtlPixel pixel);

        /// <summary>
        ///     Draws ellipse.
        /// </summary>
        /// <param name="ellipse">Ellipse.</param>
        void Draw(MtlEllipse ellipse);

        /// <summary>
        /// Draws line.
        /// </summary>
        /// <param name="line">Line.</param>
        void Draw(MtlLine line);

        /// <summary>
        /// Draws lines.
        /// </summary>
        /// <param name="lines">Lines.</param>
        void Draw(IEnumerable<MtlLine> lines);

        /// <summary>
        /// Draws rectangle.
        /// </summary>
        /// <param name="rectangle">Rectangle.</param>
        void Draw(MtlRectangle rectangle);

        /// <summary>
        /// Draws text.
        /// </summary>
        /// <param name="text">Text.</param>
        void Draw(MtlText text);

        /// <summary>
        /// Draws image.
        /// </summary>
        /// <param name="image">Image.</param>
        void Draw(MtlImage image);

        /// <summary>
        ///     Measures text size.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="paint">Paint.</param>
        /// <returns>Text's size.</returns>
        MtlSize MeasureText(string text, IMtlTextPaint paint);
    }
}
