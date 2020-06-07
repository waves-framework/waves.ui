using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;

namespace Waves.UI.Modality.Extensions
{
    /// <summary>
    ///     Modality windows actions extensions.
    /// </summary>
    public static class ModalityWindow
    {
        /// <summary>
        ///     Adds action to presentation.
        /// </summary>
        /// <param name="presentation">Presentation.</param>
        /// <param name="action">Action.</param>
        /// <returns>Presentation.</returns>
        public static IModalWindowPresentation AddAction(this IModalWindowPresentation presentation,
            IModalWindowAction action)
        {
            presentation.Actions.Add(action);

            return presentation;
        }
    }
}