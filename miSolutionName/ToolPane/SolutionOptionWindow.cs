namespace miSolutionName.ToolPane
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [Guid("c7e26954-fee1-47c6-9e2a-9e441ddcfa91")]
    public class SolutionOptionWindow : ToolWindowPane
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionOptionWindow"/> class.
        /// </summary>
        public SolutionOptionWindow() : base(null)
        {
            this.Caption = "miSolutionName Option Editor";
            Initialized = false;
            this.Content = new SolutionOptionWindowControl();
        }
        private bool Initialized;
        
        public void Init(SettingStore settings)
        {
            if (Initialized) return;
            (this.Content as SolutionOptionWindowControl).Init(settings);
            Initialized = true;
        }

        public static void Show(miSolutionNamePackage package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = FindWindow(package);
            if (window == null)
            {
                throw new NotSupportedException("Cannot create tool window");
            }
            window.Init(package.Settings);

            var frame = (IVsWindowFrame)window.Frame;
            Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(frame.Show());
        }
        public static void ConnectExisits(miSolutionNamePackage package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = FindWindow(package, false);
            if (window != null)
                window.Init(package.Settings);
        }

        public static void Close(miSolutionNamePackage package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = FindWindow(package, false);
            if (window == null)
                return;
            var frame = (IVsWindowFrame)window.Frame;
            frame.CloseFrame((uint)__FRAMECLOSE.FRAMECLOSE_NoSave);
        }

        private static SolutionOptionWindow FindWindow(miSolutionNamePackage package, bool create=true)
        {
            var window = package.FindToolWindow(typeof(SolutionOptionWindow), 0, create) as SolutionOptionWindow;
            if ((null == window) || (null == window.Frame))
            {
                return null;
            }
            return window;
        }


    }
}
