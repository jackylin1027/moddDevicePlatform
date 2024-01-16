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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.Windows.Threading;

namespace FineTekDeivePlatform.component
{
    /// <summary>
    /// fk_basiclinechart.xaml 的互動邏輯
    /// </summary>
    /// 
    public partial class fk_basiclinechart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }
        public ChartValues<ObservablePoint> V_line1Values { get; set; }
        public ChartValues<ObservablePoint> V_line2Values { get; set; }
        public ChartValues<ObservablePoint> Measure_lineValues { get; set; }
        public ChartValues<ObservablePoint> H_lineValues { get; set; }

        int m_Measure_lineValues_ScalesXAt;
        int m_MaxValue_X0, m_MaxValue_X1;
        int m_MinValue_X0, m_MinValue_X1;
        int m_Step_X0, m_Step_X1;
        Brush m_Foreground_X0, m_Foreground_X1;

        public int Measure_lineValues_ScalesXAt { get { return m_Measure_lineValues_ScalesXAt; } set { m_Measure_lineValues_ScalesXAt = value;  } }
        public int MaxValue_X0 { get { return m_MaxValue_X0; } set { m_MaxValue_X0 = value; } }
        public int MaxValue_X1 { get {return m_MaxValue_X1; } set { m_MaxValue_X1 = value; } }
        public int MinValue_X0 { get { return m_MinValue_X0; } set { m_MinValue_X0 = value; } }
        public int MinValue_X1 { get { return m_MinValue_X1; } set { m_MinValue_X1 = value; } }
        public Brush Foreground_X0 { get { return m_Foreground_X0; } set { m_Foreground_X0 = value; } }
        public Brush Foreground_X1 { get { return m_Foreground_X1; } set { m_Foreground_X1 = value; } }
        public int Step_X0 { get { return m_Step_X0; } set { m_Step_X0 = value; } }
        public int Step_X1 { get { return m_Step_X1; } set { m_Step_X1 = value; } }
        public fk_basiclinechart()
        {
            InitializeComponent();
            //===== Measure_lineValues X軸參考 X1 =====
            m_Measure_lineValues_ScalesXAt = 0;
            //===== X0軸相關參數初始化 =====
            m_MinValue_X0 = 0;
            m_MaxValue_X0 = 500;
            m_Foreground_X0 = Brushes.Black;
            Step_X0 = 20;
            //===== X1軸相關參數初始化 =====
            m_MinValue_X1 = 0;
            m_MaxValue_X1 = 200;          
            m_Foreground_X1 = Brushes.Transparent;
            Step_X1 = 10;
            //===== Measure_lineValues 初始化 =====
            Measure_lineValues = new ChartValues<ObservablePoint>();
            

            V_line1Values = new ChartValues<ObservablePoint>{
                        new ObservablePoint(40,MainChart.AxisY[0].MinValue),
                        new ObservablePoint(40,MainChart.AxisY[0].MaxValue),
            };
            V_line2Values = new ChartValues<ObservablePoint>{
                        new ObservablePoint(80,MainChart.AxisY[0].MinValue),
                        new ObservablePoint(80,MainChart.AxisY[0].MaxValue),
            };
            H_lineValues = new ChartValues<ObservablePoint> {
                        new ObservablePoint(MainChart.AxisX[1].MinValue, 2000),
                        new ObservablePoint(MainChart.AxisX[1].MaxValue, 2000),
            };
            //Formatter = value => Math.Pow(Base, value).ToString("N");
            DataContext = this;
           
        }
    }
}
