namespace Waves.UI.Presentation.Interfaces.View.Controls
{
    /// <summary>
    /// Interface for content controls.
    /// </summary>
    /// <typeparam name="TContent">Content control type.</typeparam>
    public interface IWavesContentControl<TContent> :
        IWavesView
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
