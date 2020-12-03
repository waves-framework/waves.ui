using System;
using System.Collections.Generic;
using Waves.Core.Base;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Services.ThemeService.ColorSets
{
    /// <summary>
    /// Miscellaneous color set abstraction.
    /// </summary>
    public class MiscellaneousColorSet : WavesObject, IKeyColorSet
    {
        /// <summary>
        /// Creates new instance of <see cref="MiscellaneousColorSet"/>.
        /// </summary>
        /// <param name="id">Color set's ID.</param>
        /// <param name="name">Color set's name.</param>
        protected MiscellaneousColorSet(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        /// <inheritdoc />
        public override Guid Id { get; }
        
        /// <inheritdoc />
        public sealed override string Name { get; set; }

        /// <summary>
        /// Gets color dictionary.
        /// </summary>
        protected Dictionary<string, WavesColor> ColorDictionary { get; } = new Dictionary<string, WavesColor>();
        
        /// <summary>
        /// Gets foreground color dictionary.
        /// </summary>
        protected Dictionary<string, WavesColor> ForegroundColorDictionary { get; } = new Dictionary<string, WavesColor>();
        
        /// <inheritdoc />
        public WavesColor GetColor(string key)
        {
            if (ColorDictionary.ContainsKey(key))
                return ColorDictionary[key];

            return WavesColor.Transparent;
        }

        /// <inheritdoc />
        public WavesColor GetForegroundColor(string key)
        {
            return ForegroundColorDictionary[key];
        }
        
        /// <inheritdoc />
        public override void Dispose()
        {
            ColorDictionary.Clear();
            ForegroundColorDictionary.Clear();
        }
    }
}