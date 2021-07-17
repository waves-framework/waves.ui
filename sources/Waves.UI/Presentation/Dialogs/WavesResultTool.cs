using System.Threading.Tasks;
using Waves.Core.Base.Extensions;
using Waves.UI.Presentation.Dialogs.Enums;

namespace Waves.UI.Presentation.Dialogs
{
    /// <summary>
    ///     Result tool.
    /// </summary>
    public class WavesResultTool : WavesActionTool
    {
        /// <summary>
        ///     Creates new instance of <see cref="WavesResultTool" />.
        /// </summary>
        /// <param name="result">Result.</param>
        public WavesResultTool(
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
