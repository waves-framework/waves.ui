using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    /// Interface of weight color set.
    /// </summary>
    public interface IWeightColorSet: IObject
    {
        /// <summary>
        ///     Gets example color.
        /// </summary>
        Color ColorExample { get; }
        
        /// <summary>
        ///     Gets color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetColor(int weight);

        /// <summary>
        ///     Gets foreground color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        Color GetForegroundColor(int weight);
    }
}