using Waves.Core.Base;
using Waves.UI.Drawing.Base.Interfaces;

namespace Waves.UI.Drawing.Base
{
    /// <summary>
    ///     Base paint.
    /// </summary>
    public class Paint : IPaint
    {
        /// <inheritdoc />
        public bool IsAntialiased { get; set; } = true;

        /// <inheritdoc />
        public WavesColor Fill { get; set; } = WavesColor.Black;

        /// <inheritdoc />
        public WavesColor Stroke { get; set; } = WavesColor.Gray;

        /// <inheritdoc />
        public float Opacity { get; set; }

        /// <inheritdoc />
        public float StrokeThickness { get; set; } = 1;

        /// <inheritdoc />
        public float[] DashPattern { get; set; } = {0, 0, 0, 0};

        /// <inheritdoc />
        public void Dispose()
        {
        }
    }
}