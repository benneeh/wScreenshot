using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using wScreenshot.Dialog;
using wScreenshot.Properties;

namespace wScreenshot.Controls
{
    /// <summary>
    ///     Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : UserControl
    {
        public Configuration()
        {
            Model = Properties.Configuration.Default;
            InitializeComponent();
        }

        internal Properties.Configuration Model
        {
            get { return DataContext as Properties.Configuration; }
            set { DataContext = value; }
        }

        private void CancelCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Properties.Configuration.Default.Reload();
        }

        private void CancelCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DirtySettingsExtender.IsDirty(e.Parameter);
        }

        private void ApplyCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Properties.Configuration.Default.Save();
        }

        private void ApplyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = DirtySettingsExtender.IsDirty(e.Parameter);
        }

        private void ResetCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !DirtySettingsExtender.IsReset(e.Parameter);
        }

        private void ResetCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");
            DirtySettingsExtender.DoReset(Properties.Configuration.Default);
        }

        private void btnChooseSeenFolder_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == true)
            {
                Properties.Configuration.Default.SeenDirectory = dlg.SelectedPath;
            }
        }
    }
}