using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FineTekDeivePlatform.additional
{
    
    class exFunctionEPH0019
    {
        int DeviceNumber;
        public object RenewDeviceNumber(object obj1)
        {
            TextBox textbox = (TextBox)obj1;
            try
            {
                if (Convert.ToInt32(textbox.Text) != DeviceNumber)
                {
                    DeviceNumber = Convert.ToInt32(textbox.Text);
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
