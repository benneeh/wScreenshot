using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

internal static class Program
{
    public static void Main(string[] a)
    {
        bool s = true;
        int c = 1000;

        while ((s) && (c > 0))
        {
            try
            {
                bool wScreenshotCleaning = false;
                List<Process> wScreenshotProcess = null;
                for (int i = 0; i < 20; i++)
                {
                    wScreenshotProcess = Process.GetProcesses().Where(x => x.ProcessName.StartsWith("wScreenshot")).ToList();
                    if (wScreenshotProcess.Count > 0)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {
                        break;
                    }
                }
                if (wScreenshotCleaning && wScreenshotProcess != null)
                {
                    wScreenshotProcess.ForEach(x => x.Kill());
                }
                System.Threading.Thread.Sleep(1000);
                if (File.Exists(a[0]))
                {
                    if (File.Exists(a[1]))
                        File.Delete(a[1]);

                    File.Move(a[0], a[1]);
                }
                else
                {
                    MessageBox.Show("Couldn't find update file,... starting the old one. Try updating manually www.nightow.de or run wScreenshot.exe as admin");
                    Process.Start(a[1]);
                    return;
                }
                s = false;
            }
            catch (UnauthorizedAccessException eee)
            {
                if (MessageBox.Show("wScreenshot updater needs admin rights to remove the old file and replace it with the new one, run as admin?", eee.ToString()) == MessageBoxResult.OK)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Assembly.GetExecutingAssembly().Location;
                    startInfo.Verb = "runas";
                    string arg = "";
                    foreach (string _a in a)
                    {
                        arg += "\"" + _a + "\" ";
                    }
                    if (arg.Length == 0) arg = "adminrequest";
                    startInfo.Arguments = arg;
                    try
                    {
                        Process p = Process.Start(startInfo);
                        return;
                    }
                    catch (System.ComponentModel.Win32Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                c--;
                System.Threading.Thread.Sleep(100);
            }
        }
        if (!s) System.Diagnostics.Process.Start(a[1]);
    }
}