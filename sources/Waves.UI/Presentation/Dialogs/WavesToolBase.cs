using ReactiveUI;
using Waves.UI.Presentation.Dialogs.Interfaces;

namespace Waves.UI.Presentation.Dialogs
{
    /// <summary>
    /// Dialog action base.
    /// </summary>
    public abstract class WavesToolBase : ReactiveObject, IWavesTool
    {
        /// <inheritdoc />
        public abstract string Caption { get; }

        /// <inheritdoc />
        public abstract string ToolTip { get; }

        /// <inheritdoc />
        public abstract void Initialize();
    }
}
