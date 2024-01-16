using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fk_lib;

namespace FineTekDeivePlatform
{
    class fn
    {
        public void stringTobyte(List<modbus_cell> data,int data_end_ptr, string content)
        {
            Int16 mINT16_value;
            int i;

            char[] mBuffer = content.ToCharArray();
            char[] mTmpBuffer = new char[48];
            int num = 0;
            int end2start_len = data[data_end_ptr].StrLen - 1;
            for (i = 0; i <= end2start_len; i++)
                data[data_end_ptr - end2start_len + i].Int_Value = Convert.ToInt16('\0');
            foreach (char ch in mBuffer)
            {
                mTmpBuffer[num++] = ch;
            }
            int len = mBuffer.Length;
            if (len % 2 == 1)
                len += 1;
            if (len >= (end2start_len+1)*2)
                len = (end2start_len + 1) * 2;
            for (i = 0; i < len / 2; i++)
            {
                mINT16_value = Convert.ToInt16(mTmpBuffer[1 + i * 2]);
                mINT16_value <<= 8;
                mINT16_value |= Convert.ToInt16(mTmpBuffer[0 + i * 2]);

                data[data_end_ptr - end2start_len + i].Int_Value = mINT16_value;
            }
        }
        public string byteTostring(List<modbus_cell> data, int ptr, int num)
        {
            string mString = "";
            for (int i = 0; i <= num; i++)
            {
                mString += Convert.ToChar((data[ptr + i]).Int_Value & 0xFF);
                mString += Convert.ToChar(((data[ptr + i]).Int_Value & 0xFF00) >> 8);
            }
            return mString;
        }
        public string byteToHexstring(List<modbus_cell> data, int ptr, int num)
        {
            string mString = "";
            for (int i = 0; i <= num; i++)
            {
                mString += string.Format("{0:X2}",((data[ptr + i]).Uint_Value & 0xFF));
                mString += string.Format("{0:X2}", (((data[ptr + i]).Uint_Value & 0xFF00) >> 8));
            }
            return mString;
        }
        public string byteToHexstring(List<UInt16> data, int ptr, int num)
        {
            string mString = "";
            for (int i = 0; i <= num; i++)
            {
                mString += string.Format("{0:X2}", ((data[ptr + i]) & 0xFF));
                mString += string.Format("{0:X2}", (((data[ptr + i]) & 0xFF00) >> 8));
            }
            return mString;
        }
        public float HexToFloat(UInt16 hexValue)
        {
            byte[] bytes = BitConverter.GetBytes(hexValue);
            if (BitConverter.IsLittleEndian)
            {
                bytes = bytes.Reverse().ToArray();
            }
            float myFloat = BitConverter.ToSingle(bytes, 1);
            return myFloat;
        }
        public UInt64 HexToBCD(UInt64 hex, int length)
        {
            int i;
            UInt64 bcd =0;
            UInt64 temp = hex;

            for (i = 0; i < length; i++)
            {
                bcd += (UInt64)((temp & 0xF) * Math.Pow(10, i));
                temp >>= 4;
            }
            return bcd;
        }
        public int BCDToDec(int bcd)
        {
            int dec = 0;
            int mult;
            for (mult = 1; bcd>0 ; bcd = bcd >> 4, mult *= 10)
                dec += (bcd & 0x0f) * mult;
            return dec;
        }

        public UInt64 AddBCDValue_3Bytes(List<modbus_cell> data, int ptr)
        {
            UInt64 HighValue = ((UInt64)data[ptr + 0].Uint_Value) << 32;
            UInt64 MiddleValue = ((UInt64)data[ptr + 1].Uint_Value) << 16;
            UInt64 LowValue = ((UInt64)data[ptr + 2].Uint_Value);
            UInt64 sum = HighValue + MiddleValue + LowValue;
            return sum;
        }
        public UInt64 AddBCDValue_3Bytes(List<UInt16> data, int ptr)
        {
            UInt64 HighValue = ((UInt64)data[ptr + 0]) << 32;
            UInt64 MiddleValue = ((UInt64)data[ptr + 1]) << 16;
            UInt64 LowValue = ((UInt64)data[ptr + 2]);
            UInt64 sum = HighValue + MiddleValue + LowValue;
            return sum;
        }
        public UInt32 AddBCDValue_2Bytes(List<modbus_cell> data, int ptr)
        {
            UInt32 MiddleValue = ((UInt32)data[ptr + 0].Uint_Value) << 16;
            UInt32 LowValue = ((UInt32)data[ptr + 1].Uint_Value);
            UInt32 sum = MiddleValue + LowValue;
            return sum;
        }
        public UInt32 AddBCDValue_2Bytes(List<UInt16> data, int ptr)
        {
            UInt32 MiddleValue = ((UInt32)data[ptr + 0]) << 16;
            UInt32 LowValue = ((UInt32)data[ptr + 1]);
            UInt32 sum = MiddleValue + LowValue;
            return sum;
        }
        public string ReverseByArray(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
