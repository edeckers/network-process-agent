using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkWatcher
{
    interface INICObserver
    {
        void Notify(NetworkInterface nic);
    }
}
