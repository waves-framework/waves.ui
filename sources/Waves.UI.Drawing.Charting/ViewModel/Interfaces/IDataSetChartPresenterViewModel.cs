using System.Collections.Generic;
using System.Collections.ObjectModel;
using Waves.Core.Base;
using Waves.UI.Drawing.Charting.Base.Interfaces;

namespace Waves.UI.Drawing.Charting.ViewModel.Interfaces
{
    /// <summary>
    ///     Interface for data chart view model.
    /// </summary>
    public interface IDataSetChartPresenterViewModel : IChartPresenterViewModel
    {
        /// <summary>
        ///     Gets collection of added data sets.
        /// </summary>
        ObservableCollection<IDataSet> DataSets { get; }

        /// <summary>
        ///     Adds new data set.
        /// </summary>
        /// <param name="dataSet">Data set.</param>
        void AddDataSet(IDataSet dataSet);

        /// <summary>
        ///     Updates existing data set.
        /// </summary>
        /// <param name="index">Data set index.</param>
        /// <param name="points">Array of points.</param>
        void UpdateDataSet(int index, WavesPoint[] points);

        /// <summary>
        ///     Removes existing data set.
        /// </summary>
        /// <param name="index">Data set index.</param>
        void RemoveDataSet(int index);
    }
}