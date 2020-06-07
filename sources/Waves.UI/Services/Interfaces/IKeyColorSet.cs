using Waves.Core.Base;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    /// Interface of key color set.
    /// </summary>
    public interface IKeyColorSet: IObject
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