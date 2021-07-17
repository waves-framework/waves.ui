using System;
using System.Threading.Tasks;
using Waves.Core.Base.Interfaces;

namespace Waves.UI.Plugins.Services.Interfaces
{
    /// <summary>
    /// Service for dispatcher tasks.
    /// </summary>
    public interface IWavesDispatcherService : IWavesService
    {
        /// <summary>
        /// Invokes action throw dispatcher.
        /// </summary>
        /// <param name="action">Action.</param>
        void Invoke(Action action);

        /// <summary>
        /// Invokes action throw dispatcher.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task InvokeAsync(Action action);
    }
}
