using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Waves.Presentation.Interfaces;
using Waves.UI.Base.Interfaces;
using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;
using Waves.UI.Modality.ViewModel.Interfaces;

namespace Waves.UI.Modality.Presentation
{
    /// <summary>
    /// Base abstract modality window presentation.
    /// </summary>
    public abstract class ModalWindowPresentation : Waves.Presentation.Base.Presentation, IModalWindowPresentation
    {
        private readonly object _locker = new object();

        /// <summary>
        /// Creates new instance of <see cref="ModalWindowPresentation"/>.
        /// </summary>
        /// <param name="core">Core instance.</param>
        protected ModalWindowPresentation(Core core)
        {
            Core = core;
        }

        /// <inheritdoc />
        public event EventHandler<IModalWindowPresentation> WindowRequestClosing;

        /// <inheritdoc />
        public abstract IVectorImage Icon { get; }

        /// <inheritdoc />
        public abstract string Title { get; }

        /// <inheritdoc />
        public virtual double MaxHeight { get; set; } = 240;

        /// <inheritdoc />
        public virtual double MaxWidth { get; set; } = 320;

        /// <inheritdoc />
        public abstract override IPresentationViewModel DataContext { get; }

        /// <inheritdoc />
        public abstract override IPresentationView View { get; }

        /// <inheritdoc />
        public ICollection<IModalWindowAction> Actions { get; protected set; } = new ObservableCollection<IModalWindowAction>();

        /// <summary>
        /// Command to "Close Window".
        /// </summary>
        public ICommand CloseWindowCommand { get; protected set; }

        /// <summary>
        /// Gets instance of UI <see cref="Core"/>.
        /// </summary>
        protected Core Core { get; }

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            if (!(DataContext is IModalWindowPresentationViewModel context)) return;

            context.AttachActions(Actions);
        }

        /// <summary>
        /// Actions when window requesting closing.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWindowRequestClosing(IModalWindowPresentation e)
        {
            WindowRequestClosing?.Invoke(this, e);
        }

        /// <summary>
        /// Actions when close window.
        /// </summary>
        /// <param name="obj"></param>
        protected void OnCloseWindow(object obj)
        {
            var presentation = obj as IModalWindowPresentation;
            if (presentation == null) return;

            OnWindowRequestClosing(presentation);
        }
    }
}