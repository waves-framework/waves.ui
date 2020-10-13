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
        public abstract override IPresenterViewModel DataContext { get; }

        /// <inheritdoc />
        public abstract override IPresenterView View { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public abstract string VectorIconPathData { get; }

        /// <inheritdoc />
        public abstract double[] VectorIconThickness { get; }
    }
}