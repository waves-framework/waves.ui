using System;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of theme group.
    /// </summary>
    public interface ITheme : IObject
    {
        /// <summary>
        /// Primary color set changed event.
        /// </summary>
        event EventHandler PrimaryColorSetChanged;
        
        /// <summary>
        /// Gets or sets whether theme is using dark primary color set.
        /// </summary>
        bool UseDarkSet { get; set; }
        
        /// <summary>
        /// Gets light color set.
        /// </summary>
        IPrimaryColorSet PrimaryColorSet { get; }

        /// <summary>
        /// Gets accent color set.
        /// </summary>
        IAccentColorSet AccentColorSet { get; }
        
        /// <summary>
        /// Gets miscellaneous color set.
        /// </summary>
        IMiscellaneousColorSet MiscellaneousColorSet { get; }
    }
}