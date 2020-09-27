using System;
using ReactiveUI;
using Waves.Core.Base.Interfaces.Services;
using Waves.Presentation.Interfaces;
using Waves.UI.Drawing.Presentation.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Drawing.View.Interfaces;
using Waves.UI.Drawing.ViewModel;
using Waves.UI.Drawing.ViewModel.Interfaces;

namespace Waves.UI.Drawing.Presentation
{
    /// <summary>
    ///     Drawing element presentation.
    /// </summary>
    public class DrawingElementPresentation : Waves.Presentation.Base.Presentation, IDrawingElementPresentation
    {
        /// <summary>
        ///     Gets or sets Data context's backing field.
        /// </summary>
        protected IDrawingElementViewModel DataContextBackingField;

        /// <summary>
        ///     Gets or sets View's backing field.
        /// </summary>
        protected IDrawingElementView ViewBackingField;

        /// <summary>
        ///     Creates new instance of <see cref="DrawingElementPresentation" />
        /// </summary>
        /// <param name="drawingService">Drawing service.</param>
        /// <param name="inputService">Input service.</param>
        public DrawingElementPresentation(IDrawingService drawingService, IInputService inputService)
        {
            DrawingService = drawingService;
            InputService = inputService;
        }

        /// <summary>
        ///     Gets or sets drawing service.
        /// </summary>
        protected IDrawingService DrawingService { get; set; }

        /// <summary>
        ///     Gets or sets input service.
        /// </summary>
        protected IInputService InputService { get; set; }

        /// <inheritdoc />
        public override IPresentationViewModel DataContext
        {
            get => DataContextBackingField;
            protected set => DataContextBackingField = (IDrawingElementViewModel) value;
        }

        /// <inheritdoc />
        public override IPresentationView View
        {
            get => ViewBackingField;
            protected set => ViewBackingField = (IDrawingElementView) value;
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            if (DrawingService?.CurrentEngine == null) return;

            SubscribeEvents();

            DataContextBackingField = new DrawingElementViewModel(DrawingService.CurrentEngine.GetDrawingElement());
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
            var dataContext = new DrawingElementViewModel(DrawingService.CurrentEngine.GetDrawingElement());

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