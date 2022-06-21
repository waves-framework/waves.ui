using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
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

        /// <inheritdoc />
        [Reactive]
        public IWavesViewModelLoadingState LoadingState { get; private set; }

        /// <summary>
        /// Gets logger.
        /// </summary>
        protected ILogger<WavesViewModelBase> Logger { get; }

        /// <inheritdoc />
        public virtual async Task InitializeAsync()
        {
            LoadingState = new WavesViewModelLoadingState();

            if (IsInitialized)
            {
                return;
            }

            LoadingState.IsLoading = true;
            LoadingState.IsIndeterminate = true;

            try
            {
                await RunInitializationAsync();
                IsInitialized = true;
                Logger.LogDebug($"View model {this} initialized");
            }
            catch (Exception e)
            {
                IsInitialized = false;
                LoadingState.IsLoading = false;
                LoadingState.IsIndeterminate = false;
                Logger?.LogError(e, $"View model {this} initialization error");
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
        public virtual Task RunPostInitializationAsync()
        {
            LoadingState.IsLoading = false;
            LoadingState.IsIndeterminate = false;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Does initialization work.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected virtual Task RunInitializationAsync()
        {
            LoadingState.IsLoading = false;
            LoadingState.IsIndeterminate = false;
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
