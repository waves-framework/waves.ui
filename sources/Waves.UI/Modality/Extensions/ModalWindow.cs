using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;

namespace Waves.UI.Modality.Extensions
{
    /// <summary>
    ///     Modality windows actions extensions.
    /// </summary>
    public static class ModalWindow
    {
        /// <summary>
        ///     Adds action to presentation.
        /// </summary>
        /// <param name="presentation">Presentation.</param>
        /// <param name="action">Action.</param>
        /// <returns>Presentation.</returns>
        public static IModalWindowPresenter AddAction(this IModalWindowPresenter presentation,
            IModalWindowAction action)
        {
            presentation.Actions.Add(action);

            return presentation;
        }
    }
}