using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using ShadowRunHelper.IO;

namespace SharedCodeTest
{
    [TestClass]
    public class Test_CharHolder_N
    {
        [TestMethod]
        public void TestOfAddition_N()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();

            CharHolder Char = new CharHolder();
            var H1 = new Handlung() { Bezeichner = "Handlung1" };
            Char.Add(H1);
            Char.Add(new Item() { Bezeichner = "Item" });

            H1.Value.Connected.Add(Char.CTRLAttribut.Charisma.Value);
            H1.Value.Connected.Add(Char.CTRLItem[0].Value);
            TestNewConnections(Char);
        }

        [TestMethod]
        public void TestLoadAndSave_N()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();
            CharHolder Char = new CharHolder();
            var H1 = new Handlung() { Bezeichner = "Handlung1" };
            Char.Add(H1);
            Char.Add(new Item() { Bezeichner = "Item" });

            H1.Value.Connected.Add(Char.CTRLAttribut.Charisma.Value);
            H1.Value.Connected.Add(Char.CTRLItem[0].Value);

            string Ser = SharedIO.Serialize(Char);
            TestNewConnections(CharHolderIO.Deserialize(Ser));
        }
        private static void TestNewConnections(CharHolder Char)
        {
            Char.CTRLAttribut.Charisma.Value.BaseValue++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLAttribut.Charisma.Value.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);

            Char.CTRLItem.Data[0].Value.BaseValue++;

            Assert.IsTrue(Char.CTRLItem.Data[0].Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);

            Char.CTRLItem.Data[0].Aktiv = true;

            Assert.IsTrue(Char.CTRLItem.Data[0].Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Value.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 2);

            Char.CTRLHandlung.Data[0].Value.BaseValue++;

            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 3);

            Char.CTRLItem.Data[0].Aktiv = false;

            Assert.IsTrue(Char.CTRLItem.Data[0].Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.BaseValue == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 2);
        }


        [TestMethod]
        public void IConvertable_Test()
        {
            var t = new Handlung();
            t.Value.BaseValue = 5;

            ConnectProperty tdp = 5;
            Assert.AreEqual(tdp.Value, t.Value.Value);
        }


        [TestMethod]
        public void AktiveTest()
        {
            var t = new Item();
            t.Aktiv = true;
            t.Value.BaseValue = 5;

            ConnectProperty tdp = 5;
            Assert.AreEqual(tdp.Value, t.Value.Value);
        }

    }
}
