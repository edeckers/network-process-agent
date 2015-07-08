using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher.UI
{
    class NetworkInterfaceRuleListViewItem
    {
        private NetworkInterface _networkInterface;
        private  string _process;

        public NetworkInterfaceRuleListViewItem(NetworkInterface networkInterface, string process)
        {
            _networkInterface = networkInterface;
            _process = process;
        }

        public string NetworkInterfaceId { get { return _networkInterface.Id;  } }

        public override string ToString()
        {
            return String.Format("{0}: {1}", _networkInterface.Name, _process);
        }
    }
}
