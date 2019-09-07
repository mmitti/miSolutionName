using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell;
using System.Windows.Media;
using System.Drawing;
using Color = System.Windows.Media.Color;

namespace WindowColor
{
    public class Options : DialogPage
    {
        private bool mPenguin = true;

        [Category("Experiment")]
        [DisplayName("Penguin")]
        [Description("Change Visual Studio icon to penguin.")]
        public bool Penguin
        {
            get { return mPenguin; }
            set { mPenguin = value; }
        }

        private Color mDefaultActiveForegroundColor = Color.FromRgb(0x40, 0x56, 0x8D);
        internal Color DefaultActiveForegroundColor => mDefaultActiveForegroundColor;
        [Category("DefaultColor")]
        [DisplayName("Active/Foreground")]
        [Description("Solution name text Color(for window active). Default Value:#40568d")]
        public string DefaultActiveForegroundColorStr
        {
            get { return ConvertColor(mDefaultActiveForegroundColor); }
            set { mDefaultActiveForegroundColor = ConvertColor(value, mDefaultActiveForegroundColor); }
        }

        private Color mDefaultInActiveForegroundColor = Color.FromRgb(0x66, 0x66, 0x66);
        internal Color DefaultInActiveForegroundColor => mDefaultInActiveForegroundColor;
        [Category("DefaultColor")]
        [DisplayName("InActive/Foreground")]
        [Description("Solution name text Color(for window inactive). Default Value:#666666")]
        public string DefaultInActiveForegroundColorStr
        {
            get { return ConvertColor(mDefaultInActiveForegroundColor); }
            set { mDefaultInActiveForegroundColor = ConvertColor(value, mDefaultInActiveForegroundColor); }
        }

        private Color mDefaultActiveBackgroundColor = Color.FromRgb(0xD9, 0xE0, 0xF8);
        internal Color DefaultActiveBackgroundColor => mDefaultActiveBackgroundColor;
        [Category("DefaultColor")]
        [DisplayName("Active/Background")]
        [Description("Solution name bar Color(for window active). Default Value:#d9e0f8")]
        public string DefaultActiveBackgroundColorStr
        {
            get { return ConvertColor(mDefaultActiveBackgroundColor); }
            set { mDefaultActiveBackgroundColor = ConvertColor(value, mDefaultActiveBackgroundColor); }
        }

        private Color mDefaultInActiveBackgroundColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
        internal Color DefaultInActiveBackgroundColor => mDefaultInActiveBackgroundColor;
        [Category("DefaultColor")]
        [DisplayName("InActive/Background")]
        [Description("Solution name bar Color(for window inactive). Default Value:#ffffff")]
        public string DefaultInActiveBackgroundColorStr
        {
            get { return ConvertColor(mDefaultInActiveBackgroundColor); }
            set { mDefaultInActiveBackgroundColor = ConvertColor(value, mDefaultInActiveBackgroundColor); }
        }


        private Color ConvertColor(string hexstr, Color current)
        {
            try
            {
                var c = ColorTranslator.FromHtml(hexstr);
                return Color.FromRgb(c.R, c.G, c.B);
            }
            catch
            {
            }
            return current;
        }
        private string ConvertColor(Color current)
        {
            return $"#{current.R:X2}{current.G:X2}{current.B:X2}";
        }

    }
}
