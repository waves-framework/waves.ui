using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Core.Base;
using Waves.UI.Presentation.Interfaces.ViewModel;

namespace Waves.UI.Presentation
{
    /// <summary>
    /// Base view model.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModelBase :
        WavesObservablePlugin,
        IWavesViewModel
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelBase"/>.
        /// </summary>
        protected WavesViewModelBase()
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelBase"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        protected WavesViewModelBase(ILogger<WavesViewModelBase> logger)
        {
            Logger = logger;
        }

        /// <inheritdoc />
        public bool IsInitialized { get; internal set; }

        /// <summary>
        /// Gets logger.
        /// </summary>
        protected ILogger<WavesViewModelBase> Logger { get; }

        /// <inheritdoc />
        public virtual async Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return;
            }

            try
            {
                await RunInitializationAsync();
                IsInitialized = true;
                Logger.LogDebug($"View model {this} initialized");
            }
            catch (Exception e)
            {
                IsInitialized = false;
                Logger?.LogError(e, "Object initialization error");
            }
        }

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

        /// <summary>
        /// Does initialization work.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task RunInitializationAsync()
        {
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesViewModelBase<TResult> :
        WavesViewModelBase,
        IWavesViewModel<TResult>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelBase"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        protected WavesViewModelBase(
            ILogger<WavesViewModelBase> logger)
            : base(logger)
        {
        }

        /// <inheritdoc />
        public event EventHandler ResultApproved;

        /// <inheritdoc />
        public TResult Result { get; protected set; }

        /// <summary>
        /// Callback when result approved.
        /// </summary>
        protected virtual void OnResultApproved()
        {
            ResultApproved?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Base view model.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesParameterizedViewModelBase<TParameter> : WavesViewModelBase, IWavesParameterizedViewModel<TParameter>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesParameterizedViewModelBase{TParameter}"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        protected WavesParameterizedViewModelBase(
            ILogger<WavesViewModelBase> logger)
            : base(logger)
        {
        }

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
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelBase{TResult}"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        protected WavesViewModelBase(
            ILogger<WavesViewModelBase> logger)
            : base(logger)
        {
        }

        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
