using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElyDeckers.NetworkProcessAgent.Network
{
    public class NetworkInterface
    {
        private  string _id;
        private string _name;

        public NetworkInterface(string id, string name) {
            _id = id;
            _name = name;
        }

        public string Id { get { return _id; } }
        public string Name { get { return _name; } }
    }
}
