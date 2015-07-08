using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher.ProcessManagement
{
    class ProcessKillerObserver : INICObserver
    {
        private List<ProcessKillerValue> _killerValues = new List<ProcessKillerValue>();

        public void RegisterKillApplicationOnInterfaceUp(NetworkInterface nic, string processName)
        {
            _killerValues.Add(new ProcessKillerValue()
            {
                NetworkInterface = nic,
                ProcessName = processName
            });
        }

        public void Notify(NetworkInterface nic)
        {
            foreach (var killerValue in _killerValues.Where(_ => _.NetworkInterface.Id == nic.Id))
            {
                ProcessManager.KillProcessByName(killerValue.ProcessName);
            }
        }
    }
}
