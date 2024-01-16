using LedDigital.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LedDigital.Controls
{
    /// <summary>
    /// 依照步驟 1a 或 1b 執行，然後執行步驟 2，以便在 XAML 檔中使用此自訂控制項。
    ///
    /// 步驟 1a) 於存在目前專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:LedDigital.Controls"
    ///
    ///
    /// 步驟 1b) 於存在其他專案的 XAML 檔中使用此自訂控制項。
    /// 加入此 XmlNamespace 屬性至標記檔案的根項目為 
    /// 要使用的: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:LedDigital.Controls;assembly=LedDigital.Controls"
    ///
    /// 您還必須將 XAML 檔所在專案的專案參考加入
    /// 此專案並重建，以免發生編譯錯誤: 
    ///
    ///     在 [方案總管] 中以滑鼠右鍵按一下目標專案，並按一下
    ///     [加入參考]->[專案]->[瀏覽並選取此專案]
    ///
    ///
    /// 步驟 2)
    /// 開始使用 XAML 檔案中的控制項。
    ///
    ///     <MyNamespace:LedDigital/>
    ///
    /// </summary>
    public class LedDigital : Control
    {
        #region Fields

        private Panel _rootPanel;

        private Path _topSegment;
        private Path _upLeftSegment;
        private Path _upRightSegment;
        private Path _middleSegment;
        private Path _downLeftSegment;
        private Path _downRightSegment;
        private Path _bottomSegment;

        private Path _dotSegment;
        private Path _colonUpSegment;
        private Path _colonDownSegment;

        private ILedDigitalSegmentCreator _segementCreator = null;

        #endregion

        #region Dependency properties

        /// <summary>
        /// Dependency property to Get/Set the current value 
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LedDigital), new PropertyMetadata(null, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// 依赖属性-LED显示颜色
        /// </summary>
        public static readonly DependencyProperty DigitalBrushProperty =
            DependencyProperty.Register("DigitalBrush", typeof(Brush), typeof(LedDigital), new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnDigitalBrushPropertyChanged)));

        /// <summary>
        /// LED高度
        /// </summary>
        public static readonly DependencyProperty DigitalHeightProperty =
            DependencyProperty.Register("DigitalHeight", typeof(double), typeof(LedDigital), new PropertyMetadata(40.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// LED的宽度
        /// </summary>
        public static readonly DependencyProperty DigitalWidthProperty =
            DependencyProperty.Register("DigitalWidth", typeof(double), typeof(LedDigital), new PropertyMetadata(20.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// LED字体的粗细
        /// </summary>
        public static readonly DependencyProperty DigitalThicknessProperty =
            DependencyProperty.Register("DigitalThickness", typeof(double), typeof(LedDigital), new PropertyMetadata(5.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// Segment间的距离
        /// </summary>
        public static readonly DependencyProperty SegmentIntervalProperty =
            DependencyProperty.Register("SegmentInterval", typeof(double), typeof(LedDigital), new PropertyMetadata(2.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// segment两端的截断长度
        /// </summary>
        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(double), typeof(LedDigital), new PropertyMetadata(2.0, new PropertyChangedCallback(OnSizePropertyChanged)));

        /// <summary>
        /// 数字片段非高亮状态下的透明度
        /// </summary>
        public static readonly DependencyProperty DigitalDimOpacityProperty =
            DependencyProperty.Register("DigitalDimOpacity", typeof(double), typeof(LedDigital), new PropertyMetadata(0.05, OnDigitalBrushPropertyChanged));

        /// <summary>
        /// 数字片段非高亮状态下的颜色
        /// </summary>
        public static readonly DependencyProperty DigitalDimBrushProperty =
            DependencyProperty.Register("DigitalDimBrush", typeof(Brush), typeof(LedDigital), new PropertyMetadata(new SolidColorBrush(Colors.Red), OnDigitalBrushPropertyChanged));

        #endregion

        #region Dependency Property Wrappers

        /// <summary>
        /// Gets/Sets the current value
        /// </summary>
        public string Value
        {
            get
            {
                return (string)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// Gets/Sets the digital brush
        /// </summary>
        public Brush DigitalBrush
        {
            get
            {
                return (Brush)GetValue(DigitalBrushProperty);
            }
            set
            {
                SetValue(DigitalBrushProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED高度
        /// </summary>
        public double DigitalHeight
        {
            get
            {
                return (double)GetValue(DigitalHeightProperty);
            }
            set
            {
                SetValue(DigitalHeightProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED宽度
        /// </summary>
        public double DigitalWidth
        {
            get
            {
                return (double)GetValue(DigitalWidthProperty);
            }
            set
            {
                SetValue(DigitalWidthProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED字体粗细
        /// </summary>
        public double DigitalThickness
        {
            get
            {
                return (double)GetValue(DigitalThicknessProperty);
            }
            set
            {
                SetValue(DigitalThicknessProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED segment间距
        /// </summary>
        public double SegmentInterval
        {
            get
            {
                return (double)GetValue(SegmentIntervalProperty);
            }
            set
            {
                SetValue(SegmentIntervalProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置LED截断宽度
        /// </summary>
        public double BevelWidth
        {
            get
            {
                return (double)GetValue(BevelWidthProperty);
            }
            set
            {
                SetValue(BevelWidthProperty, value);
            }
        }

        public double DigitalDimOpacity
        {
            get { return (double)GetValue(DigitalDimOpacityProperty); }
            set { SetValue(DigitalDimOpacityProperty, value); }
        }

        public Brush DigitalDimBrush
        {
            get { return (Brush)GetValue(DigitalDimBrushProperty); }
            set { SetValue(DigitalDimBrushProperty, value); }
        }

        #endregion

        #region Constructor

        static LedDigital()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LedDigital), new FrameworkPropertyMetadata(typeof(LedDigital)));
        }

        #endregion

        #region Property Changed Callbacks

        /// <summary>
        /// 控件属性值发生变化时调用的方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "Height" || e.Property.Name == "ActualHeight")
            {
                DigitalHeight = (double)e.NewValue;
            }
            else if (e.Property.Name == "Width" || e.Property.Name == "ActualWidth")
            {
                DigitalWidth = (double)e.NewValue - DigitalThickness;
            }
        }

        /// <summary>
        /// 当前值发生变化时候调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigital led = d as LedDigital;
            if (led != null)
            {
                led.SetDisplayDigitalValue(led.Value);
            }
        }

        /// <summary>
        /// LED颜色发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDigitalBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigital led = d as LedDigital;
            if (led != null)
            {
                led.UpdateAllSegmentsBrush();
            }
        }

        /// <summary>
        /// 当led形状参数发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigital led = d as LedDigital;

            if (led == null || led._rootPanel == null)
            {
                return;
            }

            led._rootPanel.Children.Clear();

            //初始化数字片段生成器
            DigitalParameter dp = led.GetDigitalParameter();
            led._segementCreator = new BaseLedDigitalSegmentCreator(dp);

            //初始化数字
            led.InitAllDigitalSegments();

            //设置初始值
            led.SetDisplayDigitalValue(led.Value);
        }

        #endregion

        #region Method

        /// <summary>
        /// 调用模板时的方法
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //获取根容器
            _rootPanel = GetTemplateChild("PART_Root") as Panel;

            //初始化数字片段生成器
            DigitalParameter dp = GetDigitalParameter();
            _segementCreator = new BaseLedDigitalSegmentCreator(dp);

            //初始化数字
            InitAllDigitalSegments();

            //设置初始值
            SetDisplayDigitalValue(Value);
        }

        private DigitalParameter GetDigitalParameter()
        {
            DigitalParameter dp = new DigitalParameter();
            dp.BevelWidth = BevelWidth;
            dp.SegmentInterval = SegmentInterval;
            dp.SegmentThickness = DigitalThickness;
            dp.DigitalHeight = DigitalHeight;
            dp.DigitalWidth = DigitalWidth;

            return dp;
        }

        /// <summary>
        /// 初始化Segments的点集
        /// </summary>
        private void InitAllDigitalSegments()
        {
            if (_segementCreator == null)
            {
                throw new Exception("The segment creator is null.");
            }

            if (_rootPanel == null)
            {
                throw new Exception("The root panel is null.");
            }

            InitDigitalSegment(_segementCreator.GetTopSegment(), ref _topSegment);
            InitDigitalSegment(_segementCreator.GetUpLeftSegment(), ref _upLeftSegment);
            InitDigitalSegment(_segementCreator.GetUpRightSegment(), ref _upRightSegment);
            InitDigitalSegment(_segementCreator.GetMiddleSegment(), ref _middleSegment);
            InitDigitalSegment(_segementCreator.GetDownLeftSegment(), ref _downLeftSegment);
            InitDigitalSegment(_segementCreator.GetDownRightSegment(), ref _downRightSegment);
            InitDigitalSegment(_segementCreator.GetBottomSegment(), ref _bottomSegment);

            InitDigitalSegment(_segementCreator.GetDotSegment(), ref _dotSegment);
            InitDigitalSegment(_segementCreator.GetUpColonSegment(), ref _colonUpSegment);
            InitDigitalSegment(_segementCreator.GetDownColonSegment(), ref _colonDownSegment);
        }

        /// <summary>
        /// 将数字片段添加到显示容器
        /// </summary>
        /// <param name="dd"></param>
        private void InitDigitalSegment(List<Point> points, ref Path path)
        {
            Path segment = null;
            if (points.Count > 1)
            {
                segment = GraphicsPloter.DrawLine(points, DigitalBrush);
            }
            else if (points.Count == 1)
            {
                segment = GraphicsPloter.DrawEllipse(points[0], DigitalThickness / 2, DigitalBrush);
            }

            path = segment;
            _rootPanel.Children.Add(segment);
        }

        /// <summary>
        /// 设置segment的状态
        /// </summary>
        /// <param name="top"></param>
        /// <param name="upRight"></param>
        /// <param name="downRight"></param>
        /// <param name="bottom"></param>
        /// <param name="downLeft"></param>
        /// <param name="upLeft"></param>
        /// <param name="middle"></param>
        /// <param name="dot"></param>
        /// <param name="colon"></param>
        private void HighLightAllDigitalSegments(bool top, bool upRight, bool downRight, bool bottom, bool downLeft, bool upLeft, bool middle, bool dot, bool colon)
        {
            LightSingleSegment(_topSegment, top);
            LightSingleSegment(_upLeftSegment, upLeft);
            LightSingleSegment(_upRightSegment, upRight);
            LightSingleSegment(_middleSegment, middle);
            LightSingleSegment(_downLeftSegment, downLeft);
            LightSingleSegment(_downRightSegment, downRight);
            LightSingleSegment(_bottomSegment, bottom);

            LightSingleSegment(_dotSegment, dot);
            LightSingleSegment(_colonUpSegment, colon);
            LightSingleSegment(_colonDownSegment, colon);
        }

        private void LightSingleSegment(Path segment, bool isHighLight)
        {
            if (segment == null)
            {
                return;
            }

            if (isHighLight)
            {
                segment.Fill = DigitalBrush;
                segment.Opacity = 1;
            }
            else
            {
                segment.Fill = DigitalDimBrush;
                segment.Opacity = 0.05;
            }
        }

        /// <summary>
        /// 根据输入字符显示相应的字符
        /// </summary>
        /// <param name="digital"></param>
        private void SetDisplayDigitalValue(string digital)
        {
            switch (digital)
            {
                case null:
                case "":
                case " ":
                    HighLightAllDigitalSegments(false, false, false, false, false, false, false, false, false);
                    break;
                case "0":
                    HighLightAllDigitalSegments(true, true, true, true, true, true, false, false, false);
                    break;
                case "1":
                    HighLightAllDigitalSegments(false, true, true, false, false, false, false, false, false);
                    break;
                case "2":
                    HighLightAllDigitalSegments(true, true, false, true, true, false, true, false, false);
                    break;
                case "3":
                    HighLightAllDigitalSegments(true, true, true, true, false, false, true, false, false);
                    break;
                case "4":
                    HighLightAllDigitalSegments(false, true, true, false, false, true, true, false, false);
                    break;
                case "5":
                    HighLightAllDigitalSegments(true, false, true, true, false, true, true, false, false);
                    break;
                case "6":
                    HighLightAllDigitalSegments(true, false, true, true, true, true, true, false, false);
                    break;
                case "7":
                    HighLightAllDigitalSegments(true, true, true, false, false, false, false, false, false);
                    break;
                case "8":
                    HighLightAllDigitalSegments(true, true, true, true, true, true, true, false, false);
                    break;
                case "9":
                    HighLightAllDigitalSegments(true, true, true, true, false, true, true, false, false);
                    break;
                case "0.":
                    HighLightAllDigitalSegments(true, true, true, true, true, true, false, true, false);
                    break;
                case "1.":
                    HighLightAllDigitalSegments(false, true, true, false, false, false, false, true, false);
                    break;
                case "2.":
                    HighLightAllDigitalSegments(true, true, false, true, true, false, true, true, false);
                    break;
                case "3.":
                    HighLightAllDigitalSegments(true, true, true, true, false, false, true, true, false);
                    break;
                case "4.":
                    HighLightAllDigitalSegments(false, true, true, false, false, true, true, true, false);
                    break;
                case "5.":
                    HighLightAllDigitalSegments(true, false, true, true, false, true, true, true, false);
                    break;
                case "6.":
                    HighLightAllDigitalSegments(true, false, true, true, true, true, true, true, false);
                    break;
                case "7.":
                    HighLightAllDigitalSegments(true, true, true, false, false, false, false, true, false);
                    break;
                case "8.":
                    HighLightAllDigitalSegments(true, true, true, true, true, true, true, true, false);
                    break;
                case "9.":
                    HighLightAllDigitalSegments(true, true, true, true, false, true, true, true, false);
                    break;
                case ":":
                case "：":
                    HighLightAllDigitalSegments(false, false, false, false, false, false, false, false, true);
                    break;
                case "-":
                    HighLightAllDigitalSegments(false, false, false, false, false, false, true, false, false);
                    break;
                default:
                    throw new Exception("输入字符错误！");
            }
        }

        private void UpdateAllSegmentsBrush()
        {
            UpdateSegmentBrush(_topSegment);
            UpdateSegmentBrush(_upLeftSegment);
            UpdateSegmentBrush(_upRightSegment);
            UpdateSegmentBrush(_middleSegment);
            UpdateSegmentBrush(_downLeftSegment);
            UpdateSegmentBrush(_downRightSegment);
            UpdateSegmentBrush(_bottomSegment);

            UpdateSegmentBrush(_dotSegment);
            UpdateSegmentBrush(_colonUpSegment);
            UpdateSegmentBrush(_colonDownSegment);
        }

        private void UpdateSegmentBrush(Path segment)
        {
            if (segment == null)
            {
                return;
            }

            if (segment.Opacity == 1)
            {
                segment.Fill = DigitalBrush;
            }
            else
            {
                segment.Fill = DigitalDimBrush;
            }
        }

        #endregion
    }
}
