using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.Charting.Base.Interfaces;

namespace Waves.UI.Drawing.Charting.Services.Interfaces
{
    /// <summary>
    /// Interface for charting service.
    /// </summary>
    public interface IChartingService : IWavesService
    {
        /// <summary>
        /// Gets chart view factory.
        /// </summary>
        /// <returns>Chart view factory.</returns>
        IChartViewFactory GetChartViewFactory();
    }
}