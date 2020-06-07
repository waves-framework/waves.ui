using System;
using System.Collections.Generic;
using Waves.Core.Base;
using Waves.UI.Services.Interfaces;
using Object = Waves.Core.Base.Object;

namespace Waves.UI.Services
{
    /// <summary>
    /// Miscellaneous color set abstraction.
    /// </summary>
    public class MiscellaneousColorSet : Object, IKeyColorSet
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
        protected Dictionary<string, Color> ColorDictionary { get; } = new Dictionary<string, Color>();
        
        /// <summary>
        /// Gets foreground color dictionary.
        /// </summary>
        protected Dictionary<string, Color> ForegroundColorDictionary { get; } = new Dictionary<string, Color>();
        
        /// <inheritdoc />
        public Color GetColor(string key)
        {
            if (ColorDictionary.ContainsKey(key))
                return ColorDictionary[key];

            return Color.Transparent;
        }

        /// <inheritdoc />
        public Color GetForegroundColor(string key)
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