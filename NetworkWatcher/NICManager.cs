using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher
{
    class NICManager
    {
        private List<INICObserver> _observers = new List<INICObserver>();
        private Dictionary<string, string> _killProcessOnNICUp = new Dictionary<string, string>();

        public NICManager()
        {
            NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(NetworkChange_NetworkAddressChanged);
        }

        public static NetworkInterface[] GetAll()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
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

        public void RegisterObserver(INICObserver observer)
        {
            _observers.Add(observer);
        }
    }
}
