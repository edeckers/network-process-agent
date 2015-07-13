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
        private Network.NetworkInterface _networkInterface;
        public NetworkInterfaceListViewItem(Network.NetworkInterface networkInterface)
        {
            _networkInterface = networkInterface;
        }

        public Network.NetworkInterface NetworkInterface { get { return _networkInterface; } }
        public string NetworkInterfaceId { get { return _networkInterface.Id;  } }

        public override string ToString()
        {
            return _networkInterface.Name;
        }
    }
}
