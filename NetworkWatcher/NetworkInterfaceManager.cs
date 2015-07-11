using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkWatcher
{
    class NetworkInterfaceManager
    {
        private List<INetworkInterfaceObserver> _observers = new List<INetworkInterfaceObserver>();
        private Dictionary<string, string> _killProcessOnNetworkInterfaceUp = new Dictionary<string, string>();

        public NetworkInterfaceManager()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);
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

        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface n in adapters)
            {
                NotifyObservers(n);
            }
        }

        private void NotifyObservers(NetworkInterface nic)
        {
            foreach (var observer in _observers)
            {
                observer.Notify(nic);
            }
        }

        public void RegisterObserver(INetworkInterfaceObserver observer)
        {
            _observers.Add(observer);
        }
    }
}
