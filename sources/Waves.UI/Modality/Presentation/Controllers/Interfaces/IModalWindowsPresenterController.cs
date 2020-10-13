using Waves.Presentation.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;

namespace Waves.UI.Modality.Presentation.Controllers.Interfaces
{
    /// <summary>
    /// Interface for modality windows presenter controller.
    /// </summary>
    public interface IModalWindowPresenterController : IPresenterController
    {
        /// <summary>
        /// Gets whether modality controller visible.
        /// </summary>
        bool IsVisible { get; }

        /// <summary>
        /// Shows window.
        /// </summary>
        /// <param name="presenter">Presenter.</param>
        void ShowWindow(IModalWindowPresenter presenter);

        /// <summary>
        /// Hides window.
        /// </summary>
        /// <param name="presenter">Presenter.</param>
        void HideWindow(IModalWindowPresenter presenter);
    }
}