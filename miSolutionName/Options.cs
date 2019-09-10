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
using Microsoft.VisualStudio.Threading;
using Microsoft.VisualStudio.Shell.Settings;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Settings;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft;
using Microsoft.VisualStudio.Shell.Interop;

namespace miSolutionName
{
    public class Options : DialogPage
    {
        #region Options
        private bool mPenguin = false;

        [LocalizedCategory("Experiment")]
        [DisplayName("Penguin")]
        [Description("Change Visual Studio icon to penguin.")]
        public bool Penguin
        {
            get { return mPenguin; }
            set { mPenguin = value; }
        }

        private Color mDefaultActiveForegroundColor = Color.FromRgb(0x40, 0x56, 0x8D);
        internal Color DefaultActiveForegroundColor => mDefaultActiveForegroundColor;
        [LocalizedCategory("DefaultColor")]
        [DisplayName("Active/Foreground")]
        [LocalizedDescription("ActiveForegroundDescription")]
        public string DefaultActiveForegroundColorStr
        {
            get { return Common.ConvertColor(mDefaultActiveForegroundColor); }
            set { mDefaultActiveForegroundColor = Common.ConvertColor(value, mDefaultActiveForegroundColor); }
        }

        private Color mDefaultInActiveForegroundColor = Color.FromRgb(0x66, 0x66, 0x66);
        internal Color DefaultInActiveForegroundColor => mDefaultInActiveForegroundColor;
        [LocalizedCategory("DefaultColor")]
        [DisplayName("InActive/Foreground")]
        [LocalizedDescription("InActiveForegroundDescription")]
        public string DefaultInActiveForegroundColorStr
        {
            get { return Common.ConvertColor(mDefaultInActiveForegroundColor); }
            set { mDefaultInActiveForegroundColor = Common.ConvertColor(value, mDefaultInActiveForegroundColor); }
        }

        private Color mDefaultActiveBackgroundColor = Color.FromRgb(0xD9, 0xE0, 0xF8);
        internal Color DefaultActiveBackgroundColor => mDefaultActiveBackgroundColor;
        [LocalizedCategory("DefaultColor")]
        [DisplayName("Active/Background")]
        [LocalizedDescription("ActiveBackgroundDescription")]
        public string DefaultActiveBackgroundColorStr
        {
            get { return Common.ConvertColor(mDefaultActiveBackgroundColor); }
            set { mDefaultActiveBackgroundColor = Common.ConvertColor(value, mDefaultActiveBackgroundColor); }
        }

        private Color mDefaultInActiveBackgroundColor = Color.FromRgb(0xFF, 0xFF, 0xFF);
        internal Color DefaultInActiveBackgroundColor => mDefaultInActiveBackgroundColor;
        [LocalizedCategory("DefaultColor")]
        [DisplayName("InActive/Background")]
        [LocalizedDescription("InActiveBackgroundDescription")]
        public string DefaultInActiveBackgroundColorStr
        {
            get { return Common.ConvertColor(mDefaultInActiveBackgroundColor); }
            set { mDefaultInActiveBackgroundColor = Common.ConvertColor(value, mDefaultInActiveBackgroundColor); }
        }
        #endregion


    }
}
