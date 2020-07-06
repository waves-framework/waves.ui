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
    public class MessageModalWindowPresentation : ModalWindowPresentation
    {
        private IPresentationViewModel _dataContext;

        /// <summary>
        /// Creates new instance of <see cref="MessageModalWindowPresentation"/>.
        /// </summary>
        /// <param name="core">Instance of <see cref="Core"/></param>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        /// <param name="messageIcon">Message icon.</param>
        /// <param name="windowIcon">Window icon.</param>
        public MessageModalWindowPresentation(Core core, string title, string message, MessageIcon messageIcon, IVectorImage windowIcon = null) : base(core)
        {
            Title = title;
            Message = message;
            MessageIcon = messageIcon;
            Icon = windowIcon;
        }

        /// <inheritdoc />
        public override IVectorImage Icon { get; }

        /// <summary>
        /// Gets message icon.
        /// </summary>
        public MessageIcon MessageIcon { get; }

        /// <inheritdoc />
        public override string Title { get; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string Message { get; set; }

        /// <inheritdoc />
        public override IPresentationViewModel DataContext { get; protected set; }

        /// <inheritdoc />
        public override IPresentationView View { get; protected set; }

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
    }
}