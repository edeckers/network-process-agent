using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace ElyDeckers.NetworkProcessAgent
{
    interface INetworkInterfaceObserver
    {
        void Notify(NetworkInterface nic);
    }
}
