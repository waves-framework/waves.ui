using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Base.Interfaces
{
    /// <summary>
    /// Interface of miscellaneous color set.
    /// </summary>
    public interface IMiscellaneousColorSet: IObject
    {
        /// <summary>
        ///     Gets color by key.
        /// </summary>
        /// <param name="key">Color's key.</param>
        /// <returns>Color.</returns>
        Color GetColor(string key);

        /// <summary>
        ///     Gets foreground color by key.
        /// </summary>
        /// <param name="key">Color's key.</param>
        /// <returns>Color.</returns>
        Color GetForegroundColor(string key);
    }
}