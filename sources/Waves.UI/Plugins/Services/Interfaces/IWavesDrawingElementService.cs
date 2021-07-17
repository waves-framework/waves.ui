using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;
using Waves.UI.Drawing.Interfaces;

namespace Waves.UI.Plugins.Services.Interfaces
{
    /// <summary>
    /// Interface for service for resolving available drawing elements.
    /// </summary>
    public interface IWavesDrawingElementService : IWavesConfigurableService
    {
        /// <summary>
        /// Gets drawing element.
        /// </summary>
        /// <returns>Returns drawing object.</returns>
        Task<IWavesDrawingElement> GetDrawingElement();

        /// <summary>
        /// Sets working drawing element name.
        /// </summary>
        /// <param name="name">Drawing element name.</param>
        void SetWorkingDrawingElementName(string name);
    }
}
