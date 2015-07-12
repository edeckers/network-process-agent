using ElyDeckers.NetworkProcessAgent.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.UI
{
    class NetworkInterfaceListViewItem
    {
        private NetworkProcessAgentNetworkInterface _networkInterface;
        public NetworkInterfaceListViewItem(NetworkProcessAgentNetworkInterface networkInterface)
        {
            _networkInterface = networkInterface;
        }

        public NetworkProcessAgentNetworkInterface NetworkInterface { get { return _networkInterface; } }
        public string NetworkInterfaceId { get { return _networkInterface.Id;  } }

        public override string ToString()
        {
            return _networkInterface.Name;
        }
    }
}
