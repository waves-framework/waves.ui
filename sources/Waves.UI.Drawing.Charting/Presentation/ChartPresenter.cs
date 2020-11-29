using System;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.View.Interface;
using Waves.UI.Drawing.Charting.ViewModel;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.Presentation;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Drawing.Charting.Presentation
{
    /// <summary>
    ///     Chart presenter.
    /// </summary>
    public abstract class ChartPresenter : DrawingElementPresenter, IChartPresenter
    {
        /// <inheritdoc />
        protected ChartPresenter(
            IWavesCore core,
            IChartViewFactory factory) 
            : base(core)
        {
            ChartViewFactory = factory;
            
            SubscribeEvents();
        }
        
        /// <summary>
        /// Gets chart view factory.
        /// </summary>
        protected IChartViewFactory ChartViewFactory { get; }

        /// <summary>
        ///     Gets or sets theme service.
        /// </summary>
        protected IThemeService ThemeService { get; set; }

        /// <inheritdoc />
        public override void Dispose()
        {
            UnsubscribeEvents();
            
            base.Dispose();
        }

        /// <inheritdoc />
        public override void Initialize()
        {
            // base.Initialize();
            
            InitializeServices();

            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));
            
            var view = ChartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);
            
            SetDataContext(new ChartPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(view);
            
            InitializeColors();
            
            SubscribeEvents();
            
            DataContextBackingField.Update();
        }

        /// <summary>
        ///     Notifies when drawing service changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            // base.Initialize();
            
            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));
            
            UnsubscribeEvents();
            
            var view = ChartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);
            
            SetDataContext(new ChartPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(view);

            SubscribeEvents();

            DataContextBackingField.Update();
        }

        /// <summary>
        ///     Updates all.
        /// </summary>
        protected abstract void Update();

        /// <summary>
        ///     Initialize colors.
        /// </summary>
        private void InitializeColors()
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
                new WavesMessage(
                    "Chart colors", 
                    "Chart colors were changed", 
                    Name, 
                    WavesMessageType.Success));
        }
        
        /// <summary>
        /// Initializes services.
        /// </summary>
        private void InitializeServices()
        {
            if (Core == null)
                throw new NullReferenceException(nameof(Core));

            ThemeService = Core.GetInstance<IThemeService>();
            
            if (ThemeService == null)
                throw new NullReferenceException(nameof(ThemeService));
        }

        /// <summary>
        ///     Subscribes events.
        /// </summary>
        private void SubscribeEvents()
        {
            if (ThemeService != null)
                ThemeService.ThemeChanged += OnThemeChanged;
        }
        
        /// <summary>
        ///     Unsubscribes events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if (ThemeService != null)
                ThemeService.ThemeChanged -= OnThemeChanged;
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