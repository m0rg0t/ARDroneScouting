using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ARDrone2Client.Common.Configuration
{
    public class ConfigurationPacketParser
    {
        public static bool TryUpdate(DroneConfiguration configuration, ConfigurationPacket packet)
        {
            if (configuration == null || packet.Data == null) return false;
            Regex _reKeyValue = new Regex(@"(?<key>\w+:\w+) = (?<value>.*)");
            bool updated = false;
            using (var ms = new MemoryStream(packet.Data))
            using (var sr = new StreamReader(ms))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Match match = _reKeyValue.Match(line);
                    if (match.Success)
                    {
                        string key = match.Groups["key"].Value;
                        IConfigurationItem item;
                        if (configuration.Items.TryGetValue(key, out item))
                        {
                            string value = match.Groups["value"].Value;
                            if (item.TryUpdate(value))
                            {
                                updated = true;
                            }
                        }
                        else
                        {
                            //Trace.TraceWarning("Configuration key {0} is not supported by parser. Original line: {1}", key, line);
                        }
                    }
                }
            }
            return updated;
        }
        public static bool TryUpdate2(DroneConfiguration configuration, ConfigurationPacket packet)
        {
            if (configuration == null || packet.Data == null) return false;
            bool updated = false;
            using (var ms = new MemoryStream(packet.Data))
            using (var sr = new StreamReader(ms))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (TryUpdateLine(configuration, line)) updated = true;
                }
            }
            return updated;
        }
        public static bool TryUpdateLine(DroneConfiguration configuration, string line)
        {
            var result = false;
            if (configuration == null || line == null) return false;
            Match match = Regex.Match(line, @"(?<key>\w+:\w+) = (?<value>.*)");
            if (match.Success)
            {
                string key = match.Groups["key"].Value;
                IConfigurationItem item;
                if (configuration.Items.TryGetValue(key, out item))
                {
                    string value = match.Groups["value"].Value;
                    if (item.TryUpdate(value))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
