using System.Collections.Generic;
using Waves.Presentation.Interfaces;
using Waves.UI.Base.Interfaces;
using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.Extensions;
using Waves.UI.Modality.Presentation.Enums;
using Waves.UI.Modality.ViewModel;

namespace Waves.UI.Modality.Presentation
{
    /// <summary>
    /// Message modal window presentation.
    /// </summary>
    public abstract class MessageModalWindowPresentation : ModalWindowPresentation
    {
        private IPresentationViewModel _dataContext;

        /// <summary>
        /// Creates new instance of <see cref="MessageModalWindowPresentation"/>.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        protected MessageModalWindowPresentation(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <inheritdoc />
        public abstract override IVectorImage Icon { get; }

        /// <inheritdoc />
        public override string Title { get; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        public override IPresentationViewModel DataContext => _dataContext;

        /// <inheritdoc />
        public abstract override IPresentationView View { get; }

        /// <inheritdoc />
        public override void Initialize()
        {
            _dataContext = new MessageModalWindowViewModel(Message, Icon);

            base.Initialize();
        }

        /// <summary>
        /// Initializes actions.
        /// </summary>
        /// <param name="actions"></param>
        public void InitializeActions(ICollection<IModalWindowAction> actions)
        {
            foreach (var action in actions)
            {
                this.AddAction(action);
            }
        }

        /// <summary>
        /// Initializes icon.
        /// </summary>
        /// <param name="icon">Icon type.</param>
        protected abstract void InitializeIcon(MessageIcon icon);
    }
}