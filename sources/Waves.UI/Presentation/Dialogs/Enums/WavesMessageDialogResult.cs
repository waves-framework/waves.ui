using System.ComponentModel;

namespace Waves.UI.Presentation.Dialogs.Enums
{
    /// <summary>
    /// Waves message dialog results enum.
    /// </summary>
    public enum WavesMessageDialogResult
    {
        /// <summary>
        /// None.
        /// </summary>
        [Description("None")]
        None,

        /// <summary>
        /// Ok.
        /// </summary>
        [Description("OK")]
        Ok,

        /// <summary>
        /// Cancel.
        /// </summary>
        [Description("Cancel")]
        Cancel,

        /// <summary>
        /// Yes.
        /// </summary>
        [Description("Yes")]
        Yes,

        /// <summary>
        /// No.
        /// </summary>
        [Description("No")]
        No,

        /// <summary>
        /// Save.
        /// </summary>
        [Description("Save")]
        Save,

        /// <summary>
        /// Don't save.
        /// </summary>
        [Description("Don't save")]
        DontSave,

        /// <summary>
        /// Retry operation.
        /// </summary>
        [Description("Retry")]
        Retry,

        /// <summary>
        /// Abort operation.
        /// </summary>
        [Description("Abort")]
        Abort,

        /// <summary>
        /// Ignore.
        /// </summary>
        [Description("Ignore")]
        Ignore,
    }
}
