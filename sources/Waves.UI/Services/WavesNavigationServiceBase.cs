using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Waves.Core;
using Waves.Core.Base;
using Waves.Core.Extensions;
using Waves.UI.Base.EventArgs;
using Waves.UI.Dialogs;
using Waves.UI.Dialogs.Interfaces;
using Waves.UI.Extensions;
using Waves.UI.Presentation.Interfaces.View;
using Waves.UI.Presentation.Interfaces.View.Controls;
using Waves.UI.Presentation.Interfaces.ViewModel;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Services;

/// <summary>
/// Navigation service abstraction.
/// </summary>
/// <typeparam name="TContent">Content control type.</typeparam>
public abstract class WavesNavigationServiceBase<TContent> :
    WavesConfigurablePlugin,
    IWavesNavigationService
{
    private readonly WavesCore _core;

    /// <summary>
    /// Creates new instance of <see cref="WavesNavigationServiceBase{TContent}"/>.
    /// </summary>
    /// <param name="core">Core.</param>
    /// <param name="configuration">Configuration.</param>
    /// <param name="logger">Logger.</param>
    protected WavesNavigationServiceBase(
        WavesCore core,
        IConfiguration configuration,
        ILogger<WavesNavigationServiceBase<TContent>> logger)
        : base(configuration, logger)
    {
        _core = core;
        Histories = new Dictionary<string, Stack<IWavesViewModel>>();
        DialogSessions = new List<IWavesDialogViewModel>();
        PendingActions = new Dictionary<string, Action>();
        OpenedWindows = new Dictionary<IWavesViewModel, IWavesWindow<TContent>>();
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
    protected Dictionary<string, Stack<IWavesViewModel>> Histories { get; }

    /// <summary>
    /// Gets dialog sessions.
    /// </summary>
    protected List<IWavesDialogViewModel> DialogSessions { get; }

    /// <summary>
    /// Gets pending actions.
    /// </summary>
    protected Dictionary<string, Action> PendingActions { get; }

    /// <summary>
    /// Gets opened windows.
    /// </summary>
    protected Dictionary<IWavesViewModel, IWavesWindow<TContent>> OpenedWindows { get; }

    /// <inheritdoc />
    public virtual async Task GoBackAsync(IWavesViewModel viewModel)
    {
        foreach (var history in Histories
                     .Select(pair => pair.Value)
                     .Where(history => Enumerable.Contains(history, viewModel)))
        {
            if (history.Count <= 1)
            {
                return;
            }

            var removingViewModel = history.Pop();
            if (removingViewModel is IWavesDialogViewModel removingDialogViewModel)
            {
                DialogSessions.Remove(removingDialogViewModel);
            }

            NotifyDialogEvents();

            await NavigateAsync(history.First(), false);

            return;
        }
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
    public async Task NavigateAsync(Type type, bool addToHistory = true)
    {
        var viewModel = await _core.GetInstanceAsync(type);
        await NavigateAsync((IWavesViewModel)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task NavigateAsync<TParameter>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _core.GetInstanceAsync(type);
        await NavigateAsync((IWavesParameterizedViewModel<TParameter>)viewModel, parameter, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult> NavigateAsync<TResult>(Type type, bool addToHistory = true)
    {
        var viewModel = await _core.GetInstanceAsync(type);
        return await NavigateAsync((IWavesViewModel<TResult>)viewModel, addToHistory);
    }

    /// <inheritdoc />
    public async Task<TResult> NavigateAsync<TParameter, TResult>(Type type, TParameter parameter, bool addToHistory = true)
    {
        var viewModel = await _core.GetInstanceAsync(type);
        return await NavigateAsync((IWavesViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
    }

    /// <param name="filter"></param>
    /// <inheritdoc />
    public abstract Task<WavesOpenFileDialogResult> ShowOpenFileDialogAsync(
        IEnumerable<WavesFileDialogFilter> filter = null);

    /// <inheritdoc />
    public abstract void RegisterContentControl(string region, object contentControl);

    /// <inheritdoc />
    public abstract void UnregisterContentControl(string region);

    /// <inheritdoc />
    public void InvokePendingActions(string region)
    {
        var actions = PendingActions.Where(x => x.Key.Equals(region)).Select(x => x.Value);
        foreach (var action in actions)
        {
            action.Invoke();
        }
    }

    /// <inheritdoc />
    public async Task NavigateAsync(IWavesViewModel viewModel, bool addToHistory = true)
    {
        try
        {
            var view = await _core.GetInstanceAsync<IWavesView>(viewModel.GetType());

            switch (view)
            {
                case IWavesWindow<TContent> window:
                    await InitializeWindowAsync(window, viewModel);
                    break;
                case IWavesUserControl<TContent> userControl:
                    await InitializeUserControlAsync(userControl, viewModel, addToHistory);
                    break;
                case IWavesPage<TContent> page:
                    await InitializePageAsync(page, viewModel, addToHistory);
                    break;
                case IWavesDialog<TContent> dialog:
                    await InitializeDialogAsync(dialog, (IWavesDialogViewModel)viewModel, addToHistory);
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
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
                case IWavesWindow<TContent> window:
                    await NavigateToWindowAsync(window, viewModel, parameter);
                    break;
                case IWavesUserControl<TContent> userControl:
                    await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                    break;
                case IWavesPage<TContent> page:
                    await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                    break;
                case IWavesDialog<TContent> dialog:
                    await NavigateToDialogAsync(dialog, viewModel, parameter, addToHistory);
                    break;
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
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
                case IWavesWindow<TContent> window:
                    return await NavigateToWindowAsync(window, viewModel);
                case IWavesUserControl<TContent> userControl:
                    return await NavigateToUserControlAsync(userControl, viewModel, addToHistory);
                case IWavesPage<TContent> page:
                    return await NavigateToPageAsync(page, viewModel, addToHistory);
                case IWavesDialog<TContent> dialog:
                    return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TResult>)viewModel, addToHistory);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
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
                case IWavesWindow<TContent> window:
                    return await NavigateToWindowAsync(window, viewModel, parameter);
                case IWavesUserControl<TContent> userControl:
                    return await NavigateToUserControlAsync(userControl, viewModel, parameter, addToHistory);
                case IWavesPage<TContent> page:
                    return await NavigateToPageAsync(page, viewModel, parameter, addToHistory);
                case IWavesDialog<TContent> dialog:
                    return await NavigateToDialogAsync(dialog, (IWavesDialogViewModel<TParameter, TResult>)viewModel, parameter, addToHistory);
            }
        }
        catch (Exception e)
        {
            Logger.LogError(e, "An error occured while navigating");
        }

        return default;
    }

    /// <summary>
    /// Navigates to windows.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializeWindowAsync(
        IWavesWindow<TContent> view,
        IWavesViewModel viewModel);

    /// <summary>
    /// Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializePageAsync(
        IWavesPage<TContent> view,
        IWavesViewModel viewModel,
        bool addToHistory = true);

    /// <summary>
    /// Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializeUserControlAsync(
        IWavesUserControl<TContent> view,
        IWavesViewModel viewModel,
        bool addToHistory = true);

    /// <summary>
    /// Navigates to dialog.
    /// </summary>
    /// <param name="view">Dialog view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected abstract Task InitializeDialogAsync(
        IWavesDialog<TContent> view,
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
    protected Task<string> InitializeComponents(IWavesView view, IWavesViewModel viewModel)
    {
        var attribute = view.GetViewAttribute();
        if (attribute == null)
        {
            throw new NullReferenceException("Current view didn't marked as \"WavesView\" attribute");
        }

        var region = attribute.Region;
        view.DataContext = viewModel;
        return Task.FromResult(region);
    }

    /// <summary>
    /// Callback when navigation "Go back" state changed.
    /// </summary>
    /// <param name="e">Arguments.</param>
    protected virtual void OnGoBackChanged(GoBackNavigationEventArgs e)
    {
        GoBackChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Callback when dialog is shown.
    /// </summary>
    protected virtual void OnDialogsShown()
    {
        DialogsShown?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Callback when dialog is hidden.
    /// </summary>
    protected virtual void OnDialogsHidden()
    {
        DialogsHidden?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Checks dialogs.
    /// </summary>
    protected void NotifyDialogEvents()
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

    /// <summary>
    /// Navigates to windows.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    private async Task<TResult> NavigateToWindowAsync<TResult>(
        IWavesWindow<TContent> view,
        IWavesViewModel<TResult> viewModel)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeWindowAsync(view, viewModel);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to window.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <param name="parameter">Parameter.</param>
    private async Task NavigateToWindowAsync<TParameter>(
        IWavesWindow<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter)
    {
        await viewModel.Prepare(parameter);
        await InitializeWindowAsync(view, viewModel);
    }

    /// <summary>
    ///     Navigates to window.
    /// </summary>
    /// <param name="view">Window view.</param>
    /// <param name="viewModel">ViewModel.</param>
    /// <param name="parameter">Parameter.</param>
    private async Task<TResult> NavigateToWindowAsync<TParameter, TResult>(
        IWavesWindow<TContent> view,
        IWavesViewModel<TParameter, TResult> viewModel,
        TParameter parameter)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializeWindowAsync(view, viewModel);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToPageAsync<TResult>(
        IWavesPage<TContent> view,
        IWavesViewModel<TResult> viewModel,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializePageAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task NavigateToPageAsync<TParameter>(
        IWavesPage<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        await InitializePageAsync(view, viewModel, addToHistory);
    }

    /// <summary>
    ///     Navigates to page.
    /// </summary>
    /// <param name="view">Page view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToPageAsync<TParameter, TResult>(
        IWavesPage<TContent> view,
        IWavesViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializePageAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToUserControlAsync<TResult>(
        IWavesUserControl<TContent> view,
        IWavesViewModel<TResult> viewModel,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeUserControlAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task NavigateToUserControlAsync<TParameter>(
        IWavesUserControl<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        await InitializeUserControlAsync(view, viewModel, addToHistory);
    }

    /// <summary>
    ///     Navigates to user control.
    /// </summary>
    /// <param name="view">User control view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToUserControlAsync<TParameter, TResult>(
        IWavesUserControl<TContent> view,
        IWavesViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await viewModel.Prepare(parameter);
        await InitializeUserControlAsync(view, viewModel, addToHistory);
        InitializeControlWithResult(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to dialog.
    /// </summary>
    /// <param name="view">Dialog view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToDialogAsync<TResult>(
        IWavesDialog<TContent> view,
        IWavesDialogViewModel<TResult> viewModel,
        bool addToHistory = true)
    {
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(_core);
        InitializeDialog(viewModel!, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    ///     Navigates to dialog.
    /// </summary>
    /// <param name="view">Dialog view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task NavigateToDialogAsync<TParameter>(
        IWavesDialog<TContent> view,
        IWavesParameterizedViewModel<TParameter> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        await InitializeDialogAsync(view, (IWavesDialogViewModel)viewModel, addToHistory);
    }

    /// <summary>
    ///     Navigates to dialog.
    /// </summary>
    /// <param name="view">Dialog view.</param>
    /// <param name="viewModel">View model.</param>
    /// <param name="parameter">Parameter.</param>
    /// <param name="addToHistory">Sets whether add navigation to history is needed.</param>
    private async Task<TResult> NavigateToDialogAsync<TParameter, TResult>(
        IWavesDialog<TContent> view,
        IWavesDialogViewModel<TParameter, TResult> viewModel,
        TParameter parameter,
        bool addToHistory = true)
    {
        await viewModel.Prepare(parameter);
        var completionSource = new TaskCompletionSource<TResult>();
        await InitializeDialogAsync(view, viewModel, addToHistory).LogExceptions(_core);
        InitializeDialog(viewModel, completionSource);
        return await completionSource.Task;
    }

    /// <summary>
    /// Initializes control with result.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="completionSource">Completion source.</param>
    private void InitializeControlWithResult<TResult>(
        IWavesViewModel<TResult> viewModel,
        TaskCompletionSource<TResult> completionSource)
    {
        void OnResultApproved(object sender, EventArgs e)
        {
            Unsubscribe();
            completionSource.SetResult(viewModel.Result);
        }

        void Unsubscribe()
        {
            viewModel.ResultApproved -= OnResultApproved;
        }

        viewModel.ResultApproved += OnResultApproved;
    }

    /// <summary>
    /// Initializes dialog with result.
    /// </summary>
    /// <typeparam name="TResult">Result type.</typeparam>
    /// <param name="viewModel">View model.</param>
    /// <param name="completionSource">Completion source.</param>
    private void InitializeDialog<TResult>(
        IWavesDialogViewModel<TResult> viewModel,
        TaskCompletionSource<TResult> completionSource)
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
}
