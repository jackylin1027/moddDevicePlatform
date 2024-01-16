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
using LiveCharts.Wpf;
using LiveCharts.Geared;

namespace FineTekDeivePlatform.component.homepage
{
    /// <summary>
    /// fk_five_variation_chart.xaml 的互動邏輯
    /// </summary>
    public partial class fk_five_variation_chart : UserControl
    {
        const int MAX_NUMBER = 32;
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }
        List<CheckBox> checkboxList = new List<CheckBox>();
        public GearedValues<MeasureModel>[] GearedValues { get; set; }
        public fk_five_variation_chart()
        {
            InitializeComponent();
            checkboxInitiate(wrappanelCHECKBOX_GROUP);
            gearedlineInitiate(cartesianchartMAIN);

        }
        void checkboxInitiate(object parents)
        {
            int i = 0;

            CheckBox checkboxALL = new CheckBox() { Name = "checkboxSLELCT_WHOLE" , Content="全選"};
            checkboxALL.Click += new RoutedEventHandler(checkbox_click);
            ((WrapPanel)parents).Children.Add(checkboxALL);

            for (i = 0; i < MAX_NUMBER; i++)
            {               
                Binding binding = new Binding { Source = checkboxALL, Path = new PropertyPath("FontSize") };
                CheckBox checkbox = new CheckBox() { Tag = i, Content="null"};
                checkbox.SetBinding(FontSizeProperty, binding);
                checkboxList.Add(checkbox);
                ((WrapPanel)parents).Children.Add(checkbox);
            }
        }
        void gearedlineInitiate(CartesianChart chart)
        {
            GLineSeries gline_series = new GLineSeries();

            //the values property will store our values array
            GearedValues = new GearedValues<MeasureModel>[MAX_NUMBER];
            for (int i = 0; i < MAX_NUMBER; i++)
            {
                GearedValues[i] = new GearedValues<MeasureModel>();
                GearedValues[i].WithQuality(Quality.Low);
            }
            Binding binding = new Binding() { Source = GearedValues[i], Path = new PropertyPath("Value") };
            chart.Series.Add(gline_series);


        }
        void checkbox_click(object sender, RoutedEventArgs e)
        {

        }
        void combobox_selection_changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            switch (combobox.Name)
            {
                case "comboboxSELECT_VALUE": break;
            }
        }

        private void button_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "buttonINQUERY": break;
                case "buttonSAVE_LOG": break;
                case "buttonRESET_ZOOM": break;
            }
        }
    }

}
