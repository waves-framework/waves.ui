using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of accent color set.
    /// </summary>
    public interface IAccentColorSet: IObject
    {
        /// <summary>
        ///     Gets example of accent color.
        /// </summary>
        Color ColorExample { get; }
        
        /// <summary>
        ///     Gets accent color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetColor(int weight);

        /// <summary>
        ///     Gets accent foreground color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetForegroundColor(int weight);
    }
}