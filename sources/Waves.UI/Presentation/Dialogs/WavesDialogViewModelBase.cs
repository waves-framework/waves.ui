using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Dialogs.Interfaces;
using Waves.UI.Presentation.Interfaces;
using Waves.UI.Presentation.Interfaces.View;
using Waves.UI.Presentation.Interfaces.ViewModel;

namespace Waves.UI.Presentation.Dialogs
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
        protected WavesDialogViewModelBase(IWavesNavigationService navigationService)
        {
            NavigationService = navigationService;

            Tools = new ObservableCollection<IWavesTool>();

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
        public ObservableCollection<IWavesTool> Tools { get; private set; }

        /// <summary>
        /// Gets instance of <see cref="IWavesNavigationService"/>.
        /// </summary>
        protected IWavesNavigationService NavigationService { get; private set; }

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return Task.CompletedTask;
            }

            IsInitialized = true;

            return Task.CompletedTask;
        }

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
        protected WavesDialogViewModelBase(IWavesNavigationService navigationService)
            : base(navigationService)
        {
        }

        /// <inheritdoc />
        public TResult Result { get; protected set; }
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
        protected WavesDialogViewModelBase(IWavesNavigationService navigationService)
            : base(navigationService)
        {
        }

        /// <inheritdoc />
        public abstract Task Prepare(TParameter t);
    }
}
