using ElyDeckers.NetworkProcessAgent.Network;
using ElyDeckers.NetworkProcessAgent.ProcessManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.Network
{
    class NetworkInterfaceChangeObserver
    {
        private List<ProcessKillerParameters> _killerValues = new List<ProcessKillerParameters>();

        public void RegisterKillProcessOnInterfaceUp(NetworkInterface networkInterface, string processName)
        {
            _killerValues.Add(new ProcessKillerParameters()
            {
                NetworkInterface = networkInterface,
                ProcessName = processName
            });
        }

        public void Clear()
        {
            _killerValues.Clear();
        }

        public void Notify(NetworkInterfaceManager.NetworkInterfaceStatusChangedEventArgs e)
        {
            var killedProcessNames = new List<string>();
            var networkInterface = e.NetworkInterface;
            foreach (var killerValue in _killerValues.Where(_ => _.NetworkInterface.Id == networkInterface.Id))
            {
                KillProcessByName(killerValue.ProcessName);
                killedProcessNames.Add(killerValue.ProcessName);
            }

            if (killedProcessNames.Count() == 0)
            {
                return;
            }

            ProcessesKilledEvent(new ProcessesKilledEventArgs(killedProcessNames));
        }

        private void KillProcessByName(string processName)
        {
            foreach (var proc in Process.GetProcessesByName(processName))
            {
                proc.Kill();
            }
        }

        public class ProcessesKilledEventArgs : EventArgs
        {
            private readonly List<string> _processNames;
            public ProcessesKilledEventArgs(List<string> processNames)
                : base()
            {
                _processNames = processNames;
            }

            public List<string> ProcessNames { get { return _processNames; } }
        }

        public delegate void ProcessesKilledHandler(ProcessesKilledEventArgs e);

        public event ProcessesKilledHandler ProcessesKilledEvent;
    }
}
