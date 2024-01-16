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
    /// fk_customlable.xaml 的互動邏輯
    /// </summary>
    public partial class fk_customlabel : UserControl
    {
        public static readonly DependencyProperty fk_customlabel_textProperty =
            DependencyProperty.Register("fk_customlabel_text", typeof(string), typeof(fk_customlabel), new PropertyMetadata("label"));

        public static readonly DependencyProperty fk_text_marginProperty =
             DependencyProperty.Register("fk_text_margin", typeof(Thickness), typeof(fk_customlabel), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnTextMarginChanged)));

        public string fk_customlabel_text
        {
            get { return (string)GetValue(fk_customlabel_textProperty); }
            set { SetValue(fk_customlabel_textProperty, value); }
        }
        public Thickness fk_text_margin
        {
            get { return (Thickness)GetValue(fk_text_marginProperty); }
            set { SetValue(fk_text_marginProperty, value); }
        }
        private static void OnTextMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_customlabel)target).fk_text_margin = (Thickness)e.NewValue;
        }
        public fk_customlabel()
        {
            InitializeComponent();
        }
    }
}
