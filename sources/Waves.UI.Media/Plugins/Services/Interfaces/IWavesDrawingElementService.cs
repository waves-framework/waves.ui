using System.Threading.Tasks;
using MTL.UI.Base.Interfaces;
using MTL.UI.Media.Drawing.Interfaces;

namespace MTL.UI.Media.Plugins.Services.Interfaces
{
    /// <summary>
    /// Interface for service for resolving available drawing elements.
    /// </summary>
    public interface IMtlDrawingElementService : IMtlConfigurableService
    {
        /// <summary>
        /// Gets drawing element.
        /// </summary>
        /// <returns>Returns drawing object.</returns>
        Task<IMtlDrawingElement> GetDrawingElement();

        /// <summary>
        /// Sets working drawing element name.
        /// </summary>
        /// <param name="name">Drawing element name.</param>
        void SetWorkingDrawingElementName(string name);
    }
}
