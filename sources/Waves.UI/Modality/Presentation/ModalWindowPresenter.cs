using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Presentation.Interfaces;
using Waves.UI.Base.Interfaces;
using Waves.UI.Modality.Base.Interfaces;
using Waves.UI.Modality.Presentation.Interfaces;
using Waves.UI.Modality.ViewModel.Interfaces;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Modality.Presentation
{
    /// <summary>
    /// Base abstract modality window presenter.
    /// </summary>
    public abstract class ModalWindowPresenter : Waves.Presentation.Base.Presenter, IModalWindowPresenter
    {
        private readonly object _locker = new object();

        /// <summary>
        /// Creates new instance of <see cref="ModalWindowPresenter"/>.
        /// </summary>
        /// <param name="core">Core instance.</param>
        protected ModalWindowPresenter(Core core)
        {
            Core = core;
        }

        /// <inheritdoc />
        public event EventHandler<IModalWindowPresenter> WindowRequestClosing;

        /// <inheritdoc />
        public abstract IVectorImage Icon { get; }

        /// <inheritdoc />
        public abstract string Title { get; }

        /// <inheritdoc />
        public virtual double MaxHeight { get; set; } = 240;

        /// <inheritdoc />
        public virtual double MaxWidth { get; set; } = 320;

        /// <summary>
        /// Gets collection synchronization service.
        /// </summary>
        public ICollectionSynchronizationService CollectionSynchronizationService { get; private set; }

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

            try
            {
                CollectionSynchronizationService = Core.GetInstance<ICollectionSynchronizationService>();

                CollectionSynchronizationService?.EnableCollectionSynchronization(Actions, _locker);

                if (!(DataContext is IModalWindowPresenterViewModel context)) return;

                context.AttachActions(Actions);
            }
            catch (Exception e)
            {
                OnMessageReceived(this,
                    new Message(
                    "Initialization", 
                    "Error initializing modal window presenter", 
                    "Modal window presentation", 
                    e, 
                    false));
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            CollectionSynchronizationService.DisableCollectionSynchronization(Actions);
        }

        /// <summary>
        /// Actions when window requesting closing.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnWindowRequestClosing(IModalWindowPresenter e)
        {
            WindowRequestClosing?.Invoke(this, e);
        }

        /// <summary>
        /// Actions when close window.
        /// </summary>
        /// <param name="obj"></param>
        protected void OnCloseWindow(object obj)
        {
            if (!(obj is IModalWindowPresenter presenter)) return;
            OnWindowRequestClosing(presenter);
        }
    }
}