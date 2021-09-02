using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waves.Core.Base;
using Waves.Core.Base.Interfaces;
using Waves.Core.Extensions;
using Waves.Core.Plugins.Services.EventArgs;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Extensions;
using Waves.UI.Presentation.Interfaces;

namespace Waves.UI.Plugins.Services
{
    /// <summary>
    /// Navigation service base.
    /// </summary>
    public abstract class WavesNavigationServiceBase 
        : WavesService, IWavesNavigationService
    {
        private readonly IWavesCore _core;

        /// <summary>
        /// Creates new instance of <see cref="WavesNavigationServiceBase"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        protected WavesNavigationServiceBase(IWavesCore core)
        {
            _core = core;
        }
        
        /// <inheritdoc />
        public event EventHandler<GoBackNavigationEventArgs> GoBackChanged;

        /// <inheritdoc />
        public event EventHandler DialogsShown;

        /// <inheritdoc />
        public event EventHandler DialogsHidden;

        /// <summary>
        /// Gets dictionary of view models keyed by region.
        /// </summary>
        private Dictionary<string, Stack<IWavesViewModel>> Histories { get; set; }
        
        /// <summary>
        /// Gets dialog sessions.
        /// </summary>
        protected List<IWavesDialogViewModel> DialogSessions { get; private set; }
        
        /// <summary>
        /// Gets pending actions.
        /// </summary>
        protected Dictionary<string, Action> PendingActions { get; private set;}
        
        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            Histories = new Dictionary<string, Stack<IWavesViewModel>>();

            DialogSessions = new List<IWavesDialogViewModel>();
            PendingActions = new Dictionary<string, Action>();

            return Task.CompletedTask;
        }
        
        /// <inheritdoc />
        public async Task GoBackAsync(IWavesViewModel viewModel)
        {
            foreach (var pair in Histories)
            {
                var history = pair.Value;

                if (!Enumerable.Contains(history, viewModel))
                {
                    continue;
                }

                if (history.Count <= 1)
                {
                    return;
                }

                var removingViewModel = history.Pop();
                if (removingViewModel is IWavesDialogViewModel removingDialogViewModel)
                {
                    DialogSessions.Remove(removingDialogViewModel);
                }

                CheckDialogs();

                await NavigateAsync(history.First(), false);

                return;
            }
        }
        
        /// <inheritdoc />
        public async Task GoBackAsync(
            string region)
        {
            var history = Histories[region];

            if (history.Count <= 1)
            {
                return;
            }

            var removingViewModel = history.Pop();
            if (removingViewModel is IWavesDialogViewModel removingDialogViewModel)
            {
                DialogSessions.Remove(removingDialogViewModel);
            }

            CheckDialogs();

            await NavigateAsync(history.First(), false);
        }
        
        /// <inheritdoc />
        public async Task NavigateAsync<T>(bool addToHistory = true)
            where T : class
        {
            var viewModel = await _core.GetInstanceAsync<T>();
            await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
        }

        /// <inheritdoc />
        public async Task NavigateAsync<T, TParameter>(
            TParameter parameter,
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await _core.GetInstanceAsync<T>();
            await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<T, TResult>(
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await _core.GetInstanceAsync<T>();
            return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<T, TParameter, TResult>(
            TParameter parameter,
            bool addToHistory = true)
            where T : class
        {
            var viewModel = await _core.GetInstanceAsync<T>();
            return await NavigateAsync((IWavesViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
        }

        /// <inheritdoc />
        public abstract void RegisterContentControl(string region, object contentControl);

        /// <inheritdoc />
        public abstract void UnregisterContentControl(string region);

        /// <inheritdoc />
        public async Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true)
        {
            try
            {
                var view = await _core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case IWavesWindow window:
                        await InitializeWindowAsync(window, viewModel);
                        break;
                    case IWavesUserControl userControl:
                        await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                        break;
                    case IWavesPage page:
                        await InitializePageAsync(page, viewModel, addToHistory);
                        break;
                    case IWavesDialog dialog:
                        await InitializeDialogAsync(dialog, (IWavesDialogViewModel)viewModel, addToHistory);
                        break;
                }
            }
            catch (Exception e)
            {
                await _core.WriteLogAsync(e, this);
            }
        }

        /// <inheritdoc />
        public async Task NavigateAsync<TParameter>(
            IWavesParameterizedViewModel<TParameter> viewModel,
            TParameter parameter,
            bool addToHistory = true)
        {
            try
            {
                var view = await _core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case IWavesWindow window:
                        await NavigateToWindowAsync(window, viewModel, parameter);
                        break;
                    case IWavesUserControl userControl:
                        await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                        break;
                    case IWavesPage page:
                        await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                        break;
                    case IWavesDialog dialog:
                        await NavigateToDialogAsync(dialog, viewModel, parameter, addToHistory);
                        break;
                }
            }
            catch (Exception e)
            {
                await _core.WriteLogAsync(e, this);
            }
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<TResult>(
            IWavesViewModel<TResult> viewModel,
            bool addToHistory = true)
        {
            try
            {
                var view = await _core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case IWavesWindow window:
                        return await NavigateToWindowAsync(window, viewModel);
                    case IWavesUserControl userControl:
                        return await NavigateToUserControlAsync(userControl, viewModel, addToHistory);
                    case IWavesPage page:
                        return await NavigateToPageAsync(page, viewModel, addToHistory);
                    case IWavesDialog dialog:
                        return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TResult>)viewModel, addToHistory);
                }
            }
            catch (Exception e)
            {
                await _core.WriteLogAsync(e, this);
            }

            return default;
        }

        /// <inheritdoc />
        public async Task<TResult> NavigateAsync<TParameter, TResult>(
            IWavesViewModel<TParameter, TResult> viewModel,
            TParameter parameter,
            bool addToHistory = true)
        {
            try
            {
                var view = await _core.GetInstanceAsync<IWavesView>(viewModel.GetType());

                switch (view)
                {
                    case IWavesWindow window:
                        return await NavigateToWindowAsync(window, viewModel, parameter);
                    case IWavesUserControl userControl:
                        return await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                    case IWavesPage page:
                        return await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                    case IWavesDialog dialog:
                        return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
                }
            }
            catch (Exception e)
            {
                await _core.WriteLogAsync(e, this);
            }

            return default;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            
            Histories.Clear();
        }

        /// <summary>
        /// Callback that notifies that go back changed.
        /// </summary>
        /// <param name="e">Arguments.</param>
        protected virtual void OnGoBackChanged(
            GoBackNavigationEventArgs e)
        {
            GoBackChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Notifies when dialogs shown.
        /// </summary>
        protected virtual void OnDialogsShown()
        {
            DialogsShown?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Notifies when all dialogs hidden.
        /// </summary>
        protected virtual void OnDialogsHidden()
        {
            DialogsHidden?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        protected abstract Task InitializeWindowAsync(IWavesWindow view, IWavesViewModel viewModel);

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected abstract Task InitializePageAsync(
            IWavesPage view,
            IWavesViewModel viewModel,
            bool addToHistory = true);

        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected abstract Task InitializeUserControlAsync(
            IWavesUserControl view,
            IWavesViewModel viewModel,
            bool addToHistory = true);

        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected abstract Task InitializeDialogAsync(
            IWavesDialog view,
            IWavesDialogViewModel viewModel,
            bool addToHistory = true);
        
        /// <summary>
        /// Adds viewModel to history stack or just create new history stack by region.
        /// </summary>
        /// <param name="region">Region.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        protected void AddToHistoryStack(string region, IWavesViewModel viewModel, bool addToHistory = true)
        {
            if (!Histories.ContainsKey(region))
            {
                Histories.Add(region, new Stack<IWavesViewModel>());
            }

            if (addToHistory)
            {
                Histories[region].Push(viewModel);
            }
        }

        /// <summary>
        /// Initializes View and ViewModel and return it's region.
        /// </summary>
        /// <param name="view">View.</param>
        /// <param name="viewModel">View model.</param>
        /// <returns>Returns region.</returns>
        protected async Task<string> InitializeComponents(IWavesView view, IWavesViewModel viewModel)
        {
            var attribute = view.GetViewAttribute();
            var region = attribute.Region;

            if (!viewModel.IsInitialized)
            {
                await viewModel.InitializeAsync();
            }
            
            view.DataContext = viewModel;
            return region;
        }

        /// <summary>
        /// Navigates to windows.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        private async Task<TResult> NavigateToWindowAsync<TResult>(IWavesWindow view, IWavesViewModel<TResult> viewModel)
        {
            await InitializeWindowAsync(view, viewModel);
            return viewModel.Result;
        }

        /// <summary>
        /// Navigates to window.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        /// <param name="parameter">Parameter.</param>
        private async Task NavigateToWindowAsync<TParameter>(IWavesWindow view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter)
        {
            await viewModel.Prepare(parameter);
            await InitializeWindowAsync(view, viewModel);
        }

        /// <summary>
        /// Navigates to window.
        /// </summary>
        /// <param name="view">Window view.</param>
        /// <param name="viewModel">ViewModel.</param>
        /// <param name="parameter">Parameter.</param>
        private async Task<TResult> NavigateToWindowAsync<TParameter, TResult>(IWavesWindow view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter)
        {
            await viewModel.Prepare(parameter);
            await InitializeWindowAsync(view, viewModel);
            return viewModel.Result;
        }

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToPageAsync<TResult>(IWavesPage view, IWavesViewModel<TResult> viewModel, bool addToHistory = true)
        {
            await InitializePageAsync(view, viewModel, addToHistory);
            return viewModel.Result;
        }

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task NavigateToPageAsync<TParameter>(IWavesPage view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            await InitializePageAsync(view, viewModel, addToHistory);
        }

        /// <summary>
        /// Navigates to page.
        /// </summary>
        /// <param name="view">Page view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToPageAsync<TParameter, TResult>(IWavesPage view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            await InitializePageAsync(view, viewModel, addToHistory);
            return viewModel.Result;
        }

        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToUserControlAsync<TResult>(IWavesUserControl view, IWavesViewModel<TResult> viewModel, bool addToHistory = true)
        {
            await InitializeUserControlAsync(view, viewModel, addToHistory);
            return viewModel.Result;
        }
        
        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task NavigateToUserControlAsync<TParameter>(IWavesUserControl view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            await InitializeUserControlAsync(view, viewModel, addToHistory);
        }
        
        /// <summary>
        /// Navigates to user control.
        /// </summary>
        /// <param name="view">User control view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToUserControlAsync<TParameter, TResult>(IWavesUserControl view, IWavesViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            await InitializeUserControlAsync(view, viewModel, addToHistory);
            return viewModel.Result;
        }
        
        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToDialogAsync<TResult>(IWavesDialog view, IWavesDialogViewModel<TResult> viewModel, bool addToHistory = true)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(_core);
            InitializeDialog(viewModel, completionSource);
            return await completionSource.Task;
        }
        
        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task NavigateToDialogAsync<TParameter>(IWavesDialog view, IWavesParameterizedViewModel<TParameter> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            await InitializeDialogAsync(view, (IWavesDialogViewModel)viewModel, addToHistory);
        }
        
        /// <summary>
        /// Navigates to dialog.
        /// </summary>
        /// <param name="view">Dialog view.</param>
        /// <param name="viewModel">View model.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
        private async Task<TResult> NavigateToDialogAsync<TParameter, TResult>(IWavesDialog view, IWavesDialogViewModel<TParameter, TResult> viewModel, TParameter parameter, bool addToHistory = true)
        {
            await viewModel.Prepare(parameter);
            var completionSource = new TaskCompletionSource<TResult>();
            await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(_core);
            InitializeDialog(viewModel, completionSource);
            return await completionSource.Task;
        }

        /// <summary>
        /// Initializes dialog with result.
        /// </summary>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <param name="viewModel">View model.</param>
        /// <param name="completionSource">Completion source.</param>
        private void InitializeDialog<TResult>(IWavesDialogViewModel<TResult> viewModel, TaskCompletionSource<TResult> completionSource)
        {
            void OnDone(object sender, EventArgs e)
            {
                Unsubscribe();
                completionSource.SetResult(viewModel.Result);
            }

            void OnCancel(object sender, EventArgs e)
            {
                Unsubscribe();
                completionSource.SetResult(viewModel.Result);
            }

            void Unsubscribe()
            {
                viewModel.Done -= OnDone;
                viewModel.Cancel -= OnCancel;
            }

            viewModel.Done += OnDone;
            viewModel.Cancel += OnCancel;
        }

        /// <summary>
        /// Checks dialogs.
        /// </summary>
        private void CheckDialogs()
        {
            if (DialogSessions.Count > 0)
            {
                OnDialogsShown();
            }
            else
            {
                OnDialogsHidden();
            }
        }
    }
}