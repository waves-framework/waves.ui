using System;
using System.Collections.Generic;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.EventArgs;
using Waves.Core.Base.Interfaces;
using Waves.Core.Base.Interfaces.Services;
using Waves.Presentation.Base;
using Waves.UI.Drawing.Base.Interfaces;
using Waves.UI.Drawing.ViewModel.Interfaces;

namespace Waves.UI.Drawing.ViewModel
{
    /// <summary>
    ///     Drawing element view model base.
    /// </summary>
    public class DrawingElementPresenterViewModel : PresenterViewModel, IDrawingElementPresenterViewModel
    {
        private readonly object _collectionLocker = new object();

        private IInputService _inputService;
        
        private WavesColor _background = WavesColor.White;
        private WavesColor _foreground = WavesColor.Black;

        /// <summary>
        ///     Creates new instance of <see cref="DrawingElementPresenterViewModel" />.
        /// </summary>
        public DrawingElementPresenterViewModel(
            IWavesCore core, 
            IDrawingElement drawingElement) 
            : base(core)
        {
            DrawingElement = drawingElement;
            
            SubscribeEvents();
        }

        /// <summary>
        ///     Redrawing requested event handler.
        /// </summary>
        public event EventHandler RedrawRequested;

        /// <inheritdoc />
        public override Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public override string Name { get; set; } = "Drawing Element Presenter View Model";

        /// <summary>
        ///     Gets or sets whether is drawing initialized.
        /// </summary>
        [Reactive]
        public bool IsDrawingInitialized { get; set; }

        /// <summary>
        ///     Gets or sets width.
        /// </summary>
        [Reactive]
        public float Width { get; set; }

        /// <summary>
        ///     Gets or sets height.
        /// </summary>
        [Reactive]
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
        protected WavesPoint LastMousePosition { get; set; }

        /// <inheritdoc />
        [Reactive]
        public WavesColor Foreground
        {
            get => _foreground;
            set => this.RaiseAndSetIfChanged(ref _foreground, value);
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor Background
        {
            get => _background;
            set => this.RaiseAndSetIfChanged(ref _background, value);
        }

        /// <summary>
        ///     Gets or sets drawing element.
        /// </summary>
        public IDrawingElement DrawingElement { get; set; }

        /// <inheritdoc />
        [Reactive]
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
        public override void Dispose()
        {
            DrawingElement?.Dispose();
            UnsubscribeEvents();
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
        protected virtual void OnInputServicePointerStateChanged(object sender, WavesPointerEventArgs e)
        {
        }

        /// <summary>
        ///     Actions when key pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnInputServiceKeyPressed(object sender, WavesKeyEventArgs e)
        {
            if (e.Key == WavesVirtualKey.Control)
                IsCtrlPressed = true;
            if (e.Key == WavesVirtualKey.Shift)
                IsShiftPressed = true;
        }

        /// <summary>
        ///     Actions when key released.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected virtual void OnInputServiceKeyReleased(object sender, WavesKeyEventArgs e)
        {
            if (e.Key == WavesVirtualKey.Control)
                IsCtrlPressed = false;
            if (e.Key == WavesVirtualKey.Shift)
                IsShiftPressed = false;
        }
        
        /// <summary>
        /// Subscribes to events.
        /// </summary>
        private void SubscribeEvents()
        {
            if (DrawingElement != null)
                DrawingElement.MessageReceived += OnDrawingElementMessageReceived;
        }

        /// <summary>
        /// Unsubscribes from events.
        /// </summary>
        private void UnsubscribeEvents()
        {
            if (DrawingElement != null)
                DrawingElement.MessageReceived -= OnDrawingElementMessageReceived;
        }
        
        /// <summary>
        /// Actions when message received from drawing element.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        private void OnDrawingElementMessageReceived(object sender, IWavesMessage e)
        {
            OnMessageReceived(this,e);
        }
    }
}