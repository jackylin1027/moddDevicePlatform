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
    /// fk_imagebutton.xaml 的互動邏輯
    /// </summary>
    public partial class fk_imagebutton : UserControl
    {
        public static readonly DependencyProperty fk_imagebutton_textProperty = DependencyProperty.Register("fk_imagebutton_text", typeof(string), typeof(fk_imagebutton), new PropertyMetadata("Button"));
        public static readonly DependencyProperty fk_imagebutton_iconProperty = DependencyProperty.Register("fk_imagebutton_icon", typeof(PackIconKind), typeof(fk_imagebutton), new PropertyMetadata(PackIconKind.TransitConnection));
        MaterialDesignThemes.Wpf.PackIcon afdfd = new MaterialDesignThemes.Wpf.PackIcon();
        public string fk_imagebutton_text
        {
            get { return (string)GetValue(fk_imagebutton_textProperty); }
            set { SetValue(fk_imagebutton_textProperty, value); }
        }
        public PackIconKind fk_imagebutton_icon
        {
            get { return (PackIconKind)GetValue(fk_imagebutton_iconProperty); }
            set { SetValue(fk_imagebutton_iconProperty, value); }
        }
        public fk_imagebutton()
        {
            InitializeComponent();
        }
    }
}
