using Waves.Core.Base.Interfaces;

namespace Waves.UI.Presentation.Interfaces.View
{
    /// <summary>
    /// Interface for all views.
    /// </summary>
    public interface IWavesView : IWavesPlugin
    {
        /// <summary>
        ///     Gets or sets view model context.
        /// </summary>
        object DataContext { get; set; }
    }
}
