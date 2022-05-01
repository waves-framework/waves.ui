using Waves.UI.Dialogs.Interfaces;

namespace Waves.UI.Dialogs
{
    /// <summary>
    /// Text action.
    /// </summary>
    public abstract class WavesDialogTextTool :
        WavesDialogToolBase,
        IWavesDialogTextTool
    {
        /// <inheritdoc />
        public abstract string Text { get; }

        /// <inheritdoc />
        public abstract override string Caption { get; }

        /// <inheritdoc />
        public abstract override void Initialize();
    }
}
