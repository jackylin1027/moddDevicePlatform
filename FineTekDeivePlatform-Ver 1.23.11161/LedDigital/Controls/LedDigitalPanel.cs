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
    ///     <MyNamespace:LedDigitalPanel/>
    ///
    /// </summary>
    public class LedDigitalPanel : Control
    {
        #region Field

        private Panel rootPanel;
        private List<LedDigital> digitalsList = new List<LedDigital>();

        #endregion

        #region Dependency properties

        /// <summary>
        /// 容器中LED Digital的个数
        /// </summary>
        public static readonly DependencyProperty DigitalCountProperty =
            DependencyProperty.Register("DigitalCount", typeof(int), typeof(LedDigitalPanel),
                new PropertyMetadata(4, new PropertyChangedCallback(OnDigitalCountPropertyChanged)));

        /// <summary>
        /// 依赖属性-获取或设置当前显示的值
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(LedDigitalPanel),
            new PropertyMetadata(null, new PropertyChangedCallback(OnValuePropertyChanged)));

        /// <summary>
        /// 依赖属性-LED Digital显示颜色
        /// </summary>
        public static readonly DependencyProperty DigitalBrushProperty =
            DependencyProperty.Register("DigitalBrush", typeof(Brush), typeof(LedDigitalPanel),
            new PropertyMetadata(new SolidColorBrush(Colors.Red), new PropertyChangedCallback(OnDigitalBrushPropertyChange)));

        public static readonly DependencyProperty DigitalDimBrushProperty
            = DependencyProperty.Register("DigitalDimBrush", typeof(Brush), typeof(LedDigitalPanel), new PropertyMetadata(new SolidColorBrush(Colors.Red), OnDigitalDimBrushPropertyChanged));

        public static readonly DependencyProperty DigitalDimOpacityProperty
            = DependencyProperty.Register("DigitalDimOpacity", typeof(double), typeof(LedDigitalPanel), new PropertyMetadata(0.05, OnDigitalDimOpacityPropertyChanged));

        /// <summary>
        /// LED Digital字体的粗细
        /// </summary>
        public static readonly DependencyProperty DigitalThicknessProperty =
            DependencyProperty.Register("DigitalThickness", typeof(double), typeof(LedDigitalPanel),
                new PropertyMetadata(5.0, new PropertyChangedCallback(OnThicknessPropertyChanged)));

        /// <summary>
        /// Segment间的距离
        /// </summary>
        public static readonly DependencyProperty SegmentIntervalProperty =
            DependencyProperty.Register("SegmentInterval", typeof(double), typeof(LedDigitalPanel),
                new PropertyMetadata(2.0, new PropertyChangedCallback(OnSegmentIntervalPropertyChanged)));

        /// <summary>
        /// segment两端的截断长度
        /// </summary>
        public static readonly DependencyProperty BevelWidthProperty =
            DependencyProperty.Register("BevelWidth", typeof(double), typeof(LedDigitalPanel),
                new PropertyMetadata(2.0, new PropertyChangedCallback(OnBevelWidthPropertyChanged)));

        #endregion

        #region Wrapper properties

        /// <summary>
        /// 获取或设置容器中LED的个数
        /// </summary>
        public int DigitalCount
        {
            get
            {
                return (int)GetValue(DigitalCountProperty);
            }
            set
            {
                SetValue(DigitalCountProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置显示的值
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
        /// 获取或设置LED颜色
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

        public Brush DigitalDimBrush
        {
            get
            {
                return (Brush)GetValue(DigitalDimBrushProperty);
            }
            set
            {
                SetValue(DigitalDimBrushProperty, value);
            }
        }

        public double DigitalDimOpacity
        {
            get
            {
                return (double)GetValue(DigitalDimOpacityProperty);
            }
            set
            {
                SetValue(DigitalDimOpacityProperty, value);
            }
        }

        /// <summary>
        /// 获取或设置字体粗细
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
        /// 获取或设置间距
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
        /// 获取或设置截断长度
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

        #endregion

        #region Constructor

        static LedDigitalPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LedDigitalPanel), new FrameworkPropertyMetadata(typeof(LedDigitalPanel)));
        }

        #endregion

        #region Dependency Property Changed Callback

        /// <summary>
        /// 控件属性值发生变化时调用的方法
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "Height" || e.Property.Name == "ActualHeight")
            {
                for (int i = 0; i < digitalsList.Count; i++)
                {
                    digitalsList[i].Height = (double)e.NewValue;
                }
            }
            else if (e.Property.Name == "Width" || e.Property.Name == "ActualWidth")
            {
                double ledWidth = (double)e.NewValue / DigitalCount;
                for (int i = 0; i < digitalsList.Count; i++)
                {
                    digitalsList[i].Width = ledWidth;
                }
            }
        }

        /// <summary>
        /// 当Led数量发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDigitalCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;
            leds.digitalsList.Clear();

            if (leds.rootPanel != null)
            {
                leds.rootPanel.Children.Clear();
                leds.InitDigitals((int)e.NewValue);

                //将Digitals 加入到rootPanel中
                foreach (LedDigital digital in leds.digitalsList)
                {
                    leds.rootPanel.Children.Add(digital);
                }

                //显示值
                leds.DisplayData(leds.Value);
            }
        }

        /// <summary>
        /// 控件的值发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;
            string newValue = (string)e.NewValue;
            leds.DisplayData(newValue);
        }

        /// <summary>
        /// LED颜色发生变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnDigitalBrushPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].DigitalBrush = (Brush)e.NewValue;
            }
        }

        private static void OnDigitalDimBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].DigitalDimBrush = (Brush)e.NewValue;
            }
        }

        private static void OnDigitalDimOpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].DigitalDimOpacity = (double)e.NewValue;
            }
        }

        /// <summary>
        /// 字体粗细变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].DigitalThickness = (double)e.NewValue;
            }
        }

        /// <summary>
        /// 间距变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSegmentIntervalPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].SegmentInterval = (double)e.NewValue;
            }
        }

        /// <summary>
        /// 截断长度变化时调用的方法
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnBevelWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LedDigitalPanel leds = d as LedDigitalPanel;

            for (int i = 0; i < leds.digitalsList.Count; i++)
            {
                leds.digitalsList[i].BevelWidth = (double)e.NewValue;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 调用模板时的方法
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //添加Led
            InitDigitals(DigitalCount);

            //获取根布局
            rootPanel = GetTemplateChild("PART_Root") as Panel;
            if (rootPanel != null)
            {
                //将Digitals 加入到rootPanel中
                foreach (LedDigital digital in digitalsList)
                {
                    rootPanel.Children.Add(digital);
                }
            }

            DisplayData(Value);
        }

        /// <summary>
        /// 添加Led Digitals
        /// </summary>
        /// <param name="digitalCount"></param>
        private void InitDigitals(int digitalCount)
        {
            digitalsList.Clear();
            double ledControlWidth = Width / digitalCount;
            double ledControlHeight = Height;

            for (int i = 0; i < digitalCount; i++)
            {
                LedDigital led = new LedDigital();
                led.Width = ledControlWidth;
                led.Height = ledControlHeight;
                led.SegmentInterval = SegmentInterval;
                led.BevelWidth = BevelWidth;
                led.DigitalThickness = DigitalThickness;

                led.DigitalBrush = DigitalBrush;
                led.DigitalDimBrush = DigitalDimBrush;
                led.DigitalDimOpacity = DigitalDimOpacity;

                digitalsList.Add(led);
            }
        }

        /// <summary>
        /// 显示值
        /// </summary>
        /// <param name="value"></param>
        private void DisplayData(string value)
        {
            if (value == null)
            {
                return;
            }

            //准备字符
            List<string> showStringList = ConvertStringToSingleDigitalCharList(value);

            //显示文字
            if (digitalsList.Count < showStringList.Count)//要显示的字符个数大于显示器的个数，截断尾数
            {
                for (int i = 0; i < digitalsList.Count; i++)
                {
                    digitalsList[i].Value = showStringList[i];
                }
            }
            else//否则从显示器的低位开始填充
            {
                //高位清空
                for (int i = 0; i < digitalsList.Count - showStringList.Count; i++)
                {
                    digitalsList[i].Value = null;
                }

                for (int i = digitalsList.Count - showStringList.Count; i < digitalsList.Count; i++)
                {
                    digitalsList[i].Value = showStringList[i - digitalsList.Count + showStringList.Count];
                }
            }
        }

        /// <summary>
        /// 将字符串转换成可显示的单个字符的集合
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static List<string> ConvertStringToSingleDigitalCharList(string value)
        {
            char[] charArray = value.ToCharArray();
            List<string> showStringList = new List<string>();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (i == charArray.Length - 1)//最后一位数字，直接复制
                {
                    showStringList.Add(charArray[i].ToString());
                    break;
                }

                if (charArray[i] >= '0' && charArray[i] <= '9' && charArray[i + 1] == '.')//带小数点数字，将小数点与数字放到同一个字符串里
                {
                    showStringList.Add(charArray[i] + ".");
                    i++;
                }
                else
                {
                    showStringList.Add(charArray[i].ToString());
                }
            }

            return showStringList;
        }

        #endregion
    }
}
