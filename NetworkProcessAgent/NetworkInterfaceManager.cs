using DotRas;
using ElyDeckers.NetworkProcessAgent.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
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

        public static Network.NetworkInterface[] GetAll()
        {
            return GetAllInterfaces().ToArray();
        }

        private static Dictionary<Guid, string> GetRASIdNameDictionary()
        {
            var phonebook = new RasPhoneBook();
            phonebook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User));

            var dictionary = new Dictionary<Guid, string>();
            foreach (var entry in phonebook.Entries)
            {
                dictionary[entry.Id] = entry.Name;
            }

            return dictionary;
        }

        private static List<Network.NetworkInterface> GetAllRASInterfaces()
        {
            var rasDictionary = GetRASIdNameDictionary();

            return rasDictionary.Keys.Select(_ => {
                var guid = _.ToString("B").ToUpper();

                return new Network.NetworkInterface(guid, rasDictionary[_]);
            }).ToList();
        }

        private static List<Network.NetworkInterface> GetAllRegularInterfaces()
        {
            var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();

            return networkInterfaces
                .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                .Select(_ => new Network.NetworkInterface(_.Id, _.Name)).ToList();
        }

        private static List<Network.NetworkInterface> GetAllInterfaces()
        {
            var rasInterfaces = GetAllRASInterfaces();
            var regularInterfaces = GetAllRegularInterfaces();

            var allInterfaces = rasInterfaces.Concat(regularInterfaces);

            return allInterfaces
                .GroupBy(_ => _.Id)
                .Select(_ => _.First())
                .ToList();
        }

        public static Network.NetworkInterface GetById(string id)
        {
            return GetAllInterfaces().FirstOrDefault(_ => _.Id == id);
        }
        
        private void NetworkStatusChanged(object sender, EventArgs e)
        {
            System.Net.NetworkInformation.NetworkInterface[] adapters = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adapter in adapters)
            {
                var networkInterface = new Network.NetworkInterface(adapter.Id, adapter.Name);

                NetworkInterfaceStatusChangedEvent(new NetworkInterfaceStatusChangedEventArgs(networkInterface));
            }
        }

        public class NetworkInterfaceStatusChangedEventArgs : EventArgs
        {
            private readonly Network.NetworkInterface _networkInterface;
            public NetworkInterfaceStatusChangedEventArgs(Network.NetworkInterface networkInterface)
                : base()
            {
                _networkInterface = networkInterface;
            }

            public Network.NetworkInterface NetworkInterface { get { return _networkInterface; } }
        }

        public delegate void NetworkInterfaceStatusChangedHandler(NetworkInterfaceStatusChangedEventArgs e);

        public event NetworkInterfaceStatusChangedHandler NetworkInterfaceStatusChangedEvent;
    }
}
