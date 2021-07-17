using System.Drawing;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    ///     Line.
    /// </summary>
    public class MtlLine : MtlDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Line";

        /// <summary>
        ///     Gets or sets first point.
        /// </summary>
        public MtlPoint Point1 { get; set; }

        /// <summary>
        ///     Gets or sets second point.
        /// </summary>
        public MtlPoint Point2 { get; set; }

        /// <summary>
        ///     Gets or sets dash pattern.
        /// </summary>
        public float[] DashPattern { get; set; } = { 0, 0, 0, 0 };

        /// <inheritdoc />
        public override void Draw(IMtlDrawingElement e)
        {
            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
