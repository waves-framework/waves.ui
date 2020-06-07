using System;
using System.Windows.Input;
using Waves.Core.Base;
using Waves.UI.Base.Interfaces;
using Waves.UI.Modality.Base.Interfaces;

namespace Waves.UI.Modality.Base
{
    /// <summary>
    /// Base class of modality window action.
    /// </summary>
    public abstract class ModalWindowAction : ObservableObject, IModalWindowAction
    {
        /// <summary>
        /// Creates new instance of <see cref="ModalWindowAction"/>. 
        /// </summary>
        /// <param name="caption">Caption.</param>
        /// <param name="action">Action.</param>
        /// <param name="icon">Icon.</param>
        /// <param name="isAccent">Is accent.</param>
        /// <param name="toolTip">Tool tip.</param>
        protected ModalWindowAction(string caption, Action action, IVectorImage icon = null, bool isAccent = false, string toolTip = null)
        {
            Caption = caption;
            ToolTip = !string.IsNullOrEmpty(toolTip) ? toolTip : caption;
            Icon = icon;
            Action = action;
            IsAccent = isAccent;
        }

        /// <inheritdoc />
        public bool IsAccent { get; protected set; }

        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <inheritdoc />
        public IVectorImage Icon { get; protected set; }

        /// <inheritdoc />
        public string Caption { get; protected set; }

        /// <inheritdoc />
        public string ToolTip { get; protected set; }

        /// <inheritdoc />
        public Action Action { get; protected set; }

        /// <summary>
        /// Gets command for action.
        /// </summary>
        public ICommand Command { get; protected set; }
    }
}