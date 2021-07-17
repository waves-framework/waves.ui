namespace Waves.UI.Presentation.Dialogs.Interfaces
{
    /// <summary>
    /// Interface for additional tools used in applications.
    /// It can be tools for lists, dialogs, etc.
    /// </summary>
    public interface IWavesTool
    {
        /// <summary>
        /// Gets caption.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets tooltip.
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Initializes tool.
        /// </summary>
        void Initialize();
    }
}
