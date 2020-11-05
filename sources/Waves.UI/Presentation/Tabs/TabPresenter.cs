using Waves.Presentation.Interfaces;
using Waves.UI.Presentation.Tabs.Interfaces;

namespace Waves.UI.Presentation.Tabs
{
    /// <summary>
    /// Base tab presenter.
    /// </summary>
    public abstract class TabPresenter : Waves.Presentation.Base.Presenter, ITabPresenter
    {
        /// <inheritdoc />
        public abstract string VectorIconPathData { get; }

        /// <inheritdoc />
        public abstract double[] VectorIconThickness { get; }
    }
}