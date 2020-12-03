using System;
using Waves.Core.Base;

namespace Waves.UI.Drawing.Base.Interfaces
{
    /// <summary>
    ///     Interface for paint instances.
    /// </summary>
    public interface IPaint : IDisposable
    {
        /// <summary>
        ///     Gets or sets whether paint is antialiased.
        /// </summary>
        bool IsAntialiased { get; set; }

        /// <summary>
        ///     Gets or sets color.
        /// </summary>
        WavesColor Fill { get; set; }

        /// <summary>
        ///     Gets or sets stroke color.
        /// </summary>
        WavesColor Stroke { get; set; }

        /// <summary>
        ///     Gets or sets opacity.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        ///     Gets or sets stroke thickness.
        /// </summary>
        float StrokeThickness { get; set; }

        /// <summary>
        ///     Gets or sets dash pattern.
        /// </summary>
        float[] DashPattern { get; set; }
    }
}