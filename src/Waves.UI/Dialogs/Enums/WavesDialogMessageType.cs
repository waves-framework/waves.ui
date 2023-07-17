using System.ComponentModel;

namespace Waves.UI.Dialogs.Enums;

/// <summary>
///     Enum of messages structures types.
/// </summary>
public enum WavesDialogMessageType
{
    /// <summary>
    ///     Information.
    /// </summary>
    [Description("INFO")]
    Information = 0,

    /// <summary>
    ///     Warning.
    /// </summary>
    [Description("WARN")]
    Warning = 1,

    /// <summary>
    ///     Error.
    /// </summary>
    [Description("ERROR")]
    Error = 2,

    /// <summary>
    ///     Fatal error.
    /// </summary>
    [Description("FATAL")]
    Fatal = 3,

    /// <summary>
    ///     Success.
    /// </summary>
    [Description("OK")]
    Success = 4,

    /// <summary>
    ///     Debug.
    /// </summary>
    [Description("DEBUG")]
    Debug = 5,
}
