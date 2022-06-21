using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Extensions;
using Waves.UI.Base.Attributes;
using Waves.UI.Dialogs.Enums;
using Waves.UI.Dialogs.Parameters;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Dialogs
{
    /// <summary>
    /// Waves message dialog view model.
    /// </summary>
    [WavesViewModel(typeof(WavesMessageDialogViewModel))]
    public class WavesMessageDialogViewModel : WavesDialogViewModelBase<WavesMessageDialogParameter, WavesDialogResult>
    {
        private List<WavesDialogResultTool> _subscribedWavesResultTools;
        private WavesDialogResult _doneResult;
        private WavesDialogResult _cancelResult;

        /// <summary>
        /// Creates new instance of <see cref="WavesMessageDialogViewModel"/>.
        /// </summary>
        /// <param name="navigationService">Instance of navigation service.</param>
        /// <param name="logger">Logger.</param>
        public WavesMessageDialogViewModel(
            IWavesNavigationService navigationService,
            ILogger<WavesMessageDialogViewModel> logger)
            : base(navigationService, logger)
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
        /// Gets title.
        /// </summary>
        [Reactive]
        public string Title { get; private set; }

        /// <summary>
        /// Gets text.
        /// </summary>
        [Reactive]
        public string Text { get; private set; }

        /// <summary>
        /// Gets sender.
        /// </summary>
        [Reactive]
        public string Sender { get; private set; }

        /// <summary>
        /// Gets exception.
        /// </summary>
        [Reactive]
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets message type.
        /// </summary>
        [Reactive]
        public WavesDialogMessageType MessageType { get; private set; }

        /// <inheritdoc />
        public override async Task Prepare(WavesMessageDialogParameter parameter)
        {
            await InitializeButtons(parameter.Buttons);
            Title = parameter.Title;
            Text = parameter.Text;
            Sender = parameter.Sender;
            Exception = parameter.Exception;
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

        /// <summary>
        /// Initializes buttons.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeButtons(WavesDialogButtons buttons)
        {
            _subscribedWavesResultTools = new List<WavesDialogResultTool>();

            return buttons switch
            {
                WavesDialogButtons.Ok => InitializeOkButtons(),
                WavesDialogButtons.OkCancel => InitializeOkCancelButtons(),
                WavesDialogButtons.RetryCancel => InitializeRetryCancelButtons(),
                WavesDialogButtons.AbortRetryIgnore => InitializeAbortRetryIgnoreButtons(),
                WavesDialogButtons.YesNo => InitializeYesNoButtons(),
                WavesDialogButtons.YesNoCancel => InitializeYesNoCancelIgnoreButtons(),
                WavesDialogButtons.SaveDontSave => InitializeSaveDontSaveButtons(),
                WavesDialogButtons.SaveDontSaveCancel => InitializeSaveDontSaveCancelButtons(),
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

            _doneResult = WavesDialogResult.Ok;

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

            _doneResult = WavesDialogResult.Ok;
            _cancelResult = WavesDialogResult.Cancel;

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

            _doneResult = WavesDialogResult.Retry;
            _cancelResult = WavesDialogResult.Cancel;

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

            _doneResult = WavesDialogResult.Retry;
            _cancelResult = WavesDialogResult.Abort;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesDialogResultTool(WavesDialogResult.Ignore);
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

            _doneResult = WavesDialogResult.Yes;
            _cancelResult = WavesDialogResult.No;

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

            _doneResult = WavesDialogResult.Yes;
            _cancelResult = WavesDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesDialogResultTool(WavesDialogResult.No);
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

            _doneResult = WavesDialogResult.Save;
            _cancelResult = WavesDialogResult.DontSave;

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

            _doneResult = WavesDialogResult.Save;
            _cancelResult = WavesDialogResult.Cancel;

            DoneText = _doneResult.ToDescription();
            CancelText = _cancelResult.ToDescription();

            var tool = new WavesDialogResultTool(WavesDialogResult.DontSave);
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
            if (e is not WavesDialogResult result)
            {
                return;
            }

            Result = result;

            base.OnDialogCancel();

            await NavigationService.GoBackAsync(this);
        }
    }
}
