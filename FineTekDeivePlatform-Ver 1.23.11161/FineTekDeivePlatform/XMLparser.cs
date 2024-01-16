using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using FineTekDeivePlatform.component;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using System.Windows.Media.Imaging;

namespace FineTekDeivePlatform
{
    class XMLparser
    {
        
        public class cbObj
        {
            public List<comboboxMapping> ComboboxMapping
            {
                get { return cbObjMappingList; }
            }
            public List<comboboxMapping> cbObjMappingList = new List<comboboxMapping>();
            public object component { get; set; }
            public Type componentType { get; set; }
            public string name { get; set; }
            public string style_string { get; set; }
            public void mapping(string[] mapping)
            {
                for (int i = 0; i < mapping.Count(); i++)
                {
                    cbObjMappingList.Add(new comboboxMapping { selectIndex = i, value = Convert.ToUInt16(mapping[i]) });
                }
            }
            public int getMappingIndex(UInt16 MeasureValue)
            {
                foreach (comboboxMapping val in cbObjMappingList)
                {
                    if (val.value == MeasureValue)
                        return val.selectIndex;
                }
                return 0;
            }
            public void setContentMapping(string[] content, string[] mappingValue )
            {
                for (int i = 0; i < content.Count(); i++)
                    cbObjMappingList.Add(new comboboxMapping { value = Convert.ToUInt16(mappingValue[i]), content = content[i] });
            }

        }
        public class comboboxMapping
        {
            public int selectIndex { get; set; }
            public UInt16 value { get; set; }
            public string content { get; set; }
        }
        
        public List<cbObj> cbObjList = new List<cbObj>();

        public int getMappingIndex(List<comboboxMapping> cpMapping, UInt16 MeasureValue)
        {
            foreach (comboboxMapping val in cpMapping)
            {
                if (val.value == MeasureValue)
                    return val.selectIndex;
            }
            return 0;
        }
        public string getMappingContent(List<comboboxMapping> cpMapping, UInt16 MeasureValue)
        {
            foreach (comboboxMapping val in cpMapping)
            {
                if (val.value == MeasureValue)
                    return val.content;
            }
            return "***";
        }
        public ushort setMappingContent(List<comboboxMapping> cpMapping, string content)
        {
            foreach (comboboxMapping val in cpMapping)
            {
                if (content == val.content)
                    return val.value;
            }
            return 0;
        }
        public ushort setMappingIndex(List<comboboxMapping> cpMapping, int selectIndex)
        {
            foreach (comboboxMapping val in cpMapping)
            {
                if (selectIndex == val.selectIndex)
                    return val.value;
            }
            return 0;
        }
        public XMLparser()
        {

        }

        public object headerContent(XmlNodeList child_nodes, Window windowMAIN, ScrollViewer scrollView, DrawerHost drawer, Object parents_component, Grid main_grid, int cycle_index)
        {
            object header_property = null;
            string point_name = child_nodes.Item(cycle_index).Name;
            switch (point_name)
            {
                case "Form": FormSetup(child_nodes, windowMAIN, scrollView, drawer,  parents_component, main_grid, cycle_index); break;
            }
            return header_property;
        }
        public void headerParameter(XmlNodeList child_nodes, ref PaltformIndentity indentity, int cycle_index)
        {
            string point_name = child_nodes.Item(cycle_index).Name;
            switch (point_name)
            {
                case "Identity": IdentitySetup(child_nodes, ref indentity, cycle_index); break;
                case "Communication": CommunicationSetup(child_nodes, ref indentity, cycle_index); break;
                case "SpecificFunction": SpecificFunctionSetup(child_nodes, ref indentity, cycle_index); break;
            }
        }
        public object elementIndicator(XmlNodeList child_nodes, Object parents_component, int cycle_index)
        {
            object greate_component = null ;
            string point_name = child_nodes.Item(cycle_index).Name;
            switch (point_name)
            {
                case "Grid": greate_component = createGrid(child_nodes, parents_component, cycle_index); break;
                case "GroupBox": greate_component = createGroupbox(child_nodes, parents_component, cycle_index); break;
                case "StackPanel": greate_component = createStackpanel(child_nodes, parents_component, cycle_index); break;
                case "WrapPanel": greate_component = createWrappanel(child_nodes, parents_component, cycle_index); break;
                case "ScrollViewer": greate_component = createScrollviewer(child_nodes, parents_component, cycle_index); break;
                case "fk_edit": greate_component = create_fkedit(child_nodes, parents_component, cycle_index); break;
                case "Label": greate_component = create_Label(child_nodes, parents_component, cycle_index); break;
                case "TextBox": greate_component = create_Textbox(child_nodes, parents_component, cycle_index); break;
                case "Button": greate_component = create_Button(child_nodes, parents_component, cycle_index); break;
                case "TextBlock": greate_component = create_Textblock(child_nodes, parents_component, cycle_index); break;
                case "CheckBox": greate_component = create_CheckBox(child_nodes, parents_component, cycle_index); break;
                case "ComboBox": greate_component = create_ComboBox(child_nodes, parents_component, cycle_index); break;
                case "Image": greate_component = create_Image(child_nodes, parents_component, cycle_index); break;
                case "fk_button": greate_component = create_fkbutton(child_nodes, parents_component, cycle_index); break;
                case "fk_combobox": greate_component = create_fkcombobox(child_nodes, parents_component, cycle_index); break;
                case "fk_basiclinechart": greate_component = create_fk_basiclinechart(child_nodes, parents_component, cycle_index); break;
                case "TabItem": greate_component = create_Tabitem(child_nodes, parents_component, cycle_index); break;
                case "dragablzTabablzControl": greate_component = create_dragablzTabablzControl(child_nodes, parents_component, cycle_index); break;
                case "materialDesignPackIcon": greate_component = create_materialDesignPackIcon(child_nodes, parents_component, cycle_index); break;
                case "ProgressBar": greate_component = create_ProgressBar(child_nodes, parents_component, cycle_index); break;
                case "materialDesignColorZone": greate_component = create_materialDesignColorZone(child_nodes, parents_component, cycle_index); break;
                case "specialmodelfk_epht_billboard": greate_component = create_specialmodelfk_epht_billboard(child_nodes, parents_component, cycle_index); break;
            }
            return greate_component;
        }
        public object SubBelongProperty(XmlNodeList child_nodes, ref Object parents_component, Object component_type, string attribute, int cycle_index)
        {
            object belong_property = null;
            string point_name = child_nodes.Item(cycle_index).Name;
            switch (point_name)
            {
                case "LinearGradientBrush": setLinearGradientBrush(child_nodes, ref parents_component, component_type, attribute,  cycle_index); break;
                case "SolidColorBrush": setSolidColorBrush(child_nodes, ref parents_component, component_type, attribute, cycle_index); break;
            }
            return belong_property;
        }

        public void gridparameterSetup(Object grid_component, XmlNodeList child_nodes, Object parents_component, int cycle_index)
        {
            int i;
            string point_name = child_nodes.Item(cycle_index).Name;

            if (point_name == "Grid.ColumnDefinitions")
            {
                XmlNodeList columndefinitions = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < columndefinitions.Count; i++)
                {
                    gridplacementSet(columndefinitions, grid_component, i);
                }
            }
            else if (point_name == "Grid.RowDefinitions")
            {
                XmlNodeList rowdefinitions = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < rowdefinitions.Count; i++)
                {
                    gridplacementSet(rowdefinitions, grid_component, i);
                }
            }
            else if (point_name == "dragablzTabablzControl.InterTabController")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    ((Dragablz.TabablzControl)grid_component).InterTabController = new Dragablz.InterTabController();
                }
            }
            else if (point_name == "TextBlock.Foreground")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                    //((TextBlock)grid_component).Foreground = Brushes.Black;
                }
            }
            else if (point_name == "TextBox.Foreground")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
            else if (point_name == "ProgressBar.Foreground")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
            else if (point_name == "materialDesignColorZone.Background")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
            else if (point_name == "Grid.Background")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
            else if (point_name == "fk_basiclinechart.Background")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
            else if (point_name == "GroupBox.Background")
            {
                XmlNodeList children = child_nodes.Item(cycle_index).ChildNodes;
                for (i = 0; i < children.Count; i++)
                {
                    SubBelongProperty(children, ref grid_component, grid_component, point_name, i);
                }
            }
        }
        public XmlNodeList receiveChildNotes(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;
            return xmlNode.ChildNodes;
        }
        public Grid createGrid(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            Grid grid = new Grid();
            grid.Name = ((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = grid, componentType = grid.GetType(), name = grid.Name });
            grid.Margin = thicknessSetup(xmlelement, "Margin");
            grid.Background = Brushes.Transparent;
            ParentsFrameworkSetup(ref ParsentComponent, grid);           
            return grid;
        }

        public GroupBox createGroupbox(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            GroupBox groupbox = new GroupBox();
            groupbox.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = groupbox, componentType = groupbox.GetType(), name = groupbox.Name, style_string = staticSourceSetup(xmlelement, "Style")});
            groupbox.Header = xmlelement.GetAttribute("Header").ToString();
            if (xmlelement.GetAttribute("Height").ToString() != "")
                groupbox.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            if (xmlelement.GetAttribute("Width").ToString() != "")
                groupbox.Width = Convert.ToDouble((xmlelement.GetAttribute("Width").ToString()));
            groupbox.MaxHeight = areaPropertySetup(xmlelement, "MaxHeight", groupbox);
            groupbox.MinHeight = areaPropertySetup(xmlelement, "MinHeight", groupbox);
            groupbox.MaxWidth = areaPropertySetup(xmlelement, "MaxWidth", groupbox);
            groupbox.MinWidth = areaPropertySetup(xmlelement, "MinWidth", groupbox);
            groupbox.Margin = thicknessSetup(xmlelement, "Margin");
            groupbox.Padding = thicknessSetup(xmlelement, "Padding");
            groupbox.BorderThickness = thicknessSetup(xmlelement, "BorderThickness");
            groupbox.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            groupbox.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            groupbox.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            groupbox.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            groupbox.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            groupbox.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            groupbox.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            groupbox.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            ParentsFrameworkSetup(ref ParsentComponent, groupbox);
            return groupbox;
        }
        public string staticSourceSetup (XmlElement xmlelement, string attribute)
        {
            string style = xmlelement.GetAttribute(attribute).Substring(16);
            style = style.TrimEnd('}');
            return style;
        }
        public StackPanel createStackpanel(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            StackPanel stackpanel = new StackPanel();
            stackpanel.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = stackpanel, componentType = stackpanel.GetType(), name = stackpanel.Name });
            stackpanel.Orientation = OrientationSetup(xmlelement);
            if (xmlelement.GetAttribute("Height").ToString() != "")
                stackpanel.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            stackpanel.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            stackpanel.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            stackpanel.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            stackpanel.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            
            stackpanel.Margin = thicknessSetup(xmlelement, "Margin");
            stackpanel.VerticalAlignment = (VerticalAlignment)AlignmentSetup(stackpanel.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            stackpanel.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(stackpanel.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");

            ParentsFrameworkSetup(ref ParsentComponent, stackpanel);
            return stackpanel;
        }
        public WrapPanel createWrappanel(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            WrapPanel wrappanel = new WrapPanel();
            wrappanel.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = wrappanel, componentType = wrappanel.GetType(), name = wrappanel.Name });
            //wrappanel.Orientation = OrientationSetup(xmlelement);
            if (xmlelement.GetAttribute("Height").ToString() != "")
                wrappanel.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            wrappanel.MaxHeight = areaPropertySetup(xmlelement, "MaxHeight", wrappanel);
            wrappanel.MinHeight = areaPropertySetup(xmlelement, "MinHeight", wrappanel);
            wrappanel.MaxWidth = areaPropertySetup(xmlelement, "MaxWidth", wrappanel);
            wrappanel.MinWidth = areaPropertySetup(xmlelement, "MinWidth", wrappanel);

            wrappanel.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            wrappanel.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            wrappanel.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            wrappanel.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            wrappanel.Margin = thicknessSetup(xmlelement, "Margin");
            wrappanel.VerticalAlignment = (VerticalAlignment)AlignmentSetup(wrappanel.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            wrappanel.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(wrappanel.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");

            ParentsFrameworkSetup(ref ParsentComponent, wrappanel);
            return wrappanel;
        }
        public ScrollViewer createScrollviewer(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            ScrollViewer scrollviewer = new ScrollViewer();
            scrollviewer.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = scrollviewer, componentType = scrollviewer.GetType(), name = scrollviewer.Name });
            //wrappanel.Orientation = OrientationSetup(xmlelement);
            if (xmlelement.GetAttribute("Height").ToString() != "")
                scrollviewer.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            scrollviewer.VerticalScrollBarVisibility = visibilitySetup(xmlelement, "VerticalScrollBarVisibility");
            scrollviewer.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            scrollviewer.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            scrollviewer.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            scrollviewer.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            scrollviewer.Margin = thicknessSetup(xmlelement, "Margin");
            scrollviewer.VerticalAlignment = (VerticalAlignment)AlignmentSetup(scrollviewer.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            scrollviewer.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(scrollviewer.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");

            ParentsFrameworkSetup(ref ParsentComponent, scrollviewer);
            return scrollviewer;
        }
        public Label create_Label(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            Label label = new Label();
            label.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = label, componentType = label.GetType(), name = label.Name });
            if (xmlelement.GetAttribute("Height").ToString() != "")
                label.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            label.Content = xmlelement.InnerText;
            label.Margin = thicknessSetup(xmlelement, "Margin");          
            label.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            label.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            label.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            label.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            ParentsFrameworkSetup(ref ParsentComponent, label);
            return label;
        }
        public TextBox create_Textbox(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            TextBox textbox = new TextBox();
            textbox.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = textbox, componentType = textbox.GetType(), name = textbox.Name });
            textbox.Width = areaPropertySetup(xmlelement, "Width", textbox);
            if (xmlelement.GetAttribute("Height").ToString() != "")
                textbox.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            textbox.MaxHeight = areaPropertySetup(xmlelement, "MaxHeight", textbox);
            textbox.MinHeight = areaPropertySetup(xmlelement, "MinHeight", textbox);
            textbox.MaxWidth = areaPropertySetup(xmlelement, "MaxWidth", textbox);
            textbox.MinWidth = areaPropertySetup(xmlelement, "MinWidth", textbox);
            textbox.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            textbox.VerticalScrollBarVisibility = visibilitySetup(xmlelement, "VerticalScrollBarVisibility");
            textbox.AcceptsReturn = boolPropertyReceive(xmlelement, "AcceptsReturn");
            textbox.Text = xmlelement.GetAttribute("Text").ToString();
            textbox.Margin = thicknessSetup( xmlelement, "Margin");
            textbox.BorderThickness = thicknessSetup(xmlelement, "BorderThickness");
            textbox.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            textbox.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            textbox.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            textbox.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            textbox.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            textbox.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            textbox.VerticalAlignment = (VerticalAlignment)AlignmentSetup(textbox.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            textbox.HorizontalContentAlignment= (HorizontalAlignment)AlignmentSetup(textbox.HorizontalContentAlignment.GetType(), xmlelement, "HorizontalContentAlignment");
            textbox.TextWrapping = textWrappingSetup(xmlelement, "TextWrapping");

            textbox.TextChanged += new TextChangedEventHandler(textbox_textchanged_renew);
            ParentsFrameworkSetup(ref ParsentComponent, textbox);
            return textbox;
        }
        private void textbox_textchanged_renew(object sender, TextChangedEventArgs args)
        {
            TextBox textbox = (TextBox)sender;
            textbox.Text = textbox.Text;
        }
        public Button create_Button(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            Button button = new Button();
            button.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = button, componentType = button.GetType(), name = button.Name });
            //button.Width = Convert.ToDouble(((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Width").ToString());
            if (xmlelement.GetAttribute("Height").ToString() != "")
                button.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            button.Margin = thicknessSetup(xmlelement, "Margin");
            button.Padding = thicknessSetup( xmlelement, "Padding");
            button.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            button.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            button.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            button.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            button.Content = ((XmlElement)NodeList.Item(elemlentPtr)).InnerText;
            ParentsFrameworkSetup(ref ParsentComponent, button);
            return button;
        }
        public ComboBox create_ComboBox(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            ComboBox combobox = new ComboBox();
            combobox.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = combobox, componentType = combobox.GetType(), name = combobox.Name });
            combobox.Width = areaPropertySetup(xmlelement, "Width", combobox);
            if (xmlelement.GetAttribute("Height").ToString() != "")
                combobox.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            combobox.MaxHeight = areaPropertySetup(xmlelement, "MaxHeight", combobox);
            combobox.MinHeight = areaPropertySetup(xmlelement, "MinHeight", combobox);
            combobox.MaxWidth = areaPropertySetup(xmlelement, "MaxWidth", combobox);
            combobox.MinWidth = areaPropertySetup(xmlelement, "MinWidth", combobox);
            combobox.BorderThickness = thicknessSetup(xmlelement, "BorderThickness");
            combobox.Margin = thicknessSetup(xmlelement, "Margin");
            combobox.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            combobox.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            combobox.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            combobox.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            combobox.VerticalAlignment = (VerticalAlignment)AlignmentSetup(combobox.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            combobox.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(combobox.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");
            combobox.HorizontalContentAlignment = (HorizontalAlignment)AlignmentSetup(combobox.VerticalAlignment.GetType(), xmlelement, "HorizontalContentAlignment");
            combobox.Background = Brushes.White;
            ParentsFrameworkSetup(ref ParsentComponent, combobox);
            return combobox;
        }
        public Image create_Image(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            Image image = new Image();
            cbObjList.Add(new cbObj { component = image, componentType = image.GetType(), name = image.Name });
            image.Source = new BitmapImage(new Uri(getURLstring(xmlelement, "Source"), UriKind.Relative));

            image.Height = areaPropertySetup(xmlelement, "Height", image);
            image.Width = areaPropertySetup(xmlelement, "Width", image);
            image.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            image.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            image.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            image.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            image.Margin = thicknessSetup(xmlelement, "Margin");
            image.Stretch = Stretch.UniformToFill;
            ParentsFrameworkSetup(ref ParsentComponent, image);
            return image;
        }
        public TextBlock create_Textblock(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            TextBlock textBlock = new TextBlock();
            textBlock.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = textBlock, componentType = textBlock.GetType(), name = textBlock.Name });
            //button.Width = Convert.ToDouble(((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Width").ToString());
            if (xmlelement.GetAttribute("Height").ToString() != "")
                textBlock.Height = Convert.ToDouble((xmlelement.GetAttribute("Height").ToString()));
            textBlock.Width = areaPropertySetup(xmlelement, "Width", textBlock);
            textBlock.MaxHeight = areaPropertySetup(xmlelement, "MaxHeight", textBlock);
            textBlock.MinHeight = areaPropertySetup(xmlelement, "MinHeight", textBlock);
            textBlock.MaxWidth = areaPropertySetup(xmlelement, "MaxWidth", textBlock);
            textBlock.MinWidth = areaPropertySetup(xmlelement, "MinWidth", textBlock);
            textBlock.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            textBlock.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            textBlock.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            textBlock.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            textBlock.Margin = thicknessSetup(xmlelement, "Margin");
            textBlock.Padding = thicknessSetup(xmlelement, "Padding");
            textBlock.Text = xmlelement.GetAttribute("Text").ToString();
            if(textBlock.Text =="") textBlock.Text = xmlelement.InnerText;
            textBlock.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            textBlock.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            textBlock.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            textBlock.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            textBlock.Foreground = ColorValueReceive(xmlelement, "Foreground");
            ParentsFrameworkSetup(ref ParsentComponent, textBlock);
            return textBlock;
        }
        public CheckBox create_CheckBox(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            CheckBox checkbox = new CheckBox();
            checkbox.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = checkbox, componentType = checkbox.GetType(), name = checkbox.Name });
            checkbox.Margin = thicknessSetup(xmlelement, "Margin");
            ParentsFrameworkSetup(ref ParsentComponent, checkbox);
            return checkbox;
        }
        public TabItem create_Tabitem(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            TabItem tabItem = new TabItem();
            tabItem.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = tabItem, componentType = tabItem.GetType(), name = tabItem.Name });
            tabItem.Header = xmlelement.GetAttribute("Header").ToString();
            //button.Width = Convert.ToDouble(((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Width").ToString());
            tabItem.Margin = thicknessSetup(xmlelement, "Margin");
            tabItem.Padding = thicknessSetup(xmlelement, "Padding");
            tabItem.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            tabItem.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            tabItem.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            tabItem.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            ParentsFrameworkSetup(ref ParsentComponent, tabItem);
            return tabItem;
        }
        public Dragablz.TabablzControl create_dragablzTabablzControl(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            Dragablz.TabablzControl tabablzControl = new Dragablz.TabablzControl();
            tabablzControl.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = tabablzControl, componentType = tabablzControl.GetType(), name = tabablzControl.Name });
            //tabablzControl.f = xmlelement.GetAttribute("FontWeight").ToString();
            //button.Width = Convert.ToDouble(((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Width").ToString());
            tabablzControl.Margin = thicknessSetup(xmlelement, "Margin");
            tabablzControl.Padding = thicknessSetup(xmlelement, "Padding");
            tabablzControl.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            tabablzControl.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            tabablzControl.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            tabablzControl.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            tabablzControl.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            tabablzControl.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            tabablzControl.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            tabablzControl.SetValue(Grid.RowSpanProperty, GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            tabablzControl.Background = Brushes.Transparent;
            ParentsFrameworkSetup(ref ParsentComponent, tabablzControl);
            return tabablzControl;
        }
        public ProgressBar create_ProgressBar(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            ProgressBar progressBar = new ProgressBar();
            progressBar.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = progressBar, componentType = progressBar.GetType(), name = progressBar.Name });
            progressBar.Maximum = DoubleValueReceive(xmlelement, "Maximum");
            progressBar.Margin = thicknessSetup(xmlelement, "Margin");
            progressBar.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            progressBar.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            progressBar.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            progressBar.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            progressBar.VerticalAlignment = (VerticalAlignment)AlignmentSetup(progressBar.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            progressBar.Height = DoubleValueReceive(xmlelement, "Height");
            progressBar.SmallChange = DoubleValueReceive(xmlelement, "SmallChange");
            progressBar.LargeChange = DoubleValueReceive(xmlelement, "LargeChange");
            ParentsFrameworkSetup(ref ParsentComponent, progressBar);
            return progressBar;
        }

        //=================================================
        public fk_edit create_fkedit(XmlNodeList NodeList, Object ParsentComponent,int elemlentPtr)
        {
            XmlElement xmlelement= ((XmlElement)NodeList.Item(elemlentPtr));
            fk_edit edit = new fk_edit();
            edit.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = edit, componentType = edit.GetType(), name = edit.Name });
            edit.fk_edit_label = xmlelement.GetAttribute("fk_edit_label").ToString();
            edit.fk_edit_text = xmlelement.GetAttribute("fk_edit_text").ToString();
            edit.fk_edit_unit = xmlelement.GetAttribute("fk_edit_unit").ToString();
            edit.fk_text_margin = thicknessSetup(xmlelement, "fk_text_margin");
            edit.fk_label_margin = thicknessSetup(xmlelement, "fk_label_margin");
            edit.fk_unit_margin = thicknessSetup(xmlelement, "fk_unit_margin");
            edit.fk_text_width = xmlelement.GetAttribute("fk_text_width").ToString();
            edit.Width = areaPropertySetup(xmlelement, "Width", edit);
            edit.Height = areaPropertySetup(xmlelement, "Height", edit);
            edit.Margin = thicknessSetup(xmlelement, "Margin");
            edit.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            edit.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            edit.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            edit.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            ParentsFrameworkSetup(ref ParsentComponent, edit);
            return edit;
        }
        public fk_button create_fkbutton(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            fk_button button = new fk_button();
            button.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = button, componentType = button.GetType(), name = button.Name });
            button.fk_button_text = xmlelement.GetAttribute("fk_button_text").ToString();
            button.Width = areaPropertySetup(xmlelement, "Width", button);
            button.Height = areaPropertySetup(xmlelement, "Height", button);
            button.Margin = thicknessSetup(xmlelement, "Margin");
            button.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            button.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            button.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            button.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            button.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            button.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            button.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            button.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            button.VerticalAlignment = (VerticalAlignment)AlignmentSetup(button.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            button.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(button.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");
            ParentsFrameworkSetup(ref ParsentComponent, button);
            return button;
        }

        public fk_combobox create_fkcombobox(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            fk_combobox combobox = new fk_combobox();
            combobox.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component=combobox, componentType=combobox.GetType(), name=combobox.Name });
            combobox.fk_combobox_label = xmlelement.GetAttribute("fk_combobox_label").ToString();
            combobox.fk_combobox_unit = xmlelement.GetAttribute("fk_combobox_unit").ToString();
            combobox.fk_combobox_margin = thicknessSetup(xmlelement, "fk_combobox_margin");
            combobox.fk_label_margin = thicknessSetup(xmlelement, "fk_label_margin");
            combobox.fk_unit_margin = thicknessSetup(xmlelement, "fk_unit_margin");
            combobox.Width = areaPropertySetup(xmlelement, "Width",combobox);
            combobox.Margin = thicknessSetup(xmlelement, "Margin");
            combobox.FontSize = DoubleValueReceive(xmlelement, "FontSize");
            combobox.FontWeight = fontweightSetup(xmlelement, "FontWeight");
            combobox.FontFamily = fontfamilySetup(xmlelement, "FontFamily");
            combobox.FontStyle = fontstyleSetup(xmlelement, "FontStyle");
            combobox.VerticalAlignment = (VerticalAlignment)AlignmentSetup(combobox.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            combobox.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(combobox.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");
            ParentsFrameworkSetup(ref ParsentComponent, combobox);
            return combobox;
        }
        public fk_basiclinechart create_fk_basiclinechart(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            fk_basiclinechart basiclinechart = new fk_basiclinechart();
            basiclinechart.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = basiclinechart, componentType = basiclinechart.GetType(), name = basiclinechart.Name });
            basiclinechart.Margin = thicknessSetup(xmlelement, "Margin");
            basiclinechart.BorderThickness = thicknessSetup(xmlelement, "BorderThickness");
            basiclinechart.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            basiclinechart.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            basiclinechart.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            basiclinechart.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            basiclinechart.Height = areaPropertySetup(xmlelement, "Height", basiclinechart);
            basiclinechart.Width = areaPropertySetup(xmlelement, "Width", basiclinechart);
            basiclinechart.BorderBrush = Brushes.Black;
            basiclinechart.Foreground_X0 = Brushes.Black;
            basiclinechart.Foreground_X1 = Brushes.SkyBlue;
            basiclinechart.MaxValue_X0 = (int)DoubleValueReceive(xmlelement, "MaxValue_X0");
            basiclinechart.MaxValue_X1 = (int)DoubleValueReceive(xmlelement, "MaxValue_X1");
            basiclinechart.Step_X0 = (int)DoubleValueReceive(xmlelement, "Step_X0");
            basiclinechart.Step_X1 = (int)DoubleValueReceive(xmlelement, "Step_X1");
            basiclinechart.Measure_lineValues_ScalesXAt=(int)DoubleValueReceive(xmlelement, "Measure_lineValues_ScalesXAt");
            ParentsFrameworkSetup(ref ParsentComponent, basiclinechart);
            return basiclinechart;
        }
        public PackIcon create_materialDesignPackIcon(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            PackIcon packIcon = new PackIcon();            
            packIcon.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = packIcon, componentType = packIcon.GetType(), name = packIcon.Name });
            ShadowDepthSetup(packIcon, xmlelement, "materialDesignShadowAssist.ShadowDepth");
            packIcon.SetValue(Grid.ColumnProperty, GridDefinationSetup(xmlelement, "Grid.Column"));
            packIcon.SetValue(Grid.RowProperty, GridDefinationSetup(xmlelement, "Grid.Row"));
            packIcon.SetValue(Grid.ColumnSpanProperty, GridDefinationSetup(xmlelement, "Grid.ColumnSpan"));
            packIcon.SetValue(Grid.RowSpanProperty, (object)GridDefinationSetup(xmlelement, "Grid.RowSpan"));
            packIcon.Margin = thicknessSetup(xmlelement, "Margin");
            packIcon.Foreground = Brushes.Gray;
            packIcon.Width = areaPropertySetup(xmlelement, "Width", packIcon);
            packIcon.Height = areaPropertySetup(xmlelement, "Height", packIcon);
            packIcon.Kind = packiconSetup(xmlelement, "Kind");
            ParentsFrameworkSetup(ref ParsentComponent, packIcon);
            return packIcon;
        }
        public ColorZone create_materialDesignColorZone(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            ColorZone colorZone = new ColorZone();
            colorZone.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = colorZone, componentType = colorZone.GetType(), name = colorZone.Name });
            colorZone.Margin = thicknessSetup(xmlelement, "Margin");
            colorZone.Padding = thicknessSetup(xmlelement, "Padding");
            //colorZone.Width = DoubleValueReceive(xmlelement, "Width");
            colorZone.Width = areaPropertySetup(xmlelement, "Width", colorZone);
            colorZone.VerticalAlignment = (VerticalAlignment)AlignmentSetup(colorZone.VerticalAlignment.GetType(), xmlelement, "VerticalAlignment");
            colorZone.HorizontalAlignment = (HorizontalAlignment)AlignmentSetup(colorZone.HorizontalAlignment.GetType(), xmlelement, "HorizontalAlignment");
            colorZone.Background = Brushes.Transparent;
            ParentsFrameworkSetup(ref ParsentComponent, colorZone);
            return colorZone;
        }

        public component.specialmodel.fk_epht_billboard create_specialmodelfk_epht_billboard(XmlNodeList NodeList, Object ParsentComponent, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            component.specialmodel.fk_epht_billboard epht_billboard = new component.specialmodel.fk_epht_billboard();
            epht_billboard.Name = xmlelement.GetAttribute("Name").ToString();
            cbObjList.Add(new cbObj { component = epht_billboard, componentType = epht_billboard.GetType(), name = epht_billboard.Name });
            ParentsFrameworkSetup(ref ParsentComponent, epht_billboard);
            return epht_billboard;
        }
        //==============================================================================================//
        //==============================================================================================//
        //==============================================================================================//
        //==============================================================================================//
        //==============================================================================================//
        #region //================================ private sub function =================================
        private Thickness thicknessSetup( XmlElement xmlelement, string attribute)
        {
            string margin = xmlelement.GetAttribute(attribute).ToString();
            Thickness myThickness = new Thickness(0,0,0,0);
            if (margin != "")
            {
                string[] sArray = margin.Split(',');              
                switch (sArray.Count())
                {
                    case 1:
                        myThickness.Left = Convert.ToDouble(sArray[0]);
                        myThickness.Top = Convert.ToDouble(sArray[0]);
                        myThickness.Right = Convert.ToDouble(sArray[0]);
                        myThickness.Bottom = Convert.ToDouble(sArray[0]);
                        break;
                    case 2:
                        myThickness.Left = Convert.ToDouble(sArray[0]);
                        myThickness.Top = Convert.ToDouble(sArray[1]);
                        myThickness.Right = Convert.ToDouble(sArray[0]);
                        myThickness.Bottom = Convert.ToDouble(sArray[1]);
                        break;
                    case 4:
                        myThickness.Left = Convert.ToDouble(sArray[0]);
                        myThickness.Top = Convert.ToDouble(sArray[1]);
                        myThickness.Right = Convert.ToDouble(sArray[2]);
                        myThickness.Bottom = Convert.ToDouble(sArray[3]);
                        break;
                }
            }
            return myThickness;
        }
        FontWeight fontweightSetup(XmlElement xmlelement, string attribute)
        {
            string m_fontweightTemp = xmlelement.GetAttribute(attribute).ToString();
            FontWeight fontweight = new FontWeight();

            switch (m_fontweightTemp)
            {
                case "Normal": fontweight = FontWeights.Normal; break;
                case "Bold": fontweight = FontWeights.Bold; break;
                case "Black": fontweight = FontWeights.Black; break;
                case "DemiBold": fontweight = FontWeights.DemiBold; break;
                case "ExtraBlack": fontweight = FontWeights.ExtraBlack; break;
                case "ExtraBold": fontweight = FontWeights.ExtraBold; break;
                case "ExtraLight": fontweight = FontWeights.ExtraLight; break;
                case "Heavy": fontweight = FontWeights.Heavy; break;
                case "Light": fontweight = FontWeights.Light; break;
                case "Medium": fontweight = FontWeights.Medium; break;
                case "Regular": fontweight = FontWeights.Regular; break;
                case "SemiBold": fontweight = FontWeights.SemiBold; break;
                case "Thin": fontweight = FontWeights.Thin; break;
                case "UltraBlack": fontweight = FontWeights.UltraBlack; break;
                case "UltraBold": fontweight = FontWeights.UltraBold; break;
                case "UltraLight": fontweight = FontWeights.UltraLight; break;
                default: fontweight = FontWeights.Normal; break;
            }
            return fontweight;
        }

        FontFamily fontfamilySetup(XmlElement xmlelement, string attribute)
        {
            string m_fontfamilyTemp = xmlelement.GetAttribute(attribute).ToString();
            FontFamily fontfamily;
            if (m_fontfamilyTemp != "")
                fontfamily = new FontFamily("Arial Black");
            else
                fontfamily = new FontFamily(m_fontfamilyTemp);
            return fontfamily;
        }

        FontStyle fontstyleSetup(XmlElement xmlelement, string attribute)
        {
            string m_fontstyleTemp = xmlelement.GetAttribute(attribute).ToString();
            FontStyle fontstyle = new FontStyle();
            switch (m_fontstyleTemp)
            {
                default: fontstyle = FontStyles.Normal; break;
                case "Italic": fontstyle = FontStyles.Italic; break;
                case "Oblique": fontstyle = FontStyles.Oblique; break;
                case "Normal": fontstyle = FontStyles.Normal; break;
            }
            return fontstyle;
        }
        string getURLstring(XmlElement xmlelement, string attribute)
        {
            return xmlelement.GetAttribute(attribute).ToString();
        }
        private Object AlignmentSetup(Object alignmentType, XmlElement xmlelement, string attribute)
        {
            string m_alignmentTemp = xmlelement.GetAttribute(attribute).ToString();
            
            if (m_alignmentTemp == "")
            {
                if (alignmentType.Equals(typeof(VerticalAlignment)))
                    return VerticalAlignment.Stretch;
                else if (alignmentType.Equals(typeof(HorizontalAlignment)))
                    return HorizontalAlignment.Stretch;
            }
            Object alignment = new Object();
            if (alignmentType.Equals(typeof(VerticalAlignment)))
            {              
                if (m_alignmentTemp == "Bottom")
                    alignment = VerticalAlignment.Bottom;
                else if (m_alignmentTemp == "Stretch")
                    alignment = VerticalAlignment.Stretch;
                else if (m_alignmentTemp == "Top")
                    alignment = VerticalAlignment.Top;
                else if (m_alignmentTemp == "Center")
                    alignment = VerticalAlignment.Center;
            }
            else if (alignmentType.Equals(typeof(HorizontalAlignment)))
            {
                if (m_alignmentTemp == "Right")
                    alignment = HorizontalAlignment.Right;
                else if (m_alignmentTemp == "Stretch")
                    alignment = HorizontalAlignment.Stretch;
                else if (m_alignmentTemp == "Left")
                    alignment = HorizontalAlignment.Left;
                else if (m_alignmentTemp == "Center")
                    alignment = HorizontalAlignment.Center;
            }
            return alignment;
        }
        private int GridDefinationSetup(XmlElement xmlelement, string attribute)
        {
            int grid_value = 0;

            if (attribute == "Grid.Column" || attribute == "Grid.Row")
                grid_value = 0;
            else if(attribute == "Grid.ColumnSpan" || attribute == "Grid.RowSpan")
                grid_value = 1;

            if(xmlelement.GetAttribute(attribute).ToString() != "")
                grid_value = Convert.ToInt32(xmlelement.GetAttribute(attribute).ToString());

            return grid_value;
        }
        private Orientation OrientationSetup(XmlElement xmlelement)
        {
            Orientation orientationValue = Orientation.Vertical;
            string orientationString = xmlelement.GetAttribute("Orientation");
            if (orientationString == "Horizontal")
                orientationValue = Orientation.Horizontal;
            if (orientationString == "Vertical")
                orientationValue = Orientation.Vertical;
            return orientationValue;
        }
        private void ParentsFrameworkSetup(ref Object parents, Object children)
        {
            if (parents == null)
                return;
            if (parents.GetType().Equals(typeof(Grid)))
                ((Grid)parents).Children.Add((UIElement)children);
            else if (parents.GetType().Equals(typeof(StackPanel)))
                ((StackPanel)parents).Children.Add((UIElement)children);
            else if (parents.GetType().Equals(typeof(WrapPanel)))
                ((WrapPanel)parents).Children.Add((UIElement)children);
            else if (parents.GetType().Equals(typeof(ScrollViewer)))
                ((ScrollViewer)parents).Content = children;
            else if (parents.GetType().Equals(typeof(GroupBox)))
                ((GroupBox)parents).Content = children;
            else if (parents.GetType().Equals(typeof(Dragablz.TabablzControl)))
                ((Dragablz.TabablzControl)parents).Items.Add(children);
            else if (parents.GetType().Equals(typeof(TabItem)))
                ((TabItem)parents).Content = children;
            else if (parents.GetType().Equals(typeof(ColorZone)))
                ((ColorZone)parents).Content = children;
            else if (parents.GetType().Equals(typeof(CheckBox)))
                ((CheckBox)parents).Content = children;
        }
        private double areaPropertySetup(XmlElement xmlelement, string attribute , Object component)
        {
            string area_property_temp = xmlelement.GetAttribute(attribute).ToString();
            double area_property_value=double.NaN;

            if (area_property_temp == "auto" || area_property_temp == "Auto")
            {
                return area_property_value;
            }
            if (area_property_temp == "")
            {
                switch (attribute)
                {
                    case "Width":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).Width;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).Width;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).Width;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).Width;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).Width;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).Width;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).Width;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).Width;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).Width;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).Width;
                        break;
                    case "Height":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).Height;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).Height;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).Height;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).Height;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).Height;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).Height;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).Height;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).Height;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).Height;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).Height;
                        break;
                    case "MaxHeight":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).MaxHeight;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).MaxHeight;
                        break;
                    case "MinHeight":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).MinHeight;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).MinHeight;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).MinHeight;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).MinHeight;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).MinHeight;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).MinHeight;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).MinHeight;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).MinHeight;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).MinHeight;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).MinHeight;
                        break;
                    case "MaxWidth":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).MaxWidth;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).MaxWidth;
                        break;
                    case "MinWidth":
                        if (component.GetType().Equals(typeof(fk_combobox)))
                            area_property_value = ((fk_combobox)component).MinWidth;
                        else if (component.GetType().Equals(typeof(fk_button)))
                            area_property_value = ((fk_button)component).MinWidth;
                        else if (component.GetType().Equals(typeof(fk_edit)))
                            area_property_value = ((fk_edit)component).MinWidth;
                        else if (component.GetType().Equals(typeof(GroupBox)))
                            area_property_value = ((GroupBox)component).MinWidth;
                        else if (component.GetType().Equals(typeof(TextBox)))
                            area_property_value = ((TextBox)component).MinWidth;
                        else if (component.GetType().Equals(typeof(TextBlock)))
                            area_property_value = ((TextBlock)component).MinWidth;
                        else if (component.GetType().Equals(typeof(ComboBox)))
                            area_property_value = ((ComboBox)component).MinWidth;
                        else if (component.GetType().Equals(typeof(ColorZone)))
                            area_property_value = ((ColorZone)component).MinWidth;
                        else if (component.GetType().Equals(typeof(fk_basiclinechart)))
                            area_property_value = ((fk_basiclinechart)component).MinWidth;
                        else if (component.GetType().Equals(typeof(WrapPanel)))
                            area_property_value = ((WrapPanel)component).MinWidth;
                        break;
                }
                return area_property_value;
            }
            return Convert.ToDouble(area_property_temp);
        }
        void ShadowDepthSetup(DependencyObject element,XmlElement xmlelement,string attribute)
        {
            string shadow_depth_temp = xmlelement.GetAttribute(attribute).ToString();
            switch (shadow_depth_temp)
            {
                case "Depth0": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth0); break;
                case "Depth1": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth1); break;
                case "Depth2": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth2); break;
                case "Depth3": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth3); break;
                case "Depth4": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth4); break;
                case "Depth5": ShadowAssist.SetShadowDepth(element, ShadowDepth.Depth5); break;
                default: return;
            }
        }

        PackIconKind packiconSetup(XmlElement xmlelement, string attribute)
        {
            string icon_kind = xmlelement.GetAttribute(attribute).ToString();
            PackIconKind icon_temp = PackIconKind.Circle;
            switch (icon_kind)
            {
                case "Circle":  icon_temp = PackIconKind.Circle; break;
                case "UserEdit": icon_temp = PackIconKind.UserEdit; break;
            }
            return icon_temp;
        }

        TextWrapping textWrappingSetup(XmlElement xmlelement, string attribute)
        {
            string textwrapping_string= xmlelement.GetAttribute(attribute).ToString();
            TextWrapping wrapping_temp = TextWrapping.NoWrap;
            switch (textwrapping_string)
            {
                case "Wrap": wrapping_temp = TextWrapping.Wrap; break;
                case "NoWrap": wrapping_temp = TextWrapping.NoWrap; break;
                case "WrapWithOverflow": wrapping_temp = TextWrapping.WrapWithOverflow; break;
            }
            return wrapping_temp;
        }
        double DoubleValueReceive(XmlElement xmlelement, string attribute)
        {
            string font_size_temp = xmlelement.GetAttribute(attribute).ToString();
            //====== 要加入所有的預設值 ======
            double font_size_value = 0.0;
            if (font_size_temp == "")
            {

                if (attribute == "Measure_lineValues_ScalesXAt")
                    font_size_value = 0.0;
                else
                    font_size_value = 10.0;
                return font_size_value;
            }
            return  Convert.ToDouble(font_size_temp);
        }
        Brush ColorValueReceive(XmlElement xmlelement, string attribute)
        {
            string color = xmlelement.GetAttribute(attribute).ToString();
            Brush color_temp = Brushes.Black;
            switch (color)
            {
                case "Red": color_temp = Brushes.Red; break;
            }
            return color_temp;
        }
        ScrollBarVisibility visibilitySetup(XmlElement xmlelement, string attribute)
        {
            string visibility_temp = xmlelement.GetAttribute(attribute).ToString();
            ScrollBarVisibility visibility_value = ScrollBarVisibility.Auto;
            switch (visibility_temp)
            {
                case "Auto": visibility_value = ScrollBarVisibility.Auto;  break;
                case "Hidden": visibility_value = ScrollBarVisibility.Hidden; break;
                case "Visible": visibility_value = ScrollBarVisibility.Visible; break;
                case "Disabled": visibility_value = ScrollBarVisibility.Disabled; break;
                default: break;
            }
            return visibility_value;
        }
        bool boolPropertyReceive(XmlElement xmlelement, string attribute)
        {
            string bool_status_temp = xmlelement.GetAttribute(attribute).ToString();
            bool bool_status_value = false;
            switch (bool_status_temp)
            {
                case "True":  bool_status_value = true; break;
                case "False": bool_status_value = false; break;
                default: break;
            }
            return bool_status_value;
        }

        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        //============================================================================================================//
        void  setLinearGradientBrush(XmlNodeList NodeList, ref Object parents_component, Object component_type, string attribute, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            LinearGradientBrush brush = new LinearGradientBrush();

            brush.EndPoint = new Point(0.5, 1);
            brush.StartPoint = new Point(0.5, 0);
            XmlNodeList children = NodeList.Item(elemlentPtr).ChildNodes;
            for (int i = 0; i < children.Count; i++)
            {
                string point_name = children.Item(i).Name;
                if (point_name == "GradientStop")
                {
                    XmlElement xmlelement1 = ((XmlElement)children.Item(i));
                    string color_temp = xmlelement1.GetAttribute("Color").ToString();
                    string offset_temp = xmlelement1.GetAttribute("Offset").ToString();
                    brush.GradientStops.Add(new GradientStop(color: (Color)ColorConverter.ConvertFromString(colorPropertySetup(color_temp)), offset:Convert.ToDouble(offset_temp)));
                }
            }
            if (component_type.GetType().Equals(typeof(ProgressBar)))
            {
                if (attribute.Contains("Foreground"))
                {
                    ((ProgressBar)parents_component).Foreground = brush;
                }
            }
        }
        void setSolidColorBrush(XmlNodeList NodeList, ref Object parents_component, Object component_type, string attribute, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            SolidColorBrush brush = new SolidColorBrush();
            string color_string = xmlelement.GetAttribute("Color").ToString();
            if(color_string.Contains("HotTrackColorKey"))
                brush.Color = SystemColors.HotTrackColor;
            else if(color_string.Contains("MenuHighlightColorKey"))
                brush.Color = SystemColors.MenuHighlightColor;
            else if(color_string.Contains("GradientInactiveCaptionColorKey"))
                brush.Color = SystemColors.GradientInactiveCaptionColor;
            else if (color_string.Contains("HighlightColorKey"))
                brush.Color = SystemColors.HighlightColor;
            else if (color_string.Contains("ControlColorKey"))
                brush.Color = SystemColors.ControlColor;

            if (component_type.GetType().Equals(typeof(TextBlock)))
            {
                if (attribute.Contains("Foreground"))
                {
                    ((TextBlock)parents_component).Foreground = brush;
                }
            }
            else if (component_type.GetType().Equals(typeof(TextBox)))
            {
                if (attribute.Contains("Foreground"))
                {
                    ((TextBox)parents_component).Foreground = brush;
                }
            }
            else if (component_type.GetType().Equals(typeof(ColorZone)))
            {
                if (attribute.Contains("Foreground"))
                {
                    ((ColorZone)parents_component).Foreground = brush;
                }
                else if (attribute.Contains("Background"))
                {
                    ((ColorZone)parents_component).Background = brush;
                }
            }
            else if (component_type.GetType().Equals(typeof(Grid)))
            {
                if (attribute.Contains("Background"))
                {
                    ((Grid)parents_component).Background = brush;
                }
            }
            else if (component_type.GetType().Equals(typeof(fk_basiclinechart)))
            {
                if (attribute.Contains("Background"))
                {
                    ((fk_basiclinechart)parents_component).Background = brush;
                }
            }
            else if (component_type.GetType().Equals(typeof(GroupBox)))
            {
                if (attribute.Contains("Background"))
                {
                    ((GroupBox)parents_component).Background = brush;
                }
            }
        }
        string colorPropertySetup(string color_string)
        {
            string color_string_temp="";
            if (!color_string.Contains("#"))
            {
                switch (color_string)
                {
                    case "Black": color_string_temp = "#FF000000"; break;
                }
                return color_string_temp;
            }
            return color_string;
        }

        void gridplacementSet(XmlNodeList NodeList, Object component, int elemlentPtr)
        {         
            string subelement = ((XmlElement)NodeList.Item(elemlentPtr)).Name;                 
            if (subelement == "ColumnDefinition")
            {
                ColumnDefinition column = new ColumnDefinition();
                string width = ((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Width").ToString();                
                if (width.Contains("*"))
                {
                    string num = width.Trim('*');
                    if (num == "" || num == "0")
                        num = "1";
                    column.Width = new GridLength(Convert.ToDouble(num), GridUnitType.Star);                   
                }
                else if (width.Contains("Auto") || width.Contains("auto"))
                    column.Width = new GridLength(1, GridUnitType.Auto);
                else
                    column.Width = new GridLength(Convert.ToDouble(width), GridUnitType.Pixel);
                ((Grid)component).ColumnDefinitions.Add(column);
            }
            else if (subelement == "RowDefinition")
            {
                RowDefinition row = new RowDefinition();
                string height = ((XmlElement)NodeList.Item(elemlentPtr)).GetAttribute("Height").ToString();
                if (height.Contains("*"))
                {
                    string num = height.Trim('*');
                    if (num == "" || num == "0")
                        num = "1";
                    row.Height = new GridLength(Convert.ToDouble(num), GridUnitType.Star);
                }
                else if (height.Contains("Auto"))
                    row.Height = new GridLength(1, GridUnitType.Auto);
                else
                    row.Height = new GridLength(Convert.ToDouble(height), GridUnitType.Pixel);
                ((Grid)component).RowDefinitions.Add(row);
            }
            
        }
        #endregion

        #region Header 參數解析
        void FormSetup(XmlNodeList NodeList,Window windowMAIN, ScrollViewer scorllView, DrawerHost drawer, Object ParsentComponent, Grid main_grid, int elemlentPtr)
        {
            double width, height;
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            width = DoubleValueReceive(xmlelement, "Width");
            height = DoubleValueReceive(xmlelement, "Height");
            //windowMAIN.Height = height;
            //windowMAIN.Width = width;
            if (ParsentComponent.GetType().Equals(typeof(Grid)))
            {
                ((Grid)ParsentComponent).Height = height;
                ((Grid)ParsentComponent).Width = width;
            }
            else if (ParsentComponent.GetType().Equals(typeof(GroupBox)))
            {
                //((GroupBox)ParsentComponent).Height = height;
                //((GroupBox)ParsentComponent).Width = width;
            }
            scorllView.Height = height;
            ///if(drawer!=null)
            //    drawer.Height = height;
            //main_grid.Height = height + 20;
        }
        void IdentitySetup(XmlNodeList NodeList, ref PaltformIndentity indentity, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            indentity.firmware = xmlelement.GetAttribute("firmware").ToString();
            indentity.hardware = xmlelement.GetAttribute("hardware").ToString();
            indentity.serial = xmlelement.GetAttribute("serial").ToString();
            indentity.sensor = xmlelement.GetAttribute("sensor").ToString();
            switch (indentity.serial)
            {
                //case "flow": break;
            }
        }
        void CommunicationSetup(XmlNodeList NodeList, ref PaltformIndentity indentity, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            indentity.CommInterface = xmlelement.GetAttribute("interface").ToString();
        }

        void SpecificFunctionSetup(XmlNodeList NodeList, ref PaltformIndentity indentity, int elemlentPtr)
        {
            XmlElement xmlelement = ((XmlElement)NodeList.Item(elemlentPtr));
            indentity.LoadDefaultAddr = Convert.ToInt32(xmlelement.GetAttribute("load_default_addr").ToString());
            indentity.LoadFirmwaretAddr = Convert.ToInt32(xmlelement.GetAttribute("load_firmware_addr").ToString());
            indentity.SaveEepromAddr = Convert.ToInt32(xmlelement.GetAttribute("save_eeprom_addr").ToString());
        }
        #endregion
    }
}
