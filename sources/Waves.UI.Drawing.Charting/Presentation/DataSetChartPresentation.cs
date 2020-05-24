using System;
using System.Collections.Generic;
using Waves.Core.Services.Interfaces;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.ViewModel;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Services;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Drawing.Charting.Presentation
{
    /// <summary>
    ///     Data set chart presentation.
    /// </summary>
    public class DataSetChartPresentation : ChartPresentation, IChartPresentation
    {
        private readonly IChartViewFactory _chartViewFactory;

        private List<IDataSet> _oldDataSets = new List<IDataSet>();

        /// <inheritdoc />
        public DataSetChartPresentation(
            IDrawingService drawingService,
            IThemeService themeService,
            IInputService inputService,
            IChartViewFactory factory)
            : base(drawingService, themeService, inputService)
        {
            _chartViewFactory = factory;
        }

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
            var dataContext = new DataSetChartViewModel(drawingElement);

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

            OnPropertyChanged(nameof(DataContext));
            OnPropertyChanged(nameof(View));

            base.Initialize();

            dataContext.Update();
        }

        /// <summary>
        ///     Initialize colors.
        /// </summary>
        protected override void InitializeColors()
        {
            base.InitializeColors();

            if (!(DataContextBackingField is DataSetChartViewModel context)) return;
        }

        /// <inheritdoc />
        protected override void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            if (!(DataContextBackingField is IDataSetChartViewModel context)) return;

            _oldDataSets = context.DataSets;

            Update();
        }
    }
}