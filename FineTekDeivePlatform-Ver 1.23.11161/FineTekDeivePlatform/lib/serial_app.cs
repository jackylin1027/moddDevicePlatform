using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;

namespace fk_lib
{
    class serial_app
    {
        private SerialPort sp = new SerialPort();
        private ListBox cb;

        public string Port
        {
            set { sp.PortName = value; }
        }

        public SerialPort serialport
        {
            get{return sp;}
        }
        public int BaudRate
        {
            get { return sp.BaudRate; }
            set { sp.BaudRate = value; }
        }

        public serial_app()
        { 
            
        }
        public serial_app(ListBox m_cb)
        {
            string name = "";
            cb = m_cb;
            sp.BaudRate = 9600;
            for (int i = 1; i < 100; i++)
            {
                sp.Close();
                name = "com" + i.ToString();
                sp.PortName = name;
                try
                {
                    sp.Open();
                    cb.Items.Add(name);
                    sp.Close();
                }
                catch
                { }
            }
        }

        public bool open()
        {
            try
            {
                sp.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void close()
        {
            try
            {
                sp.Close();
            }
            catch
            {
            }
        }

        public bool status()
        {
            return sp.IsOpen;
        }

        public void send(byte[] m_buffer)
        {
            if( m_buffer.Length > 0 )
                sp.Write(m_buffer, 0, m_buffer.Length);
        }

        public byte[] Receiv()
        {
            byte[] m_data;
            if (sp.BytesToRead > 0)
            {
                m_data = new byte[sp.BytesToRead+100];
                sp.Read(m_data, 0, sp.BytesToRead);
                return m_data;
            }
            else
                return m_data = new byte[1];
        }
    }
}
