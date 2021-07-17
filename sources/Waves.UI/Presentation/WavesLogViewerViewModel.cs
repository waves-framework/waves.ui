using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base.Interfaces;
using Waves.Core.Extensions;
using Waves.UI.Presentation.Attributes;

namespace Waves.UI.Presentation
{
    /// <summary>
    /// Log viewer view model.
    /// </summary>
    [WavesViewModel(typeof(WavesLogViewerViewModel), true)]
    public class WavesLogViewerViewModel : WavesViewModelBase
    {
        private readonly IWavesCore _core;

        /// <summary>
        /// Creates new instance of <see cref="WavesLogViewerViewModel"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        public WavesLogViewerViewModel(IWavesCore core)
        {
            _core = core;
        }

        /// <summary>
        /// Gets collection of message objects.
        /// </summary>
        [Reactive]
        public ObservableCollection<IWavesMessageObject> Messages { get; private set; }

        /// <summary>
        /// Gets "Clear" command.
        /// </summary>
        public ICommand ClearCommand { get; private set; }

        /// <inheritdoc />
        public override async Task InitializeAsync()
        {
            if (IsInitialized)
            {
                return;
            }

            Messages = new ObservableCollection<IWavesMessageObject>();

            await InitializeCommands();
            await SubscribeEvents();

            IsInitialized = true;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnsubscribeEvents().FireAndForget();
            }
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task InitializeCommands()
        {
            ClearCommand = ReactiveCommand.CreateFromTask(OnClear);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Subscribe to events.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task SubscribeEvents()
        {
            _core.MessageReceived += OnCoreMessageReceived;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Unsubscribe from events.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task UnsubscribeEvents()
        {
            _core.MessageReceived -= OnCoreMessageReceived;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Clear list.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        private Task OnClear()
        {
            Messages.Clear();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Actions when message from <see cref="IWavesCore"/> received.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnCoreMessageReceived(object sender, IWavesMessageObject e)
        {
            Messages.Add(e);
        }
    }
}
