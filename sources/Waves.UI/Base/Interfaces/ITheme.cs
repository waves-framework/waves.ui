using System;
using Waves.Core.Base.Interfaces;
using Waves.UI.Services.Interfaces;

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
        IWeightColorSet PrimaryColorSet { get; }

        /// <summary>
        /// Gets accent color set.
        /// </summary>
        IWeightColorSet AccentColorSet { get; }
        
        /// <summary>
        /// Gets miscellaneous color set.
        /// </summary>
        IKeyColorSet MiscellaneousColorSet { get; }
    }
}