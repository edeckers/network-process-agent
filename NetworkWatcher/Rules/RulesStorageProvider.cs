using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ElyDeckers.NetworkProcessAgent.Rules
{
    class RulesStorageProvider
    {
        private readonly static string _filepath = Application.StartupPath + "\\rules.xml";

        public static void Write(List<NetworkWatcherRule> rules)
        {
            using (var fileStream = new FileStream(_filepath, FileMode.Create)) {
                var serializer = CreateSerializer();

                using (var streamWriter = XmlWriter.Create(fileStream))
                {
                    serializer.Serialize(streamWriter, rules.Select(_ => BuildSerializableRuleFromRule(_)).ToList());
                    streamWriter.Close();
                }
            }
        }

        private static XmlSerializer CreateSerializer()
        {
            return new XmlSerializer(typeof(List<SerializableRule>));
        }

        public static List<NetworkWatcherRule> Read()
        {
            if (!File.Exists(_filepath))
            {
                return new List<NetworkWatcherRule> { };
            }

            using (var fileStream = new FileStream(_filepath, FileMode.Open))
            {
                var serializer = CreateSerializer();
                var serializableRules = (List<SerializableRule>)serializer.Deserialize(fileStream);

                return serializableRules.Select(_ => BuildRuleFromSerializedRule(_)).ToList();
            }
        }

        private static NetworkWatcherRule BuildRuleFromSerializedRule(SerializableRule rule)
        {
            var nic = NetworkInterfaceManager.GetById(rule.NetworkInterfaceId);
            if (nic == null)
            {
                return null;
            }

            return new NetworkWatcherRule(nic, rule.ProcessName);
        }

        private static SerializableRule BuildSerializableRuleFromRule(NetworkWatcherRule rule)
        {
            return new SerializableRule()
            {
                NetworkInterfaceId = rule.NetworkInterfaceId,
                ProcessName = rule.ProcessName
            };
        }
    }
}
