using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AesEncoding.Tests
{
    [TestClass]
    public class EncoderTests
    {
        private static readonly byte[] PreSharedKey = { 0x37, 0x0e, 0x29, 0x1e, 0x31, 0x19, 0x75, 0x21, 0x2f, 0x19, 0x6a, 0x1c, 0x3d, 0x2b, 0x54, 0x47 };

        [TestMethod]
        public void Encode_Returns_Expected()
        {
            var input = System.Text.Encoding.UTF8.GetBytes("0202");
            var expectedOutput = "af17e3b519c18d73703afc3ed10d82f1";
            var key = PreSharedKey;

            var encoder = new Encoder();
            var actualOutput = encoder.BytesToHex(encoder.Encode(key, input));

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [TestMethod]
        public void Decode_Returns_Expected()
        {
            var expectedOutput = "0202";
            var key = PreSharedKey;

            var encoder = new Encoder();
            var input = encoder.StringToBytes("af17e3b519c18d73703afc3ed10d82f1");
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

            var key = PreSharedKey;
            var encoder = new Encoder();

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
