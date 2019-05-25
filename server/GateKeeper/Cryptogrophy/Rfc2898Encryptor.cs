using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GateKeeper.Cryptogrophy
{
    /// <summary>
    /// Contains logic to encrypt and decrypt a string. Note that the same
    /// encryption key and salt that was used to encrypt a string must be used
    /// to decrypt it.
    /// </summary>
    public class Rfc2898Encryptor : ICryptor
    {
        public string Encrypt(string raw, string encryptionKey, byte[] salt)
        {
            string encryptString = raw;
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using(Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)) {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        /// <summary>
        /// Decrypts the string using the encryption key and salt. If the
        /// encryption key or salt are incorrect, a <see cref="CroptographicException" />
        /// is thrown.
        /// </summary>
        public string Decrypt(string encrypted, string encryptionKey, byte[] salt)   
        {
            string decrypted = encrypted.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(decrypted);
            using(Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, salt);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)) {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    decrypted = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return decrypted;
        } 
    }
}