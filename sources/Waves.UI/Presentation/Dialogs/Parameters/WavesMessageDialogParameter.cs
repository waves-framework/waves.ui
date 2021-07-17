using Waves.Core.Base.Interfaces;
using Waves.UI.Presentation.Dialogs.Enums;

namespace Waves.UI.Presentation.Dialogs.Parameters
{
    /// <summary>
    /// Parameter for message dialog.
    /// </summary>
    public class WavesMessageDialogParameter
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesMessageDialogParameter"/>.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons.</param>
        public WavesMessageDialogParameter(IWavesMessageObject message, WavesMessageDialogButtons buttons = WavesMessageDialogButtons.Ok)
        {
            Message = message;
            Buttons = buttons;
        }

        /// <summary>
        /// Gets text.
        /// </summary>
        public IWavesMessageObject Message { get; }

        /// <summary>
        /// Gets buttons.
        /// </summary>
        public WavesMessageDialogButtons Buttons { get; }
    }
}
