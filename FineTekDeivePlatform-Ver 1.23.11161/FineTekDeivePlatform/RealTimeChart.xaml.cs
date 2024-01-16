using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FineTekDeivePlatform
{
    /// <summary>
    /// RealTimeChart.xaml 的互動邏輯
    /// </summary>
    public partial class RealTimeChart : UserControl , INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _axisStep;
        string _yaxisTitle;
        private ZoomingOptions _zoomingMode;
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        public ChartValues<MeasureModel>[] LineValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public Func<double, string> ValueFormatter { get; set; }
        public string YaxisTitle
        {
            get { return _yaxisTitle; }
            set { _yaxisTitle = value; OnPropertyChanged("YaxisTitle"); }
        }
        public ZoomingOptions ZoomingMode
        {
            get { return _zoomingMode; }
            set
            {
                _zoomingMode = value;
                OnPropertyChanged("ZoomingMode");
            }
        }
        public double AxisStep
        {
            get { return _axisStep; }
            set
            {
                _axisStep = value;
                OnPropertyChanged("AxisStep");
            }
        }
        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }
        public RealTimeChart()
        {
            InitializeComponent();
            var mapper = Mappers.Xy<MeasureModel>()
            .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
            .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            LineValues = new ChartValues<MeasureModel>[2];
            for (int i = 0; i < 2; i++)
            {
                LineValues[i] = new ChartValues<MeasureModel>();
                //LineValues[i].WithQuality(Quality.Highest);
            }
            DateTimeFormatter = value => new DateTime((long)value).ToString();
            ValueFormatter = value => (value).ToString();

            AxisStep = TimeSpan.FromSeconds(10).Ticks;
            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerSecond * 1;

            SetAxisLimits(DateTime.Now);

            DataContext = this;
        }
        public void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(0).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(100).Ticks; // and 8 seconds behind
        }
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void buttonInquery_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
