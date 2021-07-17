using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Primitive drawing object.
    /// </summary>
    public abstract class WavesShapeDrawingObject : WavesDrawingObject, IWavesShapeDrawingObject
    {
        /// <inheritdoc />
        public float Height { get; set; } = 0;

        /// <inheritdoc />
        public float Width { get; set; } = 0;

        /// <inheritdoc />
        public WavesPoint Location { get; set; } = new WavesPoint(0, 0);

        /// <inheritdoc />
        public abstract override string Name { get; set; }

        /// <inheritdoc />
        public abstract override void Draw(IWavesDrawingElement e);
    }
}
