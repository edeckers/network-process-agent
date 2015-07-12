using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ElyDeckers.NetworkProcessAgent.Rules
{
    public class SerializableNetworkProcessAgentRule
    {
        public SerializableNetworkProcessAgentRule() { }

        public string NetworkInterfaceId { get; set; }
        public string ProcessName { get; set; }
    }
}
