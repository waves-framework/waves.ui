using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;
using Waves.UI.Presentation.Interfaces.View;

namespace Waves.UI.Base.Attributes
{
    /// <summary>
    /// Attribute for all views.
    /// </summary>
    public class WavesViewAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewAttribute"/>.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="region">Region.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name.</param>
        public WavesViewAttribute(
            object key,
            string region = "Main",
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(key, typeof(IWavesView), lifetimeType, name)
        {
            Region = region;
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Key.</param>
        /// <param name="region">Region.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name.</param>
        public WavesViewAttribute(
            Guid id,
            object key,
            string region = "Main",
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(id, key, typeof(IWavesView), lifetimeType, name)
        {
            Region = region;
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Key.</param>
        /// <param name="region">Region.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name.</param>
        public WavesViewAttribute(
            string id,
            object key,
            string region = "Main",
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(id, key, typeof(IWavesView), lifetimeType, name)
        {
            Region = region;
        }

        /// <summary>
        ///     Gets region.
        /// </summary>
        public string Region { get; }
    }
}
