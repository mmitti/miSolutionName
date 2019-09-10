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
}
