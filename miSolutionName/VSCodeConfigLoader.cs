using miSolutionName.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace miSolutionName
{
    public class VSCodeConfigLoader
    {
        public bool IsLoaded { get; private set; }
        public string ConfigFilePath { get; private set; }
        public Color? ActiveBackground { get; private set; }
        public Color? ActiveForeground { get; private set; }
        public Color? InActiveBackground { get; private set; }
        public Color? InActiveForeground { get; private set; }
        public VSCodeConfigLoader(string directory)
        {
            foreach(var p in Directory.EnumerateFiles(directory, "*.code-workspace", SearchOption.TopDirectoryOnly))
            {
                IsLoaded = TryLoadWorkspace(p);
                if (IsLoaded) return;
            }
            IsLoaded = TryLoadVSConfig(Path.Combine(directory, ".vscode", "settings.json"));
        }

        private bool TryLoadVSColorCustomization(JToken root)
        {
            try
            {
                var customize = root["workbench.colorCustomizations"];
                var back = Common.ConvertColor(customize["titleBar.activeBackground"].ToObject<string>());
                var fore = Common.ConvertColor(customize["titleBar.activeForeground"].ToObject<string>());
                var iback = Common.TryConvertColor(customize["titleBar.inactiveBackground"]?.ToObject<string>());
                var ifore = Common.TryConvertColor(customize["titleBar.inactiveForeground"]?.ToObject<string>());
                Color default_backgroud = back.IsDark() ? Color.FromArgb(0x99, 0x25, 0x25, 0x25) : Color.FromArgb(0x99, 0xF3, 0xF3, 0xF3);
                if (!iback.HasValue) iback = back.Blend(default_backgroud);
                if (!ifore.HasValue) ifore = fore.Blend(default_backgroud);
                ActiveBackground = back;
                ActiveForeground = fore;
                InActiveBackground = iback;
                InActiveForeground = ifore;
                return true;
            }
            catch { }
            return false;
        }

        private bool TryLoadWorkspace(string path)
        {
            try
            {
                using (var f = File.OpenText(path))
                using (var r = new JsonTextReader(f))
                {
                    var obj = (JObject)JToken.ReadFrom(r);
                    var settings = obj["settings"];
                    if (TryLoadVSColorCustomization(settings))
                    {
                        ConfigFilePath = path;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }
        private bool TryLoadVSConfig(string path)
        {
            try
            {
                using (var f = File.OpenText(path))
                using (var r = new JsonTextReader(f))
                {
                    if (TryLoadVSColorCustomization(JToken.ReadFrom(r)))
                    {
                        ConfigFilePath = path;
                        return true;
                    }
                }
            }
            catch { }
            return false;
        }


        public static VSCodeConfigLoader FindFromSolutionDir(string sln)
        {
            if (sln == null) return null;
            var d = Directory.GetParent(sln);
            while (d != null)
            {
                var s = new VSCodeConfigLoader(d.FullName);
                if (s.IsLoaded)
                {
                    return s;
                }
                d = d.Parent;
            }
            return null;
        }
    }
}
