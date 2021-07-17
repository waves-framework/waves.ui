using System;
using System.Threading.Tasks;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.UI.Presentation.Dialogs.Enums;

namespace Waves.UI.Plugins.Services.Interfaces
{
    /// <summary>
    /// Interface for dialog service.
    /// </summary>
    public interface IWavesDialogService : IWavesService
    {
        /// <summary>
        /// Event that fired when dialog shown.
        /// </summary>
        event EventHandler DialogsShown;

        /// <summary>
        /// Event that fired when all dialog hidden.
        /// </summary>
        event EventHandler DialogsHidden;

        /// <summary>
        /// Shows dialog with text.
        /// </summary>
        /// <param name="messageObject">Message.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns>Dialog result.</returns>
        Task<WavesMessageDialogResult> ShowDialogAsync(IWavesMessageObject messageObject, WavesMessageDialogButtons buttons = WavesMessageDialogButtons.Ok);

        /// <summary>
        /// Shows dialog with text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="title">Title.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="type">Type.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns>Dialog result.</returns>
        Task<WavesMessageDialogResult> ShowDialogAsync(string text, string title = "Message", IWavesObject sender = null, WavesMessageType type = WavesMessageType.Information, WavesMessageDialogButtons buttons = WavesMessageDialogButtons.Ok);
    }
}
