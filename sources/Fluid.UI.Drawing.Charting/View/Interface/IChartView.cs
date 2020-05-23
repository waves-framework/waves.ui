using Fluid.UI.Windows.Controls.Drawing.View.Interfaces;

namespace Fluid.UI.Drawing.Charting.View.Interface
{
    public interface IChartView : IDrawingElementView
    {
        /// <summary>
        ///     Gets drawing element view.
        /// </summary>
        IDrawingElementView DrawingElementView { get; set; }
    }
}