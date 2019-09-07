namespace GateKeeper.Cryptogrophy
{
    public interface ICryptor
    {
        string Encrypt(string raw, string encryptionKey, byte[] salt);

        string Decrypt(string encrypted, string encryptionKey, byte[] salt);
    }
}