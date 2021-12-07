using NUnit.Framework;

namespace io.protostream.extensions.tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

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
    }
}