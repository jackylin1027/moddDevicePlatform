using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FineTekDeivePlatform.additional
{
    class exFunctionEPH
    {
        public object MergeContent(object obj1, object obj2, object obj3, object obj4)
        {
            ComboBox combobox1 = (ComboBox)obj1, combobox2 = (ComboBox)obj2, combobox3 = (ComboBox)obj3;
            TextBox textbox = (TextBox)obj4;
            TextBox result = new TextBox();
            result.Text = (string)combobox1.SelectedValue + (string)combobox2.SelectedValue + (string)combobox3.SelectedValue +  textbox.Text;
            return result;
        }

        public object SelectionContent(object obj1, object obj2)
        {
            ComboBox combobox1 = (ComboBox)obj1;
            ComboBox result = (ComboBox)obj2;
            switch (combobox1.SelectedIndex)
            {
                case 0: result.SelectedIndex = 1; break;
                case 1: result.SelectedIndex = 1; break;
                case 2: result.SelectedIndex = 1; break;
                case 3: result.SelectedIndex = 1; break;
                case 4: result.SelectedIndex = 0; break;
            }       
            return result;
        }
        public class TemplateContanst 
        {
            static public string[] caliberDN15 = new string[]{ "3" , "0.30100" , "0.30100" , "0.30100" , "0.28165" , "0.28165" , "0.28165" , "0.28165" ,
            "5.75","4.75","3.50","4","7","13","25","6","12","24","41"};
            static public string[] caliberDN20 = new string[]{ "3" , "0.44757" , "0.44757" , "0.44757" , "0.42569" , "0.42569" , "0.42569" , "0.42569" ,
            "5.50","4.75","3.50","4","7","12","25","6","11","24","41"};
            static public string[] caliberDN25 = new string[]{ "3" , "0.63684" , "0.63684" , "0.63684" , "0.55363" , "0.55363" , "0.55363" , "0.55363" ,
            "5.50","4.75","3.50","4","7","14","25","6","13","24","41"};
            static public string[] caliberDN40 = new string[]{ "3" , "1.55560" , "1.55560" , "1.55560" , "1.50405" , "1.50405" , "1.50405" , "1.50405" ,
            "4.75","4.00","3.25","4","9","15","30","8","14","29","41"};
            static public string[] caliberDN50 = new string[]{ "3" , "2.72669" , "2.64808" , "2.60963" , "2.60635" , "2.61356" , "2.62153" , "2.62229" ,
            "6.50","4.25","3.25","4","8","14","25","7","13","24","41"};
        }
        public object LoadTempalete(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8, object obj9, object obj10, object obj11, object obj12, object obj13, object obj14, object obj15, object obj16, object obj17, object obj18, object obj19, object obj20)
        {
            TextBox  textbox2 = (TextBox)obj2, textbox3 = (TextBox)obj3, textbox4 = (TextBox)obj4, textbox5 = (TextBox)obj5, textbox6 = (TextBox)obj6;
            object[] objectArray = new object[] {obj2, obj3, obj4, obj5, obj6, obj7, obj8, obj9, obj10, obj11, obj12, obj13, obj14, obj15, obj16, obj17, obj18, obj19, obj20, };          
            ComboBox combobox = (ComboBox)obj1;
            int i =0;
            switch (((ComboBox)obj1).SelectedIndex)
            {
                case 0: //==== DN15
                    for (i = 0; i < objectArray.Count(); i++)
                        ((TextBox)objectArray[i]).Text = TemplateContanst.caliberDN15[i];
                    break;
                case 1:
                    for (i = 0; i < objectArray.Count(); i++)
                        ((TextBox)objectArray[i]).Text = TemplateContanst.caliberDN20[i];
                    break;
                case 2:
                    for (i = 0; i < objectArray.Count(); i++)
                        ((TextBox)objectArray[i]).Text = TemplateContanst.caliberDN25[i];
                    break;
                case 3:
                    for (i = 0; i < objectArray.Count(); i++)
                        ((TextBox)objectArray[i]).Text = TemplateContanst.caliberDN40[i];
                    break;
                case 4:
                    for (i = 0; i < objectArray.Count(); i++)
                        ((TextBox)objectArray[i]).Text = TemplateContanst.caliberDN50[i];
                    
                    break;
            }
            return null;
        }
    }
}
