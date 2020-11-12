using ApiFrontEnd.Utils;
using NUnit.Framework;

namespace StackOverflowTests
{
    public class EncryptionTest
    {

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void TestEncryptedStringEqualsDerctyptedString()
        {
            string text = "some text";
            string encryptedText = text.Encrypt();
            string decryptedText = encryptedText.Decrypt();

            Assert.AreNotEqual(text, encryptedText);
            Assert.AreEqual(text, decryptedText);
        }


    }
}