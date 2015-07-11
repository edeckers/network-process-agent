using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkWatcher.Rules
{
    public class SerializableRule
    {
        public SerializableRule() { }

        public string NetworkInterfaceId { get; set; }
        public string ProcessName { get; set; }
    }
}
