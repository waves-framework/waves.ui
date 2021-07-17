namespace Waves.UI.Drawing.Interfaces
{
    /// <summary>
    ///     Interface of text paint instances.
    /// </summary>
    public interface IWavesTextPaint : IWavesPaint
    {
        /// <summary>
        ///     Gets or sets text style.
        /// </summary>
        IWavesTextStyle TextStyle { get; set; }
    }
}
