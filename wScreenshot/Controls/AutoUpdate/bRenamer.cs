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
    /// <summary>
    /// first argument is process name to kill/wait for
    /// second argument is the update file
    /// third argument is the to overwrite file
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
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
                    wScreenshotProcess = Process.GetProcesses().Where(x => x.ProcessName.StartsWith(args[0])).ToList();
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
                if (File.Exists(args[1]))
                {
                    if (args[2] == "zip")
                    {
                    }
                    else
                    {
                        if (File.Exists(args[2]))
                        {
                            File.Delete(args[2]);
                        }

                        File.Move(args[1], args[2]);
                    }
                }
                else
                {
                    MessageBox.Show("Couldn't find update file,... starting the old one. Try updating manually www.nightow.de or run wScreenshot.exe as admin");
                    Process.Start(args[2]);
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
                    foreach (string _a in args)
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
        if (!s) System.Diagnostics.Process.Start(args[2]);
    }
}