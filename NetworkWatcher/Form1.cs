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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var nicManager = new NICManager();
            var nic = NICManager.GetById("{4382130D-B5BE-4A9C-87D5-F78184C8B3AF}");
            var processKillerObserver = new ProcessKillerObserver();

            nicManager.RegisterObserver(processKillerObserver);
            processKillerObserver.RegisterKillApplicationOnInterfaceUp(nic, "skype");
        }

    }
}
