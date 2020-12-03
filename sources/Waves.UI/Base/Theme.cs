using System;
using ReactiveUI;
using Waves.Core.Base;
using Waves.UI.Base.Interfaces;
using Waves.UI.Services.Interfaces;

namespace Waves.UI.Base
{
    /// <summary>
    /// Color theme abstraction.
    /// </summary>
    public abstract class Theme : WavesObject, ITheme
    {
        private bool _useDarkSet = false;

        /// <summary>
        /// Creates new instance of <see cref="Theme"/>.
        /// </summary>
        /// <param name="id">Theme's id.</param>
        /// <param name="name">Theme's name.</param>
        /// <param name="lightColorSet">Primary light color set.</param>
        /// <param name="darkColorSet">Primary dark color set.</param>
        /// <param name="accentColorSet">Accent color set.</param>
        /// <param name="miscellaneousColorSet">Miscellaneous color set.</param>
        protected Theme(Guid id,
            string name,
            IWeightColorSet lightColorSet,
            IWeightColorSet darkColorSet,
            IWeightColorSet accentColorSet,
            IKeyColorSet miscellaneousColorSet)
        {
            Id = id;
            Name = name;
            
            PrimaryLightColorSet = lightColorSet;
            PrimaryDarkColorSet = darkColorSet;
            AccentColorSet = accentColorSet;
            MiscellaneousColorSet = miscellaneousColorSet;

            PrimaryColorSet = PrimaryLightColorSet;
        }
        
        /// <inheritdoc />
        public event EventHandler PrimaryColorSetChanged;
        
        /// <inheritdoc />
        public override Guid Id { get; }
        
        /// <inheritdoc />
        public sealed override string Name { get; set; }

        /// <inheritdoc />
        public bool UseDarkSet
        {
            get => _useDarkSet;
            set
            {
                this.RaiseAndSetIfChanged(ref _useDarkSet, value);
                
                PrimaryColorSet = _useDarkSet ? PrimaryDarkColorSet : PrimaryLightColorSet;

                OnPrimaryColorSetChanged();
            }
        }

        /// <inheritdoc />
        public IWeightColorSet PrimaryColorSet { get; private set; }
        
        /// <inheritdoc />
        public IWeightColorSet AccentColorSet { get; private set;}
        
        /// <inheritdoc />
        public IKeyColorSet MiscellaneousColorSet { get; }
        
        /// <summary>
        /// Gets primary light color set.
        /// </summary>
        protected IWeightColorSet PrimaryLightColorSet { get; set; }
        
        /// <summary>
        /// Gets primary dark color set.
        /// </summary>
        protected IWeightColorSet PrimaryDarkColorSet { get; set; }

        /// <summary>
        /// Notifies when primary color set changed.
        /// </summary>
        protected virtual void OnPrimaryColorSetChanged()
        {
            PrimaryColorSetChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}