using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;

namespace io.protostream.extensions.tests
{
    public class StreamExtensionTests
    {
        #region GetString
        [Test]
        public async Task Test_GetString_ValidAsync()
        {
            string original = "this is a string";

            Stream stream = original.ToStream();

            Assert.AreEqual(original, await stream.GetString());
        }
        #endregion

        #region ToBase64String
        [Test]
        public async Task Test_ToBase64String_ValidAsync()
        {
            string original = "this is a string";
            string expected = "dGhpcyBpcyBhIHN0cmluZw==";

            Stream stream = original.ToStream();

            Assert.AreEqual(expected, await stream.ToBase64String());
        }
        #endregion
    }
}