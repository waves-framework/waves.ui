using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of primary color set.
    /// </summary>
    public interface IPrimaryColorSet : IObject
    {
        /// <summary>
        ///     Gets example of primary color.
        /// </summary>
        Color ColorExample { get; }
        
        /// <summary>
        ///     Gets primary color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetColor(int weight);

        /// <summary>
        ///     Gets primary foreground color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetForegroundColor(int weight);
    }
}