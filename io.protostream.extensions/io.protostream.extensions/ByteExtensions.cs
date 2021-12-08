using System.Text;

namespace io.protostream.extensions
{
    public static class ByteExtensions
    {
        private static readonly uint[] _lookup32 = CreateLookup32();

        /// <summary>
        /// Creates byte lookup table for converting bytes into hex strings
        /// Copied from: https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static uint[] CreateLookup32()
        {
            var result = new uint[256];
            for (int i = 0; i < 256; i++)
            {
                string s = i.ToString("X2");
                result[i] = ((uint)s[0]) + ((uint)s[1] << 16);
            }
            return result;
        }

        /// <summary>
        /// Converts a byte array to a hex string.
        /// Copied from: https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(this byte[] bytes)
        {
            var lookup32 = _lookup32;
            var result = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                var val = lookup32[bytes[i]];
                result[2 * i] = (char)val;
                result[2 * i + 1] = (char)(val >> 16);
            }
            return new string(result);
        }

        /// <summary>
        /// Converts a byte array into a UTF8 string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUtf8String(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
