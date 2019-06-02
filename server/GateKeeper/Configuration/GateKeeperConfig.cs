namespace GateKeeper.Configuration
{
    public class GateKeeperConfig
    {
        /// <summary>
        /// Secret key in appsettings.json
        /// </summary>
        public string EncryptionKey { get; set; }
        public byte[] Salt { get; set; }
    }
}