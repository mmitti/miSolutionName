using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace miSolutionName
{
    public static class Common
    {
        public static Color? TryConvertColor(string hexstr)
        {
            try
            {
                return ConvertColor(hexstr);
            }
            catch
            {
            }
            return null;
        }

        public static Color ConvertColor(string hexstr)
        {
            if (string.IsNullOrWhiteSpace(hexstr)) throw new ArgumentNullException();
            var c = ColorTranslator.FromHtml(hexstr);
            return Color.FromRgb(c.R, c.G, c.B);
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
    static class LocalizedStringResource
    {
        public static string GetString(string key)
        {
            try
            {
                return Properties.StringResource.ResourceManager.GetString(key) ?? key;
            }
            catch { }
            return key;
        }
    }
    class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private readonly string mDisplayName;
        public LocalizedDisplayNameAttribute(string displayName) : base()
        {
            mDisplayName = displayName;
        }

        public override string DisplayName => LocalizedStringResource.GetString(mDisplayName);
    }
    class LocalizedCategoryAttribute : CategoryAttribute
    {
        private string mCategory;
        public LocalizedCategoryAttribute(string category) : base(category)
        {
            mCategory = category;
        }
        protected override string GetLocalizedString(string value)
         => LocalizedStringResource.GetString(mCategory);
    }
    class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private string mDescription;
        public LocalizedDescriptionAttribute(string description) : base()
        {
            mDescription = description;
        }
        public override string Description => LocalizedStringResource.GetString(mDescription);
    }

}
