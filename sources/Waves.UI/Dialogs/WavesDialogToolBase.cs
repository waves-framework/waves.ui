using ReactiveUI;
using Waves.UI.Dialogs.Interfaces;

namespace Waves.UI.Dialogs
{
    /// <summary>
    /// Dialog action base.
    /// </summary>
    public abstract class WavesDialogToolBase : ReactiveObject, IWavesDialogTool
    {
        /// <inheritdoc />
        public abstract string Caption { get; }

        /// <inheritdoc />
        public abstract string ToolTip { get; }

        /// <inheritdoc />
        public abstract void Initialize();
    }
}
