using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Base text paint.
    /// </summary>
    public class MtlTextPaint : MtlPaint, IMtlTextPaint
    {
        /// <inheritdoc />
        public IMtlTextStyle TextStyle { get; set; } = new MtlTextStyle();
    }
}
