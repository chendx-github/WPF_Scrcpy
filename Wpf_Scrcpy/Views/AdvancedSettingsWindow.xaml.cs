using System;
using System.Windows;
using Wpf_Scrcpy.ViewModel;

namespace Wpf_Scrcpy.Views
{
    public partial class AdvancedSettingsWindow : Window
    {
        public AdvancedSettingsViewModel ViewModel { get; private set; }

        public AdvancedSettingsWindow()
        {
            InitializeComponent();
            ViewModel = new AdvancedSettingsViewModel();
            DataContext = ViewModel;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // 重置所有设置为默认值
            ViewModel?.ResetToDefaults();
        }
    }
}
