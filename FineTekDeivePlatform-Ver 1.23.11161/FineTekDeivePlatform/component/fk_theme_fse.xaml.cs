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
using MaterialDesignThemes.Wpf;
namespace FineTekDeivePlatform.component
{
    /// <summary>
    /// fk_theme_fse.xaml 的互動邏輯
    /// </summary>
    public partial class fk_theme_fse : UserControl
    {
        public event RoutedEventHandler CONNClick;
        public event RoutedEventHandler EXITClick;
        public event RoutedEventHandler TBDClick;
        public event RoutedEventHandler SYNCClick;

        public static readonly DependencyProperty FK_SOFTWATR_VERSIONproperty = DependencyProperty.Register("FK_SOFTWATR_VERSION", typeof(string), typeof(fk_theme_fse), new PropertyMetadata(""));
        public static readonly DependencyProperty FK_BAUDRATEproperty = DependencyProperty.Register("FK_BAUDRATE", typeof(object), typeof(fk_theme_fse), new PropertyMetadata("9600"));
        public static readonly DependencyProperty FK_MODBUS_IDproperty = DependencyProperty.Register("FK_MODBUS_ID", typeof(string), typeof(fk_theme_fse), new PropertyMetadata("1"));
        public static readonly DependencyProperty FK_COMPORTDproperty = DependencyProperty.Register("FK_COMPORT", typeof(object), typeof(fk_theme_fse), new PropertyMetadata("COM1"));

        public static readonly DependencyProperty FK_TBD_TEXTproperty = DependencyProperty.Register("FK_TBD_TEXT", typeof(string), typeof(fk_theme_fse), new PropertyMetadata("TBD", new PropertyChangedCallback(OnTBD_TextChanged)));
        public static readonly DependencyProperty FK_CONNECT_TEXTproperty = DependencyProperty.Register("FK_CONNECT_TEXT", typeof(string), typeof(fk_theme_fse), new PropertyMetadata("CONN.", new PropertyChangedCallback(OnCONN_TextChanged)));
        public static readonly DependencyProperty FK_EXIT_TEXTproperty = DependencyProperty.Register("FK_EXIT_TEXT", typeof(string), typeof(fk_theme_fse), new PropertyMetadata("EXIT", new PropertyChangedCallback(OnEXIT_TextChanged)));
        public static readonly DependencyProperty FK_SYNC_TEXTproperty = DependencyProperty.Register("FK_SYNC_TEXT", typeof(string), typeof(fk_theme_fse), new PropertyMetadata("SYNC", new PropertyChangedCallback(OnSYNC_TextChanged)));

        public static readonly DependencyProperty FK_CONNECT_ICONproperty = DependencyProperty.Register("FK_CONNECT_ICON", typeof(PackIconKind), typeof(fk_theme_fse), new PropertyMetadata(PackIconKind.LanConnect));
        public static readonly DependencyProperty FK_TBD_ICONproperty = DependencyProperty.Register("FK_TBD_ICON", typeof(PackIconKind), typeof(fk_theme_fse), new PropertyMetadata(PackIconKind.CloseOutline));
        public static readonly DependencyProperty FK_SYNC_ICONproperty = DependencyProperty.Register("FK_SYNC_ICON", typeof(PackIconKind), typeof(fk_theme_fse), new PropertyMetadata(PackIconKind.Sync));
        public static readonly DependencyProperty FK_EXIT_ICONproperty = DependencyProperty.Register("FK_EXIT_ICON", typeof(PackIconKind), typeof(fk_theme_fse), new PropertyMetadata(PackIconKind.ExitToApp));

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
        public string FK_TBD_TEXT
        {
            get { return (string)GetValue(FK_TBD_TEXTproperty); }
            set { SetValue(FK_TBD_TEXTproperty, value); }
        }
        public string FK_CONNECT_TEXT
        {
            get { return (string)GetValue(FK_CONNECT_TEXTproperty); }
            set { SetValue(FK_CONNECT_TEXTproperty, value); }
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
        public PackIconKind FK_CONNECT_ICON
        {
            get { return (PackIconKind)GetValue(FK_CONNECT_ICONproperty); }
            set { SetValue(FK_CONNECT_ICONproperty, value); }
        }
        public PackIconKind FK_TBD_ICON
        {
            get { return (PackIconKind)GetValue(FK_TBD_ICONproperty); }
            set { SetValue(FK_TBD_ICONproperty, value); }
        }
        public PackIconKind FK_SYNC_ICON
        {
            get { return (PackIconKind)GetValue(FK_SYNC_ICONproperty); }
            set { SetValue(FK_SYNC_ICONproperty, value); }
        }
        public PackIconKind FK_EXIT_ICON
        {
            get { return (PackIconKind)GetValue(FK_EXIT_ICONproperty); }
            set { SetValue(FK_EXIT_ICONproperty, value); }
        }
        string[] comportSelection = new string[100];
        string[] baudrateSelection = new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", };
        public fk_theme_fse()
        {
            int i;
            InitializeComponent();
            for (i = 0; i < 100; i++) comportSelection[i] = "COM" + (i + 1).ToString();
            comboboxCOM.ItemsSource = comportSelection;
            comboboxBaudrte.ItemsSource = baudrateSelection;
        }

        private void imagebuttonTBD_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TBDClick != null)
            {
                TBDClick(this, e);
            }
        }
        private void imagebuttonCONN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (CONNClick != null)
            {
                CONNClick(this, e);
            }
        }
        private void imagebuttonSYNC_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SYNCClick != null)
            {
                SYNCClick(this, e);
            }
        }
        private void imagebuttonEXIT_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EXITClick != null)
            {
                EXITClick(this, e);
            }
        }
        private static void OnTBD_TextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_theme_fse)target).FK_TBD_TEXT = (string)e.NewValue;
            if (((fk_theme_fse)target).FK_TBD_TEXT == "TBD")
            {
                ((fk_theme_fse)target).FK_TBD_ICON = PackIconKind.CloseOutline;
            }
            else
            {
                ((fk_theme_fse)target).FK_TBD_ICON = PackIconKind.CloseOutline;
            }
        }
        private static void OnCONN_TextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_theme_fse)target).FK_CONNECT_TEXT = (string)e.NewValue;
            if (((fk_theme_fse)target).FK_CONNECT_TEXT == "CONN.")
            {
                ((fk_theme_fse)target).FK_CONNECT_ICON = PackIconKind.LanConnect;
            }
            else
            {
                ((fk_theme_fse)target).FK_CONNECT_ICON = PackIconKind.LanDisconnect;
            }
        }
        private static void OnSYNC_TextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_theme_fse)target).FK_SYNC_TEXT = (string)e.NewValue;
            if (((fk_theme_fse)target).FK_SYNC_TEXT == "SYNC")
            {
                ((fk_theme_fse)target).FK_SYNC_ICON = PackIconKind.Sync;
            }
            else
            {
                ((fk_theme_fse)target).FK_SYNC_ICON = PackIconKind.SyncOff;
            }
        }
        private static void OnEXIT_TextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_theme_fse)target).FK_EXIT_TEXT = (string)e.NewValue;
            if (((fk_theme_fse)target).FK_EXIT_TEXT == "EXIT")
            {
                ((fk_theme_fse)target).FK_EXIT_ICON = PackIconKind.ExitToApp;
            }
            else
            {
                ((fk_theme_fse)target).FK_EXIT_ICON = PackIconKind.ExitToApp;
            }
        }

    }
}
