using System;
using System.Runtime.CompilerServices;
using Waves.Core.Base.Attributes;

namespace Waves.UI.Presentation.Attributes
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
        /// <param name="isSingleInstance">Whether register view model as single instance.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            Type pluginType,
            bool isSingleInstance = false,
            [CallerMemberName] string name = default)
            : base(pluginType, isSingleInstance, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="isSingleInstance">Whether register view model as single instance.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            object key,
            Type pluginType,
            bool isSingleInstance = false,
            [CallerMemberName] string name = default)
            : base(key, pluginType, isSingleInstance, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="id">Unique Id.</param>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="isSingleInstance">Whether register view model as single instance.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            Guid id,
            object key,
            Type pluginType,
            bool isSingleInstance = false,
            [CallerMemberName] string name = default)
            : base(id, key, pluginType, isSingleInstance, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesViewModelAttribute"/>.
        /// </summary>
        /// <param name="id">Unique Id.</param>
        /// <param name="key">Key.</param>
        /// <param name="pluginType">View model type.</param>
        /// <param name="isSingleInstance">Whether register view model as single instance.</param>
        /// <param name="name">Name of view model.</param>
        public WavesViewModelAttribute(
            string id,
            object key,
            Type pluginType,
            bool isSingleInstance = false,
            [CallerMemberName] string name = default)
            : base(id, key, pluginType, isSingleInstance, name)
        {
        }
    }
}
