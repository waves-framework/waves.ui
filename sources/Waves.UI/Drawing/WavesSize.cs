namespace Waves.UI.Drawing
{
    /// <summary>
    ///     Size base structure.
    /// </summary>
    public readonly struct WavesSize
    {
        /// <summary>
        ///     Creates new instance of size (square).
        /// </summary>
        /// <param name="length">Length.</param>
        public WavesSize(float length)
        {
            Width = length;
            Height = length;
        }

        /// <summary>
        ///     Creates new instance of size (rectangle).
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public WavesSize(float width, float height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets width.
        /// </summary>
        public float Width { get; }

        /// <summary>
        /// Gets height.
        /// </summary>
        public float Height { get; }

        /// <summary>
        /// Gets space.
        /// </summary>
        public float Space => Width * Height;

        /// <summary>
        /// Gets aspect ratio.
        /// </summary>
        public float Aspect => Width / Height;
    }
}
