using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Waves.UI.Presentation.Dialogs.Interfaces;

namespace Waves.UI.Presentation.Dialogs
{
    /// <summary>
    /// Action tool base.
    /// </summary>
    public abstract class WavesActionTool : WavesToolBase, IWavesActionTool
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesActionTool"/>.
        /// </summary>
        protected WavesActionTool()
        {
            Command = ReactiveCommand.CreateFromTask<object>(OnCommand);
        }

        /// <inheritdoc />
        public event EventHandler<object> Invoked;

        /// <inheritdoc />
        public abstract override string Caption { get; }

        /// <inheritdoc />
        public abstract override string ToolTip { get; }

        /// <inheritdoc />
        public ICommand Command { get; private set; }

        /// <summary>
        /// Gets command action.
        /// </summary>
        public abstract Task CommandTask { get; protected set; }

        /// <inheritdoc />
        public abstract override void Initialize();

        /// <summary>
        /// Actions when command invoked.
        /// </summary>
        /// <param name="obj">Parameter.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected abstract Task OnCommand(object obj);

        /// <summary>
        /// Callback for command invoking.
        /// </summary>
        /// <param name="result">Result.</param>
        protected virtual void OnInvoked(object result)
        {
            Invoked?.Invoke(this, result);
        }
    }
}
