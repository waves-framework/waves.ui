using Waves.Core.Base;
using Waves.Core.Base.Interfaces;
using Waves.UI.Drawing.Charting.Base.Enums;

namespace Waves.UI.Drawing.Charting.Base.Interfaces
{
    /// <summary>
    ///     Interface for data set.
    /// </summary>
    public interface IDataSet : IWavesObject
    {
        /// <summary>
        ///     Gets or sets data set data.
        /// </summary>
        WavesPoint[] Data { get; }

        /// <summary>
        ///     Gets or sets data set descriptions.
        /// </summary>
        string[] Description { get; }

        /// <summary>
        ///     Gets or sets data set type.
        /// </summary>
        DataSetType Type { get; set; }

        /// <summary>
        ///     Gets or sets data set opacity.
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        ///     Updates data set data.
        /// </summary>
        /// <param name="data">Data.</param>
        void UpdateDataSet(WavesPoint[] data);

        /// <summary>
        ///     Updates data set data and descriptions.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="description">Descriptions.</param>
        void UpdateDataSet(WavesPoint[] data, string[] description);
    }
}