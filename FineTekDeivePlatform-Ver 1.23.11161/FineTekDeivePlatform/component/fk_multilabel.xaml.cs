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
    /// fk_multilabel.xaml 的互動邏輯
    /// </summary>
    public partial class fk_multilabel : UserControl
    {
        public static readonly DependencyProperty fk_multilabel_labelProperty =
        DependencyProperty.Register("fk_multilabel_label", typeof(string), typeof(fk_multilabel), new PropertyMetadata("label"));
        public static readonly DependencyProperty fk_multilabel_editProperty =
        DependencyProperty.Register("fk_multilabel_edit", typeof(string), typeof(fk_multilabel), new PropertyMetadata("null"));
        public static readonly DependencyProperty fk_label_marginProperty =
        DependencyProperty.Register("fk_label_margin", typeof(Thickness), typeof(fk_multilabel), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnLabelMarginChanged)));
        public static readonly DependencyProperty fk_edit_marginProperty =
        DependencyProperty.Register("fk_edit_margin", typeof(Thickness), typeof(fk_multilabel), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnEditMarginChanged)));
        public Thickness fk_label_margin
        {
            get { return (Thickness)GetValue(fk_label_marginProperty); }
            set { SetValue(fk_label_marginProperty, value); }
        }
        public Thickness fk_edit_margin
        {
            get { return (Thickness)GetValue(fk_edit_marginProperty); }
            set { SetValue(fk_edit_marginProperty, value); }
        }
        public string fk_multilabel_label
        {
            get { return (string)GetValue(fk_multilabel_labelProperty); }
            set { SetValue(fk_multilabel_labelProperty, value); }
        }
        public string fk_multilabel_edit
        {
            get { return (string)GetValue(fk_multilabel_editProperty); }
            set { SetValue(fk_multilabel_editProperty, value); }
        }
        public fk_multilabel()
        {
            InitializeComponent();
        }
        private static void OnLabelMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_multilabel)target).fk_label_margin = (Thickness)e.NewValue;
        }
        private static void OnEditMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_multilabel)target).fk_edit_margin = (Thickness)e.NewValue;
        }
    }
}
