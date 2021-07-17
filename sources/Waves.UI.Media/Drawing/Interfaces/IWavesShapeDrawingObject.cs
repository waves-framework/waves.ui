using System.Drawing;

namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Interface of primitive drawing object.
    /// </summary>
    public interface IMtlShapeDrawingObject : IMtlDrawingObject
    {
        /// <summary>
        ///     Gets or sets height.
        /// </summary>
        float Height { get; set; }

        /// <summary>
        ///     Gets or sets width.
        /// </summary>
        float Width { get; set; }

        /// <summary>
        ///     Gets or sets location.
        /// </summary>
        MtlPoint Location { get; set; }
    }
}
