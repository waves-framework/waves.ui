using Waves.Core.Base;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Base
{
    /// <summary>
    /// Vector path.
    /// </summary>
    public class VectorPath : IVectorPath
    {
        /// <summary>
        /// Creates new instance of vector path.
        /// </summary>
        /// <param name="pathData">Geometry path data.</param>
        /// <param name="color">Path color.</param>
        public VectorPath(string pathData, WavesColor color)
        {
            GeometryPathData = pathData;
            Color = color;
        }

        /// <inheritdoc />
        public string GeometryPathData { get; }
        
        /// <inheritdoc />
        public WavesColor Color { get; }
    }
}