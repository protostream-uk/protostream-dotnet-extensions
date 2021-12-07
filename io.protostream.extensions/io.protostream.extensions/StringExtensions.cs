using System;
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
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        /// <summary>
        /// Returns the integer value of a hex character.
        /// Inspired by: https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        private static int GetHexVal(char hex)
        {
            if (!hex.IsHex())
                throw new Exception("Strings contains non-hex characters.");

            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        /// <summary>
        /// Returns true if the character provided is hex.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsHex(this char c)
        {
            return (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || (c >= '0' && c <= '9');
        }
    }
}
