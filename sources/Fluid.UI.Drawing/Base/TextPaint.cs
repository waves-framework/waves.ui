using Fluid.UI.Drawing.Base.Interfaces;
using Fluid.UI.Windows.Controls.Drawing.Base;

namespace Fluid.UI.Drawing.Base
{
    /// <summary>
    ///     Base text paint.
    /// </summary>
    public class TextPaint : Paint, ITextPaint
    {
        /// <inheritdoc />
        public ITextStyle TextStyle { get; set; } = new TextStyle();
    }
}