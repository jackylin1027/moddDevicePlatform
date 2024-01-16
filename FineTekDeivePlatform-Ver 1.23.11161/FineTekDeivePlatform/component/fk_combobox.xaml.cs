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
    /// fk_combobox.xaml 的互動邏輯
    /// </summary>
    public partial class fk_combobox : UserControl
    {
        public static readonly DependencyProperty fk_combobox_labelProperty =
            DependencyProperty.Register("fk_combobox_label", typeof(string), typeof(fk_combobox), new PropertyMetadata("combobox"));
        public static readonly DependencyProperty fk_combobox_unitProperty =
            DependencyProperty.Register("fk_combobox_unit", typeof(string), typeof(fk_combobox), new PropertyMetadata("unit"));

        public static readonly DependencyProperty fk_combobox_marginProperty =
             DependencyProperty.Register("fk_combobox_margin", typeof(Thickness), typeof(fk_combobox), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnTextMarginChanged)));
        public static readonly DependencyProperty fk_label_marginProperty =
            DependencyProperty.Register("fk_label_margin", typeof(Thickness), typeof(fk_combobox), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnLabelMarginChanged)));
        public static readonly DependencyProperty fk_unit_marginProperty =
            DependencyProperty.Register("fk_unit_margin", typeof(Thickness), typeof(fk_combobox), new PropertyMetadata(new Thickness(0), new PropertyChangedCallback(OnUnitMarginChanged)));

        public static readonly DependencyProperty fk_combobox_ItemSourceProperty =
            DependencyProperty.Register("fk_combobox_ItemSource", typeof(IEnumerable<string>), typeof(fk_combobox), new PropertyMetadata(null));
        public static readonly DependencyProperty fk_combobox_SelectedItemProperty =
            DependencyProperty.Register("fk_combobox_SelectedItem", typeof(Object), typeof(fk_combobox), new PropertyMetadata(""));
        public static readonly DependencyProperty fk_combobox_SelectedIndexProperty =
            DependencyProperty.Register("fk_combobox_SelectedIndex", typeof(int), typeof(fk_combobox), new PropertyMetadata());
        public static readonly DependencyProperty fk_combobox_SelectedValueProperty =
            DependencyProperty.Register("fk_combobox_SelectedValue", typeof(object), typeof(fk_combobox), new PropertyMetadata(""));
        public static readonly RoutedEvent SelectionChangedEvent = 
            EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(fk_combobox));

        //public static readonly DependencyProperty SelectedWidgetProperty = DependencyProperty.RegisterAttached("SelectedWidget", typeof(Object), typeof(fk_combobox), new UIPropertyMetadata(OnPropertyChanged));
        public IEnumerable<string> fk_combobox_ItemSource
        {
            get { return (IEnumerable<string>)GetValue(fk_combobox_ItemSourceProperty); }
            set { SetValue(fk_combobox_ItemSourceProperty, value); }
        }
        public Object fk_combobox_SelectedItem
        {
            get { return (Object)GetValue(fk_combobox_SelectedItemProperty); }
            set { SetValue(fk_combobox_SelectedItemProperty, value); }
        }
        public int fk_combobox_SelectedIndex
        {
            get { return (int)GetValue(fk_combobox_SelectedIndexProperty); }
            set { SetValue(fk_combobox_SelectedIndexProperty, value); }
        }
        public object fk_combobox_SelectedValue
        {
            get { return (object)GetValue(fk_combobox_SelectedValueProperty); }
            set { SetValue(fk_combobox_SelectedValueProperty, value); }
        }
        public event RoutedEventHandler SelectionChanged
        {
            add { AddHandler(SelectionChangedEvent, value); }
            remove { RemoveHandler(SelectionChangedEvent, value); }
        }
        public string fk_combobox_label
        {
            get { return (string)GetValue(fk_combobox_labelProperty); }
            set { SetValue(fk_combobox_labelProperty, value); }
        }
        public string fk_combobox_unit
        {
            get { return (string)GetValue(fk_combobox_unitProperty); }
            set { SetValue(fk_combobox_unitProperty, value); }
        }
        public Thickness fk_combobox_margin
        {
            get { return (Thickness)GetValue(fk_combobox_marginProperty); }
            set { SetValue(fk_combobox_marginProperty, value); }
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
        //public Object SelectedWidget
        //{
        //    get { return (Object)GetValue(SelectedWidgetProperty); }
        //    set { SetValue(SelectedWidgetProperty, value); }
        //}
        private static void OnTextMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_combobox)target).fk_combobox_margin = (Thickness)e.NewValue;
        }
        private static void OnLabelMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_combobox)target).fk_label_margin = (Thickness)e.NewValue;
        }
        private static void OnUnitMarginChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            ((fk_combobox)target).fk_unit_margin = (Thickness)e.NewValue;
        }
        public fk_combobox()
        {
            InitializeComponent();
        }
        private void nameCombobx_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
        {
            //Object obj = e.AddedItems[0] as Object;
            //if (obj != null)
            //{
            //    //this.SelectedWidget = (Object)e.AddedItems[0];
            //    this.fk_combobox_SelectedItem = (Object)e.AddedItems[0];
            //}

            RaiseEvent(new RoutedEventArgs(SelectionChangedEvent));
        }
        //private static void OnPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    UserControl item = sender as UserControl;
        //
        //    //Some code
        //}
    }
}
