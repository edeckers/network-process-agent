using NetworkWatcher.ProcessManagement;
using NetworkWatcher.Rules;
using NetworkWatcher.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkWatcher
{
    public partial class MainForm : Form
    {
        private readonly NICManager _nicManager = new NICManager();
        private readonly ProcessKillerObserver _processKillerObserver = new ProcessKillerObserver();
        private List<NetworkWatcherRule> _rules = new List<NetworkWatcherRule>();

        public MainForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            FillNetworkInterfaceList();
            InitializeProcessKillerObserver();
            FillRulesList();
        }

        private void FillRulesList()
        {
            LoadRules(RulesStorageProvider.Read());
        }

        private void FillNetworkInterfaceList()
        {
            var items = NICManager.GetAll().Select(_ => new NetworkInterfaceListViewItem(_)).ToList();

            lstNetworkInterfaces.Items.Clear();
            foreach (var item in items)
            {
                lstNetworkInterfaces.Items.Add(item);
            }
        }

        private void LoadRules(List<NetworkWatcherRule> rules)
        {
            _rules = rules;
            lstNetworkInterfaceRules.Items.Clear();
            foreach (var rule in rules)
            {
                lstNetworkInterfaceRules.Items.Add(new NetworkInterfaceRuleListViewItem(rule));
            }
        }

        private void InitializeProcessKillerObserver()
        {
            _nicManager.RegisterObserver(_processKillerObserver);
        }

        private void btnAddNetworkInterfaceRule_Click(object sender, EventArgs e)
        {
            var nicViewListItem = lstNetworkInterfaces.SelectedItem;
            if (nicViewListItem == null)
            {
                throw new Exception("Select an NIC");
            }

            var processName = txtProcessName.Text;
            if (String.IsNullOrEmpty(processName))
            {
                throw new Exception("Provide a process name");
            }

            var nic = ((NetworkInterfaceListViewItem)nicViewListItem).NetworkInterface;

            _processKillerObserver.RegisterKillApplicationOnInterfaceUp(nic, processName);

            _rules.Add(new NetworkWatcherRule(nic, processName));

            RulesStorageProvider.Write(_rules);

            FillRulesList();
        }

        private void RemoveNetworkWatcherRule(Guid ruleId)
        {
            _rules = _rules.Where(_ => _.Id != ruleId).ToList();

            RulesStorageProvider.Write(_rules);
        }

        private void lstNetworkInterfaceRules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
            {
                return;
            }

            var networkWatcherRuleItem = (NetworkInterfaceRuleListViewItem)lstNetworkInterfaceRules.SelectedItem;
            RemoveNetworkWatcherRule(networkWatcherRuleItem.RuleId);
            FillRulesList();
        }
    }
}
