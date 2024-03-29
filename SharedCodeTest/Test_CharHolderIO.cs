﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.IO;
using System.Linq;
using ShadowRunHelper.IO;
using ShadowRunHelper;

namespace SharedCodeTest
{
    [TestClass]
    public class Test_CharHolderIO
    {
        [TestMethod]
        public void SerializingSTDChar()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();

            var Should = CharHolderGenerator.CreateCharWithStandardContent();
            var Is = CharHolderIO.Deserialize(SharedIO.Serialize(Should));
            TestHelper.CompareCharHolder(Should, Is);
        }
        [TestMethod]
        public void SerializingTestChar()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();
            var Should = TestHelper.CreateTestChar();
            var Current = CharHolderIO.Deserialize(SharedIO.Serialize(Should));
            TestHelper.CompareCharHolder(Should, Current);
        }
        [TestMethod]
        public void SerializingFileChar()
        {
            AppModel.Initialize();
            _ = SettingsModel.Initialize();
            var FileContent = File.ReadAllText(Environment.CurrentDirectory + @"\assets\Flash.SRHChar");
            var Expected = CharHolderIO.Deserialize(FileContent);
            string fileContent = SharedIO.Serialize(Expected);
            var Actual = CharHolderIO.Deserialize(fileContent);
            TestHelper.CompareCharHolder(Expected, Actual);
        }
    }
}
