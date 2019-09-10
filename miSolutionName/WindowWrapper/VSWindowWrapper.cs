using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace miSolutionName.WindowWrapper
{
    public abstract class VSWindowWrapper
    {
        protected TextBlock Text;
        protected Border Border;
        public event Action<VSWindowWrapper> Closed;
        protected Window Window;
        protected string mTitle;
        protected SettingStore Settings;
        public string Tilte
        {
            set
            {
                if (Text != null)
                    ExecInUI(() => { Text.Text = value; });
                mTitle = value;
            }
            get => mTitle;
        }

        public bool IsLoaded => Window?.IsLoaded ?? false;

        public VSWindowWrapper(Window w, SettingStore settings)
        {
            Settings = settings;
            Window = TryParseWindow(w, settings);
            if (Window == null) return;
            Window.Closed += OnClosed; ;
            Window.Activated += OnActivated;
            Window.Deactivated += OnDeactivated;
            UpdateColor();
        }

        protected void ExecInUI(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
        protected abstract Window TryParseWindow(Window w, SettingStore settings);        

        protected void OnClosed(object sender, EventArgs e)
        {
            Text = null;
            Border = null;
            Window = null;
            Closed?.Invoke(this);
        }

        protected void OnDeactivated(object sender, EventArgs e)
        {
            UpdateColor();
        }

        protected void OnActivated(object sender, EventArgs e)
        {
            UpdateColor();
        }

        protected void UpdateColor()
        {
            if (Window == null) return;
            ExecInUI(() =>
            {
                if (Window.IsActive)
                {
                    Border.Background = new SolidColorBrush(Settings.ActiveBackground.Value);
                    Text.Foreground = new SolidColorBrush(Settings.ActiveForeground.Value);
                }
                else
                {
                    Border.Background = new SolidColorBrush(Settings.InActiveBackground.Value);
                    Text.Foreground = new SolidColorBrush(Settings.InActiveForeground.Value);
                }
            });
        }

    }
}
