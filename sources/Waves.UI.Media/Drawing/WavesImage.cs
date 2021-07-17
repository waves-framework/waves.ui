using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Drawing
{
    /// <summary>
    /// Image class.
    /// </summary>
    public class MtlImage : MtlShapeDrawingObject
    {
        /// <inheritdoc />
        public override string Name { get; set; } = "Image";

        /// <summary>
        /// Loads image from file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public void Load(string fileName)
        {
        }

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
