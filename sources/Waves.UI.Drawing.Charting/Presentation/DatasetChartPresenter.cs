using System;
using System.Collections.Generic;
using Waves.Core.Base.Interfaces;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.ViewModel;

namespace Waves.UI.Drawing.Charting.Presentation
{
    /// <summary>
    ///     Data set chart presentation.
    /// </summary>
    public class DatasetChartPresenter : ChartPresenter
    {
        private List<IDataSet> _oldDataSets = new List<IDataSet>();

        /// <inheritdoc />
        public DatasetChartPresenter(
            IWavesCore core,
            IChartViewFactory factory)
            : base(core, factory)
        {
        }

        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Dataset Chart Presentation";

        /// <inheritdoc />
        public override void Initialize()
        {
            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));

            InitializeView();

            SetDataContext(new DataSetChartPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));

            InitializeColors();

            DataContextBackingField.Update();
        }

        /// <inheritdoc />
        protected override void Update()
        {
            DataContextBackingField.Update();
        }

        /// <inheritdoc />
        protected override void OnDrawingServiceEngineChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        /// <summary>
        ///     Initializes view.
        /// </summary>
        private void InitializeView()
        {
            var view = ChartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);
            SetView(view);
        }
    }
}