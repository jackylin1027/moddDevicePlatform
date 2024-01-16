using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 000000-Tx:01 03 00 00 00 04 44 09

namespace fk_lib
{
    public class modbus
    {
        private SerialPort m_serial;

        public SerialPort serial_port
        {
            get { return m_serial; }
            set { m_serial = value; }
        }

        public modbus()
        {
        }

        public bool modbus_open(SerialPort m_sp)
        {
            m_serial = m_sp;
           
            try
            {
                m_serial.Open();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void modbus_close()
        {
            m_serial.Close();
        }

        public void clean_serialport_rx_buffer()
        {
            m_serial.DiscardInBuffer();
        }
        //send write value for int
        public void send_write_int(int id, UInt16 address, Int16 value)
        {
            byte[] send_buffer = new byte[8]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            Char m_char;
            
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x06;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            m_char = Convert.ToChar(value >> 8);
            send_buffer[4] = (byte)m_char;
            m_char = Convert.ToChar(value);
            send_buffer[5] = (byte)m_char;
            m_buf = ModRTU_CRC(send_buffer, 6);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[6] = m_temp[0];
            send_buffer[7] = m_temp[1];

            m_serial.Write(send_buffer, 0, 8);
        }
        //send write value for folat
        public void send_write_float(int id, UInt16 address, float value)
        {
            byte[] send_buffer = new byte[20]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            byte[] m_temp_float = new byte[4];
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x10;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = 0x00;
            send_buffer[5] = 0x02;
            send_buffer[6] = 4;
            m_temp_float = BitConverter.GetBytes(value);
            send_buffer[7] = m_temp_float[1];
            send_buffer[8] = m_temp_float[0];
            send_buffer[9] = m_temp_float[3];
            send_buffer[10] = m_temp_float[2];
            m_buf = ModRTU_CRC(send_buffer, 11);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[11] = m_temp[0];
            send_buffer[12] = m_temp[1];

            m_serial.Write(send_buffer, 0, 13);
        }
        //send write value for dodubl
        public void send_write_double(int id, UInt16 address, double value)
        {
            byte[] send_buffer = new byte[20]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            byte[] m_temp_double = new byte[8];
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x10;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = 0x00;
            send_buffer[5] = 4;
            send_buffer[6] = 8;
            m_temp_double = BitConverter.GetBytes(value);
            send_buffer[7] = m_temp_double[1];
            send_buffer[8] = m_temp_double[0];
            send_buffer[9] = m_temp_double[3];
            send_buffer[10] = m_temp_double[2];
            send_buffer[11] = m_temp_double[5];
            send_buffer[12] = m_temp_double[4];
            send_buffer[13] = m_temp_double[7];
            send_buffer[14] = m_temp_double[6];
            m_buf = ModRTU_CRC(send_buffer, 15);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[15] = m_temp[0];
            send_buffer[16] = m_temp[1];

            m_serial.Write(send_buffer, 0, 17);
        }
        //send write value for dodubl
        public void send_write_multiple(int id, UInt16 address, UInt16 word_count, byte[] byte_buf)
        {
            byte[] send_buffer = new byte[word_count * 2 + 9]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            byte[] m_temp_double = new byte[8];

            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x10;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = 0x00;
            send_buffer[5] = Convert.ToByte(word_count);
            send_buffer[6] = Convert.ToByte(word_count * 2);
            for (int i = 0; i < word_count * 2; i++)
                send_buffer[i + 7] = byte_buf[i];

            m_buf = ModRTU_CRC(send_buffer, word_count * 2 + 7);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[word_count * 2 + 7] = m_temp[0];
            send_buffer[word_count * 2 + 8] = m_temp[1];

            m_serial.Write(send_buffer, 0, word_count * 2 + 9);
            //m_serial.DiscardInBuffer();
        }
        //send write value for UInt
        public void send_write_uint(int id, UInt16 address, UInt16 value)
        {
            byte[] send_buffer = new byte[8]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            //Char m_char;
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x06;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            m_temp = BitConverter.GetBytes(value);
            //m_char = Convert.ToChar(value >> 8);
            //send_buffer[4] = (byte)m_char;
            //m_char = Convert.ToChar(value);
            //send_buffer[5] = (byte)m_char;
            send_buffer[4] = m_temp[1];
            send_buffer[5] = m_temp[0];
            m_buf = ModRTU_CRC(send_buffer, 6);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[6] = m_temp[0];
            send_buffer[7] = m_temp[1];

            m_serial.Write(send_buffer, 0, 8);
        }
        public void send_write_uint32(int id, UInt16 address, UInt32 value)
        {
            byte[] send_buffer = new byte[20]; //8
            UInt32 m_buf = 0;
            byte[] m_temp;
            byte[] m_temp_uint32 = new byte[4];
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x10;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = 0x00;
            send_buffer[5] = 0x02;
            send_buffer[6] = 4;
            m_temp_uint32 = BitConverter.GetBytes(value);
            send_buffer[7] = m_temp_uint32[1];
            send_buffer[8] = m_temp_uint32[0];
            send_buffer[9] = m_temp_uint32[3];
            send_buffer[10] = m_temp_uint32[2];
            m_buf = ModRTU_CRC(send_buffer, 11);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[11] = m_temp[0];
            send_buffer[12] = m_temp[1];

            m_serial.Write(send_buffer, 0, 13);
        }
        public void set_real_wave(int modbus_id)
        {
            byte[] send_buffer = new byte[8]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;

            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(modbus_id);
            send_buffer[1] = 0x17;
            send_buffer[2] = Convert.ToByte(0);
            send_buffer[3] = Convert.ToByte(0);
            send_buffer[4] = Convert.ToByte(0);
            send_buffer[5] = Convert.ToByte(0);

            m_buf = ModRTU_CRC(send_buffer, 6);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[6] = m_temp[0];
            send_buffer[7] = m_temp[1];

            m_serial.Write(send_buffer, 0, 8);
        }
        public void leave_real_wave(int modbus_id)
        {
            byte[] send_buffer = new byte[8]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;

            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(modbus_id);
            send_buffer[1] = 0x18;
            send_buffer[2] = Convert.ToByte(0);
            send_buffer[3] = Convert.ToByte(0);
            send_buffer[4] = Convert.ToByte(0);
            send_buffer[5] = Convert.ToByte(0);

            m_buf = ModRTU_CRC(send_buffer, 6);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[6] = m_temp[0];
            send_buffer[7] = m_temp[1];

            m_serial.Write(send_buffer, 0, 8);
        }
        public int read_real_wave(byte[] m_buffer)
        {
            int buffer_count;
            byte[] m_temp = new byte[2];

            if (m_serial.IsOpen == false)
                return 0;
            buffer_count = m_serial.BytesToRead;
            if (buffer_count > 1)
            {
                m_serial.Read(m_buffer, 0, buffer_count);
            }
            else
                return 0;
            return buffer_count;
        }
        //send modbus command
        public void send_buf(int id, UInt16 address, UInt16 count)
        {
            byte[] send_buffer = new byte[8]; //8
            UInt16 m_buf = 0;
            byte[] m_temp;
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x03;
            send_buffer[2] = Convert.ToByte(address >> 8);
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = Convert.ToByte(count >> 8);
            send_buffer[5] = Convert.ToByte(count & 0x00FF);

            m_buf = ModRTU_CRC(send_buffer, 6);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[6] = m_temp[0];
            send_buffer[7] = m_temp[1];
            
            m_serial.Write(send_buffer, 0, 8);
        }

        public void send_rs485_command(char[] product_serial, byte command)
        {
            byte[] send_buffer = new byte[10]; //8
            int groupNumber = 0x00, stationNumber = 0x00;
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte('*');
            send_buffer[1] = Convert.ToByte(product_serial[0]);
            send_buffer[2] = Convert.ToByte(product_serial[1]);
            send_buffer[3] = Convert.ToByte(product_serial[2]);
            send_buffer[4] = Convert.ToByte(groupNumber);
            send_buffer[5] = Convert.ToByte((byte)(~groupNumber));
            send_buffer[6] = Convert.ToByte(stationNumber); ;
            send_buffer[7] = Convert.ToByte((byte)(~stationNumber)); ;
            send_buffer[8] = Convert.ToByte(command);
            send_buffer[9] = Convert.ToByte((byte)(~command));

            m_serial.Write(send_buffer, 0, 10);
        }

        public void send_cht_procotol_buf(int id, UInt16 address, int timestamp)
        {
            //===== cht 中華電信protocol command 有10 Bytes, Ver 0.0.7 ===== 
            byte[] send_buffer = new byte[10]; 
            UInt16 m_buf = 0;
            byte[] m_temp;
            if (m_serial.IsOpen == false)
                return;

            send_buffer[0] = Convert.ToByte(id);
            send_buffer[1] = 0x03;
            send_buffer[2] = Convert.ToByte(address >> 8);
            //===== 修正傳給EPH的 timestamp 轉換格式, ver 0.0.7 =====
            send_buffer[3] = Convert.ToByte(address & 0x00FF);
            send_buffer[4] = Convert.ToByte(timestamp & 0x000000FF);
            send_buffer[5] = Convert.ToByte((timestamp >> 8) & 0x000000FF);
            send_buffer[6] = Convert.ToByte((timestamp >> 16) & 0x000000FF);
            send_buffer[7] = Convert.ToByte((timestamp >> 24) & 0x000000FF);
            
            m_buf = ModRTU_CRC(send_buffer, 8);
            m_temp = BitConverter.GetBytes(m_buf);
            send_buffer[8] = m_temp[0];
            send_buffer[9] = m_temp[1];

            m_serial.Write(send_buffer, 0, 10);
        }
        //receive modbus data
        public bool recelive_buf(byte[] m_buffer)
        {
            UInt16 m_crc = 0;
            int buffer_count;
            byte[] m_temp = new byte[2];

            if (m_serial.IsOpen == false)
                return false;

            buffer_count = m_serial.BytesToRead;
            if (buffer_count > 1)
            {
                m_serial.Read(m_buffer, 0, buffer_count);
                //判斷CRC
                m_crc = ModRTU_CRC(m_buffer, buffer_count - 2);
                m_temp = BitConverter.GetBytes(m_crc);
                if (m_temp[0] != m_buffer[buffer_count - 2] || m_temp[1] != m_buffer[buffer_count - 1])
                    return false;
            }
            else
                return false;

            return true;
        }

        public bool listen_buf(byte[] m_buffer)
        {
            int buffer_count;
            if (m_serial.IsOpen == false)
                return false;
            buffer_count = m_serial.BytesToRead;
            if (buffer_count > 1)
            {
                m_serial.Read(m_buffer, 0, buffer_count);
            }
            else
                return false;

            return true;
        }
        // Compute the MODBUS RTU CRC
        private UInt16 ModRTU_CRC(byte[] buf, int len)
        {
            UInt16 crc = 0xFFFF;

            for (int pos = 0; pos < len; pos++)
            {
                crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

                for (int i = 8; i != 0; i--)
                {    // Loop over each bit
                    if ((crc & 0x0001) != 0)
                    {      // If the LSB is set
                        crc >>= 1;                    // Shift right and XOR 0xA001
                        crc ^= 0xA001;
                    }
                    else                            // Else LSB is not set
                        crc >>= 1;                    // Just shift right
                }
            }
            // Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
            return crc;
        }

    }
}
