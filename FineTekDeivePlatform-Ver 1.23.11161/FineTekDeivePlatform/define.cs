using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTekDeivePlatform
{
    enum IDX_EPHITEM : int
    {
        ID01_EPHITEM,
        ID02_EPHITEM,
        ID03_EPHITEM,
        ID04_EPHITEM,
        ID05_EPHITEM,
        ID06_EPHITEM,
        ID07_EPHITEM,
        ID08_EPHITEM,
        ID09_EPHITEM,
        ID10_EPHITEM,
        ID11_EPHITEM,
        ID12_EPHITEM,
        ID13_EPHITEM,
        ID14_EPHITEM,
        ID15_EPHITEM,
        ID16_EPHITEM,
        ID17_EPHITEM,
        ID18_EPHITEM,
        ID19_EPHITEM,
        ID20_EPHITEM,
        ID21_EPHITEM,
        ID22_EPHITEM,
        ID23_EPHITEM,
        ID24_EPHITEM,
        ID25_EPHITEM,
        ID26_EPHITEM,
        ID27_EPHITEM,
        ID28_EPHITEM,
        ID29_EPHITEM,
        ID30_EPHITEM,
        ID31_EPHITEM,
        ID32_EPHITEM,
        EPHITEM_TOTAL,
    };
    enum IDX_PAGE : int
    {
        NO1_PAGE,
        NO2_PAGE,
        NO3_PAGE,
        NO4_PAGE,
        PAGE_TOTAL,
    };
    enum IDX_GROUPBOX : int
    {
        DEVICE_HEADER_GROUP, 
        DISTRIBUTION_GROUP,
        DEVICE_NUMBER_MAP_GROUP,
        PARAMETER_SETTING_GROUP,
        PRODUCT_INFO_GROUP,
        MONITOR_ID_SELECT_GROUP,
        MANUAL_ID_SELECT_GROUP,
        GROUP_TOTAL,
    };

    enum IDX_EDIT : int
    {
        HEADER_NUMBER_RW_EDIT,
        HEADER_MODBUS_RW_ID_EDIT,
        HEADER_ADDRESS_RW_EDIT,
        DISTRIBUT_DEVICE_NUM_EDIT,
        SCANRATE_RW_EDIT,
        DELAYPOLLS_RW_EDIT,
        MAX_DEVNUM_RW_EDIT,
        REPLAY_TIMES_RW_EDIT,
        RESPONSE_TIME_RW_EDIT,
        PRODUCT_SERIAL_RW,
        PRODUCT_DATE_RW,
        PRODUCT_NAME_RW,     
        FIRMWARE_VERSION_RW,
        HARDWARE_VERSION_RW,        
        SF_PRODUCT_SERIAL_RW,
        SF_PRODUCT_DATE_RW,
        EDIT_TOTAL,
    };
    enum IDX_COMBOBOX : int
    {
        OPERATION_MODE_COMBOBOX,
        WATER_FACTORY_SUPPORT_COMBOBOX,
        MONITOR_ID_SELECT_COMBOBOX,
        MANUAL_ID_SELECT_COMBOBOX,
        COMBOBOX_TOTAL,
    };

    enum IDX_BUTTON : int
    {
        DISTRIBUTION_TASK_NUTTON,
        SAVE_SETTING_PARAMETER_BUTTON,
        MODBUS_FAST_READ, 
        MODBUS_READ_BUTTON,
        RS485_READ_BUTTON,
        RS485_MONITOR_BUTTON,
        BUTTON_TOTAL,
    };
    enum IDX_CUSTOM_LABEL : int
    {
        SOFTWARE_VERSION_CUSTOM_LABEL,
        CUSTOM_LABEL_TOTAL,
    };
    enum DISTRIBUT_FLAG : int
    {
        DISTRIBUT_FLAG_NOT_BUSY,
        DISTRIBUT_FLAG_BUSY, 
    };
    enum IDX_CONTEXT : int
    {
        ID1_userNumber_CONTEXT,
        ID1_meterID_CONTEXT,
        ID1_firmwareVersion_CONTEXT,
        ID1_caliber_CONTEXT,
        ID1_Wmpower_CONTEXT,
        ID1_volume_CONTEXT,
        ID1_flowRate_CONTEXT,
        ID1_plusTotalFlow_CONTEXT,
        ID1_minusTotalFlow_CONTEXT,
        ID1_lDay_CONTEXT,
        ID1_nDay_CONTEXT,
        ID1_oDay_CONTEXT,
        ID1_uDay_CONTEXT,
        ID1_hDay_CONTEXT,
        ID1_bDay_CONTEXT,
        ID1_userCount_CONTEXT,
        ID1_status_CONTEXT,
        ID1_datetime_CONTEXT,
        CONTEXT_TOTAL,
    };
    enum IDX_CONTEXT_UI : int
    {
        IDX_REMAIN_CONTEXT_UI,
        IDX_CONTEXT_UI_TOTAL, 
    };
    enum IDX_DATAGRIDVIEW : int
    {
        IDX_DISTRIBUT_DATAGRIDVIEW,
        IDX_DATAGRIDVIEW_TOTAL,
    };
    enum IDX_DATATABLE : int
    {
        IDX_DISTRIBUT_DATATABLE,
        IDX_DATATABLE_TOTAL,
    };

    enum IDX_ESMODBUS : int
    {
        IDX_MONITOR_ESMODBUS,
        IDX_MANUAL_ESMODBUS,
        IDX_ESMODBUS_TOTAL,
    };
    enum IDX_CHECKBOX : int
    {
        LAW_DATA_CHECKBOX,       
        IDX_CHECKBOX_TOTAL,
    };
    enum IDX_CMD_STATE
    {
        SettingParameter_CMD_STATE,
        ID1_Transmitter_CMD_STATE,
        ID2_Transmitter_CMD_STATE,
        ID3_Transmitter_CMD_STATE,
        ID4_Transmitter_CMD_STATE,
        ID5_Transmitter_CMD_STATE,
        ID6_Transmitter_CMD_STATE,
        ID7_Transmitter_CMD_STATE,
        ID8_Transmitter_CMD_STATE,
        ID9_Transmitter_CMD_STATE,
        ID10_Transmitter_CMD_STATE,
        ID11_Transmitter_CMD_STATE,
        ID12_Transmitter_CMD_STATE,
        ID13_Transmitter_CMD_STATE,
        ID14_Transmitter_CMD_STATE,
        ID15_Transmitter_CMD_STATE,
        ID16_Transmitter_CMD_STATE,
        ID17_Transmitter_CMD_STATE,
        ID18_Transmitter_CMD_STATE,
        ID19_Transmitter_CMD_STATE,
        ID20_Transmitter_CMD_STATE,
        ID21_Transmitter_CMD_STATE,
        ID22_Transmitter_CMD_STATE,
        ID23_Transmitter_CMD_STATE,
        ID24_Transmitter_CMD_STATE,
        ID25_Transmitter_CMD_STATE,
        ID26_Transmitter_CMD_STATE,
        ID27_Transmitter_CMD_STATE,
        ID28_Transmitter_CMD_STATE,
        ID29_Transmitter_CMD_STATE,
        ID30_Transmitter_CMD_STATE,
        ID31_Transmitter_CMD_STATE,
        ID32_Transmitter_CMD_STATE,
        Factory_304h_CMD_STATE,
        Factory_404h_CMD_STATE,
        Factory_504h_CMD_STATE,
        eph_comm_arrangement_1_8_CMD_STATE,
        eph_comm_arrangement_9_16_CMD_STATE,
        eph_comm_arrangement_17_24_CMD_STATE,
        eph_comm_arrangement_25_32_CMD_STATE,
        CMD_STATE_TOTAL,
    }
    enum IDX_COMMULICATON_MODE : int
    {
        COMMULICATON_MODBUS,
        COMMULICATON_RS485,
    };

    enum COMMULICATION_MODE
    {
        CHT_COMMULICATION_MODE,
        MODBUS_COMMULICATION_MODE,
    };
    enum FACTORY_POLLING_STATE : int
    {
        FACTORY_POLLING_STATE_FK,
        FACTORY_POLLING_STATE_304H,
        FACTORY_POLLING_STATE_404H,
        FACTORY_POLLING_STATE_504H,
    };
    enum REALWANE_FREQUENCY
    {
        RF500HZ,
        RF1000HZ,
        RF2000HZ,
    }
    enum WATER_FACTORY_SUPPORT : int
    {
        WATER_FACTORY_SUPPORT_NO,
        WATER_FACTORY_SUPPORT_YES,
    };

    enum OPERATION_MODE : int
    {
        OPERATION_MODE_FIXREAD,
        OPERATION_MODE_COMREAD,
        OPERATION_MODE_DISTRIBUTION,
    };

    enum PFC_DISTRIBUT_STATE
    {
        PFC_DISTRIBUT_STATE_INIT,
        PFC_DISTRIBUT_STATE_CHT2MODBUS,
        PFC_DISTRIBUT_STATE_POLLING,
        PFC_DISTRIBUT_STATE_SETTING,
        PFC_DISTRIBUT_STATE_MODBUS2CHT,
        PFC_DISTRIBUT_STATE_COMPLATE,
    };
    enum PFC_DISTRIBUT_TASK:int
    {
        PFC_DISTRIBUT_TASK_STOP,
        PFC_DISTRIBUT_TASK_START,
        PFC_DISTRIBUT_TASK_HALT,
    };
    class define
    {

    }
     class Contanst
    {
        public const string IniFileName = @"\setup.ini";
        public const string LogFileName = "EPH_log.csv";
        public const string SOFTWARE_VERSION = "Ver 1.0.07";
        public const string COMPATIBLE_FIRMWARE_VERSION = "Ver 0.0.08";
        public const string SOFTWARE_DATE = "2022/12/23";
        public const int MAX_DEVICE_NUM = 32;
        public const int MAPADDR = 40;
        public const int POLLING_INTERVAL = 700;
        public const int CONTEXT_CNT = (int)IDX_CONTEXT.ID1_userNumber_CONTEXT - (int)IDX_CONTEXT.ID1_status_CONTEXT + 1;
        public const int IMMEDIALETY_TASK_START = 0x100;
        public const byte RS485_COMMAND_READ = 0x11;
        public const byte RS485_COMMAND_MONITOR = 0x22;
        public const bool USE_XML_FK_COMPONENT = false;    
    }

    public class tlb
    {
        static public string[] context_label = new string[]
        {
            "用戶水號","水量計器號","版本","口徑","電池電壓","總積算值","瞬時流量","正向總累積量",
            "反向總累積量","漏水天數","運轉天數","靜止天數","逆流天數","磁干擾天數","電力不足天數","開關次數","狀態旗標",
        };
        static public string[] context_unit = new string[]
        {
            "","","","DN(mm)","0.1V","m³","m³/s","m³",
            "m³","","","","","","","","",
        };
        static public string[] water_factory_support_combobox_items = new string[]
        {
            "不支援","支援",
        };
        static public string[] commulcation_mode_combobox_items = new string[]
        {
            "固定監聽","主機讀取","自動分配水表ID",
        };
        static public string[] commulcation_type_combobox_items = new string[]
        {
           "Modbus","RS-485",
        };
        static public string[] id_select_combobox_items = new string[]
        {
           "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26","27","28","29","30","31","32",
        };
        static public string[] combobox_label = new string[(int)IDX_COMBOBOX.COMBOBOX_TOTAL]
        {
           "工作模式", "水廠通訊模式","選擇水表ID","選擇水表ID",
        };
        static public string[] button_label = new string[(int)IDX_BUTTON.BUTTON_TOTAL]
        {
            "開始分配ID","儲存設定參數","快速量測模式","MODBUS即時讀取","水廠模式讀取","水廠模式監控"
        };
        static public string[] edit_label = new string[(int)IDX_EDIT.EDIT_TOTAL]
        {
            "傳訊器編號:", "Modbus位址:", "水廠模式位址:","自動分配水表數量",
            "循環時間","水表間隔時間","通訊數量","嘗試連線次數","Response time",
            "序號","工單","產品名稱","韌體版本","硬體版本","半成品序號","半成品工單",
        };
        static public string[] edit_unit = new string[(int)IDX_EDIT.EDIT_TOTAL]
        {
            "","","","",
            "s","s","","","ms",
            "","","","","","","",
        };
        static public readonly string[] CONNECT_LABEL = { "連線", "停止連線" };
        static public readonly string[] SYNC_LABEL = { "暫停讀取", "同步寫入" };
    }
    enum zone_info : int
    {
        SettingParameter_Zone,
        Zone_Total,
    };

    enum PFC_INFO : int
    {
        ephData0_userNumber0=0,
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

        PFC_PRODUCT_NAME_RW = 1280,
        PFC_PRODUCT_NAME_RW_1,
        PFC_PRODUCT_NAME_RW_2,
        PFC_PRODUCT_NAME_RW_3,
        PFC_PRODUCT_NAME_RW_4,
        PFC_PRODUCT_NAME_RW_5,
        PFC_PRODUCT_NAME_RW_6,
        PFC_PRODUCT_NAME_RW_7,
        PFC_PRODUCT_NAME_RW_8,
        PFC_PRODUCT_NAME_RW_9,
        PFC_PRODUCT_NAME_RW_10,
        PFC_PRODUCT_NAME_RW_11,
        PFC_PRODUCT_NAME_RW_12,
        PFC_PRODUCT_NAME_RW_13,
        PFC_PRODUCT_NAME_RW_14,
        PFC_PRODUCT_NAME_RW_15,
        PFC_PRODUCT_NAME_RW_16,
        PFC_PRODUCT_NAME_RW_17,
        PFC_PRODUCT_SERIAL_RW,
        PFC_PRODUCT_SERIAL_RW_1,
        PFC_PRODUCT_SERIAL_RW_2,
        PFC_PRODUCT_SERIAL_RW_3,
        PFC_PRODUCT_SERIAL_RW_4,
        PFC_PRODUCT_SERIAL_RW_5,
        PFC_PRODUCT_SERIAL_RW_6,
        PFC_PRODUCT_SERIAL_RW_7,
        PFC_PRODUCT_SERIAL_RW_8,
        PFC_PRODUCT_SERIAL_RW_9,
        PFC_FIRMWARE_VERSION_RW,
        PFC_FIRMWARE_VERSION_RW_1,
        PFC_FIRMWARE_VERSION_RW_2,
        PFC_FIRMWARE_VERSION_RW_3,
        PFC_FIRMWARE_VERSION_RW_4,
        PFC_FIRMWARE_VERSION_RW_5,
        PFC_HARDWARE_VERSION_RW,
        PFC_HARDWARE_VERSION_RW_1,
        PFC_HARDWARE_VERSION_RW_2,
        PFC_HARDWARE_VERSION_RW_3,
        PFC_HARDWARE_VERSION_RW_4,
        PFC_HARDWARE_VERSION_RW_5,
        PFC_HARDWARE_VERSION_RW_6,
        PFC_HARDWARE_VERSION_RW_7,
        PFC_HARDWARE_VERSION_RW_8,
        PFC_HARDWARE_VERSION_RW_9,
        PFC_HARDWARE_VERSION_RW_10,
        PFC_PRODUCT_DATE_RW,
        PFC_PRODUCT_DATE_RW_1,
        PFC_PRODUCT_DATE_RW_2,
        PFC_PRODUCT_DATE_RW_3,
        PFC_PRODUCT_DATE_RW_4,
        PFC_PRODUCT_DATE_RW_5,
        PFC_SF_PRODUCT_SERIAL_RW,
        PFC_SF_PRODUCT_SERIAL_RW_1,
        PFC_SF_PRODUCT_SERIAL_RW_2,
        PFC_SF_PRODUCT_SERIAL_RW_3,
        PFC_SF_PRODUCT_SERIAL_RW_4,
        PFC_SF_PRODUCT_SERIAL_RW_5,
        PFC_SF_PRODUCT_SERIAL_RW_6,
        PFC_SF_PRODUCT_DATE_RW,
        PFC_SF_PRODUCT_DATE_RW_1,
        PFC_SF_PRODUCT_DATE_RW_2,
        PFC_SF_PRODUCT_DATE_RW_3,
        PFC_SF_PRODUCT_DATE_RW_4,
        PFC_SF_PRODUCT_DATE_RW_5,
        PFC_MODBUS_MASTER_DELEYPOLLS_RW,
        PFC_MODBUS_MASTER_SCANRATE_RW,
        PFC_MODBUS_MASTER_MAX_DEVICE_NUM_RW,
        PFC_MODBUS_MASTER_REPLAY_TIMES_RW,
        PFC_MODBUS_MASTER_RESPONSE_TIME_RW,
        PFC_WATER_FACTORY_SUPPORT_RW,
        PFC_OPERATION_MODE_RW,
        PFC_MODBUS_ID_RW,
        PFC_DUSTRIBUT_MAX_DEVICE_RW,
        PFC_DUSTRIBUT_FIRST_REPEAT_TARGET_RW,
        PFC_DUSTRIBUT_ERROR_TARGET_RW,
        PFC_DISTRIBUTION_TASK_RW,
        //===== modbus table 新增自動分配ID的index 跟 address, Ver 1.0.03 ====
        PFC_DISTRIBUT_STATE_RW,
        PFC_DUSTRIBUT_POLLING_ID_RW,
        PFC_DUSTRIBUT_DEVICE_COMPLETE_NUM_RW,
        //===== modbus table新增即時讀取的  address, Ver 1.0.05 =====
        PFC_IMMEDIATELY_CMD_RW,
        PFC_RESERVE9_RW,
        PFC_SAVE_CALIBRATION_SETTING_RW,
        PFC_LOAD_DEFAULT_SETTING_RW,
        PFC_SAVE_SYSTEM_VAR_TO_FRAM_RW,
        PFC_LOAD_FIRMWARE_SETTING_RW,
        //===== modbus table 新增RS-485的index 跟 address, Ver 1.0.03 ====
        PFC_RS485_NUMBER_RW,
        PFC_RS485_NUMBER_RW_1,
        PFC_RS485_NUMBER_RW_2,
        PFC_RS485_NUMBER_RW_3,
        PFC_RS485_NUMBER_RW_4,
        PFC_RS485_NUMBER_RW_5,
        PFC_RS485_ADDRESS_RW,
        PFC_RS485_ADDRESS_RW_1,
        PFC_RS485_ADDRESS_RW_2,
        //===== modbus table 新增自動分配ID 所有資訊的(表號)的index 跟 address, Ver 1.0.03 =====
        gs_eph_comm_arrangement_1_1,
        gs_eph_comm_arrangement_1_2,
        gs_eph_comm_arrangement_1_3,
        gs_eph_comm_arrangement_1_4,
        gs_eph_comm_arrangement_1_5,
        gs_eph_comm_arrangement_1_6,
        gs_eph_comm_arrangement_1_7,
        gs_eph_comm_arrangement_1_8,
        gs_eph_comm_arrangement_1_9,

        //===== 0x304 , 9 address, Ver 0.0.02 =====
        factory_volumeBCD_dotBCD3 = (int)PFC_INFO.gs_eph_comm_arrangement_1_9 + 280,
        factory_volumeBCD_dotBCD2,
        factory_volumeBCD_dotBCD1,
        factory_volumeBCD_dotBCD0,
        factory_lDayBCD_nDayBCD,
        factory_oDayBCD_uDayBCD,
        factory_hDayBCD_bDayBCD,
        factory_userCountBCD,
        factory_statusBCD,
        //===== 0x404 , 3 address, Ver 0.0.02 =====
        factory_flowRateBCD_dotBCD2,
        factory_flowRateBCD_dotBCD1,
        factory_flowRateBCD_dotBCD0,
        //===== 0x504 , 16 address, Ver 0.0.02 =====
        factory_plusTotalFlowBCD_dotBCD3,
        factory_plusTotalFlowBCD_dotBCD2,
        factory_plusTotalFlowBCD_dotBCD1,
        factory_plusTotalFlowBCD_dotBCD0,
        factory_minusTotalFlowBCD_dotBCD3,
        factory_minusTotalFlowBCD_dotBCD2,
        factory_minusTotalFlowBCD_dotBCD1,
        factory_minusTotalFlowBCD_dotBCD0,
        factory_lDayBCD     ,
        factory_nDayBCD     ,
        factory_oDayBCD     ,
        factory_uDayBCD     ,
        factory_hDayBCD     ,
        factory_bDayBCD     ,
        factory_userCountBCD1,
        factory_statusBCD1,
    };

}
