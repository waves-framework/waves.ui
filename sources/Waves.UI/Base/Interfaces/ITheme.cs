using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of theme group.
    /// </summary>
    public interface ITheme : IObject
    {
        /// <summary>
        /// Gets light color set.
        /// </summary>
        IPrimaryColorSet LightColorSet { get; }
        
        /// <summary>
        /// Gets dark color set.
        /// </summary>
        IPrimaryColorSet DarkColorSet { get; }
        
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