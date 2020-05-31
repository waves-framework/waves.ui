using System.Collections.Generic;
using PropertyChanged;
using Waves.Core.Base;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Base
{
    /// <summary>
    /// Vector image base.
    /// </summary>
    public abstract class VectorImage: ObservableObject, IVectorImage
    {
        /// <inheritdoc />
        [SuppressPropertyChangedWarnings]
        public abstract string Name { get; set; }

        /// <inheritdoc />
        public double Width { get; } = 14.0d;

        /// <inheritdoc />
        public double Height { get; } = 14.0d;
        
        /// <inheritdoc />
        public double[] Padding { get; }  = new double[4] {0,0,0,0};
        
        /// <inheritdoc />
        public List<IVectorPath> Paths { get; private set; } = new List<IVectorPath>(); 
    }
}