using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Navigation;

namespace wScreenshot
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ObjectCache cache = MemoryCache.Default;

        private void Application_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string AppPath = Assembly.GetExecutingAssembly().GetName().FullName;
            string[] Modules = Directory.GetFiles("Modules");
            foreach (string module in Modules)
            {
                Assembly mod = Assembly.LoadFrom(module);
                Module[] mods = mod.GetModules();

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
        }
    }
}