using System;
using ReactiveUI;
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
        /// <param name="drawingService">Drawing service.</param>
        /// <param name="inputService">Input service.</param>
        public DrawingElementPresenter(
            IDrawingService drawingService,
            IInputService inputService)
        {
            DrawingService = drawingService;
            InputService = inputService;
        }

        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Drawing Element Presenter";
        
        /// <summary>
        ///     Gets or sets Data context's backing field.
        /// </summary>
        protected IDrawingElementPresenterViewModel DataContextBackingField;

        /// <summary>
        ///     Gets or sets View's backing field.
        /// </summary>
        protected IDrawingElementPresenterView ViewBackingField;

        /// <summary>
        ///     Gets or sets drawing service.
        /// </summary>
        protected IDrawingService DrawingService { get; set; }

        /// <summary>
        ///     Gets or sets input service.
        /// </summary>
        protected IInputService InputService { get; set; }

        /// <inheritdoc />
        public override IPresenterViewModel DataContext
        {
            get => DataContextBackingField;
            protected set => DataContextBackingField = (IDrawingElementPresenterViewModel) value;
        }

        /// <inheritdoc />
        public override IPresenterView View
        {
            get => ViewBackingField;
            protected set => ViewBackingField = (IDrawingElementPresenterView) value;
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            if (DrawingService?.CurrentEngine == null) return;

            SubscribeEvents();

            DataContextBackingField =
                new DrawingElementPresenterViewModel(DrawingService.CurrentEngine.GetDrawingElement());
            ViewBackingField = DrawingService.CurrentEngine.GetView(InputService);

            this.RaisePropertyChanged(nameof(DataContext));
            this.RaisePropertyChanged(nameof(View));

            base.Initialize();
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            DrawingService.EngineChanged -= OnDrawingServiceEngineChanged;
        }

        /// <summary>
        ///     Actions when drawing engine changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            var dataContext = new DrawingElementPresenterViewModel(DrawingService.CurrentEngine.GetDrawingElement());

            var currentContext = DataContextBackingField;
            if (currentContext == null) return;

            var view = DrawingService.CurrentEngine.GetView(InputService);

            DataContextBackingField = dataContext;
            ViewBackingField = view;

            ViewBackingField.DataContext = DataContextBackingField;

            dataContext.Update();

            this.RaisePropertyChanged(nameof(DataContext));
            this.RaisePropertyChanged(nameof(View));
        }

        /// <summary>
        ///     Subscribes events.
        /// </summary>
        private void SubscribeEvents()
        {
            DrawingService.EngineChanged += OnDrawingServiceEngineChanged;
        }
    }
}