using System;
using System.Windows;

namespace Wpf_Scrcpy.Views
{
    public partial class AdvancedSettingsWindow : Window
    {
        public AdvancedSettingsWindow()
        {
            InitializeComponent();
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
            var viewModel = DataContext as ViewModel.AdvancedSettingsViewModel;
            viewModel?.ResetToDefaults();
        }
    }
}
