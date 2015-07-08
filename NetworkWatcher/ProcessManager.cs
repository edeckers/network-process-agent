using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher
{
    class ProcessManager
    {
        public static void KillProcessByName(string processName)
        {
            foreach (Process proc in Process.GetProcessesByName(processName))
            {
                proc.Kill();
            }
        }
    }
}
