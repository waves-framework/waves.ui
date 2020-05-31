using System;
using System.Collections.Generic;
using Waves.Core.Base;
using Waves.UI.Base.Interfaces;
using Object = Waves.Core.Base.Object;

namespace Waves.UI.Base
{
    /// <summary>
    /// Accent color set abstraction.
    /// </summary>
    public abstract class AccentColorSet : Object, IAccentColorSet
    {
        /// <summary>
        /// Creates new instance of <see cref="AccentColorSet"/>.
        /// </summary>
        /// <param name="id">Color set's ID.</param>
        /// <param name="name">Color set's name.</param>
        protected AccentColorSet(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        
        /// <inheritdoc />
        public override Guid Id { get; }
        
        /// <inheritdoc />
        public sealed override string Name { get; set; }

        /// <inheritdoc />
        public Color ColorExample => GetColor(500);
        
        /// <summary>
        /// Gets color dictionary.
        /// </summary>
        protected Dictionary<int, Color> ColorDictionary { get; } = new Dictionary<int, Color>();
        
        /// <summary>
        /// Gets foreground color dictionary.
        /// </summary>
        protected Dictionary<int, Color> ForegroundColorDictionary { get; } = new Dictionary<int, Color>();
        
        /// <inheritdoc />
        public Color GetColor(int weight)
        {
            if (ColorDictionary.ContainsKey(500))
                return ColorDictionary[weight];

            return Color.Transparent;
        }

        /// <inheritdoc />
        public Color GetForegroundColor(int weight)
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