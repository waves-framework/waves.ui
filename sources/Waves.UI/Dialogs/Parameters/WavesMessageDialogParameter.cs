using System;
using Waves.UI.Dialogs.Enums;

namespace Waves.UI.Dialogs.Parameters
{
    /// <summary>
    /// Parameter for message dialog.
    /// </summary>
    public class WavesMessageDialogParameter
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesMessageDialogParameter"/>.
        /// </summary>
        /// <param name="text">Message text.</param>
        /// <param name="title">Message title.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="messageType">Message type.</param>
        /// <param name="buttons">Buttons.</param>
        public WavesMessageDialogParameter(
            string text,
            string title = null,
            string sender = null,
            Exception exception = null,
            WavesDialogMessageType messageType = WavesDialogMessageType.Information,
            WavesDialogButtons buttons = WavesDialogButtons.Ok)
        {
            Text = text;
            Title = title;
            Sender = sender;
            Exception = exception;
            MessageType = messageType;
            Buttons = buttons;
        }

        /// <summary>
        /// Gets message text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets message title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets message sender.
        /// </summary>
        public string Sender { get; }

        /// <summary>
        /// Gets exception.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Gets message type.
        /// </summary>
        public WavesDialogMessageType MessageType { get; }

        /// <summary>
        /// Gets buttons.
        /// </summary>
        public WavesDialogButtons Buttons { get; }
    }
}
