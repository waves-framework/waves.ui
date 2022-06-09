using System;
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
    public abstract class WavesConfigurableViewModelBase :
        WavesObservableConfigurablePlugin,
        IWavesViewModel
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesConfigurableViewModelBase"/>.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="logger">Logger. </param>
        protected WavesConfigurableViewModelBase(
            IConfiguration configuration,
            ILogger<WavesConfigurableViewModelBase> logger)
            : base(configuration, logger)
        {
        }

        /// <inheritdoc />
        [Reactive]
        public IWavesViewModelLoadingState LoadingState { get; private set; }

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

        /// <inheritdoc />
        public override async Task InitializeAsync()
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
                await LoadConfigurationAsync();
                await RunInitializationAsync();
                IsInitialized = true;
                Logger.LogDebug($"View model {this} initialized");
            }
            catch (Exception e)
            {
                IsInitialized = false;
                LoadingState.IsLoading = false;
                LoadingState.IsIndeterminate = false;
                Logger.LogError(e, $"View model {this} initialization error");
            }
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
        protected override Task RunInitializationAsync()
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
    public abstract class WavesConfigurableViewModelBase<TResult> :
        WavesConfigurableViewModelBase,
        IWavesViewModel<TResult>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelBase"/>.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="logger">Logger.</param>
        protected WavesConfigurableViewModelBase(
            IConfiguration configuration,
            ILogger<WavesConfigurableViewModelBase<TResult>> logger)
            : base(configuration, logger)
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
    public abstract class WavesConfigurableParameterizedViewModelBase<TParameter> :
        WavesConfigurableViewModelBase,
        IWavesParameterizedViewModel<TParameter>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesConfigurableParameterizedViewModelBase{TParameter}"/>.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="logger">Logger.</param>
        protected WavesConfigurableParameterizedViewModelBase(
            IConfiguration configuration,
            ILogger<WavesConfigurableParameterizedViewModelBase<TParameter>> logger)
            : base(configuration, logger)
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
    public abstract class WavesConfigurableViewModelBase<TParameter, TResult> : WavesConfigurableViewModelBase<TResult>, IWavesViewModel<TParameter, TResult>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesConfigurableViewModelBase{TResult}"/>.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        /// <param name="logger">Logger.</param>
        protected WavesConfigurableViewModelBase(
            IConfiguration configuration,
            ILogger<WavesConfigurableViewModelBase<TParameter, TResult>> logger)
            : base(configuration, logger)
        {
        }

        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
