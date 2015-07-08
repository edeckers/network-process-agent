using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher.UI
{
    class NetworkInterfaceListViewItem
    {
        private NetworkInterface _networkInterface;
        public NetworkInterfaceListViewItem(NetworkInterface networkInterface)
        {
            _networkInterface = networkInterface;
        }

        public NetworkInterface NetworkInterface { get { return _networkInterface; } }
        public string NetworkInterfaceId { get { return _networkInterface.Id;  } }

        public override string ToString()
        {
            return _networkInterface.Name;
        }
    }
}
