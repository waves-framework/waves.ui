using System.Collections.Generic;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    ///     Interface for vector image.
    /// </summary>
    public interface IVectorImage
    {
        /// <summary>
        ///     Gets name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets width of image.
        /// </summary>
        double Width { get; }

        /// <summary>
        ///     Gets height of image.
        /// </summary>
        double Height { get; }

        /// <summary>
        ///     Gets image padding.
        /// </summary>
        /// <remarks>
        ///     Not all vector images are centered.
        ///     In this regard, this parameter is required.
        /// </remarks>
        double[] Padding { get; }

        /// <summary>
        ///     Gets collection of vector paths.
        /// </summary>
        List<IVectorPath> Paths { get; }
    }
}