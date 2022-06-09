using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.UI.Presentation.Interfaces.ViewModel;

namespace Waves.UI.Presentation;

/// <summary>
/// Waves view model loading state.
/// </summary>
public class WavesViewModelLoadingState : ReactiveObject, IWavesViewModelLoadingState
{
    /// <inheritdoc />
    [Reactive]
    public bool IsLoading { get; set; }

    /// <inheritdoc />
    [Reactive]
    public bool IsIntermediate { get; set; }

    /// <inheritdoc />
    [Reactive]
    public int ProgressValue { get; set; }
}
