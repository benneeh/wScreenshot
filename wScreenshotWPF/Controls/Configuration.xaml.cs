using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wScreenshot.Controls
{
    /// <summary>
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : UserControl
    {
        public Configuration()
        {
            Model = Properties.Configuration.Default;
            InitializeComponent();
        }

        private void CancelCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Properties.Configuration.Default.Reload();
        }

        private void CancelCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Properties.DirtySettingsExtender.IsDirty(e.Parameter);
        }

        private void ApplyCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Properties.Configuration.Default.Save();
        }

        private void ApplyCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Properties.DirtySettingsExtender.IsDirty(e.Parameter);
        }

        internal Properties.Configuration Model
        {
            get
            {
                return DataContext as Properties.Configuration;
            }
            set
            {
                DataContext = value;
            }
        }

        private void ResetCommand_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Properties.DirtySettingsExtender.IsReset(e.Parameter);
        }

        private void ResetCommand_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Properties.DirtySettingsExtender.DoReset(Properties.Configuration.Default);
        }

        private void btnChooseSeenFolder_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dlg = new Dialog.FolderBrowserDialog();

            if (dlg.ShowDialog() == true)
            {
                Properties.Configuration.Default.SeenDirectory = dlg.SelectedPath;
            }
        }
    }
}