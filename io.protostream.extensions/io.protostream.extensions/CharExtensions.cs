using System;

namespace io.protostream.extensions
{
    public static class CharExtensions
    {

        /// <summary>
        /// NOT UNIT TESTED .: SET TO INTERNAL
        /// Returns the integer value of a hex character.
        /// Inspired by: https://stackoverflow.com/questions/321370/how-can-i-convert-a-hex-string-to-a-byte-array
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        internal static int GetHexVal(this char hex)
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
        /// NOT UNIT TESTED .: SET TO INTERNAL
        /// Returns true if the character provided is hex.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        internal static bool IsHex(this char c)
        {
            return (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F') || (c >= '0' && c <= '9');
        }
    }
}
