using System;
using System.Linq;
using Waves.Presentation.Base;
using Waves.Presentation.Interfaces;
using Waves.UI.Modality.Presentation.Controllers.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;

namespace Waves.UI.Modality.Presentation.Controllers
{
    /// <summary>
    /// Modality windows presentation controller.
    /// </summary>
    public abstract class ModalWindowsPresentationController : PresentationController, IModalWindowsPresentationController
    {
        private IPresentation _presentation;

        /// <inheritdoc />
        public bool IsVisible { get; private set; }

        /// <inheritdoc />
        public override IPresentation SelectedPresentation
        {
            get => _presentation;
            set
            {
                if (Equals(value, _presentation)) return;

                if (_presentation != null)
                {
                    Presentations.Add(_presentation);
                }

                _presentation = value;

                if (_presentation != null)
                {
                    Presentations.Remove(_presentation);
                }

                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            UpdateVisibility();
        }

        /// <inheritdoc />
        public void ShowWindow(IModalWindowPresentation presentation)
        {
            RegisterPresentation(presentation);

            presentation.WindowRequestClosing += OnPresentationWindowRequestClosing;

            SelectedPresentation = presentation;

            FadeInWindow(presentation);

            UpdateVisibility();
        }

        /// <inheritdoc />
        public void HideWindow(IModalWindowPresentation presentation)
        {
            if (SelectedPresentation.Equals(presentation))
                SelectedPresentation = null;

            FadeOutWindow(presentation);

            UnregisterPresentation(presentation);

            if (Presentations.Count > 0)
                SelectedPresentation = Presentations.Last();

            UpdateVisibility();
        }

        /// <summary>
        /// Checks controllers visibility.
        /// </summary>
        private void UpdateVisibility()
        {
            IsVisible = SelectedPresentation != null;
        }

        /// <summary>
        /// Notifies when window request closing.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Window presentation.</param>
        private void OnPresentationWindowRequestClosing(object sender, IModalWindowPresentation e)
        {
            HideWindow(e);
        }

        /// <summary>
        /// Animates fade in.
        /// </summary>
        /// <param name="presentation">Presentation.</param>
        protected abstract void FadeInWindow(IPresentation presentation);

        /// <summary>
        /// Animates fade out.
        /// </summary>
        /// <param name="presentation">Presentation.</param>
        protected abstract void FadeOutWindow(IPresentation presentation);
    }
}