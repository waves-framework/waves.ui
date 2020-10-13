using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Drawing.Charting.View.Interface
{
    /// <summary>
    /// Interface of chart view element.
    /// </summary>
    public interface IChartPresenterView : IDrawingElementPresenterView
    {
        /// <summary>
        ///     Gets drawing element view.
        /// </summary>
        IDrawingElementPresenterView DrawingElementView { get; set; }
    }
}