using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.ProcessManagement
{
    class ProcessNameAutoCompleteSource : AutoCompleteStringCollection
    {
        private string[] _processNames;
        public ProcessNameAutoCompleteSource()
        {
            UpdateProcessList();
        }

        public void UpdateProcessList()
        {
            var runningProcesses = Process.GetProcesses();
            _processNames = runningProcesses.Select(_ => _.ProcessName).ToArray();

            base.Clear();
            base.AddRange(_processNames);
        }
    }
}
