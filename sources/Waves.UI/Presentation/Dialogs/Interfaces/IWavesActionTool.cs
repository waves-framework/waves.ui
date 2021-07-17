using System;
using System.Windows.Input;

namespace Waves.UI.Presentation.Dialogs.Interfaces
{
    /// <summary>
    /// Interface for action tools.
    /// </summary>
    public interface IWavesActionTool : IWavesTool
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
