using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SoccerX.Common.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string Base64Key = Environment.GetEnvironmentVariable("EncryptKeys") ?? "hYscnZJxS5M+6R4UeJvJ9nbG58J8XZ/y0hn2u+0BF4E="; // 32-Byte Base64 AES-256 Key


        /// <summary>
        /// Encrypts a given text using AES-256 encryption.
        /// </summary>
        /// <param name="plainText">The input text to encrypt.</param>
        /// <returns>Base64-encoded encrypted string.</returns>
        public static string Encrypt(this string plainText)
        {
            byte[] key = Convert.FromBase64String(Base64Key);
            byte[] iv = new byte[16]; // AES IV is always 16 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv); // Generate random IV
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(iv, 0, iv.Length); // Store IV at the beginning
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        /// <summary>
        /// Decrypts a given AES-256 encrypted text.
        /// </summary>
        /// <param name="cipherText">The Base64-encoded encrypted string.</param>
        /// <returns>The decrypted string.</returns>
        public static string Decrypt(this string cipherText)
        {
            byte[] key = Convert.FromBase64String(Base64Key);
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16]; // AES IV is always 16 bytes
            Array.Copy(fullCipher, 0, iv, 0, iv.Length); // Extract IV from the encrypted data
            byte[] cipher = new byte[fullCipher.Length - iv.Length];
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                using (var msDecrypt = new MemoryStream(cipher))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }
    }
}
