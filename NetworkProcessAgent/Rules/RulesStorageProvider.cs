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
        private readonly static string _filepath = Path.Combine(Application.CommonAppDataPath, "rules.xml");

        public static void Write(List<NetworkProcessAgentRule> rules)
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
            return new XmlSerializer(typeof(List<SerializableNetworkProcessAgentRule>));
        }

        public static List<NetworkProcessAgentRule> Read()
        {
            if (!File.Exists(_filepath))
            {
                return new List<NetworkProcessAgentRule> { };
            }

            using (var fileStream = new FileStream(_filepath, FileMode.Open))
            {
                var serializer = CreateSerializer();
                var serializableRules = (List<SerializableNetworkProcessAgentRule>)serializer.Deserialize(fileStream);

                return serializableRules
                        .Select(_ => BuildRuleFromSerializedRule(_))
                        .Where(_ => _ != null)
                        .ToList();
            }
        }

        private static NetworkProcessAgentRule BuildRuleFromSerializedRule(SerializableNetworkProcessAgentRule rule)
        {
            var nic = NetworkInterfaceManager.GetById(rule.NetworkInterfaceId);
            if (nic == null)
            {
                return null;
            }

            return new NetworkProcessAgentRule(nic, rule.ProcessName);
        }

        private static SerializableNetworkProcessAgentRule BuildSerializableRuleFromRule(NetworkProcessAgentRule rule)
        {
            return new SerializableNetworkProcessAgentRule()
            {
                NetworkInterfaceId = rule.NetworkInterfaceId,
                ProcessName = rule.ProcessName
            };
        }
    }
}
