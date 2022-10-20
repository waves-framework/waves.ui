using System;
using System.Windows.Input;

namespace Waves.UI.Dialogs.Interfaces
{
    /// <summary>
    /// Interface for dialog action tools.
    /// </summary>
    public interface IWavesDialogActionTool : IWavesDialogTool
    {
        /// <summary>
        /// Gets invoked event.
        /// </summary>
        event EventHandler<object> Invoked;

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        ICommand Command { get; }
    }
}
