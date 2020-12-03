using System;
using ReactiveUI;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.Presentation.Base;
using Waves.Presentation.Interfaces;
using Waves.UI.Drawing.Presentation.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Drawing.View.Interfaces;
using Waves.UI.Drawing.ViewModel;
using Waves.UI.Drawing.ViewModel.Interfaces;

namespace Waves.UI.Drawing.Presentation
{
    /// <summary>
    ///     Drawing element presenter.
    /// </summary>
    public class DrawingElementPresenter :
        Presenter,
        IDrawingElementPresenter
    {
        /// <summary>
        ///     Creates new instance of <see cref="DrawingElementPresenter" />
        /// </summary>
        /// <param name="core">Instance of core.</param>
        protected DrawingElementPresenter(
            IWavesCore core) :
            base(core)
        {
            InitializeServices();
        }

        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Drawing Element Presenter";
        
        /// <summary>
        ///     Gets or sets Data context's backing field.
        /// </summary>
        protected IDrawingElementPresenterViewModel DataContextBackingField => DataContext as IDrawingElementPresenterViewModel;

        /// <summary>
        ///     Gets or sets View's backing field.
        /// </summary>
        protected IDrawingElementPresenterView ViewBackingField => View as IDrawingElementPresenterView;

        /// <summary>
        ///     Gets or sets drawing service.
        /// </summary>
        protected IDrawingService DrawingService { get; set; }

        /// <summary>
        ///     Gets or sets input service.
        /// </summary>
        protected IInputService InputService { get; set; }

        /// <inheritdoc />
        public override void Initialize()
        {
            InitializeServices();

            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));
            
            SetDataContext(new DrawingElementPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(DrawingService.CurrentEngine.GetView(InputService));
            
            SubscribeEvents();
            
            base.Initialize();
            
            DataContextBackingField.Update();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            UnsubscribeEvents();
            
            base.Dispose();
        }

        /// <summary>
        ///     Actions when drawing engine changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));
            
            UnsubscribeEvents();
            
            SetDataContext(new DrawingElementPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(DrawingService.CurrentEngine.GetView(InputService));
            
            SubscribeEvents();
            
            base.Initialize();
            
            DataContextBackingField.Update();
        }

        /// <summary>
        /// Initializes services.
        /// </summary>
        private void InitializeServices()
        {
            if (Core == null)
                throw new NullReferenceException(nameof(Core));

            DrawingService = Core.GetInstance<IDrawingService>();
            InputService = Core.GetInstance<IInputService>();
            
            if (DrawingService == null)
                throw new NullReferenceException(nameof(DrawingService));

            if (InputService == null)
                throw new NullReferenceException(nameof(InputService));
        }

        /// <summary>
        ///     Subscribes events.
        /// </summary>
        private void SubscribeEvents()
        {
            if (DrawingService != null)
                DrawingService.EngineChanged += OnDrawingServiceEngineChanged;
            
            if (DataContextBackingField != null)
                DataContextBackingField.MessageReceived += OnMessageReceived;
            
            if (ViewBackingField != null)
                ViewBackingField.MessageReceived += OnMessageReceived;
        }

        /// <summary>
        /// Unsubscribes from events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if (DrawingService != null)
                DrawingService.EngineChanged -= OnDrawingServiceEngineChanged;
            
            if (DataContextBackingField != null)
                DataContext.MessageReceived -= OnMessageReceived;
            
            if (ViewBackingField != null)
                View.MessageReceived -= OnMessageReceived;
        }
    }
}