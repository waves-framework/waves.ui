﻿using System;
using System.Collections.Generic;
using PropertyChanged;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.EventArgs;
using Waves.Core.Services.Interfaces;
using Waves.Presentation.Base;
using Waves.UI.Drawing.Base.Interfaces;
using Waves.UI.Drawing.ViewModel.Interfaces;

namespace Waves.UI.Drawing.ViewModel
{
    /// <summary>
    ///     Drawing element view model base.
    /// </summary>
    public class DrawingElementViewModel : PresentationViewModel, IDrawingElementViewModel
    {
        private readonly object _collectionLocker = new object();

        private IInputService _inputService;

        /// <summary>
        ///     Creates new instance of <see cref="DrawingElementViewModel" />.
        /// </summary>
        public DrawingElementViewModel(IDrawingElement drawingElement)
        {
            DrawingElement = drawingElement;
        }

        /// <summary>
        ///     Gets or sets whether is drawing initialized.
        /// </summary>
        public bool IsDrawingInitialized { get; set; }

        /// <summary>
        ///     Gets or sets width.
        /// </summary>
        public float Width { get; set; }

        /// <summary>
        ///     Gets or sets height.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        ///     Gets or sets input service.
        /// </summary>
        public IInputService InputService
        {
            get => _inputService;
            set
            {
                if (_inputService != null)
                {
                    _inputService.PointerStateChanged -= OnInputServicePointerStateChanged;
                    _inputService.KeyPressed -= OnInputServiceKeyPressed;
                    _inputService.KeyReleased -= OnInputServiceKeyReleased;
                }

                _inputService = value;

                if (_inputService != null)
                {
                    _inputService.PointerStateChanged += OnInputServicePointerStateChanged;
                    _inputService.KeyPressed += OnInputServiceKeyPressed;
                    _inputService.KeyReleased += OnInputServiceKeyReleased;
                }
            }
        }

        /// <summary>
        ///     Gets or sets whether CTRL key is pressed.
        /// </summary>
        protected bool IsCtrlPressed { get; set; }

        /// <summary>
        ///     Gets or sets whether SHIFT key is pressed.
        /// </summary>
        protected bool IsShiftPressed { get; set; }

        /// <summary>
        ///     Gets or sets whether is mouse over chart.
        /// </summary>
        protected bool IsMouseOver { get; set; }

        /// <summary>
        ///     Gets or sets last mouse delta.
        /// </summary>
        protected int LastMouseDelta { get; set; }

        /// <summary>
        ///     Gets or sets last mouse position.
        /// </summary>
        protected Point LastMousePosition { get; set; }

        /// <inheritdoc />
        public Color Foreground { get; set; } = Color.Black;

        /// <inheritdoc />
        public Color Background { get; set; } = Color.White;

        /// <summary>
        ///     Gets or sets drawing element.
        /// </summary>
        public IDrawingElement DrawingElement { get; set; }

        /// <inheritdoc />
        public ICollection<IDrawingObject> DrawingObjects { get; } = new List<IDrawingObject>();

        /// <inheritdoc />
        public override void Initialize()
        {
        }

        /// <inheritdoc />
        public void AddDrawingObject(IDrawingObject obj)
        {
            lock (_collectionLocker)
            {
                DrawingObjects.Add(obj);
            }

            OnRedrawRequested();
        }

        /// <inheritdoc />
        public void RemoveDrawingObject(IDrawingObject obj)
        {
            lock (_collectionLocker)
            {
                DrawingObjects.Remove(obj);
            }

            OnRedrawRequested();
        }

        /// <inheritdoc />
        public virtual void Update()
        {
            OnRedrawRequested();
        }

        /// <summary>
        ///     Draws objects.
        /// </summary>
        public virtual void Draw(object element)
        {
            DrawingElement?.Draw(element, DrawingObjects);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DrawingElement?.Dispose();
        }

        /// <inheritdoc />
        public void Clear()
        {
            lock (_collectionLocker)
            {
                DrawingObjects.Clear();
            }

            Update();
        }

        /// <summary>
        ///     Redrawing requested event handler.
        /// </summary>
        public event EventHandler RedrawRequested;

        /// <summary>
        ///     Notifies when redrawing requested.
        /// </summary>
        protected virtual void OnRedrawRequested()
        {
            RedrawRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Actions when pointer state changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        [SuppressPropertyChangedWarnings]
        protected virtual void OnInputServicePointerStateChanged(object sender, PointerEventArgs e)
        {
        }

        /// <summary>
        ///     Actions when key pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnInputServiceKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == VirtualKey.Control)
                IsCtrlPressed = true;
            if (e.Key == VirtualKey.Shift)
                IsShiftPressed = true;
        }

        /// <summary>
        ///     Actions when key released.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnInputServiceKeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Key == VirtualKey.Control)
                IsCtrlPressed = false;
            if (e.Key == VirtualKey.Shift)
                IsShiftPressed = false;
        }
    }
}