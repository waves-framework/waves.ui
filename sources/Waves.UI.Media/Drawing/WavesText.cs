using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Text.
    /// </summary>
    public class MtlText : MtlDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Text";

        /// <summary>
        ///     Gets or sets text style.
        /// </summary>
        public IMtlTextStyle Style { get; set; }

        /// <summary>
        ///     Gets or sets text value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets text location.
        /// </summary>
        public MtlPoint Location { get; set; } = new MtlPoint(0, 0);

        /// <inheritdoc />
        public override void Draw(IMtlDrawingElement e)
        {
            if (Style == null)
            {
                return;
            }

            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
