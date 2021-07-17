using System;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Plugins.Services.Interfaces
{
    /// <summary>
    /// Interface for navigation service.
    /// </summary>
    public interface IWavesNavigationService : IWavesService
    {
        /// <summary>
        /// Events for notifying that can go back changed.
        /// </summary>
        event EventHandler<GoBackNavigationEventArgs> GoBackChanged;

        /// <summary>
        /// Event that fired when dialog shown.
        /// </summary>
        event EventHandler DialogsShown;

        /// <summary>
        /// Event that fired when all dialog hidden.
        /// </summary>
        event EventHandler DialogsHidden;

        /// <summary>
        /// Goes back.
        /// </summary>
        /// <param name="viewModel">Instance of <see cref="IWavesViewModel"/>.</param>
        /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
        Task GoBackAsync(IWavesViewModel viewModel);

        /// <summary>
        /// Goes back.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <returns>A<see cref="Task"/> representing the asynchronous operation.</returns>
        Task GoBackAsync(string region);

        /// <summary>
        /// Navigates to current view model.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true);

        /// <summary>
        /// Navigates to current view model which returns Result.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task NavigateAsync<TParameter>(IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true);

        /// <summary>
        /// Navigates to current view model which returns Result.
        /// </summary>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<TResult> NavigateAsync<TResult>(IWavesViewModel<TResult> viewModel, bool addToHistory = true);

        /// <summary>
        /// Navigates to current view model with Parameter with current type which returns result Result.
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<TResult> NavigateAsync<TParameter, TResult>(IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true);

        /// <summary>
        /// Navigates to view model with current type.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task NavigateAsync<T>(bool addToHistory = true)
            where T : class;

        /// <summary>
        /// Navigates to view model with current type.
        /// </summary>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task NavigateAsync<T, TParameter>(TParameter parameter, bool addToHistory = true)
            where T : class;

        /// <summary>
        /// Navigates to view model with current type which returns result Result.
        /// </summary>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <typeparam name="TResult">Type of result.</typeparam>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<TResult> NavigateAsync<T, TResult>(bool addToHistory = true)
            where T : class;

        /// <summary>
        /// Navigates to view model with Parameter with current type which returns result Result.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether view model needed to be add to View history.</param>
        /// <typeparam name="T">Type of view model.</typeparam>
        /// <typeparam name="TParameter">Type of parameter.</typeparam>
        /// <typeparam name="TResult">Type of result..</typeparam>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<TResult> NavigateAsync<T, TParameter, TResult>(TParameter parameter, bool addToHistory = true)
            where T : class;

        /// <summary>
        /// Registers content control.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <param name="contentControl">Content control.</param>
        void RegisterContentControl(string region, object contentControl);

        /// <summary>
        /// Unregisters content control.
        /// </summary>
        /// <param name="region">Region.</param>
        void UnregisterContentControl(string region);
    }
}
