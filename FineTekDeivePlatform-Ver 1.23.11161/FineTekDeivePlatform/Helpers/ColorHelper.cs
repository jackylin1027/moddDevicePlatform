using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FineTekDeivePlatform.Helpers
{
    public class ColorHelper
    {
        public static Color GetRandomColor()
        {
            Random random = new Random();
            return Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
        }
    }
}
