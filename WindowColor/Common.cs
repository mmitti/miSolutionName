using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace WindowColor
{
    public static class Common
    {
        public static Color? TryConvertColor(string hexstr)
        {
            if (string.IsNullOrWhiteSpace(hexstr)) return null;
            try
            {
                var c = ColorTranslator.FromHtml(hexstr);
                return Color.FromRgb(c.R, c.G, c.B);
            }
            catch
            {
            }
            return null;
        }

        public static Color ConvertColor(string hexstr, Color current)
        {
            return TryConvertColor(hexstr) ?? current;
        }

        public static bool CanConvertColor(string hexstr)
        {
            return TryConvertColor(hexstr).HasValue;
        }
        
        public static string ConvertColor(Color current)
        {
            return $"#{current.R:X2}{current.G:X2}{current.B:X2}";
        }
    }
}
