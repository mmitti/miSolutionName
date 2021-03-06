﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using miSolutionName.Extensions;

namespace miSolutionName.WindowWrapper
{
    public class VSFloatingWindowWrapper : VSWindowWrapper
    {
        public VSFloatingWindowWrapper(Window w, SettingStore settings) : base(w, settings)
        {

        }

        protected override Window TryParseWindow(Window w, SettingStore settings)
        {
            try
            {
                if (w.GetType() != Type.GetType("Microsoft.VisualStudio.PlatformUI.Shell.Controls.FloatingWindow, Microsoft.VisualStudio.Shell.ViewManager", false)) return null;
                
                var c = w.GetElement<Path>("VectorIcon");
                var title_bar = w.GetElement<DockPanel>("TitleBarContainer");
                var root_grid = title_bar.GetElement<Grid>("");
                if (new List<object> { c, title_bar, root_grid }.Any(x => x == null))
                {
                    return null;
                }
                if (settings.IsPenguin)
                {
                    var xaml = @"<PathGeometry xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""  xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""   Figures=""M 10.49977 4.4756836e-7 C 6.7261539 -0.03841458 2.8144496 1.998951 1.2229537 5.5204763 0.10829506 7.9104429 0.36520235 10.845543 1.82947 13.030553 c -0.6098233 0 -1.21964667 0 -1.82947 0 0 1.550331 0 3.100661 0 4.650992 7 0 14 0 21 0 0 -1.550331 0 -3.100661 0 -4.650992 -0.609973 0 -1.219947 0 -1.82992 0 1.729065 -2.549594 1.728303 -6.1140125 -0.02422 -8.6541028 C 17.277693 1.5363309 13.847864 -0.01889395 10.49977 4.4756836e-7 Z M 5.2686499 2.9540086 c 1.2424823 0.2019807 2.1694867 1.1722171 3.1318575 1.9100797 0.6881101 0.5073475 1.2729137 1.2419912 2.0993126 1.5105515 0.865706 -0.2810224 1.492993 -1.0316552 2.223386 -1.5524832 0.957385 -0.7046062 1.878035 -1.6415046 3.095446 -1.8300128 1.060533 0.4665045 1.7707 1.5853421 2.355768 2.5632769 1.019905 1.9091739 1.005161 4.2945056 0.171193 6.2561163 -0.282078 0.199533 0.134764 -0.681523 -0.296853 -0.641662 -0.265573 0.243417 -0.531147 0.486833 -0.79672 0.73025 0 -0.44237 0 -0.884741 0 -1.327111 -0.346145 -0.217413 -0.454951 0.363382 -0.686023 0.532974 -0.252534 0.362628 -0.591354 0.686366 -0.798418 1.06981 0.0467 0.234414 0.317612 0.0428 0.348799 -0.103937 0.298964 -0.383861 0.597928 -0.767721 0.896892 -1.151582 0 0.417049 0 0.834097 0 1.251146 0.330336 0.216384 0.536419 -0.321714 0.806975 -0.468324 0.393244 -0.461071 0.01417 0.438956 0.290497 0.512702 0.04519 0.331782 -0.304536 1.01342 -0.830166 0.814736 -4.684042 0 -9.3680841 0 -14.0521261 0 -0.2935048 -0.504835 -0.7712744 -1.092062 -0.1557598 -1.523581 0.2127389 -0.262596 0.5051968 -0.753108 0.40672 -0.125941 0.077074 0.285744 -0.1688101 0.780832 0.1503615 0.909645 0.2916946 -0.115468 0.5463218 -0.468085 0.8236816 -0.675264 0.075709 0.116753 -0.1130599 0.873299 0.302404 0.622953 0.2149764 -0.23924 0.5472393 -0.404796 0.7061439 -0.680148 C 5.4189669 11.30103 5.1320969 11.53293 5.060562 11.64876 4.7135584 12.08715 4.6643471 11.85892 4.7133902 11.410745 4.5543296 11.01528 4.1958638 11.580811 3.9850017 11.718394 3.6123384 12.21219 3.7286907 11.611558 3.7154601 11.300936 3.7206324 11.068643 3.7962534 10.401379 3.4367353 10.671967 3.1563035 11.023622 2.8758718 11.375277 2.59544 11.726932 1.7009855 9.4556718 1.865733 6.7188836 3.3799873 4.7442133 3.926431 4.120609 4.4456859 3.1891668 5.2686499 2.9540086 Z M 14.73748 5.9794595 c -0.905267 0.3333812 -0.832217 1.6145766 -0.607006 2.3950959 0.603131 1.45035 1.6634 -0.315506 1.34276 -1.1804807 -0.0567 -0.4576264 -0.176313 -1.1446453 -0.735754 -1.2146152 z m -8.4749601 0.0027 C 5.3576279 6.3154264 5.4308238 7.5965425 5.6559604 8.376804 6.2589992 9.8275923 7.3191274 8.0613741 6.9983418 7.1963201 6.9420522 6.738735 6.8224698 6.0510508 6.2625199 5.9821595 Z M 14.59144 6.2765207 c 0.263768 0.1557317 0.246988 0.6634574 0.03148 0.8609445 -0.278924 -0.047506 -0.233936 -0.4946879 -0.173602 -0.7066599 0.02494 -0.063455 0.06355 -0.1491795 0.142121 -0.1542846 z M 6.4705099 6.4013172 C 6.7342783 6.557049 6.717498 7.0647746 6.5019914 7.262262 6.2225728 7.2154611 6.2675835 6.7675342 6.3279486 6.5556017 6.3529128 6.4920727 6.3916946 6.4060088 6.4705099 6.4013172 Z M 10.49977 8.7245523 C 9.5004708 8.7912943 8.3951086 8.8713485 7.5859308 9.5228413 6.8956854 10.497201 8.4697721 10.977919 9.2031835 11.085271 10.556939 11.327694 12.061147 11.255072 13.267876 10.544643 14.27165 9.6964971 12.707078 8.9349152 11.927316 8.8727309 11.458142 8.7767031 10.979171 8.7192781 10.49977 8.7245523 Z"" FillRule=""NonZero""/>";
                    var pg = XamlReader.Parse(xaml) as PathGeometry;
                    c.Data = pg;
                }
                Border = new Border();
                Border.Margin = new Thickness(6, 3, 6, 3);
                Border.HorizontalAlignment = HorizontalAlignment.Right;
                Border.Padding = new Thickness(8, 0, 8, 0);
                Text = new TextBlock();
                Text.VerticalAlignment = VerticalAlignment.Center;
                Text.HorizontalAlignment = HorizontalAlignment.Center;
                Border.Child = Text;
                Grid.SetColumn(Border, 1);
                Panel.SetZIndex(Border, -1);
                root_grid.Children.Add(Border);

                return w;
            }
            catch
            {
                return null;
            }
        }

        public static VSFloatingWindowWrapper Create(Window w, SettingStore settings)
        {
            var r = new VSFloatingWindowWrapper(w, settings);
            if (!r.IsLoaded) return null;
            return r;
        }
    }
}
