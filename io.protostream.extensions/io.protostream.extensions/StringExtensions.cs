using System.Text;

namespace io.protostream.extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a string with all special characters removed except those specified in the keep array.
        /// Inspired by: https://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
        /// </summary>
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
    }
}
