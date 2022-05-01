using System.Windows.Input;

namespace Waves.UI.Presentation.Interfaces.View.Controls
{
    /// <summary>
    /// Interface for window controls.
    /// </summary>
    /// <typeparam name="TContent">Content control type.</typeparam>
    public interface IWavesWindow<TContent> : IWavesContentControl<TContent>
    {
        /// <summary>
        /// Gets or sets whether navigation can go back.
        /// </summary>
        bool CanGoBack { get; set; }

        /// <summary>
        /// Gets or sets front layer content.
        /// </summary>
        object FrontContent { get; set; }

        /// <summary>
        /// Gets or sets "Go Back" command.
        /// </summary>
        ICommand GoBackCommand { get; set; }

        /// <summary>
        /// Shows window.
        /// </summary>
        void Show();
    }
}
