using MTL.UI.Media.Drawing.Enums;

namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Text style interface.
    /// </summary>
    public interface IMtlTextStyle
    {
        /// <summary>
        ///     Gets or sets font size.
        /// </summary>
        float FontSize { get; set; }

        /// <summary>
        ///     Gets or sets font family.
        /// </summary>
        string FontFamily { get; set; }

        /// <summary>
        ///     Gets or sets font weight.
        /// </summary>
        int Weight { get; set; }

        /// <summary>
        ///     Gets or sets whether the text may be in subpixels.
        /// </summary>
        bool IsSubpixelText { get; set; }

        /// <summary>
        ///     Gets or sets text alignment.
        /// </summary>
        MtlTextAlignment Alignment { get; set; }
    }
}
