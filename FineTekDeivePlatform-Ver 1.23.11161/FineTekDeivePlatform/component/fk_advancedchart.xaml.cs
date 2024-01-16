using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using fk_lib;
using System.Data;

namespace FineTekDeivePlatform.component
{
    /// <summary>
    /// fk_advancedchart.xaml 的互動邏輯
    /// </summary>
    public partial class fk_advancedchart : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private double _axisStep;
        private ZoomingOptions _zoomingMode;
        private setupIni ini = new setupIni();
        string localAppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Windows.Forms.Application.ProductName);
        string iniPath;
        private fkMysqlCommand _sqlContent;
        private string _sql_sensor_series;
        private string _sql_sensor_sn;
        private string _sql_sensor_name;
        string _sql_sensor_station;
        private int _sql_sensor_modbus_id;
        public fkMysqlCommand SQLContent { get; set; }
        public string SQLSensorSeries
        {
            get { return _sql_sensor_series; }
            set { _sql_sensor_series = value; }
        }
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
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        public ChartValues<MeasureModel> H_LineValue { get; set; }
        public ChartValues<MeasureModel> L_LineValue { get; set; }
        public GearedValues<MeasureModel>[] GearedValues { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public Func<double, string> ValueFormatter { get; set; }
        public List<MeasureModel> ChartValuesSource { get; set; }
        //public List<MeasureModel> ChartValuesSource_1 { get; set; }
        public ChartValues<MeasureModel> ChartValuesDisplay { get; set; }
        private bool _gearedValue1Visibility;
        private bool _gearedValue2Visibility;
        private bool _gearedValue3Visibility;
        private bool _gearedValue4Visibility;
        private bool _gearedValue5Visibility;
        private bool _gearedValue6Visibility;
        private bool _gearedValue7Visibility;
        private bool _gearedValue8Visibility;
        private bool _gearedValue9Visibility;
        private bool _gearedValue10Visibility;
        private bool _gearedValue11Visibility;
        private bool _gearedValue12Visibility;
        private bool _gearedValue13Visibility;
        private bool _gearedValue14Visibility;
        private bool _gearedValue15Visibility;
        private bool _gearedValue16Visibility;
        private bool _gearedValue17Visibility;
        private bool _gearedValue18Visibility;
        private bool _gearedValue19Visibility;
        private bool _gearedValue20Visibility;
        private bool _gearedValue21Visibility;
        private bool _gearedValue22Visibility;
        private bool _gearedValue23Visibility;
        private bool _gearedValue24Visibility;
        private bool _gearedValue25Visibility;
        private bool _gearedValue26Visibility;
        private bool _gearedValue27Visibility;
        private bool _gearedValue28Visibility;
        private bool _gearedValue29Visibility;
        private bool _gearedValue30Visibility;
        private bool _gearedValue31Visibility;
        private bool _gearedValue32Visibility;
        private bool _gearedValue33Visibility;
        private bool _gearedValue34Visibility;
        private bool _gearedValue35Visibility;
        private bool _gearedValue36Visibility;
        string _yaxisTitle;
        Brushes[] brushes_tbl = new Brushes[] {

        };
        
        public bool GearedValue1Visibility
        {
            get { return _gearedValue1Visibility; }
            set { _gearedValue1Visibility = value; OnPropertyChanged("GearedValue1Visibility"); }
        }
        public bool GearedValue2Visibility
        {
            get { return _gearedValue2Visibility; }
            set { _gearedValue2Visibility = value; OnPropertyChanged("GearedValue2Visibility"); }
        }
        public bool GearedValue3Visibility
        {
            get { return _gearedValue3Visibility; }
            set { _gearedValue3Visibility = value; OnPropertyChanged("GearedValue3Visibility"); }
        }
        public bool GearedValue4Visibility
        {
            get { return _gearedValue4Visibility; }
            set { _gearedValue4Visibility = value; OnPropertyChanged("GearedValue4Visibility"); }
        }
        public bool GearedValue5Visibility
        {
            get { return _gearedValue5Visibility; }
            set { _gearedValue5Visibility = value; OnPropertyChanged("GearedValue5Visibility"); }
        }
        public bool GearedValue6Visibility
        {
            get { return _gearedValue6Visibility; }
            set { _gearedValue6Visibility = value; OnPropertyChanged("GearedValue6Visibility"); }
        }
        public bool GearedValue7Visibility
        {
            get { return _gearedValue7Visibility; }
            set { _gearedValue7Visibility = value; OnPropertyChanged("GearedValue7Visibility"); }
        }
        public bool GearedValue8Visibility
        {
            get { return _gearedValue8Visibility; }
            set { _gearedValue8Visibility = value; OnPropertyChanged("GearedValue8Visibility"); }
        }
        public bool GearedValue9Visibility
        {
            get { return _gearedValue9Visibility; }
            set { _gearedValue9Visibility = value; OnPropertyChanged("GearedValue9Visibility"); }
        }
        public bool GearedValue10Visibility
        {
            get { return _gearedValue10Visibility; }
            set { _gearedValue10Visibility = value; OnPropertyChanged("GearedValue10Visibility"); }
        }
        public bool GearedValue11Visibility
        {
            get { return _gearedValue11Visibility; }
            set { _gearedValue11Visibility = value; OnPropertyChanged("GearedValue11Visibility"); }
        }
        public bool GearedValue12Visibility
        {
            get { return _gearedValue12Visibility; }
            set { _gearedValue12Visibility = value; OnPropertyChanged("GearedValue12Visibility"); }
        }
        public bool GearedValue13Visibility
        {
            get { return _gearedValue13Visibility; }
            set { _gearedValue13Visibility = value; OnPropertyChanged("GearedValue13Visibility"); }
        }
        public bool GearedValue14Visibility
        {
            get { return _gearedValue14Visibility; }
            set { _gearedValue14Visibility = value; OnPropertyChanged("GearedValue14Visibility"); }
        }
        public bool GearedValue15Visibility
        {
            get { return _gearedValue15Visibility; }
            set { _gearedValue15Visibility = value; OnPropertyChanged("GearedValue15Visibility"); }
        }
        public bool GearedValue16Visibility
        {
            get { return _gearedValue16Visibility; }
            set { _gearedValue16Visibility = value; OnPropertyChanged("GearedValue16Visibility"); }
        }
        public bool GearedValue17Visibility
        {
            get { return _gearedValue17Visibility; }
            set { _gearedValue17Visibility = value; OnPropertyChanged("GearedValue17Visibility"); }
        }
        public bool GearedValue18Visibility
        {
            get { return _gearedValue18Visibility; }
            set { _gearedValue18Visibility = value; OnPropertyChanged("GearedValue18Visibility"); }
        }
        public bool GearedValue19Visibility
        {
            get { return _gearedValue19Visibility; }
            set { _gearedValue19Visibility = value; OnPropertyChanged("GearedValue19Visibility"); }
        }
        public bool GearedValue20Visibility
        {
            get { return _gearedValue20Visibility; }
            set { _gearedValue20Visibility = value; OnPropertyChanged("GearedValue20Visibility"); }
        }
        public bool GearedValue21Visibility
        {
            get { return _gearedValue21Visibility; }
            set { _gearedValue21Visibility = value; OnPropertyChanged("GearedValue21Visibility"); }
        }
        public bool GearedValue22Visibility
        {
            get { return _gearedValue22Visibility; }
            set { _gearedValue22Visibility = value; OnPropertyChanged("GearedValue22Visibility"); }
        }
        public bool GearedValue23Visibility
        {
            get { return _gearedValue23Visibility; }
            set { _gearedValue23Visibility = value; OnPropertyChanged("GearedValue23Visibility"); }
        }
        public bool GearedValue24Visibility
        {
            get { return _gearedValue24Visibility; }
            set { _gearedValue24Visibility = value; OnPropertyChanged("GearedValue24Visibility"); }
        }
        public bool GearedValue25Visibility
        {
            get { return _gearedValue25Visibility; }
            set { _gearedValue25Visibility = value; OnPropertyChanged("GearedValue25Visibility"); }
        }
        public bool GearedValue26Visibility
        {
            get { return _gearedValue26Visibility; }
            set { _gearedValue26Visibility = value; OnPropertyChanged("GearedValue26Visibility"); }
        }
        public bool GearedValue27Visibility
        {
            get { return _gearedValue27Visibility; }
            set { _gearedValue27Visibility = value; OnPropertyChanged("GearedValue27Visibility"); }
        }
        public bool GearedValue28Visibility
        {
            get { return _gearedValue28Visibility; }
            set { _gearedValue28Visibility = value; OnPropertyChanged("GearedValue28Visibility"); }
        }
        public bool GearedValue29Visibility
        {
            get { return _gearedValue29Visibility; }
            set { _gearedValue29Visibility = value; OnPropertyChanged("GearedValue29Visibility"); }
        }
        public bool GearedValue30Visibility
        {
            get { return _gearedValue30Visibility; }
            set { _gearedValue30Visibility = value; OnPropertyChanged("GearedValue30Visibility"); }
        }
        public bool GearedValue31Visibility
        {
            get { return _gearedValue31Visibility; }
            set { _gearedValue31Visibility = value; OnPropertyChanged("GearedValue31Visibility"); }
        }
        public bool GearedValue32Visibility
        {
            get { return _gearedValue32Visibility; }
            set { _gearedValue32Visibility = value; OnPropertyChanged("GearedValue32Visibility"); }
        }
        public bool GearedValue33Visibility
        {
            get { return _gearedValue33Visibility; }
            set { _gearedValue33Visibility = value; OnPropertyChanged("GearedValue33Visibility"); }
        }
        public bool GearedValue34Visibility
        {
            get { return _gearedValue34Visibility; }
            set { _gearedValue34Visibility = value; OnPropertyChanged("GearedValue34Visibility"); }
        }
        public bool GearedValue35Visibility
        {
            get { return _gearedValue35Visibility; }
            set { _gearedValue35Visibility = value; OnPropertyChanged("GearedValue35Visibility"); }
        }
        public bool GearedValue36Visibility
        {
            get { return _gearedValue36Visibility; }
            set { _gearedValue36Visibility = value; OnPropertyChanged("GearedValue36Visibility"); }
        }
        public  string YaxisTitle
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
        public List<CheckBox> checkboxList = new List<CheckBox>();
        public fk_advancedchart()
        {
            InitializeComponent();
            iniPath = localAppData + @"\_chart_setup.ini";
            //To handle live data easily, in this case we built a specialized type
            //the MeasureModel class, it only contains 2 properties
            //DateTime and Value
            //We need to configure LiveCharts to handle MeasureModel class
            //The next code configures MeasureModel  globally, this means
            //that LiveCharts learns to plot MeasureModel and will use this config every time
            //a IChartValues instance uses this type.
            //this code ideally should only run once
            //you can configure series in many ways, learn more at 
            //http://lvcharts.net/App/examples/v1/wpf/Types%20and%20Configuration

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            //the values property will store our values array
            GearedValues = new GearedValues<MeasureModel>[36];
            for (int i = 0; i < 36; i++)
            {
                GearedValues[i] = new GearedValues<MeasureModel>();
                GearedValues[i].WithQuality(Quality.Low);
            }

            //lets set how to display the X Labels
            DateTimeFormatter = value => new DateTime((long)value).ToString();
            ValueFormatter = value => (value).ToString();

            //AxisStep forces the distance between each separator in the X axis
            AxisStep = TimeSpan.FromHours(1).Ticks;
            //AxisUnit forces lets the axis know that we are plotting seconds
            //this is not always necessary, but it can prevent wrong labeling
            AxisUnit = TimeSpan.TicksPerHour* 1;

            SetAxisLimits(DateTime.Now);

           // ChartValuesSource_1 = new List<MeasureModel>();
            ChartValuesDisplay = new ChartValues<MeasureModel>();
            GearedValue1Visibility = true;
            DataContext = this;


            YaxisTitle = (string)comboboxSELECT_VALUE.SelectedValue;
            ZoomingMode = ZoomingOptions.Xy;
            checkboxList.Add(radiobuttonS1);
            checkboxList.Add(radiobuttonS2);
            checkboxList.Add(radiobuttonS3);
            checkboxList.Add(radiobuttonS4);
            checkboxList.Add(radiobuttonS5);
            checkboxList.Add(radiobuttonS6);
            checkboxList.Add(radiobuttonS7);
            checkboxList.Add(radiobuttonS8);
            checkboxList.Add(radiobuttonS9);
            checkboxList.Add(radiobuttonS10);
            checkboxList.Add(radiobuttonS11);
            checkboxList.Add(radiobuttonS12);
            checkboxList.Add(radiobuttonS13);
            checkboxList.Add(radiobuttonS14);
            checkboxList.Add(radiobuttonS15);
            checkboxList.Add(radiobuttonS16);
            checkboxList.Add(radiobuttonS17);
            checkboxList.Add(radiobuttonS18);
            checkboxList.Add(radiobuttonS19);
            checkboxList.Add(radiobuttonS20);
            checkboxList.Add(radiobuttonS21);
            checkboxList.Add(radiobuttonS22);
            checkboxList.Add(radiobuttonS23);
            checkboxList.Add(radiobuttonS24);
            checkboxList.Add(radiobuttonS25);
            checkboxList.Add(radiobuttonS26);
            checkboxList.Add(radiobuttonS27);
            checkboxList.Add(radiobuttonS28);
            checkboxList.Add(radiobuttonS29);
            checkboxList.Add(radiobuttonS30);
            checkboxList.Add(radiobuttonS31);
            checkboxList.Add(radiobuttonS32);
            checkboxList.Add(radiobuttonS33);
            checkboxList.Add(radiobuttonS34);
            checkboxList.Add(radiobuttonS35);
            checkboxList.Add(radiobuttonS36);
            //===== 設定檔新增 mysql 連線字串, Ver 1.1.06 =====

            #region /*** setup.ini 初始設定 ***/
            if (ini.CkeckFileExists(iniPath))
                Load_iniFile();
            else
                Save_IniFile();
            #endregion
        }
        public void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
        }


        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void AxisX_RangeChanged(LiveCharts.Events.RangeChangedEventArgs eventArgs)
        {
            /*double min = ((Axis)eventArgs.Axis).MinValue;
            double max = ((Axis)eventArgs.Axis).MaxValue;
            _axisMin = min;
            _axisMax = max;
            double p3 = ChartValuesSource[3].DateTime.Ticks;
            int p1 = ChartValuesSource.FindIndex(x => x.DateTime.Ticks == _axisMin);
            int p2 = ChartValuesSource.FindIndex(x => x.DateTime.Ticks == _axisMax);
            int delta = p2 - p1;*/
        }
        string[] old_content= new string[36] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",  };
        public void update_checkbox_content(int device_id, string new_content)
        {
            if (!old_content[device_id].Equals(new_content) && !new_content.Equals("00000000000000"))
            {
                checkboxList[device_id].Content = new_content;
                old_content[device_id] = new_content;
                checkboxList[device_id].Visibility = Visibility.Visible;
            }
            for (int i = 0;i < checkboxList.Count;i++)
            {
                if (checkboxList[i].Content.Equals("null"))
                    checkboxList[i].Visibility = Visibility.Hidden;
            }     
        }
        public void initiate_check_content()
        {
            for (int i = 0; i < checkboxList.Count; i++)
            {
                checkboxList[i].Content = "null";
                old_content[i] = "null";
            }
        }
        private void comboboxSELECT_VALUE_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string ComboboxContent = (string)comboboxSELECT_VALUE.SelectedValue;
            YaxisTitle = (string)comboboxSELECT_VALUE.SelectedValue;
        }

        private void Save_IniFile()
        {
           //ini.IniWriteValue("GLOBEL", "linehighlimit", textboxFLOWRATE_HI_LIMIT.Text, iniPath);
           //ini.IniWriteValue("GLOBEL", "linelowlimit", textboxFLOWRATE_LO_LIMIT.Text, iniPath);
        }
        private void Load_iniFile()
        {
          // textboxFLOWRATE_HI_LIMIT.Text = ini.IniReadValue("GLOBEL", "linehighlimit", iniPath);
          // textboxFLOWRATE_LO_LIMIT.Text = ini.IniReadValue("GLOBEL", "linelowlimit", iniPath);
        }

        private void checkboxSLELCT_WHOLE_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            switch (checkbox.Name)
            {
                case "checkboxSLELCT_WHOLE":
                    if (checkbox.IsChecked == true)
                    {
                        for (int i = 0; i < checkboxList.Count; i++)
                        {
                            if (checkboxList[i].Visibility == Visibility.Visible)
                                checkboxList[i].IsChecked = true;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < checkboxList.Count; i++)
                        {
                            if (checkboxList[i].Visibility == Visibility.Visible)
                                checkboxList[i].IsChecked = false;
                        }
                    }
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisX[0].MaxValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MaxValue = double.NaN;
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
        public string interpretENG(string src_string)
        {
            string dst_string="";
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
            bool Firststate = false;
            bool CheckboxNotNull = false;
            int startCount = 0, endCount = 0;
            string inquire_key = interpretENG((string)comboboxSELECT_VALUE.SelectedValue);

            for (int i = 0; i < checkboxList.Count; i++)
            {
                if (checkboxList[i].IsChecked == true && checkboxList[i].Content.ToString() != "null")
                {
                    if (Firststate == false)
                    {
                        Firststate = true;
                        MysqlCmdSectoin += " ";
                    }
                    else
                    {
                        MysqlCmdSectoin += " or ";
                    }

                    MysqlCmdSectoin += "sn = " + "\"" + checkboxList[i].Content + "\"";
                    CheckboxNotNull = true;
                }
            }
            MysqlCmdSectoin += ")";

            switch (_sql_sensor_series)
            {
                case "Level":
                    if (_sql_sensor_name == "SIS100")
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
                        if (_sql_sensor_name == "EPH" || _sql_sensor_name == "EPH0019" || _sql_sensor_name=="EPD3" || _sql_sensor_name == "EPU")
                        {
                            //===== 如果inquire data == NULL, chart 曲線就不顯示, =====
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
            //===== 用 Linq 將 數據分組, 並判斷筆數, Ver 1.1.06 =====
            var chartGroup1 = dataList.GroupBy(x => x.gt_header.sn).Where(g => g.Count() > 0).Select(x => new { Element = x.Key, Count = x.Count() });
            chartGroup1 = chartGroup1.OrderBy(x => x.Element).ToList();
            dataList = dataList.OrderBy(x => x.gt_header.sn).ToList();
            //===== 清除圖表 =====
            for (int i = 0; i < chartGroup1.Count(); i++)
                GearedValues[i].Clear();

            DateTime dt = DateTime.Now;
            for (int i = 0; i < chartGroup1.Count(); i++)
            {
                int checkboxIndex = 0;
                endCount += chartGroup1.ElementAt(i).Count;
                ChartValuesSource = new List<component.fk_advancedchart.MeasureModel>();
                for (int j = 0; j < checkboxList.Count; j++)
                {
                    if (chartGroup1.ElementAt(i).Element.Equals(checkboxList[j].Content))
                    {
                        checkboxIndex = j;
                        break;
                    }
                }
                //====== 將inquire 的 data 放到 chart 的 buffer 中 =====
                for (int j = startCount; j < endCount; j++)
                {
                    ChartValuesSource.Add(new component.fk_advancedchart.MeasureModel
                    {
                        DateTime = dataList[j].gt_header.response_time,
                        Value = Convert.ToDouble(dataList[j].inquire_value)
                    });
                }
                //HistoryChart.ChartValuesSource = HistoryChart.ChartValuesSource.OrderBy(t => t.DateTime).ToList();
                GearedValues[checkboxIndex].AddRange(ChartValuesSource);
                startCount += chartGroup1.ElementAt(i).Count;
            }
            //updateFlowRateLimitLine(chartDataList.Min(t => t.connT), chartDataList.Max(t => t.connT), HistoryChart.comboboxSELECT_VALUE.SelectedValue.ToString().Equals("瞬時流率(LPM)"));
            chartXYreset(this);
            cartesianchartMAIN.AxisX[0].MaxValue = double.NaN;
            //===== 即時更新X軸的可視範圍 =====
            cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            //RealtimeChart.cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            cartesianchartMAIN.AxisY[0].MaxValue = double.NaN;
            ////===== 顯示目前時間 =====
        }
        public void value_selection_create(string sensor,List<string>  sql_key_content)
        {
            comboboxSELECT_VALUE.ItemsSource = null;

            if ((sensor.Equals("EPH0019")))
            {
                if (sql_key_content[0].Equals("*"))
                    comboboxSELECT_VALUE.ItemsSource = new string[] { "總積算值", "瞬間值", "正積算值", "反積算值", "漏水天數", "正轉天數", "靜止天數", "反轉天數", "受磁天數", "電量不足天數", "開關次數", "電池電壓" };
            }
            comboboxSELECT_VALUE.SelectedIndex = 0;
        }
        private void buttonSAVE_LOG_Click(object sender, RoutedEventArgs e)
        {

        }
        public static long GetUnixTimeStamp(DateTime dt)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
            long timeStamp = Convert.ToInt32((dt - dtStart).TotalSeconds);
            return timeStamp;
        }
        void chartXYreset(component.fk_advancedchart chart)
        {
            chart.cartesianchartMAIN.AxisX[0].MinValue = double.NaN;
            chart.cartesianchartMAIN.AxisX[0].MaxValue = double.NaN;
            chart.cartesianchartMAIN.AxisY[0].MinValue = double.NaN;
            chart.cartesianchartMAIN.AxisY[0].MaxValue = double.NaN;
        }
    }
}
