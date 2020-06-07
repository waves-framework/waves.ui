using System.Collections.Generic;
using Waves.Presentation.Interfaces;
using Waves.UI.Modality.Base.Interfaces;

namespace Waves.UI.Modality.ViewModel.Interfaces
{
    /// <summary>
    ///     Interface for modality window presentation view model.
    /// </summary>
    public interface IModalWindowPresentationViewModel : IPresentationViewModel
    {
        /// <summary>
        /// Gets collections of actions.
        /// </summary>
        ICollection<IModalWindowAction> Actions { get; }

        /// <summary>
        /// Attaches actions.
        /// </summary>
        /// <param name="actions">Actions.</param>
        void AttachActions(ICollection<IModalWindowAction> actions);

        /// <summary>
        /// Clear actions.
        /// </summary>
        void ClearActions();
    }
}