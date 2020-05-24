using Waves.UI.Drawing.Charting.View.Interface;

namespace Waves.UI.Drawing.Charting.Base.Interfaces
{
    /// <summary>
    ///     Chart view factory.
    /// </summary>
    public interface IChartViewFactory
    {
        /// <summary>
        ///     Gets chart view.
        /// </summary>
        /// <returns>Chart view.</returns>
        IChartView GetChartView();
    }
}