using Fluid.Core.Base;

namespace Fluid.UI.Base.Interfaces
{
    /// <summary>
    ///     Interface for vector path.
    /// </summary>
    public interface IVectorPath
    {
        /// <summary>
        ///     Gets or sets geometry path data.
        /// </summary>
        string GeometryPathData { get; }

        /// <summary>
        ///     Gets or sets color of path.
        /// </summary>
        Color Color { get; }
    }
}