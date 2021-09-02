namespace Waves.UI.Presentation.Interfaces
{
    /// <summary>
    /// Interface for Waves content controls.
    /// </summary>
    public interface IWavesContentControl : IWavesView
    {
        /// <summary>
        /// Gets or sets opacity.
        /// </summary>
        double Opacity { get; set; }
    
        /// <summary>
        /// Gets or sets content.
        /// </summary>
        object Content { get; set; }
    }
}