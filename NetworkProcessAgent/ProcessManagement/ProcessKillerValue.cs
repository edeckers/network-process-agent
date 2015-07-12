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
    class ProcessKillerValue
    {
        public NetworkProcessAgentNetworkInterface NetworkInterface { get; set; }
        public string ProcessName { get; set; }
    }
}
