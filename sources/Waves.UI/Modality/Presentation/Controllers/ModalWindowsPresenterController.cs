using System;
using System.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base.Interfaces;
using Waves.Presentation.Base;
using Waves.Presentation.Interfaces;
using Waves.UI.Modality.Presentation.Controllers.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;

namespace Waves.UI.Modality.Presentation.Controllers
{
    /// <summary>
    /// Modality windows presenter controller.
    /// </summary>
    public abstract class ModalWindowPresenterController : 
        PresenterController, 
        IModalWindowPresenterController
    {
        private IPresenter _presenter;
        private bool _isVisible;

        /// <summary>
        /// Creates new instance of <see cref="ModalWindowPresenterController"/>.
        /// </summary>
        /// <param name="core">Instance of core.</param>
        protected ModalWindowPresenterController(IWavesCore core) : base(core)
        {
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsVisible
        {
            get => _isVisible;
            private set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }

        /// <inheritdoc />
        [Reactive]
        public override IPresenter SelectedPresenter
        {
            get => _presenter;
            set
            {
                if (Equals(value, _presenter)) return;
                
                if (_presenter != null)
                {
                    Presenters.Add(_presenter);
                }

                this.RaiseAndSetIfChanged(ref _presenter, value);

                if (_presenter != null)
                {
                    Presenters.Remove(_presenter);
                }
            }
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            UpdateVisibility();
        }

        /// <inheritdoc />
        public void ShowWindow(IModalWindowPresenter presenter)
        {
            RegisterPresenter(presenter);

            presenter.WindowRequestClosing += OnPresenterWindowRequestClosing;

            SelectedPresenter = presenter;

            FadeInWindow(presenter);

            UpdateVisibility();
        }

        /// <inheritdoc />
        public void HideWindow(IModalWindowPresenter presenter)
        {
            if (SelectedPresenter.Equals(presenter))
                SelectedPresenter = null;

            FadeOutWindow(presenter);

            UnregisterPresenter(presenter);

            if (Presenters.Count > 0)
                SelectedPresenter = Presenters.Last();

            UpdateVisibility();
        }

        /// <summary>
        /// Checks controllers visibility.
        /// </summary>
        private void UpdateVisibility()
        {
            IsVisible = SelectedPresenter != null;
        }

        /// <summary>
        /// Notifies when window request closing.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Window presentation.</param>
        private void OnPresenterWindowRequestClosing(object sender, IModalWindowPresenter e)
        {
            HideWindow(e);
        }

        /// <summary>
        /// Animates fade in.
        /// </summary>
        /// <param name="presenter">Presenter.</param>
        protected abstract void FadeInWindow(IPresenter presenter);

        /// <summary>
        /// Animates fade out.
        /// </summary>
        /// <param name="presenter">Presenter.</param>
        protected abstract void FadeOutWindow(IPresenter presenter);
    }
}