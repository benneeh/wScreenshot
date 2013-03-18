using System.IO;
using System.Reflection;
using System.Windows;

namespace wScreenshot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string AppPath = Assembly.GetExecutingAssembly().GetName().FullName;
            string[] Modules = Directory.GetFiles("Modules");
            foreach (var module in Modules)
            {
                var mod = Assembly.LoadFrom(module);
                var mods = mod.GetModules();
                //mod.
            }
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //string AppPath = Assembly.GetExecutingAssembly().GetName().FullName;
            //string[] Modules = Directory.GetFiles("Modules", "*.dll");
            //foreach (var module in Modules)
            //{
            //    var mod = Assembly.LoadFrom(module);

            //    var mods = mod.GetModules();
            //    foreach (var m in mods)
            //    {
            //        var types = m.GetTypes();
            //        types[0].get
            //    }
            //    mod.
            //}
            new ScreenshotModule.RedBoxTool().Show();
        }
    }
}