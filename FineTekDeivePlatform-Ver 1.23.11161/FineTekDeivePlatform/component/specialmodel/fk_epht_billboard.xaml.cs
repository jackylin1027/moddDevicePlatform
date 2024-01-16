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

namespace FineTekDeivePlatform.component.specialmodel
{
    /// <summary>
    /// fk_epht_billboard.xaml 的互動邏輯
    /// </summary>
    public partial class fk_epht_billboard : UserControl
    {
        private fn fk_function = new fn();
        public static readonly DependencyProperty PFC_METER_IDProperty =
        DependencyProperty.Register("PFC_METER_ID", typeof(string), typeof(fk_epht_billboard), new PropertyMetadata("FFFFFFFFFF"));
        public static readonly DependencyProperty PFC_METER_VOLUMNProperty =
        DependencyProperty.Register("PFC_METER_VOLUMN", typeof(string), typeof(fk_epht_billboard), new PropertyMetadata("0"));
        public string PFC_METER_ID
        {
            get { return (string)GetValue(PFC_METER_IDProperty); }
            set { SetValue(PFC_METER_IDProperty, value); }
        }
        public string PFC_METER_VOLUMN
        {
            get { return (string)GetValue(PFC_METER_VOLUMNProperty); }
            set { SetValue(PFC_METER_VOLUMNProperty, value); }
        }
        public fk_epht_billboard()
        {
            InitializeComponent();
        }
        public void update_parmeter2userinterface(List<UInt16> parameterList)
        {
            PFC_METER_ID = fk_function.byteToHexstring(parameterList, (int)PFC_INFO.ephData0_userNumber0, ((int)PFC_INFO.ephData0_userNumber3) - ((int)PFC_INFO.ephData0_userNumber0)).Substring(0, 14);
            PFC_METER_VOLUMN = ((fk_function.HexToBCD(fk_function.AddBCDValue_3Bytes(parameterList, (int)PFC_INFO.ephData0_volumeBCD_dotBCD3), 10)) / Math.Pow(10, parameterList[(int)PFC_INFO.ephData0_volumeBCD_dotBCD0])).ToString();
            editMETER_FIRMWARE_VERSION.Text = string.Format("{0:X4}", parameterList[(int)PFC_INFO.ephData0_firmwareVersionBCD ]); 
            editMETER_CALIBER.Text = string.Format("{0:X2}", parameterList[(int)PFC_INFO.ephData0_caliberWmpowerBCD ] >> 8).ToString(); 
            editMETER_BATTERY_VOLTAGE.Text = string.Format("{0:X2}", parameterList[(int)PFC_INFO.ephData0_caliberWmpowerBCD ] & 0xFF).ToString();
            editMETER_FLOW_RATE.Text = ((fk_function.HexToBCD(fk_function.AddBCDValue_2Bytes(parameterList, (int)PFC_INFO.ephData0_flowRateBCD_dotBCD2 ), 6)) / Math.Pow(10, parameterList[(int)PFC_INFO.ephData0_flowRateBCD_dotBCD0 ])).ToString();
            editMETER_PLUS_TOTALFLOW.Text = ((fk_function.HexToBCD(fk_function.AddBCDValue_3Bytes(parameterList, (int)PFC_INFO.ephData0_plusTotalFlowBCD_dotBCD3 ), 10)) / Math.Pow(10, parameterList[(int)PFC_INFO.ephData0_plusTotalFlowBCD_dotBCD0])).ToString();
            editMETER_MINUS_TOTALFLOW.Text = ((fk_function.HexToBCD(fk_function.AddBCDValue_3Bytes(parameterList, (int)PFC_INFO.ephData0_minusTotalFlowBCD_dotBCD3 ), 10)) / Math.Pow(10, parameterList[(int)PFC_INFO.ephData0_minusTotalFlowBCD_dotBCD0 ])).ToString();
            editMETER_REFLASH_TIME.Text = DateTime.Now.ToString() ;
            editMETER_LDAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_lDayBCD])).ToString();
            editMETER_NDAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_nDayBCD])).ToString();
            editMETER_ODAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_oDayBCD])).ToString();
            editMETER_UDAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_uDayBCD])).ToString();;
            editMETER_HDAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_hDayBCD])).ToString();
            editMETER_BDAY.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_bDayBCD])).ToString();
            editMETER_USER_COUNT.Text = (fk_function.BCDToDec(parameterList[(int)PFC_INFO.ephData0_userCountBCD ])).ToString();
            editMETER_STATUS.Text = parameterList[(int)PFC_INFO.ephData0_statusBCD ].ToString();
        }
        public void update_parmeter2SQLcommand(ref string key_prompt,ref  string value_prompt)
        {
            key_prompt += "sn" + ",";
            value_prompt += "\"" + PFC_METER_ID.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "b_accu_flow" + ",";
            value_prompt += "\"" + PFC_METER_VOLUMN.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "firmware" + ",";
            value_prompt += "\"" + editMETER_FIRMWARE_VERSION.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "f_accu_flow" + ",";
            value_prompt += "\"" + editMETER_PLUS_TOTALFLOW.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "r_accu_flow" + ",";
            value_prompt += "\"" + editMETER_MINUS_TOTALFLOW.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "flow_rate" + ",";
            value_prompt += "\"" + editMETER_FLOW_RATE.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "battery" + ",";
            value_prompt += "\"" + editMETER_BATTERY_VOLTAGE.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "f_usage" + ",";
            value_prompt += "\"" + editMETER_USER_COUNT.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "b_day" + ",";
            value_prompt += "\"" + editMETER_BDAY.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "l_day" + ",";
            value_prompt += "\"" + editMETER_LDAY.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "n_day" + ",";
            value_prompt += "\"" + editMETER_NDAY.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "o_day" + ",";
            value_prompt += "\"" + editMETER_ODAY.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "u_day" + ",";
            value_prompt += "\"" + editMETER_UDAY.Text.Replace("\0", String.Empty) + "\"" + ",";
            key_prompt += "h_day" + ",";
            value_prompt += "\"" + editMETER_HDAY.Text.Replace("\0", String.Empty) + "\"" + ",";
        }
        public enum INFO : int
        {
            ephData0_userNumber0 = 0,
            ephData0_userNumber1,
            ephData0_userNumber2,
            ephData0_userNumber3,
            ephData0_userNumber4,
            ephData0_userNumber5,
            ephData0_meterID0,
            ephData0_meterID1,
            ephData0_meterID2,
            ephData0_meterID3,
            ephData0_meterID4,
            ephData0_meterID5,
            ephData0_firmwareVersionBCD,
            ephData0_caliberWmpowerBCD,
            ephData0_volumeBCD_dotBCD3,
            ephData0_volumeBCD_dotBCD2,
            ephData0_volumeBCD_dotBCD1,
            ephData0_volumeBCD_dotBCD0,
            ephData0_flowRateBCD_dotBCD2,
            ephData0_flowRateBCD_dotBCD1,
            ephData0_flowRateBCD_dotBCD0,
            ephData0_plusTotalFlowBCD_dotBCD3,
            ephData0_plusTotalFlowBCD_dotBCD2,
            ephData0_plusTotalFlowBCD_dotBCD1,
            ephData0_plusTotalFlowBCD_dotBCD0,
            ephData0_minusTotalFlowBCD_dotBCD3,
            ephData0_minusTotalFlowBCD_dotBCD2,
            ephData0_minusTotalFlowBCD_dotBCD1,
            ephData0_minusTotalFlowBCD_dotBCD0,
            ephData0_lDayBCD,
            ephData0_nDayBCD,
            ephData0_oDayBCD,
            ephData0_uDayBCD,
            ephData0_hDayBCD,
            ephData0_bDayBCD,
            ephData0_userCountBCD,
            ephData0_statusBCD,
            ephData0_reserver0,
            ephData0_reserver1,
            ephData0_reserver2,
        };
    }
}
