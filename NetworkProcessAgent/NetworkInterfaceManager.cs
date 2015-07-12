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

        public static NetworkProcessAgentNetworkInterface[] GetAll()
        {
            return GetAllNetworkAdapters().ToArray();
            /*return NetworkInterface.GetAllNetworkInterfaces()
                    .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    .Where(_ => _.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .ToArray();*/
        }

        private static Dictionary<Guid, string> GetRASIdNameDictionary()
        {
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.User);
            RasPhoneBook pbk = new RasPhoneBook();
            pbk.Open(path);

            var dictionary = new Dictionary<Guid, string>();
            foreach (RasEntry entry in pbk.Entries)
            {
                dictionary[entry.Id] = entry.Name;
            }

            return dictionary;
        }

        private static List<NetworkProcessAgentNetworkInterface> GetAllNetworkAdapters()
        {
            var rasDictionary = GetRASIdNameDictionary();

            return rasDictionary.Keys.Select(_ => {
                var guid = _.ToString("B").ToUpper();

                return new NetworkProcessAgentNetworkInterface(guid, rasDictionary[_]);
            }).ToList();
            /*
            var tempList = new List<ManagementObject>();

            ManagementObjectCollection moc =
                new ManagementClass("Win32_NetworkAdapterConfiguration").GetInstances();

            foreach (ManagementObject obj in moc)
            {
                tempList.Add(obj);
            }

            return tempList.Select(_ => {
                var settingID = (string)_["SettingID"];
                var caption = (string)_["Caption"];
                var description = (string)_["Description"];
                var serviceName = (string)_["ServiceName"];

                string name = String.Empty;
                var adapters = _.GetRelated("Win32_NetworkAdapter");
                foreach( var adapter in adapters) {
                    var adapterName = (string)adapter["NetConnectionID"];
                    var guidString = (string)adapter["GUID"];

                    if (adapterName == null && guidString!=null)
                    {
                        var guid = Guid.Parse(guidString);
                        if (rasDictionary.ContainsKey(guid))
                        {
                            adapterName = rasDictionary[guid];
                        }
                    }
                    
                    name = String.Format("{0} {1} {2} {3}", adapterName, serviceName, caption, description);
                    Console.WriteLine(name);
                }

                return new NetworkProcessAgentNetworkInterface(settingID, name);
            }).ToList();*/
        }

        public static NetworkProcessAgentNetworkInterface GetById(string id)
        {
            return GetAllNetworkAdapters().FirstOrDefault(_ => _.Id == id);
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
