namespace Waves.UI.Presentation.Interfaces.ViewModel;

/// <summary>
/// View model loading state.
/// </summary>
public interface IWavesViewModelLoadingState
{
    /// <summary>
    /// Gets or sets whether view-model is loading.
    /// </summary>
    bool IsLoading { get; set; }

    /// <summary>
    /// Gets or sets whether view-model loading state is intermediate.
    /// </summary>
    bool IsIntermediate { get; set; }

    /// <summary>
    /// Gets or sets progress value.
    /// </summary>
    int ProgressValue { get; set; }
}
