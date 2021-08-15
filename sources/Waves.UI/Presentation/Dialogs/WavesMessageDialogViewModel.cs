using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base.Extensions;
using Waves.Core.Base.Interfaces;
using Waves.UI.Plugins.Services.Interfaces;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Presentation.Dialogs.Enums;
using Waves.UI.Presentation.Dialogs.Parameters;

namespace Waves.UI.Presentation.Dialogs
{
    /// <summary>
    /// Waves message dialog view model.
    /// </summary>
    [WavesViewModel(typeof(WavesMessageDialogViewModel))]
    public class WavesMessageDialogViewModel : WavesDialogViewModelBase<WavesMessageDialogParameter, WavesMessageDialogResult>
    {
        private List<WavesResultTool> _subscribedWavesResultTools;
        private WavesMessageDialogResult _doneResult;
        private WavesMessageDialogResult _cancelResult;

        /// <summary>
        /// Creates new instance of <see cref="WavesMessageDialogViewModel"/>.
        /// </summary>
        /// <param name="navigationService">Instance of navigation service.</param>
        public WavesMessageDialogViewModel(IWavesNavigationService navigationService)
            : base(navigationService)
        {
        }

        /// <summary>
        /// Gets text for Done.
        /// </summary>
        [Reactive]
        public string DoneText { get; private set; }

        /// <summary>
        /// Gets text for Cancel.
        /// </summary>
        [Reactive]
        public string CancelText { get; private set; }

        /// <summary>
        /// Gets message title.
        /// </summary>
        public IWavesMessageObject Message { get; private set; }

        /// <inheritdoc />
        public override Task InitializeAsync()
        {
            return base.InitializeAsync();
        }

        /// <inheritdoc />
        public override async Task Prepare(WavesMessageDialogParameter parameter)
        {
            await InitializeButtons(parameter.Buttons);
            Message = parameter.Message;
        }

        /// <inheritdoc />
        protected override void OnDialogDone()
        {
            Result = _doneResult;

            base.OnDialogDone();
        }

        /// <inheritdoc />
        protected override void OnDialogCancel()
        {
            Result = _cancelResult;

            base.OnDialogCancel();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnsubscribeFromTools();
            }

            // TODO: your code for release unmanaged resources.
        }

        /// <summary>
        /// Initializes buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeButtons(WavesMessageDialogButtons buttons)
        {
            _subscribedWavesResultTools = new List<WavesResultTool>();

            return buttons switch
            {
                WavesMessageDialogButtons.Ok => InitializeOkButtons(),
                WavesMessageDialogButtons.OkCancel => InitializeOkCancelButtons(),
                WavesMessageDialogButtons.RetryCancel => InitializeRetryCancelButtons(),
                WavesMessageDialogButtons.AbortRetryIgnore => InitializeAbortRetryIgnoreButtons(),
                WavesMessageDialogButtons.YesNo => InitializeYesNoButtons(),
                WavesMessageDialogButtons.YesNoCancel => InitializeYesNoCancelIgnoreButtons(),
                WavesMessageDialogButtons.SaveDontSave => InitializeSaveDontSaveButtons(),
                WavesMessageDialogButtons.SaveDontSaveCancel => InitializeSaveDontSaveCancelButtons(),
                _ => throw new ArgumentOutOfRangeException(nameof(buttons), buttons, null),
            };
        }

        /// <summary>
        /// Initializes "Ok" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeOkButtons()
        {
            IsCancelAvailable = false;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Ok;

            DoneText = _doneResult.ToDescription();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Ok / Cancel" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeOkCancelButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Ok;
            _cancelResult = WavesMessageDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Retry / Cancel" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeRetryCancelButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Retry;
            _cancelResult = WavesMessageDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Abort / Retry / Ignore" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeAbortRetryIgnoreButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Retry;
            _cancelResult = WavesMessageDialogResult.Abort;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesResultTool(WavesMessageDialogResult.Ignore);
            _subscribedWavesResultTools.Add(tool);

            Tools.Add(tool);

            SubscribeToTools();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Yes / No" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeYesNoButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Yes;
            _cancelResult = WavesMessageDialogResult.No;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Yes / No / Cancel" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeYesNoCancelIgnoreButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Yes;
            _cancelResult = WavesMessageDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesResultTool(WavesMessageDialogResult.No);
            _subscribedWavesResultTools.Add(tool);

            Tools.Add(tool);

            SubscribeToTools();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Save / Don't save" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeSaveDontSaveButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Save;
            _cancelResult = WavesMessageDialogResult.DontSave;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Initializes "Yes / No / Cancel" buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeSaveDontSaveCancelButtons()
        {
            IsCancelAvailable = true;
            IsDoneAvailable = true;

            _doneResult = WavesMessageDialogResult.Save;
            _cancelResult = WavesMessageDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesResultTool(WavesMessageDialogResult.DontSave);
            _subscribedWavesResultTools.Add(tool);

            Tools.Add(tool);

            SubscribeToTools();

            return Task.CompletedTask;
        }

        private void SubscribeToTools()
        {
            foreach (var tool in _subscribedWavesResultTools)
            {
                tool.Invoked += OnToolInvoked;
            }
        }

        private void UnsubscribeFromTools()
        {
            foreach (var tool in _subscribedWavesResultTools)
            {
                tool.Invoked -= OnToolInvoked;
            }
        }

        /// <summary>
        /// Callback when tool's command invoked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private async void OnToolInvoked(
            object sender,
            object e)
        {
            if (e is not WavesMessageDialogResult result)
            {
                return;
            }

            Result = result;

            base.OnDialogCancel();

            await NavigationService.GoBackAsync(this);
        }
    }
}
