using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace GateKeeper.Configuration
{
    public class ConfigurationReader
    {
        public const string ConfigurationKey = "GateKeeper";
        
        public static GateKeeperConfig FromAppConfiguration(IConfiguration config)
        {
            string saltText = config.GetSection(ConfigurationKey)["Salt"];
            GateKeeperConfig gateKeeperConfig = new GateKeeperConfig() {
                EncryptionKey = config.GetSection(ConfigurationKey)["SecretKey"],
                Salt = Encoding.ASCII.GetBytes(saltText)
            };
            return gateKeeperConfig;
        }
    }
}