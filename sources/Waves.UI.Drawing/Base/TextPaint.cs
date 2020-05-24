using Waves.UI.Drawing.Base.Interfaces;
using Waves.UI.Windows.Controls.Drawing.Base;

namespace Waves.UI.Drawing.Base
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