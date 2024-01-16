using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using fk_lib;
using System.Windows;
using static FineTekDeivePlatform.XMLparser;
using FineTekDeivePlatform.component.specialmodel;

namespace FineTekDeivePlatform
{
    class modbus_table
    {
        public class modbusinfo
        {
            public int x_address { get; set; }
            public string x_name { get; set; }
            public string secondx_name { get; set; }
            public string x_type { get; set; }
            public int x_str_len { get; set; }
        }
        public class ResponseAssociate
        {
            public Object obj{get;set;}
            public int relevantAddr { get; set; }
            public string name { get; set; }
            public List<comboboxMapping> combobox_mapping { get; set; }
        }
        private List<ResponseAssociate> RspsAsciate = new List<ResponseAssociate>();
        private modbus_cell m_cell;
        private List<modbus_cell> List_cell = new List<modbus_cell>();
        private List<modbus_cell> Write_List_cell = new List<modbus_cell>();
        private UInt16 modbus_start = 4128; //modbus 起始位置
        private UInt16 count = 0;

        private float display_value = 0;
        private UInt16 adc_value = 0;
        private float m_percentage = 0;

        public List<modbus_cell> Modbus_List
        { get { return List_cell; } }
        public List<ResponseAssociate> Modbus_Response
        { get { return RspsAsciate; } }
        public UInt16 Modbus_address
        { get { return modbus_start; } }

        public UInt16 Modbus_count
        { get { return count; } }

        public float Percentage
        { get { return m_percentage; } }

        //===========產品modbus表===========
        //依照產品屬性填入modbus_cell
        public modbus_table()
        {
           // int i = 0;
           // UInt16 address = modbus_start;
           // double m_double = 0;
           // float m_float = 0;
           // Int16 m_int = 0;
           // UInt16 m_uint = 0;
           // UInt32 m_uint32 = 0;
           //
           // //modbus table建立 4128
           // #region //========= EPH ID 1 的 ReadOnly ==========
          //  //===== gs_eph_comm_transmitter[0] 4128 ~ 5407(40 * Contanst.MAX_DEVICE_NUM) =====
          //  for (i = 0; i < 40 * Contanst.MAX_DEVICE_NUM; i++)
          //  {
          //      count += 1;
          //      m_cell = new modbus_cell(address, m_uint, false);
          //      List_cell.Add(m_cell);
          //      address += 1;
          //  }
          //  #endregion
           //
           // //===== readwrite zone 5408 ~ 5501(94) =====
           // for (i = 0; i < 94; i++)
           // { 
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell); 
           //     address += 1;       
           // }
           // //===== 分配ID第 1-8 組 readwrite zone 5502 ~ 5573(72) =====
           // for (i = 0; i < 72; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           // //===== 分配ID第 9-16 組 readwrite zone 5574 ~ 5645(72) =====
           // for (i = 0; i < 72; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           // //===== 分配ID第 17-24 組 readwrite zone 5646 ~ 5717(72) =====
           // for (i = 0; i < 72; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           // //===== 分配ID第 25-32 組 readwrite zone 5718 ~ 5789(72) =====
           // for (i = 0; i < 72; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           //
           // //===== 新增水廠模式的modbus table, 共有 0x304, 0x404, 0x504三段, Ver 0.0.02 =====
           // //===== factory zone 0x304 (9) =====
           // address = 0x304;
           // for (i = 0; i < 9; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           // //===== factory zone 0x404 (3) =====
           // address = 0x404;
           // for (i = 0; i < 3; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
           // //===== factory zone 0x504 (16) =====
           // address = 0x504;
           // for (i = 0; i < 16; i++)
           // {
           //     count += 1;
           //     m_cell = new modbus_cell(address, m_uint, false);
           //     List_cell.Add(m_cell);
           //     address += 1;
           // }
        }
        public modbus_table(List<cbObj> objList, List<modbusinfo> infoList)
        {
            var data_type = 0;
            object object_type = null;
            count = 0;
            modbus_start = (UInt16)infoList[0].x_address;
            for (int i = 0; i < infoList.Count; i++)
            {
                switch (infoList[i].x_type)
                {
                    case "INT16":
                        m_cell = new modbus_cell((ushort)infoList[i].x_address, (Int16)data_type, infoList[i].x_str_len,  false);
                        count += 1;
                        break;
                    //====== 專門給 EPH0019/EPH0022使用的類別，代表傳訊器的一台 device 的 object =====
                    case "UINT16":
                    
                        m_cell = new modbus_cell((ushort)infoList[i].x_address, (UInt16)data_type, infoList[i].x_str_len, false);
                        count += 1;
                        break;
                    case "FLOAT32":
                        m_cell = new modbus_cell((ushort)infoList[i].x_address, (float)data_type, infoList[i].x_str_len, false);
                        count += 2;
                        break;
                    case "INT32":
                    case "UINT32":
                        m_cell = new modbus_cell((ushort)infoList[i].x_address, (UInt32)data_type, infoList[i].x_str_len, false);
                        count += 2;
                        break;
                    case "FLOAT64":
                        m_cell = new modbus_cell((ushort)infoList[i].x_address, (double)data_type, infoList[i].x_str_len, false);
                        count += 4;
                        break;
                    //====== 專門給 EPH0019/EPH0022使用的類別，代表傳訊器的一台 device 的 object =====
                    case "EPHT_T":
                        int accessary_address;
                        for ( accessary_address = 0; accessary_address < 40 - 1; accessary_address++)
                        {
                            m_cell = new modbus_cell((ushort)(infoList[i].x_address+ accessary_address), (UInt16)data_type, infoList[i].x_str_len, false);
                            count += 1;
                            List_cell.Add(m_cell);
                        }
                        m_cell = new modbus_cell((ushort)(infoList[i].x_address + accessary_address), (UInt16)data_type, infoList[i].x_str_len, false);
                        count += 1;
                        break;
                    case "INT64":
                    case "UINT64":
                        MessageBox.Show("不支援INT64、UINT64", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                        // m_cell = new modbus_cell((ushort)infoList[i].x_address, (UInt64)data_type, false);
                        //total_cnt += 4;
                        break;
                }
                //===== XML的輸出跟UI的物件綁定 =====
                for (int j = 0; j < objList.Count; j++)
                {
                    //====== infoList[i].x_name【modbus table 上的 component name】 , objList[j].name【MBDD動態產生的 component.Name】 =====
                    if (infoList[i].x_name == objList[j].name && infoList[i].x_name!="")
                    {
                        RspsAsciate.Add(new ResponseAssociate { obj = objList[j].component, name= infoList[i].x_name, relevantAddr = infoList[i].x_address, combobox_mapping= objList[j].cbObjMappingList });
                        //XML.cbObjList[i].mapping(itemsMapping);
                        //if (XML.cbObjList[i].name == id)
                    }
                    if(infoList[i].secondx_name == objList[j].name && infoList[i].secondx_name != "")
                    {
                        RspsAsciate.Add(new ResponseAssociate { obj = objList[j].component, name = infoList[i].secondx_name, relevantAddr = infoList[i].x_address, combobox_mapping = objList[j].cbObjMappingList });
                    }
                }

                List_cell.Add(m_cell);
            }
        }


        //=============public API function======
        //send data by address
        public bool get_data_by_address(modbus m_modbus, int modbus_id, UInt16 address)
        {
            byte[] m_byte_temp = new byte[50];
            UInt16 read_count = 0;

            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address == address)
                {
                    switch (m_obj.value_type)
                    {
                        case modbus_cell.Value_Type.Int_type:
                        case modbus_cell.Value_Type.UInt_type:
                            read_count = 1;
                            break;
                        case modbus_cell.Value_Type.float_type:
                            read_count = 2;
                            break;
                        case modbus_cell.Value_Type.double_type:
                            read_count = 4;
                            break;
                    }
                    break;
                }
            }

            if (read_count == 0)
                return false;

            m_modbus.send_buf(modbus_id, address, read_count);
            return true;
        }
        //===== 非常重要, 修改 range --> address_word_cnt, 輸入的 address_word_cnt 都使用 modbus address的單位輛【UINT16】
        public bool write_data_by_word_count(modbus m_modbus, int modbus_id, UInt16 address, UInt16 address_word_cnt)
        {
            byte[] write_byte_data = new byte[256];
            bool write_success=false;
            byte[] m_byte_temp = new byte[1024];

            write_byte_data = encode_data_by_word_count(m_modbus, address, address_word_cnt);
            m_modbus.send_write_multiple(modbus_id, address, address_word_cnt, write_byte_data);
            Thread.Sleep(600);
            m_modbus.recelive_buf(m_byte_temp);
            write_success = true;
            return write_success;
        }
        //read data by address
        public bool updata_by_address(modbus m_modbus, UInt16 address)
        {
            byte[] m_byte_temp = new byte[1024];
            if (m_modbus.recelive_buf(m_byte_temp))
                decode_by_address(address, m_byte_temp);
            else
                return false;

            return true;
        }

        public bool listen_by_serialPort(modbus m_modbus, byte[] m_buffer)
        {
            return m_modbus.listen_buf(m_buffer);
        }
        //send data by address range
        //===== 中華電信通訊模式切回標準modbus 通訊模式的command, Ver 0.0.7 =====
        public void send_cht_change_commulication_mode_command(modbus m_modbus, int modbus_id, int timestamp)
        {
            m_modbus.send_cht_procotol_buf(modbus_id, 0x084B, timestamp);
        }
        //===== 非常重要, 修改 range --> address_word_cnt, 輸入的 address_word_cnt 都使用 modbus address的單位輛【UINT16】
        public bool get_data_by_word_count(modbus m_modbus, int modbus_id, UInt16 address, UInt16 address_word_cnt)
        {
            byte[] m_byte_temp = new byte[1024];
            UInt16 read_count = 0;
            bool address_start = false;

            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address == address || address_start == true)
                {
                    address_start = true;
                    if (read_count >= address_word_cnt)
                        break;
                    switch (m_obj.value_type)
                    {
                        case modbus_cell.Value_Type.Int_type:
                        case modbus_cell.Value_Type.UInt_type:
                            read_count += 1;
                            break;
                        case modbus_cell.Value_Type.float_type:
                            read_count += 2;
                            break;
                        case modbus_cell.Value_Type.double_type:
                            read_count += 4;
                            break;
                        case modbus_cell.Value_Type.UInt32_type:
                            read_count += 2;
                            break;
                    }
                }
            }

            if (read_count == 0)
                return false;

            m_modbus.send_buf(modbus_id, address, read_count);
            return true;
        }
        //===== 非常重要, 修改 range --> address_word_cnt, 輸入的 address_word_cnt 都使用 modbus address的單位輛【UINT16】
        public bool updata_by_word_count(modbus m_modbus, UInt16 address, UInt16 address_word_cnt)
        {
            byte[] m_byte_temp = new byte[1024];
            if (m_modbus.recelive_buf(m_byte_temp))
                decode_by_word_count(address, address_word_cnt, m_byte_temp);
            else
                return false;

            return true;
        }
        //send all data
        public bool get_all(modbus m_modbus, int modbus_id)
        {
            byte[] m_byte_temp = new byte[1024];

            m_modbus.send_buf(modbus_id, modbus_start, count);
            Thread.Sleep(1500);
            if (m_modbus.recelive_buf(m_byte_temp))
                decode(m_byte_temp);
            else
                return false;

            return true;
        }
        //write value
        public bool write_to_value(modbus m_modbus, int modbus_id, UInt16 address)
        {
            byte[] m_byte_temp = new byte[1024];

            foreach (modbus_cell m_cell in List_cell)
            {
                if (m_cell.Address == address && m_cell.Write == true)
                {
                    switch (m_cell.value_type)
                    {
                        case modbus_cell.Value_Type.Int_type:
                            m_modbus.send_write_int(modbus_id, address, m_cell.Int_Value);
                            m_cell.Write = false;
                            break;
                        case modbus_cell.Value_Type.float_type:
                            m_modbus.send_write_float(modbus_id, address, m_cell.Float_Value);
                            m_cell.Write = false;
                            break;
                        case modbus_cell.Value_Type.double_type:
                            m_modbus.send_write_double(modbus_id, address, m_cell.Double_Value);
                            m_cell.Write = false;
                            break;
                        case modbus_cell.Value_Type.UInt_type:
                            m_modbus.send_write_uint(modbus_id, address, m_cell.Uint_Value);
                            m_cell.Write = false;
                            break;
                        case modbus_cell.Value_Type.UInt32_type:
                            m_modbus.send_write_uint32(modbus_id, address, m_cell.Uint32_Value);
                            m_cell.Write = false;
                            break;

                    }
                    Thread.Sleep(300);
                    m_modbus.recelive_buf(m_byte_temp);
                    return true;
                }
            }

            return false;
        }
        //==============function=================
        private Int16 decode_int(Byte[] m_data_int)
        {
            Int16 value = 0;

            value = m_data_int[0]; //High Byte
            value <<= 8;
            value += m_data_int[1]; //Lo Byte

            return value;
        }

        private UInt16 decode_Uint(Byte[] m_data_int)
        {
            UInt16 value = 0;

            value = m_data_int[0]; //High Byte
            value <<= 8;
            value += m_data_int[1]; //Lo Byte

            return value;
        }

        private UInt32 decode_Uint32(Byte[] m_data_int)
        {
            UInt32 value = 0;

            value = m_data_int[2]; //High Byte
            value <<= 8;
            value += m_data_int[3]; //Lo Byte
            value <<= 8;
            value += m_data_int[0]; //Lo Byte
            value <<= 8;
            value += m_data_int[1]; //Lo Byte
            
            return value;
        }

        private float decode_float(Byte[] m_data_float)
        {
            byte[] m_temp = new byte[4];
            byte m_buffer;
            float value = 0;

            for (int i = 0; i < 4; i++)
                m_temp[i] = m_data_float[i];

            //swap

            m_buffer = m_temp[0];
            m_temp[0] = m_temp[1];
            m_temp[1] = m_buffer;
            m_buffer = m_temp[2];
            m_temp[2] = m_temp[3];
            m_temp[3] = m_buffer;

            //byte to float
            value = BitConverter.ToSingle(m_temp, 0);
            return value;
        }

        private double decode_double(byte[] m_data_double)
        {
            byte[] m_temp = new byte[8];
            byte[] m_buffer = new byte[2];
            double value = 0;

            for (int i = 0; i < 8; i++)
                m_temp[i] = m_data_double[i];

            //swap
            m_buffer[0] = m_temp[1];
            m_buffer[1] = m_temp[0];
            m_temp[0] = m_buffer[0];
            m_temp[1] = m_buffer[1];

            m_buffer[0] = m_temp[3];
            m_buffer[1] = m_temp[2];
            m_temp[2] = m_buffer[0];
            m_temp[3] = m_buffer[1];

            m_buffer[0] = m_temp[5];
            m_buffer[1] = m_temp[4];
            m_temp[4] = m_buffer[0];
            m_temp[5] = m_buffer[1];

            m_buffer[0] = m_temp[7];
            m_buffer[1] = m_temp[6];
            m_temp[6] = m_buffer[0];
            m_temp[7] = m_buffer[1];
            //byte to float
            value = BitConverter.ToDouble(m_temp, 0);
            return value;
        }

        private void decode(byte[] m_data)
        {
            Int16 m_data_point = 3;
            UInt16 address = modbus_start;
            byte[] for_double = new byte[8];
            byte[] for_flaot = new byte[4];
            byte[] for_int = new byte[2];

            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address == address)
                {
                    switch (m_obj.value_type)
                    {
                        case modbus_cell.Value_Type.Int_type:
                            {
                                for (int i = 0; i < 2; )
                                    for_int[i++] = m_data[m_data_point++];

                                m_obj.Write_Value(decode_int(for_int));
                                address += 1;
                                break;
                            }
                        case modbus_cell.Value_Type.float_type:
                            {
                                for (int i = 0; i < 4; )
                                    for_flaot[i++] = m_data[m_data_point++];

                                m_obj.Write_Value(decode_float(for_flaot));
                                address += 2;
                                break;
                            }
                        case modbus_cell.Value_Type.double_type:
                            {
                                for (int i = 0; i < 8; )
                                    for_double[i++] = m_data[m_data_point++];

                                m_obj.Write_Value(decode_double(for_double));
                                address += 4;
                                break;
                            }
                    }

                }
            }

        }

        private void decode_by_address(UInt16 address, byte[] m_data)
        {
            Int16 m_data_point = 3;
            byte[] for_double = new byte[8];
            byte[] for_flaot = new byte[4];
            byte[] for_int = new byte[2];

            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address == address)
                {
                    write_obj(m_obj, m_data_point, m_data);
                    break;
                }
            }
        }
        //====== 非常重要 修改 cell_count --> address_count, 這裡的 address_count 代表的就是 要寫入 modbus address 的長度【UINT16】=====
        private void decode_by_word_count(UInt16 address, UInt16 address_count, byte[] m_data)
        {
            Int16 m_data_point = 3;
            Int16 counter = 0;
            Int16 countTemp = m_data_point;
            bool address_start = false;
            // byte[] for_double = new byte[8];
            // byte[] for_flaot = new byte[4];
            // byte[] for_int = new byte[2];

            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address == address || address_start == true)
                {
                    if (counter >= address_count)
                        return;
                    address_start = true;
                    m_data_point = write_obj(m_obj, m_data_point, m_data);
                    counter += (short)((m_data_point- countTemp)/2);
                    countTemp = m_data_point;
                }
            }
        }

        private Int16 write_obj(modbus_cell m_cell, Int16 m_data_point, byte[] m_data)
        {
            byte[] for_double = new byte[8];
            byte[] for_flaot = new byte[4];
            byte[] for_int = new byte[2];
            byte[] for_int32 = new byte[4];

            switch (m_cell.value_type)
            {
                case modbus_cell.Value_Type.Int_type:
                    {
                        for (int i = 0; i < 2; )
                            for_int[i++] = m_data[m_data_point++];

                        m_cell.Write_Value(decode_int(for_int));
                        break;
                    }
                case modbus_cell.Value_Type.float_type:
                    {
                        for (int i = 0; i < 4; )
                            for_flaot[i++] = m_data[m_data_point++];

                        m_cell.Write_Value(decode_float(for_flaot));
                        break;
                    }
                case modbus_cell.Value_Type.double_type:
                    {
                        for (int i = 0; i < 8; )
                            for_double[i++] = m_data[m_data_point++];

                        m_cell.Write_Value(decode_double(for_double));
                        break;
                    }
                case modbus_cell.Value_Type.UInt_type:
                    {
                        for (int i = 0; i < 2; )
                            for_int[i++] = m_data[m_data_point++];

                        m_cell.Write_Value(decode_Uint(for_int));
                        break;
                    }
                case modbus_cell.Value_Type.UInt32_type:
                    {
                        for (int i = 0; i < 4;)
                            for_int32[i++] = m_data[m_data_point++];

                        m_cell.Write_Value(decode_Uint32(for_int32));
                        break;
                    }

            }
            return m_data_point;
        }
        //====== 非常重要, 這裡的 address_count 代表的就是 要寫入 modbus address 的長度【UINT16】=====
        private byte[] encode_data_by_word_count(modbus m_modbus, UInt16 address, UInt16 address_count)
        {
            int world_count = 0, byte_count = 0;
            byte[] encode_byte = new byte[4];
            byte[] tmp_encode_byte = new byte[256];
           
            foreach (modbus_cell m_obj in List_cell)
            {
                if (m_obj.Address >= address && world_count < address_count)
                {
                    switch (m_obj.value_type)
                    {
                        case modbus_cell.Value_Type.double_type:
                            encode_byte = BitConverter.GetBytes(m_obj.Double_Value);
                            tmp_encode_byte[byte_count++] = encode_byte[1];
                            tmp_encode_byte[byte_count++] = encode_byte[0];
                            tmp_encode_byte[byte_count++] = encode_byte[3];
                            tmp_encode_byte[byte_count++] = encode_byte[2];
                            tmp_encode_byte[byte_count++] = encode_byte[5];
                            tmp_encode_byte[byte_count++] = encode_byte[4];
                            tmp_encode_byte[byte_count++] = encode_byte[7];
                            tmp_encode_byte[byte_count++] = encode_byte[6];
                            world_count += 4;
                            break;
                        case modbus_cell.Value_Type.float_type:
                            encode_byte = BitConverter.GetBytes(m_obj.Float_Value);
                            tmp_encode_byte[byte_count++] = encode_byte[1];
                            tmp_encode_byte[byte_count++] = encode_byte[0];
                            tmp_encode_byte[byte_count++] = encode_byte[3];
                            tmp_encode_byte[byte_count++] = encode_byte[2];
                            world_count += 2;
                            break;
                        case modbus_cell.Value_Type.Int_type:
                            encode_byte = BitConverter.GetBytes(m_obj.Int_Value);
                            tmp_encode_byte[byte_count++] = encode_byte[1];
                            tmp_encode_byte[byte_count++] = encode_byte[0];
                            world_count += 1;
                            break;
                        case modbus_cell.Value_Type.UInt_type:
                            encode_byte = BitConverter.GetBytes(m_obj.Uint_Value);
                            tmp_encode_byte[byte_count++] = encode_byte[1];
                            tmp_encode_byte[byte_count++] = encode_byte[0];
                            world_count += 1;
                            break;
                    }
                }
            }
            return tmp_encode_byte;
        }
    }
}
