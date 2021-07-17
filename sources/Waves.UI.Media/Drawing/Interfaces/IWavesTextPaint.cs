namespace MTL.UI.Media.Drawing.Interfaces
{
    /// <summary>
    ///     Interface of text paint instances.
    /// </summary>
    public interface IMtlTextPaint : IMtlPaint
    {
        /// <summary>
        ///     Gets or sets text style.
        /// </summary>
        IMtlTextStyle TextStyle { get; set; }
    }
}
