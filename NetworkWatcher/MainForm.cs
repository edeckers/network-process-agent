using NetworkWatcher.ProcessManagement;
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

        public MainForm()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize() {
            FillNetworkInterfaceList();
            InitializeProcessKillerObserver();
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

            lstNetworkInterfaceRules.Items.Add(new NetworkInterfaceRuleListViewItem(nic, processName));
        }
    }
}
