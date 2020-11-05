using System;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Modality.ViewModel
{
    /// <summary>
    ///     Message modal window presenter view model.
    /// </summary>
    public class MessageModalWindowPresenterViewModel : ModalWindowPresenterViewModel
    {
        /// <summary>
        ///     Creates new instance of message modal window view model.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="icon">Icon.</param>
        public MessageModalWindowPresenterViewModel(string message, IVectorImage icon)
        {
            Message = message;
            Icon = icon;
        }
        
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Message Modal Window Presenter View Model";

        /// <summary>
        ///     Gets or sets modal window message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets modal window icon.
        /// </summary>
        public IVectorImage Icon { get; set; }

        /// <inheritdoc />
        public override void Initialize()
        {
        }

        /// <inheritdoc />
        public override void Dispose()
        {
        }
    }
}