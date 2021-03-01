using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_Scrcpy.Mothod
{
    public static class cmdHelp
    {
        public static Process cmdPorcess(string cmd_str, Action<object, DataReceivedEventArgs> outAction, string cd_str = "")
        {
            Process process = new Process();
            process.StartInfo.FileName = @"cmd";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.OutputDataReceived += (s, s1) => { outAction(s, s1); };
            process.ErrorDataReceived += (s, s1) => { outAction(s, s1); };
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            Task.Run(() =>
            {
                process.StandardInput.WriteLine(cd_str);
                process.StandardInput.WriteLine(cmd_str);
                process.StandardInput.WriteLine("exit");
            });
            return process;
        }
    }
}
