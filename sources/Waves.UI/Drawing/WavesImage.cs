using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    /// Image class.
    /// </summary>
    public class WavesImage : WavesShapeDrawingObject
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
        public override void Draw(IWavesDrawingElement e)
        {
            if (!IsVisible)
            {
                return;
            }

            e.Draw(this);
        }
    }
}
