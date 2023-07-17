using System;
using Waves.Core.Base.Attributes;
using Waves.Core.Base.Enums;

namespace Waves.UI.Dialogs.Attributes
{
    /// <summary>
    /// Attribute for tools.
    /// </summary>
    public class WavesDialogToolAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="WavesDialogToolAttribute"/>.
        /// </summary>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public WavesDialogToolAttribute(
            Type pluginType,
            string name = default)
            : base(pluginType, WavesLifetime.Transient, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesDialogToolAttribute"/>.
        /// </summary>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public WavesDialogToolAttribute(
            object key,
            Type pluginType,
            string name = default)
            : base(key, pluginType, WavesLifetime.Transient, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesDialogToolAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public WavesDialogToolAttribute(
            Guid id,
            object key,
            Type pluginType,
            string name = default)
            : base(id, key, pluginType, WavesLifetime.Transient, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="WavesDialogToolAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public WavesDialogToolAttribute(
            string id,
            object key,
            Type pluginType,
            string name = default)
            : base(id, key, pluginType, WavesLifetime.Transient, name)
        {
        }
    }
}
