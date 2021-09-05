using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Waves.UI.Presentation.Dialogs.Interfaces;

namespace Waves.UI.Presentation.Interfaces.ViewModel
{
    /// <summary>
    /// Interface for dialog.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public interface IWavesDialogViewModel : IWavesViewModel
    {
        /// <summary>
        /// Event for "Done".
        /// </summary>
        event EventHandler Done;

        /// <summary>
        /// Event for "Cancel".
        /// </summary>
        event EventHandler Cancel;

        /// <summary>
        /// Gets "Done" visibility.
        /// </summary>
        bool IsDoneAvailable { get; }

        /// <summary>
        /// Gets "Cancel" visibility.
        /// </summary>
        bool IsCancelAvailable { get; }

        /// <summary>
        /// Gets done command.
        /// </summary>
        ICommand DoneCommand { get; }

        /// <summary>
        /// Gets cancel command.
        /// </summary>
        ICommand CancelCommand { get; }

        /// <summary>
        /// Gets collection of additional actions.
        /// </summary>
        ObservableCollection<IWavesTool> Tools { get; }
    }

#pragma warning disable SA1402 // File may only contain a single type
    /// <summary>
    /// Interface for dialogs with result.
    /// </summary>
    /// <typeparam name="TResult">Result.</typeparam>
    public interface IWavesDialogViewModel<out TResult> : IWavesDialogViewModel, IWavesViewModel<TResult>
    {
    }

#pragma warning disable SA1402 // File may only contain a single type
    /// <summary>
    /// Interface for dialogs with parameter and result.
    /// </summary>
    /// <typeparam name="TParameter">Parameter.</typeparam>
    /// <typeparam name="TResult">Result.</typeparam>
    public interface IWavesDialogViewModel<in TParameter, out TResult> : IWavesDialogViewModel<TResult>, IWavesViewModel<TParameter, TResult>
    {
    }
}
