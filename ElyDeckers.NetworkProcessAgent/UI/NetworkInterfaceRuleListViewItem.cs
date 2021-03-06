﻿using ElyDeckers.NetworkProcessAgent.Network;
using ElyDeckers.NetworkProcessAgent.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent.UI
{
    class NetworkInterfaceRuleListViewItem
    {
        private Network.NetworkInterface _networkInterface;
        private NetworkProcessAgentRule _rule;

        public NetworkInterfaceRuleListViewItem(NetworkProcessAgentRule rule)
        {
            _networkInterface = rule.NetworkInterface;
            _rule = rule;
        }

        public Guid RuleId { get { return _rule.Id; } }

        public override string ToString()
        {
            return String.Format("{0}: {1}", _networkInterface.Name, _rule.ProcessName);
        }
    }
}
