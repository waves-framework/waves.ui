using Waves.Core.Base.Interfaces;
using Waves.Presentation.Interfaces;
using Waves.UI.Presentation.Tabs.Interfaces;

namespace Waves.UI.Presentation.Tabs
{
    /// <summary>
    /// Base tab presenter.
    /// </summary>
    public abstract class TabPresenter : Waves.Presentation.Base.Presenter, ITabPresenter
    {
        /// <summary>
        /// Creates new instance of <see cref="TabPresenter"/>.
        /// </summary>
        /// <param name="core">Core.</param>
        protected TabPresenter(IWavesCore core) : base(core)
        {
            
        }
        
        /// <inheritdoc />
        public abstract string VectorIconPathData { get; }

        /// <inheritdoc />
        public abstract double[] VectorIconThickness { get; }
    }
}