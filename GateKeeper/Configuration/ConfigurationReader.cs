using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace GateKeeper.Configuration
{
    public class ConfigurationReader
    {
        public static GateKeeperConfig FromAppConfiguration(IConfiguration config)
        {
            string saltText = config.GetSection("GateKeeper")["Salt"];
            GateKeeperConfig gateKeeperConfig = new GateKeeperConfig() {
                EncryptionKey = config.GetSection("GateKeeper")["SecretKey"],
                Salt = Encoding.ASCII.GetBytes(saltText)
            };
            return gateKeeperConfig;
        }
    }
}