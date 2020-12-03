using System.Collections;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    /// Interface for collection synchronization service.
    /// </summary>
    public interface ICollectionSynchronizationService : IWavesService
    {
        /// <summary>
        ///  Register a callback used to synchronize access to a given collection.
        /// </summary>
        /// <param name="collection">Collection.</param>
        /// <param name="locker">Synchronization locker.</param>
        void EnableCollectionSynchronization(IEnumerable collection, object locker);

        /// <summary>
        /// Unregister a callback used to synchronize access to a given collection.
        /// </summary>
        /// <param name="collection">Collection.</param>
        void DisableCollectionSynchronization(IEnumerable collection);
    }
}