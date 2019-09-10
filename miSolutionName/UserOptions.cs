using Microsoft.Internal.VisualStudio.Shell;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace miSolutionName
{
    public class UserOptions : BindableBase, IVsPersistSolutionOpts
    {
        struct Data
        {
            public string ActiveBackground;
            public string ActiveForeground;
            public string InActiveBackground;
            public string InActiveForeground;
            public bool IsEnabled;
        }
        Data mData;

        public string ActiveBackground
        {
            get { return mData.ActiveBackground; }
            set { SetProperty(ref mData.ActiveBackground, value); }
        }
        public string ActiveForeground
        {
            get { return mData.ActiveForeground; }
            set { SetProperty(ref mData.ActiveForeground, value); }
        }
        public string InActiveBackground
        {
            get { return mData.InActiveBackground; }
            set { SetProperty(ref mData.InActiveBackground, value); }
        }
        public string InActiveForeground
        {
            get { return mData.InActiveForeground; }
            set { SetProperty(ref mData.InActiveForeground, value); }
        }

        public bool IsEnabled
        {
            get { return mData.IsEnabled; }
            set { SetProperty(ref mData.IsEnabled, value); }
        }


        #region LoadSaveFunctions
        private const string OPTION_KEY = "miSolutionName";


        public void LoadSetting()
        {
            var persistence = Package.GetGlobalService(typeof(SVsSolutionPersistence)) as IVsSolutionPersistence;
            persistence.LoadPackageUserOpts(this, OPTION_KEY);
        }

        public int SaveUserOptions(IVsSolutionPersistence pPersistence)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            pPersistence.SavePackageUserOpts(this, OPTION_KEY);
            return VSConstants.S_OK;
        }

        public int LoadUserOptions(IVsSolutionPersistence pPersistence, uint grfLoadOpts)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            pPersistence.LoadPackageUserOpts(this, OPTION_KEY);
            return VSConstants.S_OK;
        }

        public int WriteUserOptions(IStream pOptionsStream, string pszKey)
        {
            try
            {
                using (var stream = new DataStreamFromComStream(pOptionsStream))
                {
                    using (var weiter = new StreamWriter(stream, Encoding.UTF8))
                    {
                        weiter.Write(JsonConvert.SerializeObject(mData));
                    }
                }
            }
            catch { }
            return VSConstants.S_OK;
        }

        public int ReadUserOptions(IStream pOptionsStream, string pszKey)
        {
            try
            {
                using (var stream = new DataStreamFromComStream(pOptionsStream))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8, false))
                    {
                        var str = reader.ReadToEnd();
                        mData = JsonConvert.DeserializeObject<Data>(str);
                        foreach (var p in typeof(UserOptions).GetProperties().Where(x=>x.MemberType == System.Reflection.MemberTypes.Property))
                            RaisePropertyChanged(p.Name);
                    }
                }
            }
            catch { }
            return VSConstants.S_OK;
        }
        #endregion
    }
}
