using System;
using System.IO;
using System.Text;

namespace io.protostream.extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string with all special characters removed except those specified in the keep array.
        /// Inspired by: https://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
        /// </summary>
        /// <performance>10,000 samples, 9-12ms</performance>
        /// <param name="str">The input string to process.</param>
        /// <param name="keep">A list of Characters you want to keep when removing special characters.</param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(this string str, params char[] keep)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9')
                    || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }

                foreach (char kc in keep)
                {
                    if (c == kc)
                    {
                        sb.Append(c);
                    }
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Covnerts a hex string into a byte array.
        /// Inspired by: https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        /// </summary>
        /// <performance>10,000 samples, 9-11ms</performance>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(this string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits.");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)(((hex[i << 1]).GetHexVal() << 4) + (hex[(i << 1) + 1]).GetHexVal());
            }

            return arr;
        }

        /// <summary>
        /// Converts the string into a Stream
        /// </summary>
        /// <performance>10,000 samples, 6-12ms</performance>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream ToStream(this string s)
        {
            return s.ToStream(Encoding.UTF8);
        }

        /// <summary>
        /// Converts the string into a Stream
        /// </summary>
        /// <performance>10,000 samples, 6-12ms</performance>
        /// <param name="s"></param>
        /// <param name="encoding">The encoding of the string.</param>
        /// <returns></returns>
        public static Stream ToStream(this string s, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(s));
        }
    }
}
