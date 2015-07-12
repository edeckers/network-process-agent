using ElyDeckers.NetworkProcessAgent.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.ProcessManagement
{
    class ProcessKillerObserver
    {
        private List<ProcessKillerValue> _killerValues = new List<ProcessKillerValue>();

        public void RegisterKillApplicationOnInterfaceUp(NetworkProcessAgentNetworkInterface nic, string processName)
        {
            _killerValues.Add(new ProcessKillerValue()
            {
                NetworkInterface = nic,
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
            var nic = e.NetworkInterface;
            foreach (var killerValue in _killerValues.Where(_ => _.NetworkInterface.Id == nic.Id))
            {
                ProcessManager.KillProcessByName(killerValue.ProcessName);
                killedProcessNames.Add(killerValue.ProcessName);
            }

            if (killedProcessNames.Count() == 0)
            {
                return;
            }

            ProcessesKilledEvent(new ProcessesKilledEventArgs(killedProcessNames));
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
