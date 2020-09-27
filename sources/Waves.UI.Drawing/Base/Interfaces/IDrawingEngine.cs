using Waves.Core.Base.Interfaces.Services;
using Waves.UI.Drawing.View.Interfaces;

namespace Waves.UI.Drawing.Base.Interfaces
{
    /// <summary>
    ///     Interface for drawing engine.
    /// </summary>
    public interface IDrawingEngine
    {
        /// <summary>
        ///     Gets name of engine.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets new instance of drawing element view.
        /// </summary>
        /// <param name="inputService">Input service.</param>
        /// <returns>Instance of drawing element.</returns>
        IDrawingElementView GetView(IInputService inputService);

        /// <summary>
        ///     Gets drawing element.
        /// </summary>
        /// <returns>Drawing element.</returns>
        IDrawingElement GetDrawingElement();
    }
}