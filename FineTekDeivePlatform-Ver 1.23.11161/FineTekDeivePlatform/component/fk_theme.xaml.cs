using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace FineTekDeivePlatform.component
{
    /// <summary>
    /// fk_theme.xaml 的互動邏輯
    /// </summary>
    public partial class fk_theme : UserControl
    {
        public event RoutedEventHandler ConnectClick;
        public event RoutedEventHandler ExitClick;
        public event RoutedEventHandler NoneClick;
        public event RoutedEventHandler SyncClick;
        public static readonly DependencyProperty FK_BAUDRATEproperty = DependencyProperty.Register("FK_BAUDRATE", typeof(object), typeof(fk_theme), new PropertyMetadata("9600"));
        public static readonly DependencyProperty FK_MODBUS_IDproperty = DependencyProperty.Register("FK_MODBUS_ID", typeof(string), typeof(fk_theme), new PropertyMetadata("1"));
        public static readonly DependencyProperty FK_COMPORTDproperty = DependencyProperty.Register("FK_COMPORT", typeof(object), typeof(fk_theme), new PropertyMetadata("COM1"));
        public static readonly DependencyProperty FK_SOFTWATR_VERSIONproperty = DependencyProperty.Register("FK_SOFTWATR_VERSION", typeof(string), typeof(fk_theme), new PropertyMetadata(""));
        public static readonly DependencyProperty FK_SOFTWATR_DESCRIBEproperty = DependencyProperty.Register("FK_SOFTWATR_DESCRIBE", typeof(string), typeof(fk_theme), new PropertyMetadata(""));
        public static readonly DependencyProperty FK_CONNECT_TEXTproperty = DependencyProperty.Register("FK_CONNECT_TEXT", typeof(string), typeof(fk_theme), new PropertyMetadata("Connect"));
        public static readonly DependencyProperty FK_HART_TEXTproperty = DependencyProperty.Register("FK_HART_TEXT", typeof(string), typeof(fk_theme), new PropertyMetadata("Hart"));
        public static readonly DependencyProperty FK_EXIT_TEXTproperty = DependencyProperty.Register("FK_EXIT_TEXT", typeof(string), typeof(fk_theme), new PropertyMetadata("Exit"));
        public static readonly DependencyProperty FK_SYNC_TEXTproperty = DependencyProperty.Register("FK_SYNC_TEXT", typeof(string), typeof(fk_theme), new PropertyMetadata("Sync"));
        public object FK_BAUDRATE
        {
            get { return (object)GetValue(FK_BAUDRATEproperty); }
            set { SetValue(FK_BAUDRATEproperty, value); }
        }
        public object FK_COMPORT
        {
            get { return (object)GetValue(FK_COMPORTDproperty); }
            set { SetValue(FK_COMPORTDproperty, value); }
        }
        public string FK_MODBUS_ID
        {
            get { return (string)GetValue(FK_MODBUS_IDproperty); }
            set { SetValue(FK_MODBUS_IDproperty, value); }
        }
        public string FK_SOFTWATR_VERSION
        {
            get { return (string)GetValue(FK_SOFTWATR_VERSIONproperty); }
            set { SetValue(FK_SOFTWATR_VERSIONproperty, value); }
        }
        public string FK_SOFTWATR_DESCRIBE
        {
            get { return (string)GetValue(FK_SOFTWATR_DESCRIBEproperty); }
            set { SetValue(FK_SOFTWATR_DESCRIBEproperty, value); }
        }
        public string FK_CONNECT_TEXT
        {
            get { return (string)GetValue(FK_CONNECT_TEXTproperty); }
            set { SetValue(FK_CONNECT_TEXTproperty, value); }
        }
        public string FK_HART_TEXT
        {
            get { return (string)GetValue(FK_HART_TEXTproperty); }
            set { SetValue(FK_HART_TEXTproperty, value); }
        }
        public string FK_EXIT_TEXT
        {
            get { return (string)GetValue(FK_EXIT_TEXTproperty); }
            set { SetValue(FK_EXIT_TEXTproperty, value); }
        }
        public string FK_SYNC_TEXT
        {
            get { return (string)GetValue(FK_SYNC_TEXTproperty); }
            set { SetValue(FK_SYNC_TEXTproperty, value); }
        }

        string[] comportSelection = new string[100];
        string[] baudrateSelection = new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", };

        
        public fk_theme()
        {
            int i;
            InitializeComponent();
            //===== 加入自動收巡 compor 功能, Ver 1.0.04 =====
            string[] ports = SerialPort.GetPortNames();
            comboboxCOM.ItemsSource = ports;
            comboboxBaudrte.ItemsSource = baudrateSelection;
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (ConnectClick != null)
            {
                ConnectClick(this, e);
            }
        }

        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            if (ExitClick != null)
            {
                ExitClick(this, e);
            }
        }

        private void buttonNone_Click(object sender, RoutedEventArgs e)
        {
            if (NoneClick != null)
            {
                NoneClick(this, e);
            }
        }

        private void buttonSync_Click(object sender, RoutedEventArgs e)
        {
            if (SyncClick != null)
            {
                SyncClick(this, e);
            }
        }
    }
}
