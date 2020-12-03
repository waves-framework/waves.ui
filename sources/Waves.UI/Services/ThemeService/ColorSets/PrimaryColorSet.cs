using System;
using System.Collections.Generic;
using Waves.Core.Base;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Services.ThemeService.ColorSets
{
    /// <summary>
    ///     Primary color set's abstraction.
    /// </summary>
    public abstract class PrimaryColorSet : WavesObject, IWeightColorSet
    {
        /// <summary>
        ///     Creates new instance of <see cref="PrimaryColorSet" />.
        /// </summary>
        /// <param name="id">Color set's ID.</param>
        /// <param name="name">Color set's name.</param>
        protected PrimaryColorSet(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <inheritdoc />
        public override Guid Id { get; }

        /// <inheritdoc />
        public sealed override string Name { get; set; }

        /// <inheritdoc />
        public WavesColor ColorExample => GetColor(100);

        /// <summary>
        ///     Gets color dictionary.
        /// </summary>
        protected Dictionary<int, WavesColor> ColorDictionary { get; } = new Dictionary<int, WavesColor>();

        /// <summary>
        ///     Gets foreground color dictionary.
        /// </summary>
        protected Dictionary<int, WavesColor> ForegroundColorDictionary { get; } = new Dictionary<int, WavesColor>();

        /// <inheritdoc />
        public WavesColor GetColor(int weight)
        {
            if (ColorDictionary.ContainsKey(500))
                return ColorDictionary[weight];

            return WavesColor.Transparent;
        }

        /// <inheritdoc />
        public WavesColor GetForegroundColor(int weight)
        {
            return ForegroundColorDictionary[weight];
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            ColorDictionary.Clear();
            ForegroundColorDictionary.Clear();
        }
    }
}