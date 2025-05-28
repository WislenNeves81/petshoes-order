using System.Security.Cryptography;

namespace Adapter.Email.Common
{
    public class Criptography
    {
        public static byte[] Encrypt(string plainText, byte[] key, byte[] vector)
        {
            byte[] encrypted;
            using (var aes = new AesManaged())
            {
                var encryptor = aes.CreateEncryptor(key, vector);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                            sw.Write(plainText);

                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = string.Empty;

            using (var aes = new AesManaged())
            {
                var decryptor = aes.CreateDecryptor(Key, IV);

                using (var ms = new MemoryStream(cipherText))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }

            return plaintext;
        }
    }
}
