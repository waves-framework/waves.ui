using System;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.UI.Dialogs;
using Waves.UI.Dialogs.Enums;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    /// Interface for dialog service.
    /// </summary>
    public interface IWavesDialogService : IWavesPlugin
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
        /// <param name="text">Message text.</param>
        /// <param name="title">Message title.</param>
        /// <param name="sender">Message sender.</param>
        /// <param name="type">Message type.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns>Dialog result.</returns>
        Task<WavesDialogResult> ShowDialogAsync(string text, string title = null, string sender = null, WavesDialogMessageType type = WavesDialogMessageType.Information, WavesDialogButtons buttons = WavesDialogButtons.Ok);

        /// <summary>
        /// Shows dialog with text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="title">Title.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="type">Type.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns>Dialog result.</returns>
        Task<WavesDialogResult> ShowDialogAsync(string text, string title = null, IWavesObject sender = null, WavesDialogMessageType type = WavesDialogMessageType.Information, WavesDialogButtons buttons = WavesDialogButtons.Ok);

        /// <summary>
        /// Shows dialog with text.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="type">Type.</param>
        /// <param name="buttons">Buttons.</param>
        /// <returns>Dialog result.</returns>
        Task<WavesDialogResult> ShowDialogAsync(string text, Exception exception, IWavesObject sender = null, WavesDialogMessageType type = WavesDialogMessageType.Error, WavesDialogButtons buttons = WavesDialogButtons.Ok);

        /// <summary>
        /// Shows open file dialog.
        /// </summary>
        /// <param name="filter">Filter.</param>
        /// <returns>Returns dialog result.</returns>
        Task<WavesOpenFileDialogResult> ShowOpenFileDialogAsync(IEnumerable<WavesFileDialogFilter> filter = null);
    }
}
