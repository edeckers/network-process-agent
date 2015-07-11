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
    class ProcessKillerObserver : INetworkInterfaceObserver
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
            var killedProcessNames = new List<string>();
            foreach (var killerValue in _killerValues.Where(_ => _.NetworkInterface.Id == nic.Id))
            {
                ProcessManager.KillProcessByName(killerValue.ProcessName);
                killedProcessNames.Add(killerValue.ProcessName);
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
