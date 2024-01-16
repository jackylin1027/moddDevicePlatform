using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTekDeivePlatform
{
    class PaltformIndentity
    {
        public string firmware { get; set; }
        public string hardware { get; set; }
        public string serial { get; set; }
        public string sensor { get; set; }
        public string CommInterface { get; set; }
        public int SaveEepromAddr { get; set; }
        public int LoadDefaultAddr { get; set; }
        public int LoadFirmwaretAddr { get; set; }
        public PaltformIndentity()
        {
            firmware = "--/---/---";
            hardware = "--/---/---";
            serial = "無法分類";
            sensor = "No Device";
            CommInterface = "Unknow";
            SaveEepromAddr = 0;
            LoadDefaultAddr = 0;
            LoadFirmwaretAddr = 0;
        }
    }
     
}
