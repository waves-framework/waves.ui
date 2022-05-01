using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Presentation.Interfaces.ViewModel
{
    /// <summary>
    ///     Interfaces for all view models.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public interface IWavesViewModel :
        IWavesObservablePlugin,
        IWavesConfigurablePlugin
    {
        /// <summary>
        ///     Actions when view appeared.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task ViewAppeared();

        /// <summary>
        ///     Actions when view disappeared.
        /// </summary>
        /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
        Task ViewDisappeared();
    }

    /// <summary>
    ///     Interfaces for all view models with return result.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public interface IWavesViewModel<out TResult>
        : IWavesViewModel
    {
        /// <summary>
        /// Event when the result approved and you want to send notification about it to the caller view model.
        /// </summary>
        event EventHandler ResultApproved;

        /// <summary>
        ///     Gets result.
        /// </summary>
        TResult Result { get; }
    }

    /// <summary>
    ///     Interfaces for all view models with accepted parameter.
    /// </summary>
    /// <typeparam name="TParameter">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public interface IWavesParameterizedViewModel<in TParameter>
        : IWavesViewModel
    {
        /// <summary>
        ///     Gets parameter.
        /// </summary>
        /// <param name="t">Parameter.</param>
        /// <returns>Returns prepare task.</returns>
        Task Prepare(
            TParameter t);
    }

    /// <summary>
    ///     Interfaces for all view models with parameter and return result.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public interface IWavesViewModel<in TParameter, out TResult>
        : IWavesParameterizedViewModel<TParameter>, IWavesViewModel<TResult>
    {
    }
}
