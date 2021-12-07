using NUnit.Framework;
using System;
using System.Text;

namespace io.protostream.extensions.tests
{
    public class ByteExtensionTests
    {
        #region ToHexString
        /// <summary>
        /// An alternative byte to Hex String function that is accurate but slow.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:X2}", b);
            }
            return hex.ToString();
        }

        [Test]
        public void Test_ToHexString_ToAndBack()
        {
            string original = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            byte[] originalAsBytes = original.HexStringToByteArray();
            string result = originalAsBytes.ToHexString();

            Assert.AreEqual(original, result);
        }

        [Test]
        public void Test_ToHexString_ValidWithSlow()
        {
            byte[] original = new byte[] { 150, 170, 187 };

            string lib = original.ToHexString();
            string slow = ByteArrayToHexString(original);

            Assert.AreEqual(slow, lib);
        }

        [Test]
        public void Test_ToHexString_ValidWithCalculated()
        {
            byte[] original = new byte[] { 150, 170, 187 };

            string lib = original.ToHexString();
            string calculated = "96AABB";

            Assert.AreEqual(calculated, lib);
        }
        #endregion
    }
}