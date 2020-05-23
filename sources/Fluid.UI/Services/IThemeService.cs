using System.Collections.ObjectModel;
using Fluid.Core.Base.Interfaces;
using Fluid.UI.Base.Interfaces;

namespace Fluid.UI.Services
{
    /// <summary>
    ///     Interface of theme service classes.
    /// </summary>
    public interface IThemeService : IService
    {
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