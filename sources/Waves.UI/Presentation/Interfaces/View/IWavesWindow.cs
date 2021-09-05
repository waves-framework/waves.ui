namespace Waves.UI.Presentation.Interfaces.View
{
    /// <summary>
    /// Interface for Waves windows.
    /// </summary>
    public interface IWavesWindow<TContent> : IWavesContentControl<TContent>
    {
        /// <summary>
        /// Shows window.
        /// </summary>
        void Show();
    }
}