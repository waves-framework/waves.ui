using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface for controls that need to be initialized via method (not a constructor).
    /// </summary>
    public interface IWavesControl
    {
        /// <summary>
        /// Attaches core to control.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        void AttachCore(IWavesCore core);
    }
}