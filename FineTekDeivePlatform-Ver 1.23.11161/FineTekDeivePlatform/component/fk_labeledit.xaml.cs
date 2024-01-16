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
    /// fk_labeledit.xaml 的互動邏輯
    /// </summary>
    public partial class fk_labeledit : UserControl
    {
        public static readonly DependencyProperty fk_labeledit_labelProperty =
        DependencyProperty.Register("fk_labeledit_label", typeof(string), typeof(fk_labeledit), new PropertyMetadata("label"));
        public static readonly DependencyProperty fk_labeledit_editProperty =
        DependencyProperty.Register("fk_labeledit_edit", typeof(string), typeof(fk_labeledit), new PropertyMetadata("null"));
        public static readonly DependencyProperty fk_label_marginProperty =
        DependencyProperty.Register("fk_label_margin", typeof(Thickness), typeof(fk_labeledit), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnLabelMarginChanged)));
        public static readonly DependencyProperty fk_edit_marginProperty =
        DependencyProperty.Register("fk_edit_margin", typeof(Thickness), typeof(fk_labeledit), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnEditMarginChanged)));

        public static readonly DependencyProperty fk_labeledit_label_fontsizeProperty =
        DependencyProperty.Register("fk_labeledit_label_fontsize", typeof(object), typeof(fk_labeledit), new PropertyMetadata("14"));
        public static readonly DependencyProperty fk_labeledit_edit_fontsizeProperty =
        DependencyProperty.Register("fk_labeledit_edit_fontsize", typeof(object), typeof(fk_labeledit), new PropertyMetadata("17"));

        public static readonly DependencyProperty fk_labeledit_label_fontweightProperty =
        DependencyProperty.Register("fk_labeledit_label_fontweight", typeof(FontWeight), typeof(fk_labeledit), new PropertyMetadata(FontWeights.Normal));
        public static readonly DependencyProperty fk_labeledit_edit_fontweightProperty =
        DependencyProperty.Register("fk_labeledit_edit_fontweight", typeof(FontWeight), typeof(fk_labeledit), new PropertyMetadata(FontWeights.Bold));
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
        public string fk_labeledit_label
        {
            get { return (string)GetValue(fk_labeledit_labelProperty); }
            set { SetValue(fk_labeledit_labelProperty, value); }
        }
        public string fk_labeledit_edit
        {
            get { return (string)GetValue(fk_labeledit_editProperty); }
            set { SetValue(fk_labeledit_editProperty, value); }
        }
        public object fk_labeledit_label_fontsize
        {
            get { return (object)GetValue(fk_labeledit_label_fontsizeProperty); }
            set { SetValue(fk_labeledit_label_fontsizeProperty, value); }
        }
        public object fk_labeledit_edit_fontsize
        {
            get { return (object)GetValue(fk_labeledit_edit_fontsizeProperty); }
            set { SetValue(fk_labeledit_edit_fontsizeProperty, value); }
        }
        public FontWeight fk_labeledit_label_fontweight
        {
            get { return (FontWeight)GetValue(fk_labeledit_label_fontweightProperty); }
            set { SetValue(fk_labeledit_label_fontweightProperty, value); }
        }
        public FontWeight fk_labeledit_edit_fontweight
        {
            get { return (FontWeight)GetValue(fk_labeledit_edit_fontweightProperty); }
            set { SetValue(fk_labeledit_edit_fontweightProperty, value); }
        }
        public fk_labeledit()
        {
            InitializeComponent();
        }
        private static void OnLabelMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_labeledit)target).fk_label_margin = (Thickness)e.NewValue;
        }
        private static void OnEditMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_labeledit)target).fk_edit_margin = (Thickness)e.NewValue;
        }
    }
}
