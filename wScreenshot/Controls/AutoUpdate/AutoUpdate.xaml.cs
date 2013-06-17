using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace wScreenshot.Controls.AutoUpdate
{
    /// <summary>
    ///     Interaction logic for AutoUpdate.xaml
    /// </summary>
    public partial class AutoUpdate : UserControl
    {
        #region ctor

        public AutoUpdate()
        {
            InitializeComponent();

            for (int i = 0; File.Exists("bRenamer.exe"); i++)
            {
                try
                {
                    File.Delete("bRenamer.exe");
                    Thread.Sleep(50);
                    if (i == 20)
                    {
                        if (
                            MessageBox.Show("Couldn't remove bRenamer.exe, retry?", "Error:", MessageBoxButton.YesNo,
                                MessageBoxImage.Asterisk, MessageBoxResult.OK) == MessageBoxResult.OK)
                        {
                            i = 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public AutoUpdate(string HostName, int Port)
            : this()
        {
            this.HostName = HostName;
            this.Port = Port;
        }

        #endregion ctor

        #region events

        public delegate void UpdateEventHandler(object sender, UpdateEventArgs e);

        /// <summary>
        ///     Occurs when an update is avialable
        /// </summary>
        [Category("Update"), Description("Sets an global update event.")]
        public event UpdateEventHandler UpdateAvialable;

        #endregion events

        #region members

        private Dictionary<string, string> _UserObjects = new Dictionary<string, string>();

        #endregion members

        #region public properties fields

        public static readonly DependencyProperty HostNameProperty = DependencyProperty.Register("HostName",
            typeof (string), typeof (AutoUpdate),
            new FrameworkPropertyMetadata("nightow.mine.nu"));

        public static readonly DependencyProperty PortProperty = DependencyProperty.Register("Port", typeof (int),
            typeof (AutoUpdate),
            new FrameworkPropertyMetadata(1315));

        public Dictionary<string, string> UserObjects
        {
            get { return _UserObjects; }
            set { _UserObjects = value; }
        }

        [Category("Update"), DefaultValue(false), Description("Gets or sets the servers hostname")]
        public string HostName
        {
            get { return (string) GetValue(HostNameProperty); }

            set { SetValue(HostNameProperty, value); }
        }

        [Category("Update"), DefaultValue(1315), Description("Gets or sets the servers port")]
        public int Port
        {
            get { return (int) GetValue(PortProperty); }

            set { SetValue(PortProperty, value); }
        }

        [Category("Update"), Description("Indicates if the current exe is x64")]
        public bool Isx64
        {
            get { return (IntPtr.Size == 8); }
        }

        [Category("Update"), Description("Indicates if the OS is x64")]
        public bool IsOsx64
        {
            get { return (IntPtr.Size == 8); }
        }

        [Category("Update"), Description("Gets the clients ComputerName")]
        public string ComputerName
        {
            get { return Environment.MachineName; }
        }

        [Category("Update"), Description("Gets the clients Version")]
        public string Version
        {
            get
            {
                if (Isx64Forced) return "force_x64";
                if (Isx86Forced) return "force_x86";
                return "v" + Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        [Category("Update"), DefaultValue(false), Description("Gets the clients Programname")]
        public string ProgramName
        {
            get
            {
                string s = Assembly.GetExecutingAssembly().GetName().Name;
                if (Isx64 || Isx64Forced)
                {
                    s += "x64";
                }
                return s;
            }
        }

        private bool Isx64Forced { get; set; }

        private bool Isx86Forced { get; set; }

        public bool IsAllSet
        {
            get { return !string.IsNullOrEmpty(HostName) && Port > 0; }
        }

        #endregion public properties fields

        #region public methods

        public void AddUserObject(string Name, string Value)
        {
            if (UserObjects.ContainsKey(Name))
            {
                UserObjects[Name] = Value;
            }
            else
            {
                UserObjects.Add(Name, Value);
            }
        }

        public bool StartConnect()
        {
            if (IsAllSet)
            {
                string host = HostName;
                int port = Port;
                var bw = new BackgroundWorker();
                bw.DoWork += (s, e) => { Updatethread(host, port); };
                bw.RunWorkerAsync();
            }

            return IsAllSet;
        }

        public bool StartConnect(bool cpuArchitectureForceX64, bool cpuArchitectureForceX86)
        {
            Isx86Forced = cpuArchitectureForceX86;
            Isx64Forced = cpuArchitectureForceX64;
            if (IsAllSet)
            {
                string host = HostName;
                int port = Port;
                var bw = new BackgroundWorker();
                bw.DoWork += (s, e) => { Updatethread(host, port); };
                bw.RunWorkerAsync();
            }
            return IsAllSet;
        }

        public void StartBuiltInUpdate(UpdateEventArgs e)
        {
            if (e.Url != "")
            {
                if (e.Success)
                {
                    if (e.Available)
                    {
                        try
                        {
                            new WebClient().DownloadFile(e.Url, ProgramName + ".update");

                            var fi = new FileInfo(ProgramName + ".update");
                            var cp = new CompilerParameters();
                            cp.ReferencedAssemblies.Add("System.dll");
                            cp.ReferencedAssemblies.Add("System.Core.dll");
                            cp.ReferencedAssemblies.Add("System.Xaml.dll");
                            cp.ReferencedAssemblies.Add("WindowsBase.dll");
                            cp.ReferencedAssemblies.Add("PresentationFramework.dll");
                            cp.ReferencedAssemblies.Add("PresentationCore.dll");
                            cp.GenerateExecutable = true;
                            cp.CompilerOptions = string.Format("/target:winexe /lib:\"{0}\"",
                                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                    @"Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0"));

                            //new string[] {"",""}.Any()
                            CodeDomProvider cdp = CodeDomProvider.CreateProvider("c#");
                            Assembly assembly = Assembly.GetExecutingAssembly();
                            string resourceName =
                                assembly.GetManifestResourceNames().Where(x => x.EndsWith("bRenamer.cs")).First();
                            var resourceStreamReader = new StreamReader(assembly.GetManifestResourceStream(resourceName));
                            string prog = resourceStreamReader.ReadToEnd();

                            CompilerResults Results = cdp.CompileAssemblyFromSource(cp, prog);

                            Process.Start(Results.PathToAssembly,
                                "\"" + fi.FullName + "\" \"" + Assembly.GetExecutingAssembly().Location + "\"");

                            Dispatcher.Invoke((Action) delegate { Application.Current.MainWindow.Close(); });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        #endregion public methods

        #region private methods

        private void Updatethread(string host, int port)
        {
            string serverOrt = "";
            string serverVers = "";
            try
            {
                var updater = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                updater.Connect(host, port);
                var ns = new NetworkStream(updater); //hier der stream für das socket
                var bw = new BinaryWriter(ns); //klasse zum binären schreiben in einen stream
                var br = new BinaryReader(ns); //   "    "     "    lesen     aus einem  "
                var e = new UpdateEventArgs(null, false, false, null, null);
                if (updater.Connected)
                {
                    bw.Write(3 + UserObjects.Count); //anzahl an zusatzinformationen + 3 die man sowieso hat
                    bw.Write("ProgrammName"); // name der 1. info
                    bw.Write(ProgramName); //daten der 1. info

                    bw.Write("Version"); // name der 2. info .....
                    bw.Write(Version);

                    bw.Write("ComputerName");
                    bw.Write(ComputerName);
                    bw.Flush();

                    foreach (string k in UserObjects.Keys)
                    {
                        bw.Write(k);
                        bw.Write(UserObjects[k]);
                        bw.Flush();
                    }

                    serverVers = br.ReadString(); //version lesen
                    serverOrt = br.ReadString(); //url zum downloaden lesen

                    bw.Close();
                    updater.Close(); //schließen

                    if (serverVers != Version) //...
                    {
                        e = new UpdateEventArgs(serverOrt, true, true, "Update available.", serverVers);
                    }
                    else
                    {
                        e = new UpdateEventArgs(serverOrt, true, false, "No update available.", serverVers);
                    }
                }
                else
                {
                    e = new UpdateEventArgs(serverOrt, false, false,
                        "Updateprogress failed. Don't know if an update is available.", serverVers);
                }
                if (UpdateAvialable != null)
                {
                    UpdateAvialable(this, e);
                }
                else
                {
                    StartBuiltInUpdate(e);
                }
            }
            catch (Exception e)
            {
                if (UpdateAvialable != null)
                {
                    UpdateAvialable(this, new UpdateEventArgs(serverOrt, false, false, e.ToString(), serverVers));
                }
                else
                {
                    StartBuiltInUpdate(new UpdateEventArgs(serverOrt, false, false, e.ToString(), serverVers));
                }
            }
        }

        #endregion private methods

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartConnect();
        }
    }
}