using NUnit.Framework;
using System;
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

        #region SHA
        [Test]
        public void Test_SHA512_HexString()
        {
            string text = "test";
            // "test" sha-512 using online tool https://passwordsgenerator.net/sha512-hash-generator/
            string expectedResult = "EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF";
            Assert.AreEqual(expectedResult, text.SHA512().ToHexString());
        }

        [Test]
        public void Test_HMACSHA512_HexString()
        {
            string text = "test";
            string secretKey = "secret";
            // "test" sha-512 using online tool https://www.freeformatter.com/hmac-generator.html#ad-output
            string expectedResult = "f8a4f0a209167bc192a1bffaa01ecdb09e06c57f96530d92ec9ccea0090d290e55071306d6b654f26ae0c8721f7e48a2d7130b881151f2cec8d61d941a6be88a".ToUpper();
            Assert.AreEqual(expectedResult, text.HMACSHA512(secretKey).ToHexString());
        }
        #endregion
    }
}