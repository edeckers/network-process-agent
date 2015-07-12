using ElyDeckers.NetworkProcessAgent.Rules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElyDeckers.NetworkProcessAgent
{
    class RulesManager
    {
        List<NetworkProcessAgentRule> _rules = new List<NetworkProcessAgentRule>();

        public List<NetworkProcessAgentRule> GetAll()
        {
            return _rules;
        }

        public void Add(NetworkProcessAgentRule rule) {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(config.AppSettings.SectionInformation.Name);
        }
    }
}
