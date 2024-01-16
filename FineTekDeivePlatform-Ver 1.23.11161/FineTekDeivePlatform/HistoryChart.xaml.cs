using fk_lib;
using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// HistoryChart.xaml 的互動邏輯
    /// </summary>
    public partial class HistoryChart : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _axisStep;
        string _yaxisTitle;
        private ZoomingOptions _zoomingMode;
        private fkMysqlCommand _sqlContent;
        private string _sql_sensor_series;
        private string _sql_sensor_sn;
        private string _sql_sensor_name;
        string _sql_sensor_station;
        private int _sql_sensor_modbus_id;
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        public ChartValues<MeasureModel>[] LineValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public Func<double, string> ValueFormatter { get; set; }

        public fkMysqlCommand SQLContent { get; set; }
        public string SQLSensorSeries
        {
            get { return _sql_sensor_series; }
            set { _sql_sensor_series = value; } }
        public string SQLSensorSN
        {
            get { return _sql_sensor_sn; }
            set { _sql_sensor_sn = value; }
        }
        public string SQLSensorStation
        {
            get { return _sql_sensor_station; }
            set { _sql_sensor_station = value; }
        }
        public string SQLsensorName
        {
            get { return _sql_sensor_name; }
            set { _sql_sensor_name = value; }
        }
        public int SQLsensorModbusID
        {
            get { return _sql_sensor_modbus_id; }
            set { _sql_sensor_modbus_id = value; }
        }
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
        public HistoryChart()
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

            YaxisTitle = (string)comboboxSELECT_VALUE.SelectedValue;
            ZoomingMode = ZoomingOptions.Xy;
            DataContext = this;
            datetimepickSTRAT.DateTimeStr = DateTime.Now.AddDays(-1).ToString();
            datetimepickEND.DateTimeStr = DateTime.Now.ToString();
        }

        public void value_selection_create(string sensor, List<string> sql_key_content)
        {
            comboboxSELECT_VALUE.ItemsSource = null;
            
            foreach (var key in sql_key_content)
            {
                if (!(key.Equals("sn") || key.Equals("firmware") || key.Equals("modbus_id")))
                    comboboxSELECT_VALUE.Items.Add(interpretCN(key.Trim()));
            }
            comboboxSELECT_VALUE.SelectedIndex = 0;
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
        public static long GetUnixTimeStamp(DateTime dt)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            long timeStamp = Convert.ToInt32((dt - dtStart).TotalSeconds);
            return timeStamp;
        }

        class chart_format_t
        {
            public string gateway_imei { get; set; }
            public DateTime response_time { get; set; }
            public string sensor { get; set; }
            public string sn { get; set; }
            public int modbus_id { get; set; }
            public string firmware { get; set; }
        }
        class chart_data_t
        {
            public chart_format_t gt_header;
            public decimal display_value;
            public object inquire_value;
        }
        private void comboboxSELECT_VALUE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string ComboboxContent = (string)comboboxSELECT_VALUE.SelectedValue;
            YaxisTitle = (string)comboboxSELECT_VALUE.SelectedValue;
        }
        public string interpretENG(string src_string)
        {
            string dst_string = "";
            switch (src_string)
            {
                case "總積算值": dst_string = "b_accu_flow"; break;
                case "瞬間值": dst_string = "flow_rate"; break;
                case "正積算值": dst_string = "f_accu_flow"; break;
                case "反積算值": dst_string = "r_accu_flow"; break;
                case "漏水天數": dst_string = "b_day"; break;
                case "正轉天數": dst_string = "l_day"; break;
                case "靜止天數": dst_string = "n_day"; break;
                case "反轉天數": dst_string = "o_day"; break;
                case "受磁天數": dst_string = "u_day"; break;
                case "電量不足天數": dst_string = "h_day"; break;
                case "開關次數": dst_string = "f_usage"; break;
                case "電池電壓": dst_string = "battery"; break;
                case "靈敏度%": dst_string = "percentage"; break;
                case "感測ADC": dst_string = "sensitivity"; break;
                case "溫度": dst_string = "temperature"; break;
                case "頻率": dst_string = "frequency"; break;
                case "震幅": dst_string = "amplitude"; break;
                case "電容值": dst_string = "capacitance"; break;
                case "轉速": dst_string = "rotating_speed"; break;
                case "距離": dst_string = "distance"; break;
                case "輸出電流": dst_string = "output_current"; break;
                case "輸出電壓": dst_string = "output_voltage"; break;
                case "系統狀態": dst_string = "system_status"; break;
                case "錯誤狀態": dst_string = "error_code"; break;
                case "壓力值": dst_string = "pressure"; break;
                case "水位高度": dst_string = "liquid_level"; break;
                case "粉塵濃度": dst_string = "dust_concentration"; break;
                case "輸出1": dst_string = "output_state_1"; break;
                case "輸出2": dst_string = "output_state_2"; break;
                default: dst_string = "Undefined"; break;
            }
            return dst_string;
        }
        public string interpretCN(string src_string)
        {
            string dst_string = "";
            switch (src_string)
            {
                case "b_accu_flow": dst_string = "總積算值"; break;
                case "flow_rate": dst_string = "瞬間值"; break;
                case "f_accu_flow": dst_string = "正積算值"; break;
                case "r_accu_flow": dst_string = "反積算值"; break;
                case "b_day": dst_string = "漏水天數"; break;
                case "l_day": dst_string = "正轉天數"; break;
                case "n_day": dst_string = "靜止天數"; break;
                case "o_day": dst_string = "反轉天數"; break;
                case "u_day": dst_string = "受磁天數"; break;
                case "h_day": dst_string = "電量不足天數"; break;
                case "f_usage": dst_string = "開關次數"; break;
                case "battery": dst_string = "電池電壓"; break;
                case "percentage": dst_string = "靈敏度%"; break;
                case "sensitivity": dst_string = "感測ADC"; break;
                case "temperature": dst_string = "溫度"; break;
                case "frequency": dst_string = "頻率"; break;
                case "amplitude": dst_string = "震幅"; break;
                case "capacitance": dst_string = "電容值"; break;
                case "rotating_speed": dst_string = "轉速"; break;
                case "distance": dst_string = "距離"; break;
                case "output_current": dst_string = "輸出電流"; break;
                case "output_voltage": dst_string = "輸出電壓"; break;
                case "system_status": dst_string = "系統狀態"; break;
                case "error_code": dst_string = "錯誤狀態"; break;
                case "pressure": dst_string = "壓力值"; break;
                case "liquid_level": dst_string = "水位高度"; break;
                case "dust_concentration": dst_string = "粉塵濃度"; break;
                case "output_state_1": dst_string = "輸出1"; break;
                case "output_state_2": dst_string = "輸出2"; break;
                default: dst_string = "未定義"; break;
            }
            return dst_string;
        }
        private void buttonInquery_Click(object sender, RoutedEventArgs e)
        {
            DataGrid datagrid1 = new DataGrid();
            List<chart_data_t> dataList = new List<chart_data_t>();
            DateTime StartTimeStamp = Convert.ToDateTime(datetimepickSTRAT.DateTimeStr);
            DateTime EndTimeStamp = Convert.ToDateTime(datetimepickEND.DateTimeStr);
            string MysqlCmdStart = "select gateway_imei,response_time,sensor,sn,modbus_id,firmware, ";
            string MysqlCmdEnd = "order by response_time";
            string MysqlCmdSectoin = " where (";
            string MysqlCmdTimeStamp = " and (response_time>=" + GetUnixTimeStamp(StartTimeStamp) + " and response_time<=" + GetUnixTimeStamp(EndTimeStamp) + ") ";
            MysqlCmdSectoin += "sn = " + "\"" + _sql_sensor_sn + "\"";
            MysqlCmdSectoin += ")";
            string inquire_key = interpretENG((string)comboboxSELECT_VALUE.SelectedValue);

            switch (_sql_sensor_series)
            {
                case "Level":
                    if(_sql_sensor_name=="SIS100")
                        MysqlCmdStart += "display_value, " + inquire_key + " from level_sensor";
                    datagrid1.DataContext = SQLContent.Inqury(MysqlCmdStart + MysqlCmdSectoin + MysqlCmdTimeStamp + MysqlCmdEnd, "level_sensor");
                    break;
                case "Flow":
                    if (_sql_sensor_name == "EPH" || _sql_sensor_name == "EPH0019" || _sql_sensor_name == "EPD3" || _sql_sensor_name == "EPU")
                        MysqlCmdStart += "display_value, " + inquire_key + " from flow_sensor";
                    datagrid1.DataContext = SQLContent.Inqury(MysqlCmdStart + MysqlCmdSectoin + MysqlCmdTimeStamp + MysqlCmdEnd, "flow_sensor");

                    break;
            }
            DataTable dataTable1 = new DataTable();
            dataTable1 = (DataTable)datagrid1.DataContext;
            for (int i = 0; i < dataTable1.Rows.Count; i++)
            {
                switch (_sql_sensor_series)
                {
                    case "Level":
                        if (_sql_sensor_name == "SIS100")
                        {
                            dataList.Add(
                                    new chart_data_t
                                    {
                                        gt_header = new chart_format_t
                                        {
                                            firmware = (string)dataTable1.Rows[i]["firmware"],
                                            sn = (string)dataTable1.Rows[i]["sn"],
                                            gateway_imei = (string)dataTable1.Rows[i]["gateway_imei"],
                                            sensor = (string)dataTable1.Rows[i]["sensor"],
                                            modbus_id = Convert.ToInt32(dataTable1.Rows[i]["modbus_id"]),
                                            response_time = (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(Convert.ToDouble(dataTable1.Rows[i]["response_time"])),
                                        },
                                        display_value = Convert.ToDecimal(dataTable1.Rows[i]["display_value"]),
                                        inquire_value = Convert.ToDecimal(dataTable1.Rows[i][inquire_key]),
                                    });
                        }
                        break;
                    case "Flow":
                        if (_sql_sensor_name == "EPH" || _sql_sensor_name == "EPH0019" || _sql_sensor_name == "EPD3" || _sql_sensor_name == "EPU")
                        {
                            if (!dataTable1.Rows[i][inquire_key].Equals(DBNull.Value))
                            {
                                dataList.Add(
                                    new chart_data_t
                                    {
                                        gt_header = new chart_format_t
                                        {
                                            firmware = (string)dataTable1.Rows[i]["firmware"],
                                            sn = (string)dataTable1.Rows[i]["sn"],
                                            gateway_imei = (string)dataTable1.Rows[i]["gateway_imei"],
                                            sensor = (string)dataTable1.Rows[i]["sensor"],
                                            modbus_id = Convert.ToInt32(dataTable1.Rows[i]["modbus_id"]),
                                            response_time = (new DateTime(1970, 1, 1, 0, 0, 0)).AddHours(8).AddSeconds(Convert.ToDouble(dataTable1.Rows[i]["response_time"])),
                                        },
                                        display_value = Convert.ToDecimal(dataTable1.Rows[i]["display_value"]),
                                        inquire_value = Convert.ToDecimal(dataTable1.Rows[i][inquire_key]),
                                    });
                            }
                        }
                        break;
                }
            }
            if (dataTable1.Rows.Count == 0)
                {
                    MessageBox.Show("目前資料庫沒資料", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }

                //===== 清除圖表 =====
                this.LineValues[0].Clear();
            for (int j = 0; j < dataList.Count; j++)
            {
                LineValues[0].Add(new MeasureModel
                {
                    DateTime = dataList[j].gt_header.response_time,
                    Value = Convert.ToDouble(dataList[j].inquire_value)
                });
            }
            cartesianchartMAIN.AxisX[0].MaxValue = double.NaN;
            //===== 即時更新X軸的可視範圍 =====
            cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            //RealtimeChart.cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MaxValue = double.NaN;
            //else if (this.comboboxSELECT_VALUE.SelectedValue.ToString() == "順時流率(LPM)")
            //{
            //    this.cartesianchartMAIN.AxisY[0].MaxValue = Convert.ToDouble(chartDataList.Max(t => t.fr) + (chartDataList.Max(t => t.fr) / 10));
            //}
        }
        private void buttonSAVE_LOG_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisX[0].MaxValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MaxValue = double.NaN;
        }
    }
}
