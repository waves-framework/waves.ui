using System;
using System.Runtime.CompilerServices;
using Waves.Core.Base.Attributes;

namespace Waves.UI.Presentation.Dialogs.Attributes
{
    /// <summary>
    /// Attribute for tools.
    /// </summary>
    public class ToolAttribute : WavesPluginAttribute
    {
        /// <summary>
        /// Creates new instance of <see cref="ToolAttribute"/>.
        /// </summary>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public ToolAttribute(
            Type pluginType,
            [CallerMemberName] string name = default)
            : base(pluginType, false, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="ToolAttribute"/>.
        /// </summary>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public ToolAttribute(
            object key,
            Type pluginType,
            [CallerMemberName] string name = default)
            : base(key, pluginType, false, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="ToolAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public ToolAttribute(
            Guid id,
            object key,
            Type pluginType,
            [CallerMemberName] string name = default)
            : base(id, key, pluginType, false, name)
        {
        }

        /// <summary>
        /// Creates new instance of <see cref="ToolAttribute"/>.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// <param name="key">Plugin key.</param>
        /// <param name="pluginType">Plugin type.</param>
        /// <param name="name">Name.</param>
        public ToolAttribute(
            string id,
            object key,
            Type pluginType,
            [CallerMemberName] string name = default)
            : base(id, key, pluginType, false, name)
        {
        }
    }
}
