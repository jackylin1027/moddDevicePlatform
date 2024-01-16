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
    /// fk_edit.xaml 的互動邏輯
    /// </summary>
    public partial class fk_edit : UserControl
    {
        public static readonly DependencyProperty fk_edit_labelProperty =
            DependencyProperty.Register("fk_edit_label", typeof(string), typeof(fk_edit), new PropertyMetadata("label"));
        public static readonly DependencyProperty fk_edit_textProperty =
            DependencyProperty.Register("fk_edit_text", typeof(string), typeof(fk_edit), new PropertyMetadata(""));
        public static readonly DependencyProperty fk_edit_unitProperty =
            DependencyProperty.Register("fk_edit_unit", typeof(string), typeof(fk_edit), new PropertyMetadata("unit"));
        public static readonly DependencyProperty fk_text_marginProperty =
            DependencyProperty.Register("fk_text_margin", typeof(Thickness), typeof(fk_edit), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnTextMarginChanged)));
        public static readonly DependencyProperty fk_label_marginProperty =
            DependencyProperty.Register("fk_label_margin", typeof(Thickness), typeof(fk_edit), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnLabelMarginChanged)));
        public static readonly DependencyProperty fk_unit_marginProperty =
            DependencyProperty.Register("fk_unit_margin", typeof(Thickness), typeof(fk_edit), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnUnitMarginChanged)));
        public static readonly DependencyProperty fk_text_widthProperty =
             DependencyProperty.Register("fk_text_width", typeof(string), typeof(fk_edit), new PropertyMetadata("auto"));

        public string fk_edit_label
        {
            get { return (string)GetValue(fk_edit_labelProperty); }
            set { SetValue(fk_edit_labelProperty, value); }
        }
        public string fk_edit_text
        {
            get { return (string)GetValue(fk_edit_textProperty); }
            set { SetValue(fk_edit_textProperty, value); }
        }
        public string fk_edit_unit
        {
            get { return (string)GetValue(fk_edit_unitProperty); }
            set { SetValue(fk_edit_unitProperty, value); }
        }
        public Thickness fk_text_margin
        {
            get { return (Thickness)GetValue(fk_text_marginProperty); }
            set { SetValue(fk_text_marginProperty, value); }
        }
        public Thickness fk_label_margin
        {
            get { return (Thickness)GetValue(fk_label_marginProperty); }
            set { SetValue(fk_label_marginProperty, value); }
        }
        public Thickness fk_unit_margin
        {
            get { return (Thickness)GetValue(fk_unit_marginProperty); }
            set { SetValue(fk_unit_marginProperty, value); }
        }
        public string fk_text_width
        {
            get { return (string)GetValue(fk_text_widthProperty); }
            set { SetValue(fk_text_widthProperty, value); }
        }

        private static void OnTextMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_edit)target).fk_text_margin = (Thickness)e.NewValue;
        }
        private static void OnLabelMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_edit)target).fk_label_margin = (Thickness)e.NewValue;
        }
        private static void OnUnitMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_edit)target).fk_unit_margin = (Thickness)e.NewValue;
        }
        public fk_edit()
        {
            InitializeComponent();
        }

        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            {
                this.Opacity = (this.IsEnabled == true) ? 1 : 0.4;
            }
        }
        //===== 必須要即時更新 fk_edit.fk_edit_text, 不然都會等到 fk_edit.fk_edit_text 的 lostfocus 才會更新 =====
        private void nameContext_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            this.fk_edit_text = textbox.Text;
        }
    }
}
