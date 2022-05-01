using Waves.Core.Extensions;
using Waves.UI.Dialogs.Enums;

namespace Waves.UI.Dialogs
{
    /// <summary>
    ///     Result tool.
    /// </summary>
    public class WavesDialogResultTool : WavesDialogActionTool
    {
        /// <summary>
        ///     Creates new instance of <see cref="WavesDialogResultTool" />.
        /// </summary>
        /// <param name="result">Result.</param>
        public WavesDialogResultTool(
            WavesMessageDialogResult result)
        {
            Result = result;
        }

        /// <summary>
        ///     Gets result.
        /// </summary>
        public WavesMessageDialogResult Result { get; }

        /// <inheritdoc />
        public override string Caption => Result.ToDescription();

        /// <inheritdoc />
        public override string ToolTip => Result.ToDescription();

        /// <inheritdoc />
        public override Task CommandTask { get; protected set; }

        /// <inheritdoc />
        public override void Initialize()
        {
        }

        /// <inheritdoc />
        protected override Task OnCommand(
            object obj)
        {
            OnInvoked(Result);
            return Task.CompletedTask;
        }
    }
}
