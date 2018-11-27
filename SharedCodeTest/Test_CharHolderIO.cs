using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.IO;
using System.Linq;
using TAPPLICATION.IO;

namespace SharedCodeTest
{
    [TestClass]
    public class Test_CharHolderIO
    {
        [TestMethod]
        public void SerializingSTDChar()
        {
            var Should = CharHolder.CreateCharWithStandardContent();
            var Is = CharHolderIO.Deserialize(SharedIO.Serialize(Should));
            TestHelper.CompareCharHolder(Should, Is);
        }
        [TestMethod]
        public void SerializingTestChar()
        {
            var Should = TestHelper.CreateTestChar();
            var Current = CharHolderIO.Deserialize(SharedIO.Serialize(Should));
            TestHelper.CompareCharHolder(Should, Current);
        }
        [TestMethod]
        public void SerializingFileChar()
        {
            var FileContent = File.ReadAllText(Environment.CurrentDirectory + @"\assets\Flash.SRHChar");
            var Should = CharHolderIO.Deserialize(FileContent);
            var Current = CharHolderIO.Deserialize(SharedIO.Serialize(Should));
            TestHelper.CompareCharHolder(Should, Current);
        }
    }
}
