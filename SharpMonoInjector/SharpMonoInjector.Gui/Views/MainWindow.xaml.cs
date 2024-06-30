using SharpMonoInjector.Gui.ViewModels;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace SharpMonoInjector.Gui.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            bool IsElevated = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

#if RELEASE || RELEASE_NOUE || RELEASE_UE
            if (!IsElevated)
            {
                string exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                System.Diagnostics.Process.Start(startInfo);
                AppDomain.Unload(AppDomain.CurrentDomain);
            }
#endif
            InitializeComponent();
            Loaded += (sender, e) =>
            {
                if (DataContext is MainWindowViewModel viewModel && viewModel.RefreshCommand.CanExecute(null))
                {
                    viewModel.RefreshCommand.Execute(null);
                }
            };



        }

        #region[Window Events]

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Window_Maximize(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }

        #endregion

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {


        }
    }
}
