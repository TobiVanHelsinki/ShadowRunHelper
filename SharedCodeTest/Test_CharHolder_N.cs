using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using TAPPLICATION.IO;

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

            H1.Wert2.Connected.Add(Char.CTRLAttribut.Charisma.Wert2);
            H1.Wert2.Connected.Add(Char.CTRLItem[0].Wert2);
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

            H1.Wert2.Connected.Add(Char.CTRLAttribut.Charisma.Wert2);
            H1.Wert2.Connected.Add(Char.CTRLItem[0].Wert2);

            string Ser = SharedIO.Serialize(Char);
            TestNewConnections(CharHolderIO.Deserialize(Ser));
        }
        private static void TestNewConnections(CharHolder Char)
        {
            Char.CTRLAttribut.Charisma.Wert2.BaseValue++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert2.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 1);

            Char.CTRLItem.Data[0].Wert2.BaseValue++;

            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 1);

            Char.CTRLItem.Data[0].Aktiv = true;

            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 2);

            Char.CTRLHandlung.Data[0].Wert2.BaseValue++;

            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 3);

            Char.CTRLItem.Data[0].Aktiv = false;

            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLItem.Data[0].Wert2.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 2);
        }


        [TestMethod]
        public void IConvertable_Test()
        {
            var t = new Handlung();
            t.Wert2.BaseValue = 5;

            CharProperty tdp = 5;
            Assert.AreEqual(tdp.Value, t.Wert2.Value);
        }


        [TestMethod]
        public void AktiveTest()
        {
            var t = new Item();
            t.Wert2.BaseValue = 5;

            CharProperty tdp = 5;
            Assert.AreEqual(tdp.Value, t.Wert2.Value);
        }

    }
}
