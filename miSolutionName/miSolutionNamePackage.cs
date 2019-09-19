using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;
using SolutionEvents = Microsoft.VisualStudio.Shell.Events.SolutionEvents;
using Task = System.Threading.Tasks.Task;
using Window = System.Windows.Window;
using System.Windows.Threading;
using System.Windows.Controls;
using System.IO;
using Path = System.Windows.Shapes.Path;
using miSolutionName.WindowWrapper;
using miSolutionName.ToolPane;
using Microsoft.VisualStudio.OLE.Interop;
//https://github.com/mayerwin/vs-customize-window-title/blob/master/CustomizeVSWindowTitle/Globals.cs

namespace miSolutionName
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideOptionPage(typeof(Options), "Environment", "miSolutionName", 100, 0, true)]
    [Guid(miSolutionNamePackage.PackageGuidString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(SolutionOptionWindow))]
    public sealed class miSolutionNamePackage : AsyncPackage, IVsPersistSolutionOpts
    {
        public miSolutionNamePackage()
        {
            Windows = new List<VSWindowWrapper>();
        }
        public const string PackageGuidString = "a6207a48-6fc0-4c07-868b-36019eba3f88";
        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            DTE = (DTE2)GetGlobalService(typeof(DTE));

            DTE.Events.WindowEvents.WindowCreated += (_) => UpdateWindow();
            DTE.Events.WindowEvents.WindowActivated += (_, __) => UpdateWindow();
            DTE.Events.WindowEvents.WindowClosing += (_) => UpdateWindow();
            DTE.Events.DocumentEvents.DocumentOpened += (_) => UpdateWindow();
            DTE.Events.DocumentEvents.DocumentClosing += (_) => UpdateWindow();

            

            SolutionEvents.OnAfterRenameSolution += (_, __) => OnSolutionUpdated();
            SolutionEvents.OnAfterOpenSolution += (_, __) => OnSolutionOpend();
            SolutionEvents.OnAfterCloseSolution += (_, __) => OnSolutionClosed();
            SolutionEvents.OnBeforeCloseSolution += (_, __) => CloseToolWindow();

            bool isSolutionLoaded = await IsSolutionLoadedAsync();
            if (isSolutionLoaded) OnSolutionOpend();
            await SolutionOptionWindowCommand.InitializeAsync(this);
            UserOption = new UserOptions();
            UserOption.LoadSetting();
            Settings = new SettingStore((Options)GetDialogPage(typeof(Options)), UserOption);
            if (isSolutionLoaded)
            {
                Settings.SolutionFilePath.Value = DTE.Solution.FileName;
            }
             SolutionOptionWindow.ConnectExisits(this);
        }
        private UserOptions UserOption;
        protected override void Dispose(bool disposing)
        {
            CloseToolWindow();
            Timer.Stop();
            base.Dispose(disposing);
        }
        DTE2 DTE;
        List<VSWindowWrapper> Windows;
        DispatcherTimer Timer;
        public SettingStore Settings;
        string SolutionName;

        private void OnSolutionClosed()
        {
            Windows.Clear();
            Settings.SolutionFilePath.Value = null; 
        }

        private void CloseToolWindow()
        {
            Application.Current.Dispatcher.Invoke(() => {
                try
                {
                    SolutionOptionWindow.Close(this);
                }
                catch { }
            });
        }

        private void OnSolutionOpend()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += (_, __) => { UpdateWindow(); };
            Timer.Start();
            if (Settings != null)
                Settings.SolutionFilePath.Value = DTE.Solution.FileName;
            OnSolutionUpdated();
        }


        private void UpdateWindow()
        {
            Application.Current.Dispatcher.Invoke(() => {
                foreach(Window w in Application.Current.Windows)
                {
                    VSWindowWrapper ww = VSMainWindowWrapper.Create(w, Settings);
                    if (ww == null) ww = VSFloatingWindowWrapper.Create(w, Settings);
                    if (ww != null)
                    {
                        Windows.Add(ww);
                        ww.Closed += OnWindowClosed;
                        ww.Tilte = SolutionName;
                    }
                }
            });
        }

        private void OnWindowClosed(VSWindowWrapper obj)
        {
            Windows.Remove(obj);
        }

        private async Task<bool> IsSolutionLoadedAsync()
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync();
            var solService = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;
            ErrorHandler.ThrowOnFailure(solService.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));
            return value is bool isSolOpen && isSolOpen;
        }

        private void OnSolutionUpdated()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            SolutionName = System.IO.Path.GetFileNameWithoutExtension(DTE.Solution.FileName);
            UpdateWindow();
        }
        
        public int SaveUserOptions(IVsSolutionPersistence pPersistence)
        {
            return UserOption?.SaveUserOptions(pPersistence) ?? 0;
        }
        public int LoadUserOptions(IVsSolutionPersistence pPersistence, uint grfLoadOpts)
        {
            return UserOption?.LoadUserOptions(pPersistence, grfLoadOpts) ?? 0;
        }
        public int WriteUserOptions(IStream pOptionsStream, string pszKey)
        {
            return UserOption?.WriteUserOptions(pOptionsStream, pszKey) ?? 0;
        }
        public int ReadUserOptions(IStream pOptionsStream, string pszKey)
        {
            return UserOption?.ReadUserOptions(pOptionsStream, pszKey) ?? 0;
        }
        #endregion
    }
}
