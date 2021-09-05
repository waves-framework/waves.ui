namespace Waves.UI.Presentation.Interfaces.View
{
    /// <summary>
    /// Interface for Waves content controls.
    /// </summary>
    public interface IWavesContentControl<TContent> : IWavesView
    {
        /// <summary>
        /// Gets or sets opacity.
        /// </summary>
        double Opacity { get; set; }
    
        /// <summary>
        /// Gets or sets content.
        /// </summary>
        TContent Content { get; set; }
    }
}