using Waves.UI.Windows.Controls.Drawing.View.Interfaces;

namespace Waves.UI.Drawing.Charting.View.Interface
{
    /// <summary>
    /// Interface of chart view element.
    /// </summary>
    public interface IChartView : IDrawingElementView
    {
        /// <summary>
        ///     Gets drawing element view.
        /// </summary>
        IDrawingElementView DrawingElementView { get; set; }
    }
}