using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace miSolutionName.Extensions
{

    static class UIElementExtensions
    {
        public static T GetElement<T>(this UIElement root, string name = null, int max_depth = int.MaxValue) where T : class
        {
            DependencyObject find(DependencyObject r, int depth)
            {
                if (depth == 0) return null;
                var c = VisualTreeHelper.GetChildrenCount(r);
                for (int i = 0; i < c; ++i)
                {
                    var e = VisualTreeHelper.GetChild(r, i);
                    if (e is T)
                    {
                        if (name == null || (e as FrameworkElement).Name == name)
                            return e;
                    }
                    e = find(e, depth - 1);
                    if (e != null) return e;
                }
                return null;
            }

            return find(root, max_depth) as T;
        }

    }


    static class ColorExtensions
    {
        /// <summary>
        /// http://24ways.org/2010/calculating-color-contrast
        /// </summary>
        public static uint GetYIQ(this Color color)
        {
            return (uint)(color.R * 299 + color.G * 587 + color.B * 114) / 1000;
        }
        public static bool IsDark(this Color color)
        {
            return color.GetYIQ() < 128;
        }
        public static bool IsLight(this Color color)
        {
            return color.GetYIQ() >= 128;
        }

        /// <summary>
        /// Alpha blend. 
        /// </summary>
        public static Color Blend(this Color color, Color background, byte? alpha = null)
        {
            
            byte a = background.A;
            if (alpha.HasValue) a = alpha.Value;

            byte blend(byte f, byte b)
            {
                float c = ((float)f * a + b * (0xFF - a)) / 0xFF;
                return (byte)(c > 0xFF ? 0xFF : c < 0 ? 0: c);
            }

            var ret = new Color();
            ret.A = 0xFF;
            ret.R = blend(color.R, background.R);
            ret.G = blend(color.G, background.G);
            ret.B = blend(color.B, background.B);
            return ret;
        }
    }
}
