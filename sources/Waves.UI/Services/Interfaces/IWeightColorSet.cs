using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    /// Interface of weight color set.
    /// </summary>
    public interface IWeightColorSet: IWavesObject
    {
        /// <summary>
        ///     Gets example color.
        /// </summary>
        WavesColor ColorExample { get; }
        
        /// <summary>
        ///     Gets color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        WavesColor GetColor(int weight);

        /// <summary>
        ///     Gets foreground color by weight.
        /// </summary>
        /// <param name="weight">Weight.</param>
        /// <returns>Color.</returns>
        WavesColor GetForegroundColor(int weight);
    }
}