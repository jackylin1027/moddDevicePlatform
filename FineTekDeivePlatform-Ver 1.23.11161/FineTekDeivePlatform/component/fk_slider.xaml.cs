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
    /// fk_slider.xaml 的互動邏輯
    /// </summary>
    public partial class fk_slider : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(fk_slider), new PropertyMetadata(0.2));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(fk_slider), new PropertyMetadata(" 0h 12min"));
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public double Text
        {
            get { return (double)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double HourValue { get; set; }
        public double MinuteValue { get; set; }
        public fk_slider()
        {
            InitializeComponent();
        }

        private void myslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider m_slider = (Slider)sender;
            HourValue = Math.Truncate(m_slider.Value);
            MinuteValue = (m_slider.Value - Math.Truncate(m_slider.Value)) * 60.0;
            if (this.textblockDATETIME != null)
                this.textblockDATETIME.Text = " " + HourValue.ToString() + "h " + MinuteValue.ToString("00") + "min";
        }
    }
}
