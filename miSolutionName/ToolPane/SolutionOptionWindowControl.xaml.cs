namespace miSolutionName.ToolPane
{
    using Microsoft.VisualStudio.Settings;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Settings;
    using Reactive.Bindings;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (null == value) return null;
            if (value is Color) return new SolidColorBrush((Color)value);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Interaction logic for SolutionOptionWindowControl.
    /// </summary>
    public partial class SolutionOptionWindowControl : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionOptionWindowControl"/> class.
        /// </summary>
        public SolutionOptionWindowControl()
        {
            this.InitializeComponent();
            IsEnabled = false;
        }
        SettingStore Settings;

        public void Init(SettingStore settings)
        {
            Settings = settings;
            DataContext = Settings;
            IsEnabled = true;
        }

        private void ColorTextChanged(object sender, TextChangedEventArgs e)
        {
            var text = (sender as TextBox).Text;
            var text_box = sender as TextBox;
            void set(ReactiveProperty<Color?> d)
            {
                text_box.Background = new SolidColorBrush(Colors.White);
                if (string.IsNullOrWhiteSpace(text)) d.Value = null;
                if (Common.CanConvertColor(text)) d.Value = Common.TryConvertColor(text);
                else
                    text_box.Background = new SolidColorBrush(Colors.Tomato);
            }

            if (sender == ActiveBackground) set(Settings.UserActiveBackground);
            if (sender == ActiveForeground) set(Settings.UserActiveForeground);
            if (sender == InActiveBackground) set(Settings.UserInActiveBackground);
            if (sender == InActiveForeground) set(Settings.UserInActiveForeground);
        }
    }
}