using System;
using System.Collections.Generic;
using Waves.Core.Base;
using Waves.UI.Base.Interfaces;
using Object = Waves.Core.Base.Object;

namespace Waves.UI.Base
{
    /// <summary>
    /// Primary color set's abstraction.
    /// </summary>
    public abstract class PrimaryColorSet: Object, IPrimaryColorSet
    {
        /// <summary>
        /// Creates new instance of <see cref="PrimaryColorSet"/>.
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
        public Color ColorExample => GetColor(100);
        
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