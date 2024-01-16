using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fk_lib
{
    class write_obj
    {
        private int m_address;
        private UInt16 m_value;

        public int Address
        { get { return m_address; } }

        public UInt16 Value
        { get { return m_value; }
          set { m_value = value; }
        }

        public write_obj(int address, UInt16 value)
        {
            m_address = address;
            m_value = value;
        }
    }
}
