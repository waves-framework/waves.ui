using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;

namespace Waves.UI.Base.Attributes
{
    /// <summary>
    /// Attribute for all view models.
    /// </summary>
    public class WavesViewModelAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="pluginType">View model type.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(pluginType, lifetimeType, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            object key,
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(key, pluginType, lifetimeType, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="id">Unique Id.</param>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            Guid id,
            object key,
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(id, key, pluginType, lifetimeType, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="id">Unique Id.</param>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="lifetimeType">Lifetime type.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            string id,
            object key,
            Type pluginType,
            WavesLifetime lifetimeType = WavesLifetime.Transient,
            string name = default)
            : base(id, key, pluginType, lifetimeType, name)
        {
        }
    }
}
