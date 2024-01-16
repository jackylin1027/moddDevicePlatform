using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FineTekDeivePlatform
{
    class address_zone
    {
        private int m_start;
        private int m_lenght;
        public int Start
        {
            set { m_start = value; }
            get { return m_start; }
        }
        public int Length
        {
            set { m_lenght = value; }
            get { return m_lenght; }
        }
        public address_zone() { }

        public address_zone(int start, int length)
        {
            m_lenght = length;
            m_start = start;
        }
    }
}
