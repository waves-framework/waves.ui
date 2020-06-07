using System;
using System.Collections.ObjectModel;
using Waves.Core.Base.Interfaces;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Services.Interfaces
{
    /// <summary>
    ///     Interface of theme service classes.
    /// </summary>
    public interface IThemeService : IService
    {
        /// <summary>
        /// Theme changed event.
        /// </summary>
        event EventHandler ThemeChanged;
        
        /// <summary>
        /// Gets or sets whether service sets dark scheme.
        /// </summary>
        bool UseDarkScheme { get; set; }
        
        /// <summary>
        ///     Gets or sets whether service sets automatic color scheme.
        /// </summary>
        bool UseAutomaticScheme { get; set; }

        /// <summary>
        ///     Gets or sets selected theme.
        /// </summary>
        ITheme SelectedTheme { get; set; }

        /// <summary>
        ///     Gets themes collection.
        /// </summary>
        ObservableCollection<ITheme> Themes { get; }
    }
}