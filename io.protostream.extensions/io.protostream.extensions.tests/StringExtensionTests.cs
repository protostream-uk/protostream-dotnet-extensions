using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace io.protostream.extensions.tests
{
    public class StringExtensionTests
    {
        #region HexStringToByteArray
        /// <summary>
        /// An alternative Hex String To Byte Array function that is accurate but slow.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public byte[] HexStringToByteArrayLINQ(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        [Test]
        public void Test_HexStringToByteArray_ToAndBack()
        {
            string original = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            byte[] originalAsBytes = original.HexStringToByteArray();
            string result = originalAsBytes.ToHexString();

            Assert.AreEqual(original, result);
        }

        [Test]
        public void Test_HexStringToByteArray_ValidWithLinq()
        {
            string original = "aabcedf123456789";

            byte[] lib = original.HexStringToByteArray();
            byte[] slow = HexStringToByteArrayLINQ(original);

            Assert.True(lib.SequenceEqual(slow));
        }

        [Test]
        public void Test_HexStringToByteArray_ValidWithCalculated()
        {
            // aa = 170
            // bb = 187
            string original = "aabb";

            byte[] lib = original.HexStringToByteArray();
            byte[] comparison = new byte[2] { 170, 187 };

            Assert.True(lib.SequenceEqual(comparison));
        }

        [Test]
        public void Test_HexStringToByteArray_InvalidLength()
        {
            Exception ex = Assert.Throws<Exception>(
              delegate { "a".HexStringToByteArray(); });

            Assert.That(ex.Message, Is.EqualTo("The binary key cannot have an odd number of digits."));
        }

        [Test]
        public void Test_HexStringToByteArray_InvalidCharacter1()
        {
            Exception ex = Assert.Throws<Exception>(
              delegate { "a-".HexStringToByteArray(); });

            Assert.That(ex.Message, Is.EqualTo("Strings contains non-hex characters."));
        }

        [Test]
        public void Test_HexStringToByteArray_InvalidCharacter2()
        {
            Exception ex = Assert.Throws<Exception>(
              delegate { "##".HexStringToByteArray(); });

            Assert.That(ex.Message, Is.EqualTo("Strings contains non-hex characters."));
        }
        #endregion

        #region RemoveSpecialCharacters
        [Test]
        public void Test_RemoveSpecialCharacters_All()
        {
            string original = "this is a string!£$%^&*()_+-=";
            Assert.AreEqual("thisisastring", original.RemoveSpecialCharacters());
        }

        [Test]
        public void Test_RemoveSpecialCharacters_AllExceptSpace()
        {
            string original = "this is a string!£$%^&*()_+-=";
            Assert.AreEqual("this is a string", original.RemoveSpecialCharacters(' '));
        }

        [Test]
        public void Test_RemoveSpecialCharacters_AllExceptSome()
        {
            string original = "this is a string!£$%^&*()_+-=";
            Assert.AreEqual("this is a string%_", original.RemoveSpecialCharacters(' ', '_', '%'));
        }

        [Test]
        public void Test_RemoveSpecialCharacters_Numbers()
        {
            string original = "0123456789";
            Assert.AreEqual("0123456789", original.RemoveSpecialCharacters());
        }

        [Test]
        public void Test_RemoveSpecialCharacters_Uppercase()
        {
            string original = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ", original.RemoveSpecialCharacters());
        }

        [Test]
        public void Test_RemoveSpecialCharacters_Lowercase()
        {
            string original = "abcdefghijklmnopqrstuvwxyz";
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz", original.RemoveSpecialCharacters());
        }
        #endregion

        #region ToStream
        [Test]
        public void Test_ToStream_Valid()
        {
            string original = "this is a string";

            Stream stream = original.ToStream();

            Assert.AreEqual(original, stream.GetString());
        }
        #endregion
    }
}