using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace io.protostream.extensions
{
    public static class StreamExtensions
    {
        /// <summary>
        /// Converts the a Stream into a UTF8 string
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static async Task<string> GetString(this Stream body)
        {
            using MemoryStream ms = new MemoryStream();
            await body.CopyToAsync(ms);
            byte[] requestBody = ms.ToArray();
            return Encoding.UTF8.GetString(requestBody);
        }

        /// <summary>
        /// Converts a Stream into a base64 string
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<string> ToBase64String(this Stream stream)
        {
            using MemoryStream ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            byte[] requestBody = ms.ToArray();
            return Convert.ToBase64String(requestBody);
        }
    }
}
