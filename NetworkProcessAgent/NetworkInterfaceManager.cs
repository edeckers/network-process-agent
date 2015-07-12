using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent
{
    class NetworkInterfaceManager
    {
        private Dictionary<string, string> _killProcessOnNetworkInterfaceUp = new Dictionary<string, string>();

        public NetworkInterfaceManager()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(NetworkStatusChanged);
        }

        public static NetworkInterface[] GetAll()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                    .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .ToArray();
        }

        public static NetworkInterface GetById(string id)
        {
            return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(_ => _.Id == id);
        }

        private void NetworkStatusChanged(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                NetworkInterfaceStatusChangedEvent(new NetworkInterfaceStatusChangedEventArgs(n));
            }
        }


        public class NetworkInterfaceStatusChangedEventArgs : EventArgs
        {
            private readonly NetworkInterface _networkInterface;
            public NetworkInterfaceStatusChangedEventArgs(NetworkInterface networkInterface)
                : base()
            {
                _networkInterface = networkInterface;
            }

            public NetworkInterface NetworkInterface { get { return _networkInterface; } }
        }

        public delegate void NetworkInterfaceStatusChangedHandler(NetworkInterfaceStatusChangedEventArgs e);

        public event NetworkInterfaceStatusChangedHandler NetworkInterfaceStatusChangedEvent;
    }
}
