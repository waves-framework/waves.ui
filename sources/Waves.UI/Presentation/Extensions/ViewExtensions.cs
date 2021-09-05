using System.Linq;
using Waves.UI.Presentation.Attributes;
using Waves.UI.Presentation.Interfaces;
using Waves.UI.Presentation.Interfaces.View;

namespace Waves.UI.Presentation.Extensions
{
    /// <summary>
    /// Extensions for Waves Views.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Gets view attribute.
        /// </summary>
        /// <param name="view">View.</param>
        /// <returns>Attribute.</returns>
        public static WavesViewAttribute GetViewAttribute(this IWavesView view)
        {
            var attributes = view.GetType().GetCustomAttributes(false);
            return (WavesViewAttribute)attributes.First(x => x.GetType() == typeof(WavesViewAttribute));
        }
    }
}
