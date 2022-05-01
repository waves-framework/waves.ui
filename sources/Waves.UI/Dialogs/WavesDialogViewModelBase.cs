using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Dialogs.Interfaces;
using Waves.UI.Presentation;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Dialogs
{
    /// <summary>
    /// View model base for dialogs.
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesDialogViewModelBase : WavesViewModelBase, IWavesDialogViewModel
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesDialogViewModelBase"/>.
        /// </summary>
        /// <param name="navigationService">Instance of <see cref="IWavesNavigationService"/>.</param>
        /// <param name="logger">Logger.</param>
        protected WavesDialogViewModelBase(
            IWavesNavigationService navigationService,
            ILogger<WavesDialogViewModelBase> logger)
            : base(logger)
        {
            NavigationService = navigationService;

            Tools = new ObservableCollection<IWavesDialogTool>();

            DoneCommand = ReactiveCommand.CreateFromTask(OnDone);
            CancelCommand = ReactiveCommand.CreateFromTask(OnCancel);

            IsDoneAvailable = true;
            IsCancelAvailable = true;
        }

        /// <inheritdoc />
        public event EventHandler Done;

        /// <inheritdoc />
        public event EventHandler Cancel;

        /// <inheritdoc />
        [Reactive]
        public bool IsDoneAvailable { get; protected set; }

        /// <inheritdoc />
        [Reactive]
        public bool IsCancelAvailable { get; protected set; }

        /// <inheritdoc />
        [Reactive]
        public ICommand DoneCommand { get; protected set; }

        /// <inheritdoc />
        [Reactive]
        public ICommand CancelCommand { get; protected set; }

        /// <inheritdoc />
        [Reactive]
        public ObservableCollection<IWavesDialogTool> Tools { get; private set; }

        /// <summary>
        /// Gets instance of <see cref="IWavesNavigationService"/>.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; private set; }

        /// <summary>
        /// Actions when "Done" button pressed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task OnDone()
        {
            OnDialogDone();
            return NavigationService.GoBackAsync(this);
        }

        /// <summary>
        /// Actions when "Cancel" button pressed.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public virtual Task OnCancel()
        {
            OnDialogCancel();
            return NavigationService.GoBackAsync(this);
        }

        /// <summary>
        /// Callback when all done.
        /// </summary>
        protected virtual void OnDialogDone()
        {
            Done?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Callback when cancelled.
        /// </summary>
        protected virtual void OnDialogCancel()
        {
            Cancel?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// View model base for dialogs with result.
    /// </summary>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesDialogViewModelBase<TResult> : WavesDialogViewModelBase, IWavesDialogViewModel<TResult>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesDialogViewModelBase{TResult}"/>.
        /// </summary>
        /// <param name="navigationService">Instance of <see cref="IWavesNavigationService"/>.</param>
        /// <param name="logger">Logger.</param>
        protected WavesDialogViewModelBase(
            IWavesNavigationService navigationService,
            ILogger<WavesDialogViewModelBase<TResult>> logger)
            : base(navigationService, logger)
        {
        }

        /// <summary>
        /// You should not use this event.
        /// Use <see cref="WavesDialogViewModelBase.Done"/> and <see cref="WavesDialogViewModelBase.Cancel"/> instead of this.
        /// </summary>
        public event EventHandler ResultApproved;

        /// <inheritdoc />
        public TResult Result { get; protected set; }

        /// <summary>
        /// Callback when result approved.
        /// </summary>
        protected virtual void OnResultApproved()
        {
            ResultApproved?.Invoke(this, EventArgs.Empty);
            throw new Exception("You should not use \"ResultApproved\" event. Use \"Done\" and \"Cancel\" event instead of this.");
        }
    }

    /// <summary>
    /// View model base for dialogs with parameter and result.
    /// </summary>
    /// <typeparam name="TParameter">Type of parameter.</typeparam>
    /// <typeparam name="TResult">Type of result.</typeparam>
#pragma warning disable SA1402 // File may only contain a single type
    public abstract class WavesDialogViewModelBase<TParameter, TResult> : WavesDialogViewModelBase<TResult>, IWavesDialogViewModel<TParameter, TResult>
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesDialogViewModelBase{TResult}"/>.
        /// </summary>
        /// <param name="navigationService">Instance of <see cref="IWavesNavigationService"/>.</param>
        /// <param name="logger">Logger.</param>
        protected WavesDialogViewModelBase(
            IWavesNavigationService navigationService,
            ILogger<WavesDialogViewModelBase<TParameter, TResult>> logger)
            : base(navigationService, logger)
        {
        }

        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
