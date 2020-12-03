using System.Collections.Generic;
using Waves.Core.Base.Interfaces;
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
        /// <summary>
        /// Creates new instance of <see cref="ModalWindowPresenterViewModel"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        protected ModalWindowPresenterViewModel(IWavesCore core) : base(core)
        {
        }
        
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