using System.Collections.Generic;
using System.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Waves.Core.Base;
using Waves.Core.Base.Enums;
using Waves.Core.Base.EventArgs;
using Waves.Core.Base.Interfaces;
using Waves.UI.Drawing.Base;
using Waves.UI.Drawing.Base.Interfaces;
using Waves.UI.Drawing.Charting.Base;
using Waves.UI.Drawing.Charting.Base.Enums;
using Waves.UI.Drawing.Charting.Utils;
using Waves.UI.Drawing.Charting.ViewModel.Interfaces;
using Waves.UI.Drawing.ViewModel;

namespace Waves.UI.Drawing.Charting.ViewModel
{
    /// <summary>
    ///     Chart view model base.
    /// </summary>
    public class ChartPresenterViewModel : DrawingElementPresenterViewModel, IChartPresenterViewModel
    {
        private readonly object _axisLocker = new object();
        private readonly List<IDrawingObject> _axisSignaturesDrawingObjects = new List<IDrawingObject>();

        private readonly List<IDrawingObject> _axisTicksDrawingObjects = new List<IDrawingObject>();
        private WavesColor _borderColor = WavesColor.White;
        private float _borderThickness = 1;
        private float _currentXMax = 1;
        private float _currentXMin;
        private float _currentYMax = 1;
        private float _currentYMin = -1;

        private bool _hasDefaultTicks = true;
        private bool _isBorderVisible = true;
        private bool _isTitleVisible = true;
        private bool _isXAxisAdditionalTicksVisible = true;
        private bool _isXAxisDescriptionVisible = true;
        private bool _isXAxisPrimaryTicksVisible = true;
        private bool _isXAxisSignaturesVisible = true;
        private bool _isXAxisZeroLineVisible = true;
        private bool _isYAxisAdditionalTicksVisible = true;
        private bool _isYAxisDescriptionVisible = true;
        private bool _isYAxisPrimaryTicksVisible = true;
        private bool _isYAxisSignaturesVisible = true;
        private bool _isYAxisZeroLineVisible = true;
        private bool _isZoomEnabled = true;

        private TextStyle _textStyle = new TextStyle();

        private string _title = "New chart";
        private WavesColor _xAxisAdditionalTicksColor = WavesColor.LightGray;
        private int _xAxisAdditionalTicksCount = 4;
        private float[] _xAxisAdditionalTicksDashArray = {8.0f, 4.0f, 0.0f, 0.0f};
        private float _xAxisAdditionalTickThickness = 1;
        private string _xAxisName = "X axis";

        private WavesColor _xAxisPrimaryTicksColor = WavesColor.Gray;

        private int _xAxisPrimaryTicksCount = 4;

        private float[] _xAxisPrimaryTicksDashArray = {4.0f, 4.0f, 0.0f, 0.0f};

        private float _xAxisPrimaryTickThickness = 1;
        private string _xAxisUnit = "unit";
        private WavesColor _xAxisZeroLineColor = WavesColor.Black;
        private float[] _xAxisZeroLineDashArray;
        private float _xAxisZeroLineThickness = 1;
        private float _xMax = 1;

        private float _xMin;
        private WavesColor _yAxisAdditionalTicksColor = WavesColor.LightGray;
        private int _yAxisAdditionalTicksCount = 4;
        private float[] _yAxisAdditionalTicksDashArray = {8.0f, 4.0f, 0.0f, 0.0f};
        private float _yAxisAdditionalTickThickness = 1;
        private string _yAxisName = "Y axis";
        private WavesColor _yAxisPrimaryTicksColor = WavesColor.Gray;
        private int _yAxisPrimaryTicksCount = 4;
        private float[] _yAxisPrimaryTicksDashArray = {4.0f, 4.0f, 0.0f, 0.0f};
        private float _yAxisPrimaryTickThickness = 1;
        private string _yAxisUnit = "unit";
        private WavesColor _yAxisZeroLineColor = WavesColor.Black;
        private float[] _yAxisZeroLineDashArray;
        private float _yAxisZeroLineThickness = 1;
        private float _yMax = 1;
        private float _yMin = -1;

        /// <inheritdoc />
        public ChartPresenterViewModel(
            IWavesCore core, 
            IDrawingElement drawingElement)
            : base(core, drawingElement)
        {
            SetDefaultTicks();
        }

        /// <summary>
        ///     Gets or sets current XMin.
        /// </summary>
        [Reactive]
        public float CurrentXMin
        {
            get => _currentXMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentXMin, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets current XMax.
        /// </summary>
        [Reactive]
        public float CurrentXMax
        {
            get => _currentXMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentXMax, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets current YMin.
        /// </summary>
        [Reactive]
        public float CurrentYMin
        {
            get => _currentYMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentYMin, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets current YMax.
        /// </summary>
        [Reactive]
        public float CurrentYMax
        {
            get => _currentYMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _currentYMax, value);
                Update();
            }
        }

        /// <summary>
        ///     Gets or sets Axis ticks list.
        /// </summary>
        private List<AxisTick> AxisTicks { get; set; } = new List<AxisTick>();

        /// <inheritdoc />
        [Reactive]
        public bool IsZoomEnabled
        {
            get => _isZoomEnabled;
            set
            {
                this.RaiseAndSetIfChanged(ref _isZoomEnabled, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsTitleVisible
        {
            get => _isTitleVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isTitleVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisPrimaryTicksVisible
        {
            get => _isXAxisPrimaryTicksVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isXAxisPrimaryTicksVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisAdditionalTicksVisible
        {
            get => _isXAxisAdditionalTicksVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isXAxisAdditionalTicksVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisSignaturesVisible
        {
            get => _isXAxisSignaturesVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isXAxisSignaturesVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisZeroLineVisible
        {
            get => _isXAxisZeroLineVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isXAxisZeroLineVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisDescriptionVisible
        {
            get => _isXAxisDescriptionVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isXAxisDescriptionVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisPrimaryTicksVisible
        {
            get => _isYAxisPrimaryTicksVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isYAxisPrimaryTicksVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisAdditionalTicksVisible
        {
            get => _isYAxisAdditionalTicksVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isYAxisAdditionalTicksVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisSignaturesVisible
        {
            get => _isYAxisSignaturesVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isYAxisSignaturesVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisZeroLineVisible
        {
            get => _isYAxisZeroLineVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isYAxisZeroLineVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisDescriptionVisible
        {
            get => _isYAxisDescriptionVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isYAxisDescriptionVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsBorderVisible
        {
            get => _isBorderVisible;
            set
            {
                this.RaiseAndSetIfChanged(ref _isBorderVisible, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public bool IsXAxisZoomAroundZero { get; set; }

        /// <inheritdoc />
        [Reactive]
        public bool IsYAxisZoomAroundZero { get; set; }

        /// <inheritdoc />
        [Reactive]
        public string Title
        {
            get => _title;
            set
            {
                this.RaiseAndSetIfChanged(ref _title, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public string XAxisName
        {
            get => _xAxisName;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisName, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public string XAxisUnit
        {
            get => _xAxisUnit;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisUnit, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public string YAxisName
        {
            get => _yAxisName;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisName, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public string YAxisUnit
        {
            get => _yAxisUnit;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisUnit, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float XMin
        {
            get => _xMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _xMin, value);
                Update();
            }
        }

        /// <inheritdoc />
        public float XMax
        {
            get => _xMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _xMax, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float YMin
        {
            get => _yMin;
            set
            {
                this.RaiseAndSetIfChanged(ref _yMin, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float YMax
        {
            get => _yMax;
            set
            {
                this.RaiseAndSetIfChanged(ref _yMax, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public int XAxisPrimaryTicksCount
        {
            get => _xAxisPrimaryTicksCount;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisPrimaryTicksCount, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public int XAxisAdditionalTicksCount
        {
            get => _xAxisAdditionalTicksCount;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisAdditionalTicksCount, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public int YAxisPrimaryTicksCount
        {
            get => _yAxisPrimaryTicksCount;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisPrimaryTicksCount, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public int YAxisAdditionalTicksCount
        {
            get => _yAxisAdditionalTicksCount;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisAdditionalTicksCount, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float XAxisPrimaryTickThickness
        {
            get => _xAxisPrimaryTickThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisPrimaryTickThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float XAxisAdditionalTickThickness
        {
            get => _xAxisAdditionalTickThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisAdditionalTickThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float XAxisZeroLineThickness
        {
            get => _xAxisZeroLineThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisZeroLineThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float YAxisPrimaryTickThickness
        {
            get => _yAxisPrimaryTickThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisPrimaryTickThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float YAxisAdditionalTickThickness
        {
            get => _yAxisAdditionalTickThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisAdditionalTickThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float YAxisZeroLineThickness
        {
            get => _yAxisZeroLineThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisZeroLineThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        public float BorderThickness
        {
            get => _borderThickness;
            set
            {
                this.RaiseAndSetIfChanged(ref _borderThickness, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] XAxisPrimaryTicksDashArray
        {
            get => _xAxisPrimaryTicksDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisPrimaryTicksDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] XAxisAdditionalTicksDashArray
        {
            get => _xAxisAdditionalTicksDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisAdditionalTicksDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] XAxisZeroLineDashArray
        {
            get => _xAxisZeroLineDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisZeroLineDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] YAxisPrimaryTicksDashArray
        {
            get => _yAxisPrimaryTicksDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisPrimaryTicksDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] YAxisAdditionalTicksDashArray
        {
            get => _yAxisAdditionalTicksDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisAdditionalTicksDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public float[] YAxisZeroLineDashArray
        {
            get => _yAxisZeroLineDashArray;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisZeroLineDashArray, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor XAxisPrimaryTicksColor
        {
            get => _xAxisPrimaryTicksColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisPrimaryTicksColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor XAxisAdditionalTicksColor
        {
            get => _xAxisAdditionalTicksColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisAdditionalTicksColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor XAxisZeroLineColor
        {
            get => _xAxisZeroLineColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _xAxisZeroLineColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor YAxisPrimaryTicksColor
        {
            get => _yAxisPrimaryTicksColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisPrimaryTicksColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor YAxisAdditionalTicksColor
        {
            get => _yAxisAdditionalTicksColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisAdditionalTicksColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public WavesColor YAxisZeroLineColor
        {
            get => _yAxisZeroLineColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _yAxisZeroLineColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        public WavesColor BorderColor
        {
            get => _borderColor;
            set
            {
                this.RaiseAndSetIfChanged(ref _borderColor, value);
                Update();
            }
        }

        /// <inheritdoc />
        [Reactive]
        public TextStyle TextStyle
        {
            get => _textStyle;
            set
            {
                this.RaiseAndSetIfChanged(ref _textStyle, value);
                Update();
            }
        }
        
        /// <inheritdoc />
        public override void Draw(object element)
        {
            lock (_axisLocker)
            {
                GenerateAxisTicksDrawingObjects();
                GenerateAxisTicksSignaturesDrawingObjects();
            }

            base.Draw(element);

            DrawingElement?.Draw(element, DrawingObjects);
        }

        /// <inheritdoc />
        public sealed override void Update()
        {
            if (_hasDefaultTicks)
                SetDefaultTicks();

            OnRedrawRequested();
        }

        /// <inheritdoc />
        public void SetDefaultTicks()
        {
            _hasDefaultTicks = true;

            lock (_axisLocker)
            {
                AxisTicks.Clear();

                AxisTicks.GenerateAxisTicks(CurrentXMin, CurrentXMax, XAxisPrimaryTicksCount, XAxisAdditionalTicksCount,
                    AxisTickOrientation.Horizontal);

                AxisTicks.GenerateAxisTicks(CurrentYMin, CurrentYMax, YAxisPrimaryTicksCount, YAxisAdditionalTicksCount,
                    AxisTickOrientation.Vertical);
            }
        }

        /// <inheritdoc />
        public void SetTicks(List<AxisTick> ticks)
        {
            lock (_axisLocker)
            {
                _hasDefaultTicks = false;
                AxisTicks = ticks;
            }
        }

        /// <summary>
        ///     Actions when pointer state changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnInputServicePointerStateChanged(object sender, WavesPointerEventArgs e)
        {
            if (e.Type == WavesPointerEventType.VerticalScroll)
            {
                LastMouseDelta = (int) e.Delta.X;
                ZoomChart(LastMouseDelta, LastMousePosition);
            }

            if (e.Type == WavesPointerEventType.Enter)
            {
                IsMouseOver = true;
                Update();
            }

            if (e.Type == WavesPointerEventType.Leave)
            {
                IsMouseOver = false;
                Update();
            }

            if (e.Type == WavesPointerEventType.Enter || e.Type == WavesPointerEventType.Leave || e.Type == WavesPointerEventType.Move)
            {
                LastMousePosition = new WavesPoint(e.X, e.Y);
                Update();
            }

            if (e.Type == WavesPointerEventType.TouchZoom)
                if (IsZoomEnabled)
                {
                    LastMouseDelta = (int) e.Delta.X;
                    ZoomChart(LastMouseDelta, LastMousePosition);
                }

            if (e.Type == WavesPointerEventType.TouchMove)
            {
                LastMousePosition = new WavesPoint(e.X, e.Y);
                Update();
            }
        }

        /// <summary>
        ///     Actions when key pressed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnInputServiceKeyPressed(object sender, WavesKeyEventArgs e)
        {
        }

        /// <summary>
        ///     Actions when key released.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Arguments.</param>
        protected override void OnInputServiceKeyReleased(object sender, WavesKeyEventArgs e)
        {
        }

        /// <summary>
        ///     Generates axis ticks drawing objects.
        /// </summary>
        private void GenerateAxisTicksDrawingObjects()
        {
            foreach (var obj in _axisTicksDrawingObjects)
                DrawingObjects.Remove(obj);

            _axisTicksDrawingObjects.Clear();

            foreach (var tick in AxisTicks.ToList())
                if (tick.Orientation == AxisTickOrientation.Horizontal)
                {
                    if (tick.Type == AxisTickType.Primary)
                    {
                        if (!IsXAxisPrimaryTicksVisible) continue;

                        var obj = Ticks.GetXAxisTickLine(tick.Value, XAxisPrimaryTickThickness,
                            XAxisPrimaryTicksColor, XAxisPrimaryTicksDashArray, 0.5f, CurrentXMin, CurrentXMax,
                            Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }

                    else if (tick.Type == AxisTickType.Additional)
                    {
                        if (!IsXAxisAdditionalTicksVisible) continue;

                        var obj = Ticks.GetXAxisTickLine(tick.Value, XAxisAdditionalTickThickness,
                            XAxisAdditionalTicksColor, XAxisAdditionalTicksDashArray, 0.25f, CurrentXMin,
                            CurrentXMax, Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }

                    else if (tick.Type == AxisTickType.Zero)
                    {
                        if (!IsXAxisZeroLineVisible) continue;

                        var obj = Ticks.GetXAxisTickLine(tick.Value, XAxisZeroLineThickness,
                            XAxisZeroLineColor, XAxisZeroLineDashArray, 1f, CurrentXMin, CurrentXMax,
                            Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }
                }

                else if (tick.Orientation == AxisTickOrientation.Vertical)
                {
                    if (tick.Type == AxisTickType.Primary)
                    {
                        if (!IsYAxisPrimaryTicksVisible) continue;

                        var obj = Ticks.GetYAxisTickLine(tick.Value, YAxisPrimaryTickThickness,
                            YAxisPrimaryTicksColor, YAxisPrimaryTicksDashArray, 0.5f, CurrentYMin, CurrentYMax,
                            Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }

                    else if (tick.Type == AxisTickType.Additional)
                    {
                        if (!IsYAxisAdditionalTicksVisible) continue;

                        var obj = Ticks.GetYAxisTickLine(tick.Value, YAxisAdditionalTickThickness,
                            YAxisAdditionalTicksColor, YAxisAdditionalTicksDashArray, 0.25f, CurrentYMin,
                            CurrentYMax, Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }

                    else if (tick.Type == AxisTickType.Zero)
                    {
                        if (!IsYAxisZeroLineVisible) continue;

                        var obj = Ticks.GetYAxisTickLine(tick.Value, YAxisZeroLineThickness,
                            YAxisZeroLineColor, YAxisZeroLineDashArray, 1f, CurrentYMin, CurrentYMax,
                            Width, Height);

                        DrawingObjects.Add(obj);

                        _axisTicksDrawingObjects.Add(obj);
                    }
                }
        }

        /// <summary>
        ///     Generates axis tick signatures drawing objects.
        /// </summary>
        private void GenerateAxisTicksSignaturesDrawingObjects()
        {
            foreach (var obj in _axisSignaturesDrawingObjects)
                DrawingObjects.Remove(obj);
            _axisSignaturesDrawingObjects.Clear();

            var paint = new TextPaint
            {
                Fill = Foreground,
                IsAntialiased = true,
                Opacity = 1.0f,
                TextStyle = TextStyle
            };

            foreach (var tick in AxisTicks.ToList())
            {
                if (tick == null) continue;
                if (tick.Type != AxisTickType.Primary && tick.Type != AxisTickType.Zero) continue;

                if (tick.Orientation == AxisTickOrientation.Horizontal)
                {
                    if (!IsXAxisSignaturesVisible) continue;

                    var text = Signatures.GetXAxisSignature(tick.Value, tick.Description, paint,
                        CurrentXMin, CurrentXMax, Width, Height);

                    var size = DrawingElement.MeasureText(tick.Description, paint);

                    text.Location = new WavesPoint(text.Location.X - size.Width / 2, text.Location.Y);

                    var rectangle = Signatures.GetXAxisSignatureRectangle(
                        tick.Value,
                        Background,
                        XAxisPrimaryTicksColor,
                        0.8f,
                        1,
                        6,
                        size.Width,
                        size.Height,
                        CurrentXMin,
                        CurrentXMax,
                        Width,
                        Height);

                    DrawingObjects.Add(rectangle);
                    DrawingObjects.Add(text);

                    _axisSignaturesDrawingObjects.Add(rectangle);
                    _axisSignaturesDrawingObjects.Add(text);
                }

                if (tick.Orientation == AxisTickOrientation.Vertical)
                {
                    if (!IsYAxisSignaturesVisible) continue;

                    var text = Signatures.GetYAxisSignature(tick.Value, tick.Description, paint,
                        CurrentYMin, CurrentYMax, Width, Height);

                    var size = DrawingElement.MeasureText(tick.Description, paint);

                    text.Location = new WavesPoint(text.Location.X, text.Location.Y + size.Height / 2);

                    var rectangle = Signatures.GetYAxisSignatureRectangle(
                        tick.Value,
                        Background,
                        YAxisPrimaryTicksColor,
                        0.8f,
                        1,
                        6,
                        size.Width,
                        size.Height,
                        CurrentYMin,
                        CurrentYMax,
                        Width,
                        Height);

                    DrawingObjects.Add(rectangle);
                    DrawingObjects.Add(text);

                    _axisSignaturesDrawingObjects.Add(rectangle);
                    _axisSignaturesDrawingObjects.Add(text);
                }
            }
        }

        /// <summary>
        ///     Zooms chart.
        /// </summary>
        /// <param name="delta">Zoom delta.</param>
        /// <param name="position">Zoom position.</param>
        private void ZoomChart(int delta, WavesPoint position)
        {
            if (!IsMouseOver) return;
            if (!IsZoomEnabled) return;

            var deltaF = delta / 1000.0f;

            var x = Valuation.DenormalizePointX2D(position.X, Width, CurrentXMin, CurrentXMax);
            var y = Valuation.DenormalizePointY2D(position.Y, Height, CurrentYMin, CurrentYMax);
            
            if (float.IsInfinity(x))
                return;
            
            if (float.IsInfinity(y))
                return;

            if (IsCtrlPressed)
            {
                var yMin = 0.0f;
                var yMax = 0.0f;

                if (IsYAxisZoomAroundZero)
                {
                    yMin = -CurrentYMin * deltaF;
                    yMax = CurrentYMax * deltaF;
                }
                else
                {
                    yMin = (y - CurrentYMin) * deltaF;
                    yMax = (CurrentYMax - y) * deltaF;
                }

                if (CurrentYMax - yMax - (CurrentYMin + yMin) > (YMax - YMin) / 1000000)
                {
                    CurrentYMax -= yMax;
                    CurrentYMin += yMin;
                }

                if (CurrentYMin < YMin)
                    CurrentYMin = YMin;
                if (CurrentYMax > YMax)
                    CurrentYMax = YMax;

                Update();
                return;
            }

            if (IsShiftPressed)
            {
                ScrollChart(deltaF, x, y);

                Update();
                return;
            }

            var xMin = (x - CurrentXMin) * deltaF;
            var xMax = (CurrentXMax - x) * deltaF;

            if (CurrentXMax - xMax - (CurrentXMin + xMin) > (XMax - XMin) / 1000000)
            {
                CurrentXMax -= xMax;
                CurrentXMin += xMin;
            }

            if (CurrentXMin < XMin)
                CurrentXMin = XMin;
            if (CurrentXMax > XMax)
                CurrentXMax = XMax;

            Update();
        }

        /// <summary>
        ///     Scrolls chart along the X axis.
        /// </summary>
        /// <param name="delta">Scrolling delta.</param>
        /// <param name="x">Scroll value along the X axis.</param>
        /// <param name="y">Scroll value along the Y axis.</param>
        private void ScrollChart(float delta, float x, float y)
        {
            var xMin = delta * 10f;
            var xMax = delta * 10f;

            if (CurrentXMax + xMax > XMax)
                return;
            if (CurrentXMin + xMin < XMin)
                return;

            CurrentXMax += xMax;
            CurrentXMin += xMin;

            if (CurrentXMin < XMin)
                CurrentXMin = XMin;
            if (CurrentXMax > XMax)
                CurrentXMax = XMax;
        }
    }
}