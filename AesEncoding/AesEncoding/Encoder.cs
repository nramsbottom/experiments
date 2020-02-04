using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AesEncoding
{
    public class Encoder
    {
        public byte[] Encode(byte[] key, byte[] data)
        {
            byte[] iv = new byte[16];

            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.ECB;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.Close();
                    }

                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] Decode(byte[] key, byte[] data)
        {
            byte[] iv = new byte[16];
            byte[] buffer = data;

            using Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var output = new MemoryStream();
            using var memoryStream = new MemoryStream(buffer);
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(output);
            }

            return output.ToArray();
        }

        public string BytesToHex(byte[] data)
        {
            var sb = new StringBuilder();
            for (var n = 0; n < data.Length; n++)
                sb.Append(data[n].ToString("x2"));
            return sb.ToString();
        }

        public byte[] StringToBytes(string s)
        {
            return Enumerable.Range(0, s.Length)
                                .Where(x => x % 2 == 0)
                                .Select(x => Convert.ToByte(s.Substring(x, 2), 16))
                                .ToArray();
        }
    }

}
