using Waves.UI.Dialogs.Enums;

namespace Waves.UI.Dialogs;

/// <summary>
/// Open file dialog result.
/// </summary>
public class WavesOpenFileDialogResult
{
    /// <summary>
    /// Gets or sets result.
    /// </summary>
    public WavesDialogResult Result { get; set; }

    /// <summary>
    /// Gets or sets file names.
    /// </summary>
    public List<string> FileNames { get; set; }
}
