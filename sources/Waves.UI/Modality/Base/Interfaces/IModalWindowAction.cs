using System;
using Waves.UI.Base.Interfaces;

namespace Waves.UI.Modality.Base.Interfaces
{
    /// <summary>
    /// Interface for modal window action.
    /// </summary>
    public interface IModalWindowAction
    {
        /// <summary>
        /// Gets whether modal window action is accent.
        /// </summary>
        bool IsAccent { get; }

        /// <summary>
        /// Gets or sets whether action is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets icon.
        /// </summary>
        IVectorImage Icon { get; }

        /// <summary>
        /// Gets caption.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets tooltip info.
        /// </summary>
        string ToolTip { get; }

        /// <summary>
        /// Action.
        /// </summary>
        Action Action { get; }
    }
}