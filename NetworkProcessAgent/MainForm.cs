﻿using ElyDeckers.NetworkProcessAgent.Network;
using ElyDeckers.NetworkProcessAgent.ProcessManagement;
using ElyDeckers.NetworkProcessAgent.Rules;
using ElyDeckers.NetworkProcessAgent.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent
{
    public partial class MainForm : Form
    {
        private readonly SystrayManager _systrayManager = new SystrayManager();
        private readonly NetworkInterfaceManager _nicManager = new NetworkInterfaceManager();
        private readonly NetworkInterfaceChangeObserver _networkInterfaceChangeObserver = new NetworkInterfaceChangeObserver();
        private List<NetworkProcessAgentRule> _rules = new List<NetworkProcessAgentRule>();

        public MainForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize() {
            _systrayManager.OnDoubleClick += OnSystrayDoubleClick;
            _networkInterfaceChangeObserver.ProcessesKilledEvent += OnProcessKilledEvent;
            InitializeProcessPlaceholder();
            FillNetworkInterfaceList();
            InitializeProcessKillerObserver();
            FillRulesList();
        }

        private void InitializeProcessPlaceholder()
        {
           // txtProcessName = new ElyDeckers.NetworkProcessAgent.UI.PlaceholderTextBox() { PlaceholderText = "Name of the process to be killed when the selected interface comes online" };


        }

        private void OnProcessKilledEvent(NetworkInterfaceChangeObserver.ProcessesKilledEventArgs e)
        {
            _systrayManager.ShowBalloonTip("Processes killed", String.Join(", ", e.ProcessNames));
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; 
            ShowInTaskbar = false;

            base.OnLoad(e);
        }

        private void FillRulesList()
        {
            try
            {
                var rules = RulesStorageProvider.Read();

                LoadRules(rules);
            }
            catch
            {
                DisplayWarning("Could not load rules", "Something went wrong reading rules from rules.xml");
            }
        }

        private void FillNetworkInterfaceList()
        {
            var items = NetworkInterfaceManager.GetAll().Select(_ => new NetworkInterfaceListViewItem(_)).ToList();

            lstNetworkInterfaces.Items.Clear();
            foreach (var item in items)
            {
                lstNetworkInterfaces.Items.Add(item);
            }
        }

        private void LoadRules(List<NetworkProcessAgentRule> rules)
        {
            _rules = rules;
            lstNetworkInterfaceRules.Items.Clear();
            foreach (var rule in rules)
            {
                lstNetworkInterfaceRules.Items.Add(new NetworkInterfaceRuleListViewItem(rule));
            }

            BindRules(rules);
        }

        private void InitializeProcessKillerObserver()
        {
            _nicManager.NetworkInterfaceStatusChangedEvent += _networkInterfaceChangeObserver.Notify;
        }

        private void btnAddNetworkInterfaceRule_Click(object sender, EventArgs e)
        {
            TryAddNetworkRule();
        }

        private void DisplayWarning(string title, string warning)
        {
            MessageBox.Show(
              warning, 
              title, 
              MessageBoxButtons.OK,
              MessageBoxIcon.Exclamation
            );
        }

        private void TryAddNetworkRule()
        {
            var nicViewListItem = lstNetworkInterfaces.SelectedItem;
            if (nicViewListItem == null)
            {
                DisplayWarning("Could not add rule", "Select a network interface");
                return;
            }

            var processName = txtProcessName.Text;
            if (String.IsNullOrEmpty(processName))
            {
                DisplayWarning("Could not add rule", "Provide a process name");
                return;
            }

            var networkInterface = ((NetworkInterfaceListViewItem)nicViewListItem).NetworkInterface;

            _rules.Add(new NetworkProcessAgentRule(networkInterface, processName));

            RulesStorageProvider.Write(_rules);

            FillRulesList();
            txtProcessName.Clear();
        }

        private void BindRules(List<NetworkProcessAgentRule> rules)
        {
            _networkInterfaceChangeObserver.Clear();
            foreach (var rule in rules)
            {
                _networkInterfaceChangeObserver.RegisterKillProcessOnInterfaceUp(rule.NetworkInterface, rule.ProcessName);
            }
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

        private void txtProcessName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            TryAddNetworkRule();
        }

        public void OnSystrayDoubleClick(object sender, EventArgs e)
        {
            Visible = true;
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private class SystrayManager
        {
            private NotifyIcon _notifyIcon;

            public SystrayManager()
            {
                var contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add("E&xit", OnMenuItemExitClick);

                var applicationIcon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);

                _notifyIcon = new NotifyIcon();
                _notifyIcon.Text = Application.ProductName;
                _notifyIcon.Icon = new Icon(applicationIcon, 40, 40);

                _notifyIcon.ContextMenu = contextMenu;
                _notifyIcon.Visible = true;
            }

            public void ShowBalloonTip(string  title, string tip) {
                _notifyIcon.ShowBalloonTip(4000, title, tip, ToolTipIcon.Info);
            }

            public event EventHandler OnDoubleClick
            {
                add { _notifyIcon.DoubleClick += value; }
                remove { _notifyIcon.DoubleClick -= value; }
            }

            private static void OnMenuItemExitClick(object sender, EventArgs e)
            {
                Application.Exit();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }
    }
}
