using System;
using System.Text;

namespace vtbbook.Core.Common
{
    public static class Cryptography
    {
        public static string SHA3(string text)
        {
            var hashAlgorithm = new Org.BouncyCastle.Crypto.Digests.Sha3Digest(256);

            byte[] input = Encoding.ASCII.GetBytes(text);

            hashAlgorithm.BlockUpdate(input, 0, input.Length);

            byte[] result = new byte[256 / 8];
            hashAlgorithm.DoFinal(result, 0);

            string hashString = BitConverter.ToString(result);
            hashString = hashString.Replace("-", "").ToLowerInvariant();

            return hashString;
        }
    }
}
