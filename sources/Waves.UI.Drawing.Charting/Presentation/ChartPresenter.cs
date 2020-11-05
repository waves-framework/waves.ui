using System;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces.Services;
using Waves.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.View.Interface;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Drawing.Charting.Presentation
{
    /// <summary>
    ///     Chart presenter.
    /// </summary>
    public abstract class ChartPresenter : Waves.Presentation.Base.Presenter, IChartPresenter
    {
        /// <summary>
        ///     Gets or sets Data context's backing field.
        /// </summary>
        protected IChartPresenterViewModel DataContextBackingField;

        /// <summary>
        ///     Gets or sets View's backing field.
        /// </summary>
        protected IChartPresenterView ViewBackingField;

        /// <inheritdoc />
        protected ChartPresenter(
            IDrawingService drawingService,
            IThemeService themeService,
            IInputService inputService)
        {
            DrawingService = drawingService;
            ThemeService = themeService;
            InputService = inputService;

            SubscribeEvents();
        }

        /// <summary>
        ///     Gets or sets theme service.
        /// </summary>
        protected IThemeService ThemeService { get; set; }

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
            protected set => DataContextBackingField = (IChartPresenterViewModel) value;
        } 

        /// <inheritdoc />
        public override IPresenterView View
        {
            get => ViewBackingField;
            protected set => ViewBackingField = (IChartPresenterView) value;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();

            if (ThemeService != null)
                ThemeService.ThemeChanged -= OnThemeChanged;

            if (DrawingService != null)
                DrawingService.EngineChanged -= OnDrawingServiceEngineChanged;
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            if (DataContext != null && View != null) base.Initialize();
        }

        /// <summary>
        ///     Notifies when drawing service changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            Update();
        }

        /// <summary>
        ///     Updates all.
        /// </summary>
        protected abstract void Update();

        /// <summary>
        ///     Initialize colors.
        /// </summary>
        protected virtual void InitializeColors()
        {
            if (!(DataContext is IChartPresenterViewModel context)) return;

            context.Background = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(100);
            context.BorderColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(900);
            context.XAxisZeroLineColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(900);
            context.XAxisPrimaryTicksColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(700);
            context.XAxisAdditionalTicksColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(500);
            context.YAxisZeroLineColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(900);
            context.YAxisPrimaryTicksColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(700);
            context.YAxisAdditionalTicksColor = ThemeService.SelectedTheme.PrimaryColorSet.GetColor(500);
            context.Foreground = ThemeService.SelectedTheme.PrimaryColorSet.GetForegroundColor(100);

            context.TextStyle.FontFamily = "#Lato Regular";
            context.TextStyle.FontSize = 12;

            OnMessageReceived(
                this, 
                new Message(
                    "Chart colors", 
                    "Chart colors were changed", 
                    "Chart", 
                    MessageType.Success));
        }

        /// <summary>
        ///     Subscribes events.
        /// </summary>
        private void SubscribeEvents()
        {
            if (ThemeService != null)
                ThemeService.ThemeChanged += OnThemeChanged;

            if (DrawingService != null)
                DrawingService.EngineChanged += OnDrawingServiceEngineChanged;
        }

        /// <summary>
        ///     Actions when theme service theme changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            InitializeColors();

            DataContextBackingField.Update();
        }
    }
}