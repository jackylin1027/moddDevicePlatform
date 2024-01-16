using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FineTekDeivePlatform
{
    class alogrithm
    {
        public int Min(int[] array)
        {
            if (array == null) throw new Exception("數組空異常");
            int value = 0;
            bool hasValue = false;
            foreach (int x in array)
            {
                if (hasValue)
                {
                    if (x < value) value = x;
                }
                else
                {
                    value = x;
                    hasValue = true;
                }
            }
            if (hasValue) return value;
            throw new Exception("沒找到");
        }

        public int Max(int[] array)
        {
            if (array == null) throw new Exception("數組空異常");
            int value = 0;
            bool hasValue = false;
            foreach (int x in array)
            {
                if (hasValue)
                {
                    if (x > value)
                        value = x;
                }
                else
                {
                    value = x;
                    hasValue = true;
                }
            }
            if (hasValue) return value;
            throw new Exception("沒找到");
        }
    }
}
