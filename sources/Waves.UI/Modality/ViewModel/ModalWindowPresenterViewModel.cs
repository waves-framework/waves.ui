using System.Collections.Generic;
using Waves.Presentation.Base;
using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.ViewModel.Interfaces;

namespace Waves.UI.Modality.ViewModel
{
    /// <summary>
    ///     Base abstract modality window presenter view model.
    /// </summary>
    public abstract class ModalWindowPresenterViewModel : 
        PresenterViewModel, 
        IModalWindowPresenterViewModel
    {
        /// <inheritdoc />
        public ICollection<IModalWindowAction> Actions { get; private set; }

        /// <inheritdoc />
        public void AttachActions(ICollection<IModalWindowAction> actions)
        {
            Actions = actions;
        }

        /// <inheritdoc />
        public void ClearActions()
        {
            Actions.Clear();
        }
    }
}