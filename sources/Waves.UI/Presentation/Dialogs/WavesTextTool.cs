using Waves.UI.Presentation.Dialogs.Interfaces;

namespace Waves.UI.Presentation.Dialogs
{
    /// <summary>
    /// Text action.
    /// </summary>
    public abstract class WavesTextTool : WavesToolBase, IWavesTextTool
    {
        /// <inheritdoc />
        public abstract string Text { get; }

        /// <inheritdoc />
        public abstract override string Caption { get; }

        /// <inheritdoc />
        public abstract override void Initialize();
    }
}
