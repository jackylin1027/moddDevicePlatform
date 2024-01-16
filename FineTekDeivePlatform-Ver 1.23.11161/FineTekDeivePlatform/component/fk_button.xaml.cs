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

namespace FineTekDeivePlatform.component
{
    /// <summary>
    /// fk_button.xaml 的互動邏輯
    /// </summary>
    public partial class fk_button : UserControl
    {
        public static readonly DependencyProperty fk_button_textProperty =
            DependencyProperty.Register("fk_button_text", typeof(string), typeof(fk_button), new PropertyMetadata("button"));
        public string fk_button_text
        {
            get { return (string)GetValue(fk_button_textProperty); }
            set { SetValue(fk_button_textProperty, value); }
        }
        public fk_button()
        {
            InitializeComponent();
        }

        private void nameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
