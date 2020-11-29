using System;
using System.Collections.Generic;
using ReactiveUI;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.Charting.Base.Interfaces;
using Waves.UI.Drawing.Charting.Presentation.Interfaces;
using Waves.UI.Drawing.Charting.ViewModel;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.Services.Interfaces;
using Waves.UI.Drawing.ViewModel;
using Waves.UI.Services.Interfaces;

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
            base.Initialize();

            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));

            var view = ChartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);
            
            SetDataContext(new DataSetChartPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(view);
            
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
            base.Initialize();
            
            if (DrawingService?.CurrentEngine == null)
                throw new NullReferenceException(nameof(DrawingService.CurrentEngine));

            var view = ChartViewFactory.GetChartView();
            view.DrawingElementView = DrawingService.CurrentEngine.GetView(InputService);
            
            SetDataContext(new DataSetChartPresenterViewModel(Core, DrawingService.CurrentEngine.GetDrawingElement()));
            SetView(view);

            DataContextBackingField.Update();
        }
    }
}