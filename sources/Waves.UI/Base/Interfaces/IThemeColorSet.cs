using System;
using Waves.UI.Base.Enums;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface for theme color sets.
    /// </summary>
    public interface IThemeColorSet
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        Guid Id { get; set; }
        
        /// <summary>
        /// Gets or sets name.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets type.
        /// </summary>
        ThemeColorSetType Type { get; set; }
    }
}