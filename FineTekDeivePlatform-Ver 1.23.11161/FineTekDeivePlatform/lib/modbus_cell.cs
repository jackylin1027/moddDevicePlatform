using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fk_lib
{
    class modbus_cell
    {
        public enum Value_Type { Int_type, float_type, double_type, UInt_type ,UInt32_type, fk_epht_billboard_type};
        private UInt16 m_address = 0;
        private UInt16 m_uint_value = 0;
        private float m_float_value = 0;
        private double m_double_value = 0;
        private Int16 m_int_value = 0;
        private bool m_property = false;
        private bool m_write;
        private UInt32 m_uint32_value = 0;
        private Value_Type m_type = Value_Type.Int_type;
        int m_str_len;


        public UInt16 Address
        { get { return m_address; }
          set { m_address = value; }
        }

        public int StrLen
        {
            get { return m_str_len; }
            set { m_str_len = value; }
        }
        public Int16 Int_Value
        {
            get { return m_int_value; }
            set { m_int_value = value; } //add by Jacky Lin 20181112
        }

        public UInt16 Uint_Value
        {
            get { return m_uint_value; }
            set { m_uint_value = value; } //add by Jacky Lin 20181112
        }

        public Value_Type value_type
        {
            get { return m_type; }
        }

        public double Double_Value 
        {
            get { return m_double_value; }
            set { m_double_value = value; } //add by Jacky Lin 20181112
        }

        public float Float_Value
        {
            get { return m_float_value; }
            set { m_float_value = value; } //add by Jacky Lin 20181112
        }

        public UInt32 Uint32_Value
        {
            get { return m_uint32_value; }
            set { m_uint32_value = value; } //add by Jacky Lin 20181112
        }
        public bool Read_Only
        {
            get { return m_property; }
            set { m_property = value; }
        }

        public bool Write
        {
            get { return m_write; }
            set { m_write = value; }
        }

        public modbus_cell()
        { 
        }

        public modbus_cell(UInt16 addres, UInt16 value, int str_len, bool read_only)
        {
            m_address = addres;
            m_uint_value = value;
            m_property = read_only;
            m_str_len = str_len;
            m_write = false;
            m_type = Value_Type.UInt_type;
        }

        public modbus_cell(UInt16 addres, UInt32 value, int str_len, bool read_only)
        {
            m_address = addres;
            m_uint32_value = value;
            m_property = read_only;
            m_str_len = str_len;
            m_write = false;
            m_type = Value_Type.UInt32_type;
        }
        public modbus_cell(UInt16 addres,Int16 value , int str_len, bool read_only)
        {
            m_address = addres;
            m_int_value = value;
            m_property = read_only;
            m_str_len = str_len;
            m_write = false;
            m_type = Value_Type.Int_type;
        }

        public modbus_cell(UInt16 addres, float value, int str_len, bool read_only)
        {
            m_address = addres;
            m_float_value = value;
            m_property = read_only;
            m_str_len = str_len;
            m_write = false;
            m_type = Value_Type.float_type;
        }

        public modbus_cell(UInt16 addres, double value, int str_len, bool read_only)
        {
            m_address = addres;
            m_double_value = value;
            m_property = read_only;
            m_str_len = str_len;
            m_write = false;
            m_type = Value_Type.double_type;
        }
        public bool Write_Value(Int16 value)
        {
            if (m_property == true)
                return false;
            else 
                m_int_value = value;

            return true;
        }

        public bool Write_Value(UInt16 value)
        {
            if (m_property == true)
                return false;
            else
                m_uint_value = value;

            return true;
        }

        public bool Write_Value(float value)
        {
            if (m_property == true)
                return false;
            else
                m_float_value = value;

            return true;
        }

        public bool Write_Value(UInt32 value)
        {
            if (m_property == true)
                return false;
            else
                m_uint32_value = value;

            return true;
        }
        public bool Write_Value(double value)
        {
            if (m_property == true)
                return false;
            else
                m_double_value = value;

            return true;
        }
    }
}
