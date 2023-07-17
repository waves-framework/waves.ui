namespace Waves.UI.Base.EventArgs;

/// <summary>
/// Event arguments for can go back navigation.
/// </summary>
public class GoBackNavigationEventArgs : System.EventArgs
{
    /// <summary>
    /// Creates new instance of <see cref="GoBackNavigationEventArgs"/>.
    /// </summary>
    /// <param name="canGoBack">Set whether we can navigate back.</param>
    /// <param name="contentControl">Sets content control.</param>
    public GoBackNavigationEventArgs(
        bool canGoBack,
        object contentControl)
    {
        CanGoBack = canGoBack;
        ContentControl = contentControl;
    }

    /// <summary>
    /// Gets whether navigation can go back.
    /// </summary>
    public bool CanGoBack { get; }

    /// <summary>
    /// Gets content control which can navigate back.
    /// </summary>
    public object ContentControl { get; }
}
