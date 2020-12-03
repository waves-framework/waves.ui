using Waves.Presentation.Interfaces;

namespace Waves.UI.Presentation.Tabs.Interfaces
{
    /// <summary>
    /// Interface for tab presenter.
    /// </summary>
    public interface ITabPresenter : IPresenter
    {
        /// <summary>
        /// Gets vector icon path data.
        /// </summary>
        string VectorIconPathData { get; }

        /// <summary>
        /// Gets vector icon thickness.
        /// </summary>
        double[] VectorIconThickness { get; }
    }
}