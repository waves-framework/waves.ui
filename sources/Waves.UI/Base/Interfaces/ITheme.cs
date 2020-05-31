using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of theme group.
    /// </summary>
    public interface ITheme : IObject
    {
        /// <summary>
        /// Gets or sets whether service sets dark scheme.
        /// </summary>
        bool UseDarkScheme { get; set; }
        
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