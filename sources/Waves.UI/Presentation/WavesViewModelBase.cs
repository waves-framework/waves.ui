using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Presentation
{
    /// <summary>
    /// Base view model.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModelBase : WavesPlugin, IWavesViewModel
    {
        /// <inheritdoc />
        public virtual Task ViewAppeared()
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual Task ViewDisappeared()
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModelBase<TResult> : WavesViewModelBase, IWavesViewModel<TResult>
    {
        /// <inheritdoc />
        public TResult Result { get; protected set; }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesParameterizedViewModelBase<TParameter> : WavesViewModelBase, IWavesParameterizedViewModel<TParameter>
    {
        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModelBase<TParameter, TResult> : WavesViewModelBase<TResult>, IWavesViewModel<TParameter, TResult>
    {
        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
