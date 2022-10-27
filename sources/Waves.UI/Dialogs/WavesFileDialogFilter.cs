namespace Waves.UI.Dialogs;

/// <summary>
/// File dialog filter.
/// </summary>
public class WavesFileDialogFilter
{
    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets Extensions.
    /// </summary>
    public List<string> Extensions { get; set; } = new();
}
