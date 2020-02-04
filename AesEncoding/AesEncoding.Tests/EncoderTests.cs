using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AesEncoding.Tests
{
    [TestClass]
    public class EncoderTests
    {
        private static readonly string Key = "!SuperSecretKey!";

        [TestMethod]
        public void Encode_With_Salt_Returns_Expected()
        {
            var key = System.Text.Encoding.UTF8.GetBytes(Key);
            var input = System.Text.Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZXXSALTVALUEXX");
            var expectedOutput = "470000f77f36add3c3df434e34af2e745cdae05ebf43c75b0b94023f8c0b85c6e4521a7fa9f49e32bb0e3c32ab2a021f";
            var encoder = new Encoder();
            var actualOutput = encoder.BytesToHex(encoder.Encode(key, input));

            Assert.AreEqual(expectedOutput, actualOutput);
        }


        [TestMethod]
        public void Encode_Returns_Expected()
        {
            var key = System.Text.Encoding.UTF8.GetBytes(Key);
            var input = System.Text.Encoding.UTF8.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var expectedOutput = "470000f77f36add3c3df434e34af2e74f09c40bc3f143b28762c7a045cb93b3c";

            var encoder = new Encoder();
            var actualOutput = encoder.BytesToHex(encoder.Encode(key, input));

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void Decode_Returns_Expected()
        {
            var key = System.Text.Encoding.UTF8.GetBytes(Key);
            var expectedOutput = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var encoder = new Encoder();
            var input = encoder.StringToBytes("470000f77f36add3c3df434e34af2e74f09c40bc3f143b28762c7a045cb93b3c");

            var actualOutput = System.Text.Encoding.UTF8.GetString(encoder.Decode(key, input));

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void BytesToHex_Returns_Expected()
        {
            var input = System.Text.Encoding.UTF8.GetBytes("Hello World");
            var expectedOutput = "48656c6c6f20576f726c64";

            var encoder = new Encoder();
            var actualOutput = encoder.BytesToHex(input);

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void CanDecode_When_EncodedBySelf()
        {
            var messageText = "Hello World";
            var messageBytes = System.Text.Encoding.UTF8.GetBytes(messageText);
            var encoder = new Encoder();
            var key = System.Text.Encoding.UTF8.GetBytes(Key);

            var encodedMessageBytes = encoder.Encode(key, messageBytes);

            var decodedMessageBytes = encoder.Decode(key, encodedMessageBytes);
            var decodedMessageText = System.Text.Encoding.UTF8.GetString(decodedMessageBytes);

            Assert.AreEqual(messageText, decodedMessageText);
        }

        [TestMethod]
        public void StringToBytes_Returns_Expected()
        {
            var input = "0A0D";
            var actualOutput = new Encoder().StringToBytes(input);
            var expectedOutput = new byte[] { 0xA, 0xD };

            CollectionAssert.AreEqual(expectedOutput, actualOutput);
        }

    }
}
