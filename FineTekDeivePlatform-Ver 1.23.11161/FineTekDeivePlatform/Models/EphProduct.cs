using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTekDeivePlatform.Models
{
    public class EphProduct
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string MeterName { get; set; }
        public int ModbusId { get; set; }
        public string FirmwareVersion { get; set; }
        public int Caliber { get; set; }
        public decimal BatteryVoltage { get; set; }
        public string Volume { get; set; }
        public decimal FlowRate { get; set; }
        public decimal PlusTotalFlow { get; set; }
        public decimal MinusTotalFlow { get; set; }
        public int lDay { get; set; }
        public int nDay { get; set; }
        public int oDay { get; set; }
        public int uDay { get; set; }
        public int hDay { get; set; }
        public int UserCount { get; set; }
        public int Status { get; set; }
        public string DataTime { get; set; }
    }
}
