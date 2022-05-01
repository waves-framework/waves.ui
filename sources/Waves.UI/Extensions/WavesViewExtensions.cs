using Waves.UI.Base.Attributes;
using Waves.UI.Presentation.Interfaces.View;

namespace Waves.UI.Extensions
{
    /// <summary>
    /// Extensions for Waves Views.
    /// </summary>
    public static class WavesViewExtensions
    {
        /// <summary>
        /// Gets view attribute.
        /// </summary>
        /// <param name="view">View.</param>
        /// <returns>Attribute.</returns>
        public static WavesViewAttribute GetViewAttribute(this IWavesView view)
        {
            var attributes = view.GetType().GetCustomAttributes(false);
            return (WavesViewAttribute)attributes.FirstOrDefault(x => x.GetType() == typeof(WavesViewAttribute)) !;
        }
    }
}
