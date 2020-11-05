using System;
using System.Collections.Generic;
using ReactiveUI;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.ViewModel;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Drawing.Charting.Presentation
{
    /// <summary>
    ///     Data set chart presentation.
    /// </summary>
    public class DatasetChartPresentation : ChartPresenter, IChartPresenter
    {
        private readonly IChartViewFactory _chartViewFactory;

        private List<IDataSet> _oldDataSets = new List<IDataSet>();

        /// <inheritdoc />
        public DatasetChartPresentation(
            IDrawingService drawingService,
            IThemeService themeService,
            IInputService inputService,
            IChartViewFactory factory)
            : base(drawingService, themeService, inputService)
        {
            _chartViewFactory = factory;
        }
        
        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Dataset Chart Presentation";

        /// <inheritdoc />
        public override void Initialize()
        {
            Update();
        }

        /// <inheritdoc />
        protected override void Update()
        {
            if (DrawingService == null) return;
            if (ThemeService == null) return;

            var drawingElement = DrawingService.CurrentEngine.GetDrawingElement();
            var dataContext = new DataSetChartPresenterViewModel(drawingElement);

            var view = _chartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);

            if (_oldDataSets != null)
            {
                foreach (var dataSet in _oldDataSets)
                    dataContext.AddDataSet(dataSet);

                _oldDataSets.Clear();
            }

            DataContextBackingField = dataContext;
            ViewBackingField = view;

            InitializeColors();
            
            this.RaisePropertyChanged(nameof(DataContext));
            this.RaisePropertyChanged(nameof(View));

            base.Initialize();

            dataContext.Update();
        }

        /// <summary>
        ///     Initialize colors.
        /// </summary>
        protected override void InitializeColors()
        {
            base.InitializeColors();

            if (!(DataContextBackingField is DataSetChartPresenterViewModel context)) return;
        }

        /// <inheritdoc />
        protected override void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            if (!(DataContextBackingField is IDataSetChartPresenterViewModel context)) return;

            _oldDataSets = context.DataSets;

            Update();
        }
    }
}