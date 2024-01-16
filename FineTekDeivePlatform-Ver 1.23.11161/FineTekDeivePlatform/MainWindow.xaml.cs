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
using System.Runtime.InteropServices;
using fk_lib;
using System.IO;
using System.IO.Ports;
using FineTekDeivePlatform.ViewModel;
using FineTekDeivePlatform.Models;
using FineTekDeivePlatform.component;
using System.Xml;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using System.Windows.Interop;
using LiveCharts.Wpf;
using System.Windows.Controls.Primitives;
using System.Reflection;
using FineTekDeivePlatform.additional;
using FineTekDeivePlatform.component.specialmodel;
using MaterialDesignThemes.Wpf;


namespace FineTekDeivePlatform
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left; 
            public int Top; 
            public int Right; 
            public int Bottom;
        }
        private setupIni ini = new setupIni();
        private bool com_open = false;
        private string sync_string = "sync";
        private serial_app serial_port;
        private modbus modbus_obj = new modbus();
        //private modbus_table modbus_ephTransmitter_table = new modbus_table();
        private List<modbus_cell> m_list_data;
        private List<modbus_table.ResponseAssociate> m_list_componentLink;
        private UInt16 Display_Value_address = 4128;
        private modbus_cell m_value_cell;
        private UInt16 modbus_id = 0;
        private alogrithm fk_alogrithm = new alogrithm();
        private fn fk_function = new fn();
        private UInt16 modbus_error_count;
        private zone_info modbus_zone_ptr = zone_info.SettingParameter_Zone;
        private bool modbus_read_flag = false;
        string EPHTFileName;
        string localAppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), System.Windows.Forms.Application.ProductName);
        string iniPath;
        System.Windows.Forms.Timer timer_modbus = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer_interval = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer timer_datetime = new System.Windows.Forms.Timer();
        modbus_table XMLmodbus_table;
        XMLparser XML; 
        int fk_command_sequence_total;
        int fk_command_state;
        
        public fkMysqlCommand fk_mysql_command;
        #region /**** MODBUS 參數 ****/
        public int fk_modbusDD_event_index;
        public int fk_modbusDD_device_index;
        public int fk_modbusDD_communication_index;
        public List<int> fk_communucation_sequence = new List<int>();
        class DeviceColloction_t
        {
            public bool gbool_communication_joint;                  //===== 是否加入 communication sequence 中 =====
            public TextBox go_DDsingle_connect_id;                  //===== 連線的 modbus id 中 =====          
            public string gs_stationName;                           //===== 裝置的 名稱/佔位名稱 =====
            public List<XMLparser.cbObj> glist_DDcomponentInfo;     //===== 裝置所有UI的List, 產生會參考 MODBUS-DD =====
            public realtime_t gt_DDrealtimeInfo;                    //===== 即時chart圖的參數, 產生會參考 MODBUS-DD =====
            public RealTimeChart go_DDrealtimechart;                //===== 物件 : 即時顯示圖【uercontrol_chart】, 產生【位置】會參考 MODBUS-DD =====
            public List<sqlitem_t> glist_DDhistory;                 //===== 歷史chart圖的參數, 產生會參考 MODBUS-DD =====
            public object go_DDhistorychart;                  //===== 物件 : 歷史顯示圖【uercontrol_chart】, 產生【位置】會參考 MODBUS-DD =====
            public sqldisplay_t gt_DDdisplay;                       //===== 定義裝置的 display_value【1~4】, 產生會參考 MODBUS-DD =====
            public List<modbus_cell> glist_DDdata_list;             //===== 裝置的 modbus table , 包括 Address 、 資料型態等等, 產生會參考 MODBUS-DD =====
            public List<modbus_table.ResponseAssociate> glist_DDcomponentLink;  //===== 裝置的 modbus table 跟UI介面的連結規則宣告, 產生會參考 MODBUS-DD =====  【較複雜功能也較延伸】
            public modbus_table gt_DDmodbusTBL;                     //===== glist_DDdata_list 跟 glist_DDcomponentLink 的主體/實體instance =====
            public List<address_zone> glist_DDrespSqguence;         //===== 裝置 modbus readonly 的規則, 產生會參考 MODBUS-DD =====
            public List<address_zone> glist_DDwriteSqguence;        //===== 裝置 modbus write 的規則, 產生會參考 MODBUS-DD =====
            public int gint_DDhistoryInterval;                      //===== 歷史資料儲存週期, glist_DDrespSqguence 為週期參考單位 =====
            public List<modbus_table.modbusinfo> glist_DDmodbusCell;//===== MODBUS-DD上 modbus cell 的 list =====
            public List<functionDescription_t>  glist_DDfunctionDescrition;
            public bool gbool_special_device_type;
            public int gint_connected_device_number;
            public PaltformIndentity gt_DDplatformIndentity;
            public bool gbool_insert_data_repatative;
            public StackPanel go_devicetree_stackpanel;
        }
        object[] XMLhandle = new object[20];
        List<UIElement> UIElList = new List<UIElement>();
        List<TextBlock> textblock_modbusIDList = new List<TextBlock>();
        List<MaterialDesignThemes.Wpf.PackIcon> packiconList = new List<MaterialDesignThemes.Wpf.PackIcon>();
        List<Button> treeviewer_buttonList = new List<Button>();
        List<DeviceColloction_t> fk_glist_DDdeviceCollection = new List<DeviceColloction_t>();
        #endregion
        #region /**** 視窗最小化 ****/
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private System.Windows.Forms.ContextMenu _contextMenu;
        private System.Windows.Forms.MenuItem _openWindow;
        private System.Windows.Forms.MenuItem _closeApp;
        private System.ComponentModel.IContainer _iContainer;
        private bool _internalClosing;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            textboxFIRMWARE_VERSION.Text =  "Ver 1.23.11152";
            iniPath = localAppData + Contanst.IniFileName;
            //===== Configurate timer_modbus =====
            timer_modbus.Tick += new EventHandler(timer_modbus_Tick);
            timer_modbus.Interval = Contanst.POLLING_INTERVAL;
            //===== Configurate timer_interval =====           
            timer_interval.Tick += new EventHandler(timer_interval_Tick);
            timer_interval.Interval = 300;
            //===== Configurate timer_interval for system.date ===== 
            timer_datetime.Tick += new EventHandler(timer_datetime_Tick); ;
            timer_datetime.Interval = 1000;
            timer_datetime.Start();
            #region /*** 其他相關設定 ***/
            serial_port = new serial_app();
            string[] ports = SerialPort.GetPortNames();
            comboboxCOMPORT.ItemsSource = ports;
            comboboxBAUDRATE.ItemsSource = new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", };
            timer_modbus.Interval = Contanst.POLLING_INTERVAL;
            timer_interval.Interval = 600;
            #endregion

            #region /*** setup.ini 初始設定 ***/
            if (!Directory.Exists(localAppData))
                System.IO.Directory.CreateDirectory(localAppData);
            
            if (File.Exists(iniPath))
                Load_iniFile(iniPath);
            else
            {
                Save_IniFile(iniPath);
            }           
            #endregion
            #region Additional sets 
            //===== 觸發叫出左側 drawer 的範圍以及事件宣告 , 並放置在 gridLEFT_DRAWER_CONTENT 中 =====
            StackPanel stackpanel_drawer_zone = new StackPanel();
            {
                stackpanel_drawer_zone.Width = 3;
                stackpanel_drawer_zone.Height = Double.NaN;
                stackpanel_drawer_zone.HorizontalAlignment = HorizontalAlignment.Left;
                stackpanel_drawer_zone.MouseEnter += new MouseEventHandler(stackpanel_drawer_zone_enter);
                stackpanel_drawer_zone.Background = Brushes.Transparent;
                stackpanel_drawer_zone.SetValue(Grid.RowProperty, 1);
                gridLEFT_DRAWER_CONTENT.Children.Add(stackpanel_drawer_zone);
            }
            fk_mysql_command = new fkMysqlCommand(ini.IniReadValue("MYSQL", "connect_prompt", iniPath));
            #endregion

            #region /**** 視窗最小化 ****/
            _contextMenu = new System.Windows.Forms.ContextMenu();
            _closeApp = new System.Windows.Forms.MenuItem() { Text = "Exit" };
            _iContainer = new System.ComponentModel.Container();

            System.Windows.Forms.MenuItem[] menuItems = new System.Windows.Forms.MenuItem[] { _closeApp };

            _contextMenu.MenuItems.AddRange(menuItems);

            _closeApp.Click += _closeApp_Click;

            
            _notifyIcon = new System.Windows.Forms.NotifyIcon(_iContainer);
            _notifyIcon.Icon = new System.Drawing.Icon(System.Environment.CurrentDirectory +  @"\desktop.ico");
            _notifyIcon.Text = "五大變量平台";
            _notifyIcon.Visible = true;
            _notifyIcon.MouseDoubleClick += _notifyIcon_MouseDoubleClick;
            _notifyIcon.ContextMenu = _contextMenu;
            #endregion
        }
        int itemCount;
        int rowCount;
        int colCount;
        #region /****  視窗最小化  ****/
        private void _notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.Show();
        }
        private void _closeApp_Click(object sender, EventArgs e)
        {
            _internalClosing = true;
            this.Close();
            _internalClosing = false;
        }
        #endregion
        List<View.EphProductView> EphProductViewList = new List<View.EphProductView>();
        public  void AddViewComponent()
        {

            itemCount++;
            if (itemCount > Contanst.MAX_DEVICE_NUM)
            {
                MessageBox.Show("數量不可超過8", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                return;
            }
                
            
            View.EphProductView view = new View.EphProductView();
            view.groupboxEPH_VIEW.Header = "eph" + itemCount.ToString();
            view.Tag = itemCount;
            view.MouseEnter+= new MouseEventHandler(mouse_enter);
            view.MouseLeave += new MouseEventHandler(mouse_leave);
            EphProductViewList.Add(view);
            //if (itemCount < 5)
            //{
            //    gridMAIN.ColumnDefinitions.Add(new ColumnDefinition());
            //}

            EphProductViewList[itemCount - 1].SetValue(Grid.RowProperty, rowCount);
            EphProductViewList[itemCount - 1].SetValue(Grid.ColumnProperty, colCount++);
            gridMAIN.Children.Add(EphProductViewList[itemCount - 1]);
            if (itemCount % 6 == 0)
            {
                rowCount++;
                colCount = 0;
            }

            if (rowCount > 3 && (itemCount %6 == 1))
            {
                gridMAIN.RowDefinitions.Add(new RowDefinition());
                groupboxMAIN.Height = 450 + ((rowCount - 3) * 112.5);
            }
        }
        private void mouse_enter(object Sender, EventArgs e)
        {
            View.EphProductView view = (View.EphProductView)Sender;
            //view.groupboxEPH_VIEW.Header = "header" + view.Tag.ToString();
            view.Pop.IsOpen = true;
        }
        private void mouse_leave(object Sender, EventArgs e)
        {
            View.EphProductView view = (View.EphProductView)Sender;
            view.Pop.IsOpen = false;
        }
        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            //if (background.fk_background_sync.Text != "")
            //    sync_string = background.fk_background_sync.Text;
            if (com_open == false)
            {
                if (comboboxCOMPORT.Text == null)
                {
                    buttonConnect.Content = tlb.CONNECT_LABEL[0];
                    MessageBox.Show("select Com Port!!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
                serial_port.Port = comboboxCOMPORT.Text;
                serial_port.BaudRate = Convert.ToInt32(comboboxBAUDRATE.Text);
                if (modbus_obj.modbus_open(serial_port.serialport) == false)
                {
                    buttonConnect.Content = tlb.CONNECT_LABEL[0];
                    System.Windows.MessageBox.Show("Com Port open error!!", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
                //modbus_id = Convert.ToUInt16(textboxMODBUSID.Text);
                timer_modbus.Start();
                timer_interval.Stop();
                modbus_read_flag = true;
                buttonConnect.Content = tlb.CONNECT_LABEL[1];
                com_open = true;
                modbus_error_count = 0;
            
            }
            else
            {
                com_open = false;
                buttonConnect.Content = tlb.CONNECT_LABEL[0];
                modbus_obj.modbus_close();
                timer_modbus.Stop();
                timer_interval.Stop();
                modbus_read_flag = false;
            }
        }
        System.Windows.Forms.Panel mainPanel;
        RECT r;
        
        private void timer_datetime_Tick(object sender, EventArgs e)
        {
            textblockSYSTEM_TIME.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }
        private void timer_modbus_Tick(object sender, EventArgs e)
        {
            if (fk_communucation_sequence.Count == 0)
                return;
            int now_device_index = fk_communucation_sequence[fk_modbusDD_communication_index];
            DeviceColloction_t collection = fk_glist_DDdeviceCollection[now_device_index];
            ushort modbus_id = Convert.ToUInt16(fk_glist_DDdeviceCollection[now_device_index].go_DDsingle_connect_id.Text);

            if (modbus_read_flag == true)
            {
                collection.gt_DDmodbusTBL.get_data_by_word_count(modbus_obj, modbus_id, 
                                                            (ushort)collection.glist_DDrespSqguence[fk_command_state].Start, 
                                                            (ushort)collection.glist_DDrespSqguence[fk_command_state].Length);
                timer_interval.Enabled = true;
                timer_modbus.Enabled = false;
            }
        }
        private void timer_interval_Tick(object sender, EventArgs e)
        {
            if (fk_communucation_sequence.Count == 0)
                return;
            //===== 顯示的 fk_modbusDD_device_index =====
            int now_device_index = fk_communucation_sequence[fk_modbusDD_communication_index];
            DeviceColloction_t collection = fk_glist_DDdeviceCollection[now_device_index];
            if (collection.gt_DDmodbusTBL.updata_by_word_count(modbus_obj, (ushort)collection.glist_DDrespSqguence[fk_command_state].Start, (ushort)collection.glist_DDrespSqguence[fk_command_state].Length) == true)
            {
                if (progressbarACTIVE.Value >= progressbarACTIVE.Maximum)
                {
                    progressbarACTIVE.Value = 0;
                }
                progressbarACTIVE.Value += progressbarACTIVE.LargeChange;
                update_parameter_value(collection.glist_DDdata_list, collection, collection.glist_DDcomponentLink);
                modbus_error_count = 0;
            }
            else
            {
                if (++modbus_error_count >= 10)
                {
                    modbus_error_count = 0;
                    com_open = false;
                    //button_modbus.ForeColor = Color.Navy;
                    buttonConnect.Content = tlb.CONNECT_LABEL[0];
                    modbus_obj.modbus_close();
                    timer_modbus.Stop();
                    timer_interval.Stop();
                    modbus_read_flag = false;
                    System.Windows.MessageBox.Show("Comulication error", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
            }
            timer_modbus.Start();
            timer_interval.Stop();
            //===== V4 非常非常重要 =====
            if (++fk_command_state >= collection.glist_DDrespSqguence.Count)
            {
                fk_command_state = 0;
                if (++fk_modbusDD_communication_index >= fk_communucation_sequence.Count)
                {
                    fk_modbusDD_communication_index = 0;
                }
            }
        }
        int stringCount;
        private void update_parameter_value(List<modbus_cell> data, DeviceColloction_t deviceCollection, List<modbus_table.ResponseAssociate> responseList)
        {
            stringCount = 0;
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < responseList.Count; j++)
                {
                    if (data[i].Address == responseList[j].relevantAddr)
                    {
                        #region Contanst.USE_XML_FK_COMPONENT == true
                        if (Contanst.USE_XML_FK_COMPONENT == true)
                        {
                            if (responseList[j].obj.GetType().Equals(typeof(fk_edit)))
                            {
                                //===== edit 字串輸出 ======
                                if (data[i].value_type == modbus_cell.Value_Type.Int_type)
                                {
                                    if (++stringCount >= data[i].StrLen)
                                    {
                                        ((fk_edit)responseList[j].obj).fk_edit_text = fk_function.byteTostring(data, i - (stringCount - 1), stringCount - 1);
                                        stringCount = 0;
                                    }
                                }
                                else if (data[i].value_type == modbus_cell.Value_Type.UInt_type)
                                {
                                    if (responseList[j].combobox_mapping.Count == 0)
                                        ((fk_edit)responseList[j].obj).fk_edit_text = data[i].Uint_Value.ToString();
                                    else

                                        ((fk_edit)responseList[j].obj).fk_edit_text = XML.getMappingContent(responseList[j].combobox_mapping, data[i].Uint_Value);
                                }
                                else if (data[i].value_type == modbus_cell.Value_Type.UInt32_type)
                                    ((fk_edit)responseList[j].obj).fk_edit_text = data[i].Uint32_Value.ToString();
                                else if (data[i].value_type == modbus_cell.Value_Type.float_type)
                                    ((fk_edit)responseList[j].obj).fk_edit_text = data[i].Float_Value.ToString();
                                else if (data[i].value_type == modbus_cell.Value_Type.double_type)
                                    ((fk_edit)responseList[j].obj).fk_edit_text = data[i].Double_Value.ToString();
                            }
                            if (responseList[j].obj.GetType().Equals(typeof(fk_combobox)))
                            {
                                if (data[i].value_type == modbus_cell.Value_Type.UInt_type)
                                    ((fk_combobox)responseList[j].obj).fk_combobox_SelectedIndex = XML.getMappingIndex(responseList[j].combobox_mapping, data[i].Uint_Value);
                            }
                        }
                        #endregion
                        if (Contanst.USE_XML_FK_COMPONENT == false)
                        {
                            if (responseList[j].obj.GetType().Equals(typeof(TextBox)))
                            {
                                //===== edit 字串輸出 ======
                                if (data[i].value_type == modbus_cell.Value_Type.Int_type)
                                {
                                    if (++stringCount >= data[i].StrLen)
                                    {
                                        ((TextBox)responseList[j].obj).Text = fk_function.byteTostring(data, i - (stringCount - 1), stringCount - 1);
                                        stringCount = 0;
                                    }
                                }
                                else if (data[i].value_type == modbus_cell.Value_Type.UInt_type)
                                {
                                    if (responseList[j].combobox_mapping.Count == 0)
                                        ((TextBox)responseList[j].obj).Text = data[i].Uint_Value.ToString();
                                    else

                                        ((TextBox)responseList[j].obj).Text = XML.getMappingContent(responseList[j].combobox_mapping, data[i].Uint_Value);
                                }
                                else if (data[i].value_type == modbus_cell.Value_Type.UInt32_type)
                                    ((TextBox)responseList[j].obj).Text = data[i].Uint32_Value.ToString();
                                else if (data[i].value_type == modbus_cell.Value_Type.float_type)
                                    ((TextBox)responseList[j].obj).Text = data[i].Float_Value.ToString("F5");
                                else if (data[i].value_type == modbus_cell.Value_Type.double_type)
                                    ((TextBox)responseList[j].obj).Text = data[i].Double_Value.ToString("F5");
                                //===== 即時 chart 顯示 ======
                                if (deviceCollection.gt_DDrealtimeInfo.component_name == responseList[j].name)
                                {
                                    if (++deviceCollection.gt_DDrealtimeInfo.real_number >= deviceCollection.gt_DDrealtimeInfo.target_number)
                                    {
                                        deviceCollection.gt_DDrealtimeInfo.real_number = 0;
                                        deviceCollection.go_DDrealtimechart.SetAxisLimits(DateTime.Now);
                                        deviceCollection.go_DDrealtimechart.LineValues[0].Add(new RealTimeChart.MeasureModel
                                        {
                                            DateTime = DateTime.Now,
                                            Value = Convert.ToSingle(((TextBox)responseList[j].obj).Text)
                                        });
                                    }
                                    if(deviceCollection.go_DDrealtimechart.LineValues[0].Count>=200)
                                        deviceCollection.go_DDrealtimechart.LineValues[0].RemoveAt(0);
                                }
                            }
                            if (responseList[j].obj.GetType().Equals(typeof(ComboBox)))
                            {
                                if (data[i].value_type == modbus_cell.Value_Type.UInt_type)
                                    ((ComboBox)responseList[j].obj).SelectedIndex = XML.getMappingIndex(responseList[j].combobox_mapping, data[i].Uint_Value);
                            }
                            if (responseList[j].obj.GetType().Equals(typeof(fk_epht_billboard)))
                            {
                                List<UInt16> uint16dataList = new List<ushort>();
                                for (int accessary_address = 0; accessary_address < 40; accessary_address++)
                                {
                                    uint16dataList.Add(data[i + accessary_address].Uint_Value);
                                }
                                ((fk_epht_billboard)responseList[j].obj).update_parmeter2userinterface(uint16dataList);
                                int device_id = Convert.ToInt32(((fk_epht_billboard)responseList[j].obj).Name.Replace("ephtbillboardSENSOR_", ""));
                                ((fk_advancedchart)deviceCollection.go_DDhistorychart).update_checkbox_content(device_id - 1, ((fk_epht_billboard)responseList[j].obj).PFC_METER_ID);
                            }
                        }
                    }
                }                
            }
            //===== 歷史資料儲存 ======
            DateTime dt = DateTime.Now;
            if (dt.Minute % deviceCollection.gint_DDhistoryInterval == 0 && dt.Second >= 0 && dt.Second < 5)
            {
                if (deviceCollection.gbool_insert_data_repatative == false)
                {
                    if (deviceCollection.gbool_special_device_type)
                    {
                        for (int i = 0; i < deviceCollection.gint_connected_device_number; i++)
                            HistoryInsertTask(deviceCollection, responseList, i, true);
                    }
                    else
                        HistoryInsertTask(deviceCollection, responseList, 0, false);
                    deviceCollection.gbool_insert_data_repatative = true;
                }
                else
                {
                    deviceCollection.gbool_insert_data_repatative = false;
                }
            }
        }
        void HistoryInsertTask(DeviceColloction_t device_collection, List<modbus_table.ResponseAssociate> response_list, int insert_cycle, bool otherType)
        {     
            int timeStamp = Convert.ToInt32(DateTime.UtcNow.AddHours(0).Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            PaltformIndentity indentity = device_collection.gt_DDplatformIndentity;
            DateTime mData = DateTime.Now;
            string sqlTableName = "";
            switch (indentity.serial)
            {
                case "Level": sqlTableName = "level_sensor"; break;
                case "Flow": sqlTableName = "flow_sensor"; break;
                case "Pressure": sqlTableName = "pressure_sensor"; break;
                case "Temperature": sqlTableName = "temperature_sensor"; break;
                case "Dust": sqlTableName = "concentration_sensor"; break;
            }
            List<sqlitem_t> collection_history = device_collection.glist_DDhistory;
            sqldisplay_t sql_display = device_collection.gt_DDdisplay;
            string sql_key_string = "", sql_value_string = "";
            bool component_match = false;

            sql_key_string += "sensor,";
            sql_value_string += "\"" + indentity.sensor + "\"" + ",";
            sql_key_string += "modbus_id,";
            sql_value_string += "\"" + device_collection.go_DDsingle_connect_id.Text + "\"" + ",";
            sql_key_string += "station_name,";
            sql_value_string += "\"" + device_collection.gs_stationName + "\"" + ",";
            if (otherType == false)
            {
                for (int i = 0; i < collection_history.Count; i++, component_match = false)
                {
                    for (int j = 0; j < response_list.Count; j++)
                    {
                        if (collection_history[i].component_name == response_list[j].name && component_match == false)
                        {
                            component_match = true;
                            sql_key_string += collection_history[i].key + ",";
                            if (response_list[j].combobox_mapping.Count == 0)
                                sql_value_string += "\"" + ((TextBox)response_list[j].obj).Text.Replace("\0", String.Empty) + "\"" + ",";
                            else
                                sql_value_string += "\"" + XML.setMappingContent(response_list[j].combobox_mapping, ((TextBox)response_list[j].obj).Text.Replace("\0", String.Empty)).ToString() + "\"" + ",";
                            //====== 非常重要 =====
                            if (response_list[j].name == "editSERIAL_NUMBER" || response_list[j].name == "editMETER_ID")
                            {
                                ((HistoryChart)device_collection.go_DDhistorychart).SQLSensorSN = ((TextBox)response_list[j].obj).Text.Replace("\0", String.Empty);
                                if (((HistoryChart)device_collection.go_DDhistorychart).SQLSensorSN.Equals("")) return;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < response_list.Count; j++, component_match = false)
                {
                    if (collection_history[insert_cycle].component_name == response_list[j].name && component_match == false)
                    {
                        component_match = true;
                        //===== 解析EPH0019 的 sql command, 在fk_epht_billboard 的 method 中 =====
                        if (response_list[j].obj.GetType().Equals(typeof(fk_epht_billboard)))
                            ((fk_epht_billboard)response_list[j].obj).update_parmeter2SQLcommand(ref sql_key_string, ref sql_value_string);

                        //====== 非常重要 =====
                        if (response_list[j].name == "editSERIAL_NUMBER" || response_list[j].name == "editMETER_ID")
                        {
                            ((fk_advancedchart)device_collection.go_DDhistorychart).SQLSensorSN = ((TextBox)response_list[j].obj).Text.Replace("\0", String.Empty);
                            if (((fk_advancedchart)device_collection.go_DDhistorychart).SQLSensorSN.Equals("")) return;
                        }
                    }
                }
            }

            if (otherType)
            {
                sql_key_string += sql_display.key;
                sql_value_string += "\"" + "0";
            }
            else
            {
                sql_key_string += sql_display.key;
                for (int j = 0; j < response_list.Count; j++)
                {
                    if (sql_display.component_name == response_list[j].name)
                    {
                        sql_value_string += "\"" + ((TextBox)response_list[j].obj).Text;
                    }
                }
            }
            //===== Change the farware totle flow unit form 'liter' to 'm^3', Ver 1.1.10 =====
            fk_mysql_command.InsertData("insert into " + sqlTableName + "(gateway_imei,response_time," + sql_key_string + ") values" +
                 "(" + "\"" + System.Security.Principal.WindowsIdentity.GetCurrent().Name + "\"" + "," +
                 +timeStamp + "," + sql_value_string + "\"" + ")");
            //responseList
        }
        void sync_parameter_function(List<modbus_cell> data, DeviceColloction_t deviceCollection, List<modbus_table.ResponseAssociate> responseList)
        {
            int sequence = 0;
            int stringCount = 0;
            ushort modbus_id = Convert.ToUInt16(deviceCollection.go_DDsingle_connect_id.Text);
            PaltformIndentity indentity = deviceCollection.gt_DDplatformIndentity;
            //===== 非常重要, io-link-rs485 要多下一次 command, 而只是rs485 就不能多下 Command =====
            if (indentity.CommInterface == "io-link-rs485")
                transmission_initial_func();
            #region Contanst.USE_XML_FK_COMPONENT == true
            if (Contanst.USE_XML_FK_COMPONENT == true)
            {
                for (int i = 0; i < responseList.Count; i++)
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j].Address == responseList[i].relevantAddr)
                        {
                            if (responseList[i].obj.GetType().Equals(typeof(fk_edit)))
                            {
                                if (data[j].value_type == modbus_cell.Value_Type.Int_type)
                                {
                                    if (++stringCount >= data[j].StrLen)
                                    {
                                        fk_function.stringTobyte(data, j, ((fk_edit)responseList[i].obj).fk_edit_text);
                                        stringCount = 0;
                                    }
                                }
                                else if (data[j].value_type == modbus_cell.Value_Type.UInt_type)
                                {
                                    if (responseList[i].combobox_mapping.Count == 0)
                                        data[j].Uint_Value = Convert.ToUInt16(((fk_edit)responseList[i].obj).fk_edit_text);
                                    else
                                        data[j].Uint_Value = XML.setMappingContent(responseList[i].combobox_mapping, ((fk_edit)responseList[j].obj).fk_edit_text);
                                }
                                else if (data[j].value_type == modbus_cell.Value_Type.UInt32_type)
                                    data[j].Uint32_Value = Convert.ToUInt32(((fk_edit)responseList[i].obj).fk_edit_text);
                                else if (data[j].value_type == modbus_cell.Value_Type.float_type)
                                    data[j].Float_Value = Convert.ToSingle(((fk_edit)responseList[i].obj).fk_edit_text);
                                else if (data[j].value_type == modbus_cell.Value_Type.double_type)
                                    data[j].Double_Value = Convert.ToDouble(((fk_edit)responseList[i].obj).fk_edit_text);
                            }
                            if (responseList[i].obj.GetType().Equals(typeof(fk_combobox)))
                            {
                                if (data[j].value_type == modbus_cell.Value_Type.UInt_type)
                                    data[j].Uint_Value = XML.setMappingIndex(responseList[i].combobox_mapping, ((fk_combobox)responseList[i].obj).fk_combobox_SelectedIndex);
                            }
                        }
                    }
                }
            }
            #endregion
            if (Contanst.USE_XML_FK_COMPONENT == false)
            {
                for (int i = 0; i < responseList.Count; i++)
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j].Address == responseList[i].relevantAddr)
                        {
                            if (responseList[i].obj.GetType().Equals(typeof(TextBox)))
                            {
                                if (data[j].value_type == modbus_cell.Value_Type.Int_type)
                                {
                                    if (++stringCount >= data[j].StrLen)
                                    {
                                        fk_function.stringTobyte(data, j, ((TextBox)responseList[i].obj).Text);
                                        stringCount = 0;
                                    }
                                }
                                else if (data[j].value_type == modbus_cell.Value_Type.UInt_type)
                                {
                                    if (responseList[i].combobox_mapping.Count == 0)
                                        data[j].Uint_Value = Convert.ToUInt16(((TextBox)responseList[i].obj).Text);
                                    else
                                        data[j].Uint_Value = XML.setMappingContent(responseList[i].combobox_mapping, ((TextBox)responseList[j].obj).Text);
                                }
                                else if (data[j].value_type == modbus_cell.Value_Type.UInt32_type)
                                    data[j].Uint32_Value = Convert.ToUInt32(((TextBox)responseList[i].obj).Text);
                                else if (data[j].value_type == modbus_cell.Value_Type.float_type)
                                    data[j].Float_Value = Convert.ToSingle(((TextBox)responseList[i].obj).Text);
                                else if (data[j].value_type == modbus_cell.Value_Type.double_type)
                                    data[j].Double_Value = Convert.ToDouble(((TextBox)responseList[i].obj).Text);
                            }
                            if (responseList[i].obj.GetType().Equals(typeof(ComboBox)))
                            {
                                if (data[j].value_type == modbus_cell.Value_Type.UInt_type)
                                    data[j].Uint_Value = XML.setMappingIndex(responseList[i].combobox_mapping, ((ComboBox)responseList[i].obj).SelectedIndex);
                            }
                        }
                    }
                }
            }
            //====== Version9 ======
            while (sequence < deviceCollection.glist_DDwriteSqguence.Count)
            {
                if (!deviceCollection.gt_DDmodbusTBL.write_data_by_word_count(modbus_obj, modbus_id, (ushort)deviceCollection.glist_DDwriteSqguence[sequence].Start,(ushort)deviceCollection.glist_DDwriteSqguence[sequence].Length))
                {
                    System.Windows.MessageBox.Show("寫入失敗", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
                sequence++;
            };
            if(SaveSystemVarToEEprom(deviceCollection, modbus_id)) 
                System.Windows.MessageBox.Show("寫入成功", "訊息", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            else
                System.Windows.MessageBox.Show("MODBUS-DD沒有定義正確儲存旗標的位置", "訊息", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
            //modbus_id = Convert.ToUInt16(textboxMODBUSID.Text);
        }
        #region 視窗拖曳功能
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        #endregion
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Window win = (Window)sender;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                this.WindowState = WindowState.Minimized;
            }
        }
        bool SaveSystemVarToEEprom( DeviceColloction_t deviceCollection, ushort modbus_id)
        {
            int i;
            for (i = 0; i < deviceCollection.glist_DDdata_list.Count; i++)
            {
                if (deviceCollection.glist_DDdata_list[i].Address == deviceCollection.gt_DDplatformIndentity.SaveEepromAddr)
                    break;
            }
            if (i == deviceCollection.glist_DDdata_list.Count)
            {               
                return false;
            }
            
            deviceCollection.glist_DDdata_list[i].Write = true;
            deviceCollection.glist_DDdata_list[i].Uint_Value = 1;
            deviceCollection.gt_DDmodbusTBL.write_to_value(modbus_obj, modbus_id, deviceCollection.glist_DDdata_list[i].Address);
            deviceCollection.gt_DDmodbusTBL.write_to_value(modbus_obj, modbus_id, deviceCollection.glist_DDdata_list[i].Address);
            return true;
        }
        public void transmission_initial_func()
        {
            byte[] tx_buf = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x01, 0x80, 0xCA, 0x00, 0x00 };
            modbus_obj.serial_port.Write(tx_buf, 0, 8);
            Thread.Sleep(200);
            if (modbus_obj.serial_port.BytesToRead > 1)
            {
                modbus_obj.serial_port.Read(tx_buf, 0, 10);
            }
        }
        void modbus_polling_disable()
        {
            modbus_read_flag = false;
            //timer_interval.Stop();
            //timer_modbus.Stop();
            //fk_theme_background.FK_CONNECT_TEXT = "Connect";
        }
        void modbus_polling_enable()
        {
            modbus_read_flag = true;
            //timer_interval.Stop();
            //timer_modbus.Start();
            //fk_theme_background.FK_CONNECT_TEXT = "Stop";
        }
        class functionDescription_t
        {
            public object className;
            public string MethodName;
            public MethodInfo theMethod;
            public object[] argumentObj;
            public object returnObj;
        }
        #region gridMAIN_Drop 事件
        private void gridMAIN_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }      
        private void gridMAIN_Drop(object sender, DragEventArgs e)
        {
            string fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            ddparameter_t drop_dd = new  ddparameter_t() { xml_name = fileName, station_name = "裝置名稱" + fk_modbusDD_event_index.ToString(), joint_communication = true, modbus_id = (fk_modbusDD_event_index + 1).ToString(), history_interval = "10" };
            gs_dd_parameterList.Add(drop_dd);
            //===== Modbus-DD 解析功能 =====
            XMLfileAnalyze(drop_dd.xml_name, drop_dd.station_name, drop_dd.joint_communication, drop_dd.modbus_id, drop_dd.history_interval);
        }
        #endregion
        class realtime_t
        {
            public  string component_name;
            public int target_number;
            public int real_number;
            public string value_unit;
            public decimal value;
        }
        class sqlitem_t
        {
            public string component_name;
            public string value_type;
            public string key;
        }
        class sqldisplay_t
        {
            public string component_name;
            public string key;
        }
        private void togglebutton_click(object sender, RoutedEventArgs e)
        {
            ToggleButton togglebutton = (ToggleButton)sender;
            if (togglebutton.Name.Equals("connecting_togglebutton" + (fk_modbusDD_device_index).ToString()))
            {
                fk_glist_DDdeviceCollection[fk_modbusDD_device_index].gbool_communication_joint = (togglebutton.IsChecked == true) ? true : false;
                packiconList[fk_modbusDD_device_index].Kind = (togglebutton.IsChecked == true) ? MaterialDesignThemes.Wpf.PackIconKind.LanConnect : MaterialDesignThemes.Wpf.PackIconKind.LanDisconnect;
                packiconList[fk_modbusDD_device_index].Foreground = (togglebutton.IsChecked == true) ? Brushes.Green : Brushes.Red;
                togglebutton.ToolTip = (togglebutton.IsChecked == true) ? "取消加入連線" : "加入連線";
                //===== 搜尋實際要通訊的 Device =====
                fk_communucation_sequence.Clear();
                for (int i = 0; i < fk_glist_DDdeviceCollection.Count; i++)
                {
                    if (fk_glist_DDdeviceCollection[i].gbool_communication_joint == true)
                    {
                        fk_communucation_sequence.Add(i);
                    }
                }
                fk_modbusDD_communication_index = 0;
                gs_dd_parameterList[fk_modbusDD_device_index].joint_communication = fk_glist_DDdeviceCollection[fk_modbusDD_device_index].gbool_communication_joint;
            }
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name.Equals("treeviewer_button"))
            {
                gridMAIN.Children.Clear();
                gridMAIN.Children.Add((UIElList[(int)button.Tag]));
                fk_modbusDD_device_index = (int)button.Tag;
                drawerhostMAIN.IsLeftDrawerOpen = false;
            }
            else if (button.Name.Equals("read_button" + (fk_modbusDD_device_index).ToString()))
            {
                if (buttonConnect.Content.ToString() == tlb.CONNECT_LABEL[1])
                {
                    if (button.Content.ToString() ==tlb.SYNC_LABEL[0])
                    {
                        button.Content = tlb.SYNC_LABEL[1];
                        timer_modbus.Enabled = false;
                        modbus_polling_disable();
                        timer_interval.Enabled = false;
                        timer_interval.Stop();
                        //return;
                    }
                    else if (button.Content.ToString() == tlb.SYNC_LABEL[1])
                    {
                        //===== 修正 modbus write register 功能, fk_modbusDD_communication_index-->fk_modbusDD_device_index, 1.2311152 =====
                        int now_device_index = fk_communucation_sequence[fk_modbusDD_device_index];
                        DeviceColloction_t collection = fk_glist_DDdeviceCollection[now_device_index];
                        sync_parameter_function(collection.glist_DDdata_list, collection, collection.glist_DDcomponentLink);
                        button.Content = tlb.SYNC_LABEL[0];
                        timer_modbus.Enabled = true;
                        modbus_polling_enable();
                    }
                }
                else
                {
                    button.Content = tlb.SYNC_LABEL[0];
                }
            }
            else if (button.Name.Equals("write_button" + (fk_modbusDD_device_index).ToString()))
            {

            }
        }

        private void textbox_textchanged(object sender, TextChangedEventArgs args)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Name.Equals("single_connect_id" + (fk_modbusDD_device_index).ToString()))
            {
                textblock_modbusIDList[fk_modbusDD_device_index].Text = textbox.Text;
                gs_dd_parameterList[fk_modbusDD_device_index].modbus_id= textbox.Text;
            }
            else if(textbox.Name.Equals("history_peroid" + (fk_modbusDD_device_index).ToString()))
            {
                fk_glist_DDdeviceCollection[fk_modbusDD_device_index].gint_DDhistoryInterval = Convert.ToInt32(textbox.Text);
                gs_dd_parameterList[fk_modbusDD_device_index].history_interval = textbox.Text;
            }
            else if (textbox.Name.Equals("textboxSENSOR_STATION"))
            {
                treeviewer_buttonList[fk_modbusDD_device_index].ToolTip = textbox.Text;
                fk_glist_DDdeviceCollection[fk_modbusDD_device_index].gs_stationName = textbox.Text;
                gs_dd_parameterList[fk_modbusDD_device_index].station_name = textbox.Text;
            }       
        }
        #region /===== 變更平台屬性 =====
        void PaltformIndentitySet(PaltformIndentity indentity)
        {
            string imageName = "";
            switch (indentity.serial)
            {
                case "Flow":  imageName = "WATER.png"; break;
                case "Level": imageName = "water_1.png"; break;
            }
            imageBrushMain.ImageSource = new BitmapImage(new Uri("..\\..\\style\\" + imageName, UriKind.Relative));
        }
        #endregion
        #region /*** ini file load function ***/
        private void Load_iniFile(string ini_name)
        {
            comboboxCOMPORT.Text = ini.IniReadValue("COMMULATION SETTING", "comport", ini_name);
            comboboxBAUDRATE.Text = ini.IniReadValue("COMMULATION SETTING", "baudrate", ini_name);
        }
        #endregion
        #region /*** ini file save function ***/
        private void Save_IniFile(string ini_name)
        {
            ini.IniWriteValue("COMMULATION SETTING", "comport", comboboxCOMPORT.Text, ini_name);
            ini.IniWriteValue("COMMULATION SETTING", "baudrate", comboboxBAUDRATE.Text, ini_name);
        }
        #endregion


        private void buttonSetting_Click(object sender, RoutedEventArgs e)
        {
            mainPanel = new System.Windows.Forms.Panel();
            mainform.Child = mainPanel;
            //var exeName = @"F:\Ongoing Project\[1]FW project\[51]五大變量調試軟體優化案\EPHTransmitter_Software_Tool - V6\EPHTransmitter_Software_Tool\bin\Debug\Release\Finetek_FPAS_UtilityTool";
            //var exeName = @"F:\Ongoing Project\[1]FW project\[43]M220603R1-EPHtranssmitter\PC軟體\EPHTransmitter_Software_Tool_Ver1007\EPHTransmitter_Software_Tool\bin\Debug\EPHTransmitter_Software_Tool";
            var exeName = @"C:\Users\lin71\Desktop\【FineTek】\ECP1\ECX_SoftwareTool_V2012\ECX_SoftwareTool\bin\Debug\ECX_SoftwareTool";
            Process p = new Process();
            p.StartInfo.FileName = exeName;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();
            while (p.MainWindowHandle.ToInt32() == 0)
            {
                System.Threading.Thread.Sleep(100);
            }
            IntPtr appWin = p.MainWindowHandle;
            IntPtr hwnd = mainform.Handle;
            GetWindowRect(appWin, ref r);
            SetParent(appWin, hwnd);
            int width = r.Right - r.Left;                 //窗口的宽度
            int height = r.Bottom - r.Top;                //窗口的高度
            MoveWindow(appWin, 0, 0, height, width, true);
            windowMAIN.Height = height + 110;
            windowMAIN.Width = width + 160;
            scollviewerMAIN.Height = height + 50;
            groupboxMAIN.Height = height + 50;
            groupboxMAIN.Width = width;
        }

        private void ex_combobox_selectindex_change(object sender, SelectionChangedEventArgs e)
        {
            DeviceColloction_t collection = fk_glist_DDdeviceCollection[fk_modbusDD_device_index];
            object textbox=null;
            for (int i = 0; i < collection.glist_DDfunctionDescrition.Count; i++)
            {
                functionDescription_t description = collection.glist_DDfunctionDescrition[i];
                if (description.MethodName == "MergeContent")
                {
                    textbox = description.theMethod.Invoke(description.className, description.argumentObj);
                    ((TextBox)description.returnObj).Text = ((TextBox)textbox).Text;
                }
                else if (description.MethodName == "SelectionContent")
                {
                   description.returnObj= description.theMethod.Invoke(description.className, description.argumentObj);
                    //SelectedIndex = ((ComboBox)textbox).SelectedIndex;
                }
                else if (description.MethodName == "LoadTempalete")
                {
                    description.theMethod.Invoke(description.className, description.argumentObj);
                }
            }
        }
        private void ex_textbox_textchanged(object sender, TextChangedEventArgs args)
        {
            DeviceColloction_t collection = fk_glist_DDdeviceCollection[fk_modbusDD_device_index];
            TextBox textbox_component = (TextBox)sender;
            object textbox = null;
            for (int i = 0; i < collection.glist_DDfunctionDescrition.Count; i++)
            {
                functionDescription_t description = collection.glist_DDfunctionDescrition[i];
                if (description.MethodName == "MergeContent")
                {
                    textbox = description.theMethod.Invoke(description.className, description.argumentObj);
                    ((TextBox)description.returnObj).Text = ((TextBox)textbox).Text;
                }
                else if (description.MethodName == "SelectionContent")
                {
                    description.returnObj = description.theMethod.Invoke(description.className, description.argumentObj);
                }
                else if (description.MethodName == "LoadTempalete")
                {
                    description.theMethod.Invoke(description.className, description.argumentObj);
                }
                else if (description.MethodName == "RenewDeviceNumber") //EPH0019
                {
                    //===== eph0019 傳訊器動態建立連接數量的方法 =====
                    if ((bool)description.theMethod.Invoke(description.className, description.argumentObj) == true )
                    {
                        timer_modbus.Stop();
                        timer_interval.Stop();
                        DeviceColloction_t device = fk_glist_DDdeviceCollection[fk_modbusDD_device_index];
                        int deviceNumber = Convert.ToInt32(((TextBox)description.argumentObj[0]).Text);
                        //===== 清除所有 ephtbillboardSENSOR 面板 =====
                        device.glist_DDcomponentInfo.RemoveAll(obj => obj.name.Contains("ephtbillboardSENSOR"));
                        device.glist_DDhistory.Clear();
                        //===== 清除所有讀取command sequence =====
                        device.glist_DDrespSqguence.Clear();
                        for ( i = 0; i < device.glist_DDcomponentInfo.Count; i++)
                        {
                            if (device.glist_DDcomponentInfo[i].name.Equals("stackpanelMAIN"))
                            {
                                //===== 清除所有 ephtbillboardSENSOR 面板 =====
                                ((WrapPanel)device.glist_DDcomponentInfo[i].component).Children.Clear();
                                for (int j = 0; j < deviceNumber; j++)
                                {
                                    fk_epht_billboard epht_billboard = new fk_epht_billboard() { Name = "ephtbillboardSENSOR_" + (j + 1).ToString() };
                                    device.glist_DDcomponentInfo.Add(new XMLparser.cbObj { component = epht_billboard, componentType = epht_billboard.GetType(), name = epht_billboard.Name });
                                    ((WrapPanel)device.glist_DDcomponentInfo[i].component).Children.Add(epht_billboard);
                                    device.glist_DDrespSqguence.Add(new address_zone(4128 + j * 40, 40));
                                    sqlitem_t sqlitemList = new sqlitem_t() { component_name = "ephtbillboardSENSOR_" + (j + 1).ToString(), key = "*", value_type = "EPHT", };
                                    device.glist_DDhistory.Add(sqlitemList);                                   
                                }                             
                            }
                        }
                        device.gint_connected_device_number = deviceNumber;
                        ((fk_advancedchart)device.go_DDhistorychart).initiate_check_content();
                        //=====連結MODBUS-DD中的 <ModbusParameter> 跟實際要顯示的物件 =====
                        XMLmodbus_table = new modbus_table(device.glist_DDcomponentInfo, device.glist_DDmodbusCell);
                        device.glist_DDcomponentLink = XMLmodbus_table.Modbus_Response;
                        timer_modbus.Start();
                    }
                }
            }
        }
        void stackpanel_drawer_zone_enter(object sender, MouseEventArgs e)
        {
                drawerhostMAIN.IsLeftDrawerOpen = true;
        }
        private void drawerhostMAIN_MouseLeave(object sender, MouseEventArgs e)
        {
            //===== close the drawer in a moment as mouse arrow left i 
            foreach (var device in fk_glist_DDdeviceCollection)
            {
                if (device.go_devicetree_stackpanel.ContextMenu.IsOpen == true)
                    return;
            }
            if (drawerhostMAIN.IsLeftDrawerOpen == true && contentmenuCOMPORT.IsOpen == false)
                drawerhostMAIN.IsLeftDrawerOpen = false;
        }
        class ddparameter_t{
            public string xml_name;
            public string station_name;
            public bool joint_communication;
            public string modbus_id;
            public string history_interval;
        }
        List<ddparameter_t> gs_dd_parameterList = new List<ddparameter_t>();
        private void button1ST_Click(object sender, RoutedEventArgs e)
        {
            gridMAIN.Children.Clear();
            gridMAIN.Children.Add((UIElList[0]));
        }

        private void button2ND_Click(object sender, RoutedEventArgs e)
        {
            gridMAIN.Children.Clear();
            gridMAIN.Children.Add((UIElList[1]));
        }
        private void ListBox_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listbox = (ListBox)sender;
            switch (listbox.SelectedIndex)
            {
                case 0:
                    this.Hide(); break;
                case 1:
                    if (this.WindowState == WindowState.Normal)
                    {
                        this.WindowState = WindowState.Maximized;
                        PackIcon packIcon = new PackIcon() { Kind = PackIconKind.WindowRestore, Height = 30, Width = 30, };
                        listboxitemWINDOWRESIZE.Content = packIcon;
                    }
                    else if (this.WindowState == WindowState.Maximized)
                    {
                        this.WindowState = WindowState.Normal;
                        PackIcon packIcon = new PackIcon() { Kind = PackIconKind.WindowMaximize, Height = 30, Width = 30, };
                        listboxitemWINDOWRESIZE.Content = packIcon;
                    }
                     break;
                case 2:
                    Save_IniFile(iniPath);
                    Environment.Exit(Environment.ExitCode); break;
            }
        }

        private void buttonOPEN_MODBUSDD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonAUTOID_Click(object sender, RoutedEventArgs e)
        {

        }

        void XMLfileAnalyze(string XMLname, string deviceName, bool jointCOMM , string modbusID, string historyINTERVAL)
        {
            gridMAIN.Children.Clear();
            //Grid gridtempraroty = new Grid() { Children = gridMAIN, Name = "gridMAIN" + groupboxList.Count.ToString() };
            XML = new XMLparser();
            List<address_zone> ReadSequence = new List<address_zone>();
            List<address_zone> WriteReadSequence = new List<address_zone>();
            realtime_t realtime_routine = new realtime_t();
            List<sqlitem_t> sqlitemList = new List<sqlitem_t>();
            sqldisplay_t sqlitemDisplay = new sqldisplay_t();
            PaltformIndentity paltformIndentity = new PaltformIndentity();
            XmlDocument xml = new XmlDocument();
            xml.Load(XMLname);
            XmlNode XmlRoot = xml.SelectSingleNode("FineTekModbusDD");
            List<modbus_table.modbusinfo> infoList = new List<modbus_table.modbusinfo>();
            //===== 個別連線ID專用 =====
            TextBox single_connect_id_textbox = null;
            TextBox single_history_period = null;
            bool special_devicee = false;
            List<functionDescription_t> functionDescriptionList = new List<functionDescription_t>();
            if (XmlRoot != null)
            {
                XmlNodeList HeadlineNodes = XML.receiveChildNotes(XmlRoot);
                //===== 第一層主標題 =====//
                for (int headline = 0; headline < HeadlineNodes.Count; headline++)
                {
                    string headline_name = HeadlineNodes.Item(headline).Name;
                    if (headline_name == "Header")
                    {
                        //===== Header標題內容開始解析 =====//
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            XMLhandle[0] = XML.headerContent(XnPjChild, windowMAIN, scollviewerMAIN, drawerhostMAIN, groupboxMAIN, gridMAIN, a);
                            XML.headerParameter(XnPjChild, ref paltformIndentity, a);
                        }
                    }
                    #region ===== MODBUS-DD 中 UserInterface 解析 ============== 
                    else if (headline_name == "UserInterface")
                    {
                        //===== UserInterface標題內容開始解析 =====//
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            XMLhandle[0] = XML.elementIndicator(XnPjChild, gridMAIN, a);

                            XmlNodeList Xnwb = XML.receiveChildNotes(XnPjChild.Item(a));
                            for (int b = 0; b < Xnwb.Count; b++)
                            {
                                XML.gridparameterSetup(XMLhandle[0], Xnwb, XMLhandle[0], b);
                                XMLhandle[1] = XML.elementIndicator(Xnwb, XMLhandle[0], b);
                                XmlNodeList XnMd = XML.receiveChildNotes(Xnwb.Item(b));
                                for (int c = 0; c < XnMd.Count; c++)
                                {
                                    XML.gridparameterSetup(XMLhandle[1], XnMd, XMLhandle[1], c);
                                    XMLhandle[2] = XML.elementIndicator(XnMd, XMLhandle[1], c);
                                    XmlNodeList Fourth = XML.receiveChildNotes(XnMd.Item(c));
                                    for (int d = 0; d < Fourth.Count; d++)
                                    {
                                        XML.gridparameterSetup(XMLhandle[2], Fourth, XMLhandle[2], d);
                                        XMLhandle[3] = XML.elementIndicator(Fourth, XMLhandle[2], d);
                                        XmlNodeList Fiveth = XML.receiveChildNotes(Fourth.Item(d));
                                        for (int E = 0; E < Fiveth.Count; E++)
                                        {
                                            XML.gridparameterSetup(XMLhandle[3], Fiveth, XMLhandle[3], E);
                                            XMLhandle[4] = XML.elementIndicator(Fiveth, XMLhandle[3], E);
                                            XmlNodeList Sixth = XML.receiveChildNotes(Fiveth.Item(E));
                                            for (int f = 0; f < Sixth.Count; f++)
                                            {
                                                XML.gridparameterSetup(XMLhandle[4], Sixth, XMLhandle[4], f);
                                                XMLhandle[5] = XML.elementIndicator(Sixth, XMLhandle[4], f);
                                                XmlNodeList Seventh = XML.receiveChildNotes(Sixth.Item(f));
                                                for (int g = 0; g < Seventh.Count; g++)
                                                {
                                                    XML.gridparameterSetup(XMLhandle[5], Seventh, XMLhandle[5], g);
                                                    XMLhandle[6] = XML.elementIndicator(Seventh, XMLhandle[5], g);
                                                    XmlNodeList Eightth = XML.receiveChildNotes(Seventh.Item(g));
                                                    for (int h = 0; h < Eightth.Count; h++)
                                                    {
                                                        XML.gridparameterSetup(XMLhandle[6], Eightth, XMLhandle[6], h);
                                                        XMLhandle[7] = XML.elementIndicator(Eightth, XMLhandle[6], h);
                                                        XmlNodeList Nineth = XML.receiveChildNotes(Eightth.Item(h));
                                                        for (int j = 0; j < Nineth.Count; j++)
                                                        {
                                                            XML.gridparameterSetup(XMLhandle[7], Nineth, XMLhandle[7], j);
                                                            XMLhandle[8] = XML.elementIndicator(Nineth, XMLhandle[7], j);
                                                            XmlNodeList Tenth = XML.receiveChildNotes(Nineth.Item(j));
                                                            for (int k = 0; k < Tenth.Count; k++)
                                                            {
                                                                XML.gridparameterSetup(XMLhandle[8], Tenth, XMLhandle[8], k);
                                                                XMLhandle[9] = XML.elementIndicator(Tenth, XMLhandle[8], k);
                                                                XmlNodeList Eleventh = XML.receiveChildNotes(Tenth.Item(k));
                                                                for (int m = 0; m < Eleventh.Count; m++)
                                                                {
                                                                    XML.gridparameterSetup(XMLhandle[9], Eleventh, XMLhandle[9], m);
                                                                    XMLhandle[10] = XML.elementIndicator(Eleventh, XMLhandle[9], m);
                                                                    XmlNodeList Twelth = XML.receiveChildNotes(Eleventh.Item(m));
                                                                    for (int n = 0; n < Twelth.Count; n++)
                                                                    {
                                                                        XML.gridparameterSetup(XMLhandle[10], Twelth, XMLhandle[10], n);
                                                                        XMLhandle[11] = XML.elementIndicator(Twelth, XMLhandle[10], n);
                                                                        XmlNodeList element13 = XML.receiveChildNotes(Twelth.Item(n));
                                                                        for (int p = 0; p < element13.Count; p++)
                                                                        {
                                                                            XML.gridparameterSetup(XMLhandle[11], element13, XMLhandle[11], p);
                                                                            XMLhandle[12] = XML.elementIndicator(element13, XMLhandle[11], p);
                                                                            XmlNodeList element14 = XML.receiveChildNotes(element13.Item(p));
                                                                            for (int t = 0; t < element14.Count; t++)
                                                                            {
                                                                                XML.gridparameterSetup(XMLhandle[12], element14, XMLhandle[12], t);
                                                                                XMLhandle[13] = XML.elementIndicator(element14, XMLhandle[12], t);
                                                                                XmlNodeList element15 = XML.receiveChildNotes(element14.Item(t));
                                                                                for (int s = 0; s < element15.Count; s++)
                                                                                {
                                                                                    XML.gridparameterSetup(XMLhandle[13], element15, XMLhandle[13], s);
                                                                                    XMLhandle[14] = XML.elementIndicator(element15, XMLhandle[13], s);
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region ===== MODBUS-DD 不要顯示的component , 目前尚未實作, V18 =====
                    else if (headline_name == "Collapsed")
                    {
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "Item")
                            {
                                string id = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Id").ToString();
                                for (int i = 0; i < XML.cbObjList.Count; i++)
                                {
                                    if (XML.cbObjList[i].name == id)
                                    {
                                        //===== Combobx 做 itemsMapping 的動作, itemindex[0,1,2,3] --> value[itemsMapping] ======
                                        ((TextBox)(XML.cbObjList[i].component)).Visibility = Visibility.Hidden;
                                        //XML.cbObjList[i].mapping(itemsMapping);
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region ===== 描述元件Combobox 下拉內容以及對應的 index =====
                    else if (headline_name == "UIscript")
                    {
                        //===== UIscript標題內容開始解析 =====//
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "ComboboxItemSource")
                            {
                                string type = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Type").ToString();
                                string[] items = (((XmlElement)XnPjChild.Item(a)).GetAttribute("Items").ToString().Replace(" ", "")).Split(',');
                                string[] itemsMapping = (((XmlElement)XnPjChild.Item(a)).GetAttribute("ItemsMapping").ToString().Replace(" ", "")).Split(',');
                                string id = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Id").ToString();
                                for (int i = 0; i < XML.cbObjList.Count; i++)
                                {
                                    if (XML.cbObjList[i].name == id)
                                    {
                                        //===== Combobx 做 itemsMapping 的動作, itemindex[0,1,2,3] --> value[itemsMapping] ======
                                        ((ComboBox)(XML.cbObjList[i].component)).ItemsSource = items;
                                        XML.cbObjList[i].mapping(itemsMapping);
                                    }
                                }
                            }
                            else if (xmlelement == "EditContentMapping")
                            {
                                string type = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Type").ToString();
                                string[] content = (((XmlElement)XnPjChild.Item(a)).GetAttribute("Content").ToString().Replace(" ", "")).Split(',');
                                string[] contentMapping = (((XmlElement)XnPjChild.Item(a)).GetAttribute("ContentMapping").ToString().Replace(" ", "")).Split(',');
                                string id = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Id").ToString();
                                for (int i = 0; i < XML.cbObjList.Count; i++)
                                {
                                    if (XML.cbObjList[i].name == id)
                                    {
                                        XML.cbObjList[i].setContentMapping(content, contentMapping);
                                        //===== Combobx 做 itemsMapping 的動作, itemindex[0,1,2,3] --> value[itemsMapping] ======                                                                              
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    #region ====== 描述 MODBUS-DD 檔中, 所有元件觸發後所產生得事件, 依需求而產生,非常非常重要, 決定了整個平常的擴充性 =====
                    else if (headline_name == "exFunction")
                    {
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "FuncInfo")
                            {
                                string name = ((XmlElement)XnPjChild.Item(a)).GetAttribute("name").ToString().Trim();
                                string response = ((XmlElement)XnPjChild.Item(a)).GetAttribute("response").ToString().Trim();
                                string[] argu = (((XmlElement)XnPjChild.Item(a)).GetAttribute("argu").ToString().Replace(" ", "")).Split(',');
                                string[] Event = (((XmlElement)XnPjChild.Item(a)).GetAttribute("event").ToString().Replace(" ", "")).Split(',');

                                functionDescription_t functionDescription = new functionDescription_t();
                                functionDescription.argumentObj = new object[argu.Count()];
                                if (paltformIndentity.sensor == "EPH")
                                {
                                    functionDescription.className = new exFunctionEPH();
                                }
                                if (paltformIndentity.sensor == "EPH0019")
                                {
                                    functionDescription.className = new exFunctionEPH0019();
                                }
                                functionDescription.theMethod = functionDescription.className.GetType().GetMethod(name);
                                functionDescription.MethodName = name;
                                for (int i = 0; i < XML.cbObjList.Count; i++)
                                {
                                    if (XML.cbObjList[i].name == response)
                                    {
                                        if (XML.cbObjList[i].component.GetType().Equals(typeof(TextBox)))
                                            functionDescription.returnObj = ((TextBox)XML.cbObjList[i].component);
                                        if (XML.cbObjList[i].component.GetType().Equals(typeof(ComboBox)))
                                            functionDescription.returnObj = ((ComboBox)XML.cbObjList[i].component);
                                    }
                                    for (int j = 0; j < argu.Count(); j++)
                                    {
                                        if (XML.cbObjList[i].name == argu[j])
                                        {
                                            if (XML.cbObjList[i].component.GetType().Equals(typeof(TextBox)))
                                            {
                                                functionDescription.argumentObj[j] = (TextBox)XML.cbObjList[i].component;
                                                //((TextBox)functionDescription.argumentObj[j]).Name = argu[j] + j.ToString();
                                            }
                                            else if (XML.cbObjList[i].component.GetType().Equals(typeof(ComboBox)))
                                            {
                                                functionDescription.argumentObj[j] = (ComboBox)XML.cbObjList[i].component;
                                                //((ComboBox)functionDescription.argumentObj[j]).Name = argu[j] + j.ToString();
                                            }
                                            switch (Event[j])
                                            {
                                                case "ci": ((ComboBox)functionDescription.argumentObj[j]).SelectionChanged += new SelectionChangedEventHandler(ex_combobox_selectindex_change); break;
                                                case "tt": ((TextBox)functionDescription.argumentObj[j]).TextChanged += new TextChangedEventHandler(ex_textbox_textchanged); break;
                                            }
                                        }
                                    }
                                }
                                functionDescriptionList.Add(functionDescription);
                            }
                        }

                    }
                    #endregion
                    else if (headline_name == "ModbusParameter")
                    {
                        //===== ModbusParameter標題內容開始解析 =====//
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "Cell")
                            {
                                string RelateName = ((XmlElement)XnPjChild.Item(a)).GetAttribute("RelateName").ToString();
                                string SecondRelateName = ((XmlElement)XnPjChild.Item(a)).GetAttribute("SecondRelateName").ToString();
                                string Address = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Address").ToString();
                                string DataType = ((XmlElement)XnPjChild.Item(a)).GetAttribute("DataType").ToString();
                                string StringLen = ((XmlElement)XnPjChild.Item(a)).GetAttribute("StringLen").ToString();
                                if (StringLen == "") StringLen = "0";
                                infoList.Add(new modbus_table.modbusinfo { x_address = Convert.ToInt32(Address), x_name = RelateName, secondx_name = SecondRelateName, x_type = DataType, x_str_len = Convert.ToInt32(StringLen) });
                                for (int i = 0; i < XML.cbObjList.Count; i++)
                                {
                                    if (XML.cbObjList[i].name == RelateName)
                                    {
                                        //XML.cbObjList[i].combobox.fk_combobox_ItemSource = items;
                                    }
                                }
                            }
                        }
                    }
                    else if (headline_name == "CommunicationRoutine")
                    {
                        //===== CommunicationRoutine標題內容開始解析 =====//
                        fk_command_sequence_total = 0;
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "Sequence")
                            {
                                string Address = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Address").ToString();
                                string Length = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Length").ToString();
                                //===== V4 =====
                                string Attribute = ((XmlElement)XnPjChild.Item(a)).GetAttribute("Attribute").ToString();
                                ReadSequence.Add(new address_zone(Convert.ToInt32(Address), Convert.ToInt32(Length)));
                                if (Attribute == "rw")
                                    WriteReadSequence.Add(new address_zone(Convert.ToInt32(Address), Convert.ToInt32(Length)));
                            }
                        }
                    }
                    else if (headline_name == "ChartDefination")
                    {
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            //====== number 代表要讀取多少次 modbus 成功才會存值, 參考 modbus_timer_interval + fk_command_state ======
                            string xmlelement = (XnPjChild.Item(a).Name);
                            if (xmlelement == "interval")
                            {
                                string calculate_targer = ((XmlElement)XnPjChild.Item(a)).GetAttribute("calculate_targer").ToString();
                                string number = ((XmlElement)XnPjChild.Item(a)).GetAttribute("number").ToString();
                                realtime_routine.target_number = ReadSequence.Count;
                            }
                            else if (xmlelement == "entity")
                            {
                                string RelateName = ((XmlElement)XnPjChild.Item(a)).GetAttribute("RelateName").ToString();
                                string unit = ((XmlElement)XnPjChild.Item(a)).GetAttribute("unit").ToString();
                                realtime_routine.component_name = RelateName;
                                realtime_routine.value_unit = unit;
                            }
                        }
                    }
                    else if (headline_name == "SqlRoutine")
                    {
                        XmlNodeList XnPjChild = XML.receiveChildNotes(HeadlineNodes.Item(headline));
                        for (int a = 0; a < XnPjChild.Count; a++)
                        {
                            string xmlelement = (XnPjChild.Item(a).Name);
                            //===== 要寫入 五大便輛資料庫的 Device Specific Command 的參數 =====
                            if (xmlelement == "historysqlitem")
                            {
                                string RelateName = ((XmlElement)XnPjChild.Item(a)).GetAttribute("RelateName").ToString();
                                string key = ((XmlElement)XnPjChild.Item(a)).GetAttribute("key").ToString();
                                string type = ((XmlElement)XnPjChild.Item(a)).GetAttribute("type").ToString();
                                sqlitemList.Add(new sqlitem_t { component_name = RelateName, key = key, value_type = type, });
                            }
                            //===== 要寫入 五大便輛資料庫的 common practice command 的參數 =====
                            if (xmlelement == "displaylitem")
                            {
                                string RelateName = ((XmlElement)XnPjChild.Item(a)).GetAttribute("RelateName").ToString();
                                string key = ((XmlElement)XnPjChild.Item(a)).GetAttribute("key").ToString();
                                sqlitemDisplay.component_name = RelateName;
                                sqlitemDisplay.key = key;
                            }
                        }
                    }
                }
            }

            //===== 變更平台屬性 =====
            //PaltformIndentitySet(paltformIndentity);           
            //=====連結MODBUS-DD中的 <ModbusParameter> 跟實際要顯示的物件 =====
            XMLmodbus_table = new modbus_table(XML.cbObjList, infoList);
            int pp = 0, tt = 0, dd = 0, oo = 0, ff = 0, ll = 0;
            UIElement UIEcontent = new UIElement();
            UIEcontent = gridMAIN.Children[0];
            UIElList.Add(UIEcontent);
            #region //===== 建立 treeviewer device 的相關顯示, 含 sensor 名稱, snesor 連線狀態, sensor 的 modbus id ======
            StackPanel devicetree_stackpanel = new StackPanel() { Orientation = Orientation.Horizontal , Name = "devicetree_stackpanel"+ fk_modbusDD_event_index.ToString() ,};
            Button treeviewer_button = new Button()
            {
                Name = "treeviewer_button",
                Tag = UIElList.Count - 1,
                Background = Brushes.Transparent,
                Foreground = Brushes.Black,
                ToolTip = deviceName,  // assign 裝置名稱
                FontSize = 12.0,
                Height = 20,
                Margin = new Thickness(0),
                BorderThickness = new Thickness(0),
            };
            treeviewer_button.Click += new RoutedEventHandler(button_click);
            treeviewer_buttonList.Add(treeviewer_button);
            //===== 顯示 device tree 的 modbus id =====
            textblock_modbusIDList.Add(new TextBlock() { Name = "textblock_modbusID" + fk_modbusDD_event_index.ToString(), Text = modbusID, FontStyle = FontStyles.Italic, Foreground = Brushes.Brown, Height = 20, Margin = new Thickness(0, 2, 10, 0) });
            PackIcon pack_icon = new PackIcon() { Height = 20, };
            pack_icon.Kind = (jointCOMM == true) ? MaterialDesignThemes.Wpf.PackIconKind.LanConnect : MaterialDesignThemes.Wpf.PackIconKind.LanDisconnect;
            pack_icon.Foreground = (jointCOMM == true) ? Brushes.Green : Brushes.Red;
            packiconList.Add(pack_icon);

            devicetree_stackpanel.Children.Add(textblock_modbusIDList[fk_modbusDD_event_index]);
            devicetree_stackpanel.Children.Add(treeviewer_button);
            devicetree_stackpanel.Children.Add(packiconList[fk_modbusDD_event_index]);
           // ContextMenu pMenu  = (ContextMenu)this.Resources["MyContextMenu"];
            ContextMenu pMenu = new ContextMenu();
            MenuItem iMenu = new MenuItem() { Header = "刪除 " + paltformIndentity.sensor, };
            iMenu.Click +=  new RoutedEventHandler(delete_dd_menuitem_click);
            iMenu.Tag = fk_modbusDD_event_index;
            pMenu.Items.Add(iMenu);
            pMenu.FontSize = 12;
            pMenu.FontWeight = FontWeights.Normal;
            devicetree_stackpanel.ContextMenu = pMenu;
            switch (paltformIndentity.serial)
            {
                case "Flow": treeviewer_button.Content = paltformIndentity.sensor; treeviewitemFLOW_SERIAL.Items.Add(devicetree_stackpanel); ff = treeviewitemFLOW_SERIAL.Items.Count - 1; break;
                case "Level": treeviewer_button.Content = paltformIndentity.sensor; treeviewitemLEVEL_SERIAL.Items.Add(devicetree_stackpanel); ll = treeviewitemLEVEL_SERIAL.Items.Count - 1; break;
                case "Dust": treeviewer_button.Content = paltformIndentity.sensor; treeviewitemDUST_SERIAL.Items.Add(devicetree_stackpanel); dd = treeviewitemDUST_SERIAL.Items.Count - 1; break;
                case "Temperature": treeviewer_button.Content = paltformIndentity.sensor; treeviewitemTEMPERATURE_SERIAL.Items.Add(devicetree_stackpanel); tt = treeviewitemTEMPERATURE_SERIAL.Items.Count - 1; break;
                case "Pressure": treeviewer_button.Content = paltformIndentity.sensor; treeviewitemPRESSURE_SERIAL.Items.Add(devicetree_stackpanel); pp = treeviewitemPRESSURE_SERIAL.Items.Count - 1; break;
                default: treeviewer_button.Content = paltformIndentity.sensor; treeviewitemOTHERS.Items.Add(devicetree_stackpanel); oo = treeviewitemOTHERS.Items.Count - 1; break;
            }
            #endregion
            RealTimeChart realtimeChart = new RealTimeChart() { Margin = new Thickness(0, 0, 0, 0), Padding = new Thickness(0), Height = double.NaN, YaxisTitle = realtime_routine.value_unit };

            object historyChart = null;

            if (paltformIndentity.sensor == "EPH0019")
            {
                historyChart = new fk_advancedchart() { Margin = new Thickness(0, 10, 0, 5), Height = 500, SQLContent = fk_mysql_command, SQLSensorSeries = paltformIndentity.serial, SQLsensorName = paltformIndentity.sensor };
                special_devicee = true;
                ((fk_advancedchart)historyChart).value_selection_create(paltformIndentity.sensor, sqlitemList.Select(x => x.key).ToList());
            }
            else
            {
                historyChart = new HistoryChart() { Margin = new Thickness(5, 0, 0, 5), SQLContent = fk_mysql_command, SQLSensorSeries = paltformIndentity.serial, SQLsensorName = paltformIndentity.sensor };
                special_devicee = false;
                ((HistoryChart)historyChart).value_selection_create(paltformIndentity.sensor, sqlitemList.Select(x => x.key).ToList());
            }
            for (int i = 0; i < XML.cbObjList.Count; i++)
            {
                if (XML.cbObjList[i].name == "groupboxREALTIME_CHART")
                    ((GroupBox)XML.cbObjList[i].component).Content = realtimeChart;

                if (XML.cbObjList[i].name == "groupboxHISTORY")
                    ((GroupBox)XML.cbObjList[i].component).Content = historyChart;

                if (XML.cbObjList[i].name == "textblockCOM")
                    ((TextBlock)XML.cbObjList[i].component).Text = treeviewitermCOMPORT1.Header.ToString();

                if (XML.cbObjList[i].name == "textboxSENSOR_STATION")
                {
                    ((TextBox)XML.cbObjList[i].component).Text = deviceName;  // assign 裝置名稱
                    ((TextBox)XML.cbObjList[i].component).TextChanged += new TextChangedEventHandler(textbox_textchanged);
                }

                if (XML.cbObjList[i].name == "textblockSERIAL")
                {
                    string series_string = "";
                    switch (paltformIndentity.serial)
                    {
                        case "Flow": series_string = treeviewitemFLOW_SERIAL.Header.ToString(); break;
                        case "Level": series_string = treeviewitemLEVEL_SERIAL.Header.ToString(); break;
                        case "Dust": series_string = treeviewitemDUST_SERIAL.Header.ToString(); break;
                        case "Temperature": series_string = treeviewitemTEMPERATURE_SERIAL.Header.ToString(); break;
                        case "Pressure": series_string = treeviewitemPRESSURE_SERIAL.Header.ToString(); break;
                        default: series_string = treeviewitemOTHERS.Header.ToString(); break;
                    }
                    ((TextBlock)XML.cbObjList[i].component).Text = series_string;
                }
                if (XML.cbObjList[i].name == "textblockSENSOR")
                {
                    ((TextBlock)XML.cbObjList[i].component).Text = treeviewer_button.Content.ToString();
                }
                if (XML.cbObjList[i].name == "stackpanelREALTIME_CONTROL")
                {
                    TextBlock textblock_joint = new TextBlock() { Text = "加入連線", Padding = new Thickness(3), Height = 20, Margin = new Thickness(0, 0, 10, 0) };
                    ToggleButton togglebutton = new ToggleButton() { Name = "connecting_togglebutton" + fk_modbusDD_event_index.ToString(), Width = 50, Style = (Style)this.FindResource("MaterialDesignSwitchToggleButton"), ToolTip = "取消加入連線", IsChecked = jointCOMM };
                    togglebutton.Click += new RoutedEventHandler(togglebutton_click);
                    TextBlock textblock = new TextBlock() { Text = "通訊ID:", Padding = new Thickness(3), Height = 20, Margin = new Thickness(0, 0, 10, 0) };
                    TextBox textbox = new TextBox() { Name = "single_connect_id" + fk_modbusDD_event_index.ToString(), Width = 50, Padding = new Thickness(0), Height = 24, BorderThickness = new Thickness(1), Margin = new Thickness(0, 0, 10, 0), FontSize = 11, HorizontalContentAlignment = HorizontalAlignment.Center, Text = modbusID };
                    textbox.TextChanged += new TextChangedEventHandler(textbox_textchanged);
                    TextBlock textblock1 = new TextBlock() { Text = "資料儲存週期:", Padding = new Thickness(3), Height = 20, Margin = new Thickness(0, 0, 10, 0) };
                    TextBox textbox1 = new TextBox() { Name = "history_peroid" + fk_modbusDD_event_index.ToString(), Width = 50, Padding = new Thickness(0), Height = 24, BorderThickness = new Thickness(1), Margin = new Thickness(0, 0, 10, 0), FontSize = 11, HorizontalContentAlignment = HorizontalAlignment.Center, Text = historyINTERVAL };
                    textbox1.TextChanged += new TextChangedEventHandler(textbox_textchanged);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(textblock);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(textbox);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(textblock_joint);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(togglebutton);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(textblock1);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(textbox1);
                    single_connect_id_textbox = textbox;
                    single_history_period = textbox1;
                }
                if (XML.cbObjList[i].name == "stackpanelSETTING_CONTROL")
                {
                    Button read = new Button() { Name = "read_button" + fk_modbusDD_event_index.ToString(), Tag = 0, Content = tlb.SYNC_LABEL[0], Height = 20, Width = double.NaN, Padding = new Thickness(3), Margin = new Thickness(0, 0, 10, 0) };
                    read.Click += new RoutedEventHandler(button_click);
                    Button write = new Button() { Name = "write_button" + fk_modbusDD_event_index.ToString(), Tag = 0, Content = "寫入", Height = 20, Width = double.NaN, Padding = new Thickness(3), Margin = new Thickness(0, 0, 10, 0) };
                    write.Click += new RoutedEventHandler(button_click);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(read);
                    ((StackPanel)XML.cbObjList[i].component).Children.Add(write);
                }

                if (XML.cbObjList[i].style_string != null)
                {
                    if (XML.cbObjList[i].componentType.Equals(typeof(GroupBox)))
                        ((GroupBox)XML.cbObjList[i].component).Style = (Style)this.FindResource(XML.cbObjList[i].style_string);
                }

            }
            List<XMLparser.cbObj> component_temp = new List<XMLparser.cbObj>();
            component_temp = XML.cbObjList;
            //====== treeviewer 的分類數量 ======
            fk_glist_DDdeviceCollection.Add(new DeviceColloction_t
            {
                glist_DDcomponentInfo = component_temp,
                gbool_communication_joint = true,
                go_DDsingle_connect_id = single_connect_id_textbox,
                gs_stationName = treeviewer_button.ToolTip.ToString(),
                glist_DDrespSqguence = ReadSequence,
                glist_DDwriteSqguence = WriteReadSequence,
                gt_DDmodbusTBL = XMLmodbus_table,
                glist_DDdata_list = XMLmodbus_table.Modbus_List,
                glist_DDcomponentLink = XMLmodbus_table.Modbus_Response,
                glist_DDhistory = sqlitemList,
                gt_DDdisplay = sqlitemDisplay,
                gt_DDrealtimeInfo = realtime_routine,
                go_DDrealtimechart = realtimeChart,
                go_DDhistorychart = historyChart,
                glist_DDmodbusCell = infoList,
                glist_DDfunctionDescrition = functionDescriptionList, //函數宣告
                gbool_special_device_type = special_devicee,
                gint_connected_device_number = 1,
                gint_DDhistoryInterval = Convert.ToInt32(single_history_period.Text),
                gt_DDplatformIndentity = paltformIndentity,
                gbool_insert_data_repatative = false,
                go_devicetree_stackpanel = devicetree_stackpanel,
            });
            //===== Modbus DD File 全部的數量 =====
            fk_modbusDD_event_index++;
            fk_modbusDD_device_index = fk_modbusDD_event_index - 1;

            //===== 搜尋實際要通訊的 Device =====
            fk_communucation_sequence.Clear();
            for (int i = 0; i < fk_glist_DDdeviceCollection.Count; i++)
            {
                if (fk_glist_DDdeviceCollection[i].gbool_communication_joint == true)
                {
                    fk_communucation_sequence.Add(i);
                }
            }
        }

        private void delete_dd_menuitem_click(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;
            int now_menu_index = (int)menuitem.Tag;
            {
                if (com_open == true)
                {
                    MessageBox.Show("請先停止連線後操作", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                    return;
                }
                gs_dd_parameterList.RemoveAt(now_menu_index);
                initial_dd_element();
                foreach (var dd in gs_dd_parameterList)
                {
                    XMLfileAnalyze(dd.xml_name, dd.station_name, dd.joint_communication, dd.modbus_id, dd.history_interval);
                }
            }
        }


        void menuitem_click(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = (MenuItem)sender;
            string tag = (string)menuitem.Tag;
            
            switch (tag)
            {
                case "menuitemINSERT_MODBUD_DD":
                    OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                    
                    dlg.Multiselect = true;
                    dlg.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
                    if (dlg.ShowDialog() == true)
                    {
                        int dd_count = gs_dd_parameterList.Count;
                        List<ddparameter_t> batch_dd_items = new List<ddparameter_t>();
                        foreach (string filename in dlg.FileNames)
                        {
                            gs_dd_parameterList.Add(new ddparameter_t() { xml_name = filename, station_name = "裝置名稱" + dd_count.ToString(), joint_communication = true, modbus_id = (dd_count + 1).ToString(), history_interval = "10" });
                            batch_dd_items.Add(new ddparameter_t() { xml_name = filename, station_name = "裝置名稱" + dd_count.ToString(), joint_communication = true, modbus_id = (dd_count + 1).ToString(), history_interval = "10" });
                            dd_count++;
                        }
                        foreach (var dd in batch_dd_items)
                        {
                            XMLfileAnalyze(dd.xml_name, dd.station_name, dd.joint_communication, dd.modbus_id, dd.history_interval);
                        }
                    }
                    Save_IniFile(iniPath);
                    break;
                case "menuitemCLEAR_MODBUD_DD":
                    if(com_open==true)
                    {
                        MessageBox.Show("請先停止連線後操作", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                        return;
                    }
                    gs_dd_parameterList.Clear();
                    initial_dd_element();
                    break;
                case "menuitemEXPAND_ALL": foreach (var item in treeviewitermCOMPORT1.Items) ((TreeViewItem)item).IsExpanded = true; break;
                case "menuitemCALLAPSE_ALL": foreach (var item in treeviewitermCOMPORT1.Items) ((TreeViewItem)item).IsExpanded = false; break;
                case "menuitemAUTO_MODBUS_DD": break;

                // ====== gridMAIN =====
                case "menuitemOPEN_FILE": 
                    OpenFileDialog odlg = new Microsoft.Win32.OpenFileDialog();
                    odlg.Multiselect = true;
                    odlg.Filter = "PLATFORM files (*.platform)|*.platform|All files (*.*)|*.*";
                    if (odlg.ShowDialog() == true)
                    {
                        string file_path = odlg.FileName;
                        if (file_path != "")
                        {
                            string dd_count = ini.IniReadValue("GLOBAL PARAMETER", "modbus_dd_number", file_path);
                            //===== load 所有MODBUS DD的內容 ======
                            for (int i = 0; i < Convert.ToInt32(dd_count); i++)
                            {
                                string xmlname = "", station = "", mid = "", interval = "";
                                bool joint = false;
                                xmlname = ini.IniReadValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "xml", file_path);
                                station = ini.IniReadValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "station", file_path);
                                joint = Convert.ToBoolean(ini.IniReadValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "joint", file_path));
                                mid = ini.IniReadValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "mid", file_path);
                                interval = ini.IniReadValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "interval", file_path);
                                gs_dd_parameterList.Add(new ddparameter_t() { xml_name = xmlname, station_name = station, joint_communication = joint, modbus_id = mid, history_interval = interval });
                            }
                        }
                        foreach (var dd in gs_dd_parameterList)
                            XMLfileAnalyze(dd.xml_name, dd.station_name, dd.joint_communication, dd.modbus_id, dd.history_interval);
                    }
                    break;
                case "menuitemSAVE_FILE":
                    SaveFileDialog sdlg = new Microsoft.Win32.SaveFileDialog();
                    sdlg.Filter = "PLATFORM files (*.platform)|*.platform|All files (*.*)|*.*";
                    if (sdlg.ShowDialog() == true)
                    {
                        string file_path = sdlg.FileName;
                        if (file_path != "")
                        {
                            ini.IniWriteValue("GLOBAL PARAMETER", "modbus_dd_number", gs_dd_parameterList.Count.ToString(), file_path);
                            try
                            {
                                //===== 儲存所有MODBUS DD的內容 ======
                                for (int i = 0; i < gs_dd_parameterList.Count; i++)
                                {
                                    ini.IniWriteValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "xml", gs_dd_parameterList[i].xml_name, file_path);
                                    ini.IniWriteValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "station", gs_dd_parameterList[i].station_name, file_path);
                                    ini.IniWriteValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "joint", gs_dd_parameterList[i].joint_communication.ToString(), file_path);
                                    ini.IniWriteValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "mid", gs_dd_parameterList[i].modbus_id, file_path);
                                    ini.IniWriteValue("MODBUSDD" + (i + 1).ToString() + " PARAMETER", "interval", gs_dd_parameterList[i].history_interval, file_path);
                                }
                            }
                            catch { }
                        }
                    }
                    break;
                case "menuitemCLOSE_APP":
                    Save_IniFile(iniPath);
                    Environment.Exit(Environment.ExitCode); break;
                default: break;
            }
        }

        void combobox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            switch (combobox.Name)
            {
                case "comboboxCOMPORT": treeviewitermCOMPORT1.Header = combobox.SelectedValue; break;
            }
        }
        void initial_dd_element()
        {
            fk_modbusDD_device_index = 0;
            fk_modbusDD_event_index = 0;
            fk_modbusDD_communication_index = 0;
            fk_glist_DDdeviceCollection.Clear();
            UIElList.Clear();
            textblock_modbusIDList.Clear();
            packiconList.Clear();
            treeviewer_buttonList.Clear();
            gridMAIN.Children.Clear();
            treeviewitemFLOW_SERIAL.Items.Clear();
            treeviewitemLEVEL_SERIAL.Items.Clear();
            treeviewitemDUST_SERIAL.Items.Clear();
            treeviewitemTEMPERATURE_SERIAL.Items.Clear();
            treeviewitemPRESSURE_SERIAL.Items.Clear();
            treeviewitemOTHERS.Items.Clear();

            fk_command_state = 0;
        }
    }
}
