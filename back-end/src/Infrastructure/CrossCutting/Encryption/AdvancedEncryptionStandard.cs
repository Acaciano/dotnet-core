using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.CrossCutting.Encryption
{
    public class AdvancedEncryptionStandard
    {
        private static byte[] _chave = { };
        private static readonly byte[] Iv = { 12, 34, 56, 78, 90, 102, 114, 126 };
        private const string EncryptionKey = "AASF9A54";

        public static string GetSha1Hash(string value)
        {
            var hasher = SHA1.Create();
            var encoding = new ASCIIEncoding();
            var array = encoding.GetBytes(value);

            array = hasher.ComputeHash(array);

            var strHexa = new StringBuilder();

            foreach (var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();
        }

        public static string Base64Encode(object value)
        {
            try
            {
                value = value != null ? string.Format("{0}{1}", value, EncryptionKey) : new object();

                var plainTextBytes = Encoding.UTF8.GetBytes(value.ToString());
                return Convert.ToBase64String(plainTextBytes);
            }
            catch
            {
                return string.Empty;
            }
        }
        
        public static string Base64Decode(object value)
        {
            try
            {
                var base64EncodedBytes = Convert.FromBase64String(value.ToString());
                return Encoding.UTF8.GetString(base64EncodedBytes).Replace(EncryptionKey,"");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
