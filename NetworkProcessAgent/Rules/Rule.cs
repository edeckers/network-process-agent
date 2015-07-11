using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ElyDeckers.NetworkProcessAgent.Rules
{
    public class NetworkWatcherRule
    {
        private NetworkInterface _networkInterface;
        private string _processName;
        private Guid _id;

        public NetworkWatcherRule(NetworkInterface nic, string processName)
        {
            _id = Guid.NewGuid();
            _networkInterface = nic;
            _processName = processName;
        }

        [XmlIgnore]
        public NetworkInterface NetworkInterface { get { return _networkInterface; } }
        public Guid Id { get { return _id; } }
        public string NetworkInterfaceId { get { return NetworkInterface.Id; } }
        public string ProcessName { get { return _processName; } }
    }
}
