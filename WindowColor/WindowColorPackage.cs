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
//https://github.com/mayerwin/vs-customize-window-title/blob/master/CustomizeVSWindowTitle/Globals.cs

namespace WindowColor
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideOptionPage(typeof(Options), "Environment", "miSolutionName", 0, 0, true)]
    [Guid(WindowColorPackage.PackageGuidString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class WindowColorPackage : AsyncPackage
    {
        public WindowColorPackage()
        {
            Windows = new List<VSWindowWrapper>();
        }
        public const string PackageGuidString = "a6207a48-6fc0-4c07-868b-36019eba3f88";
        #region Package Members
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);


            Options = (Options)GetDialogPage(typeof(Options));

            DTE = (DTE2)GetGlobalService(typeof(DTE));

            DTE.Events.WindowEvents.WindowCreated += (_) => UpdateWindow();
            DTE.Events.WindowEvents.WindowActivated += (_, __) => UpdateWindow();
            DTE.Events.WindowEvents.WindowClosing += (_) => UpdateWindow();
            DTE.Events.DocumentEvents.DocumentOpened += (_) => UpdateWindow();
            DTE.Events.DocumentEvents.DocumentClosing += (_) => UpdateWindow();

            

            SolutionEvents.OnAfterRenameSolution += (_, __) => OnSolutionUpdated();
            SolutionEvents.OnAfterOpenSolution += (_, __) => OnSolutionOpend();
            SolutionEvents.OnAfterCloseSolution += (_, __) => OnSolutionClosed();

            bool isSolutionLoaded = await IsSolutionLoadedAsync();
            if (isSolutionLoaded) OnSolutionOpend();

        }
        protected override void Dispose(bool disposing)
        {
            Timer.Stop();
            base.Dispose(disposing);
        }
        DTE2 DTE;
        List<VSWindowWrapper> Windows;
        DispatcherTimer Timer;
        Options Options;
        string SolutionName;

        private void OnSolutionClosed()
        {
            Windows.Clear();
        }

        private void OnSolutionOpend()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 5);
            Timer.Tick += (_, __) => { UpdateWindow(); };
            Timer.Start();
            OnSolutionUpdated();
        }


        private void UpdateWindow()
        {
            Application.Current.Dispatcher.Invoke(() => {
                foreach(Window w in Application.Current.Windows)
                {
                    VSWindowWrapper ww = VSMainWindowWrapper.Create(w, Options);
                    if (ww == null) ww = VSFloatingWindowWrapper.Create(w, Options);
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
        #endregion
    }
}
