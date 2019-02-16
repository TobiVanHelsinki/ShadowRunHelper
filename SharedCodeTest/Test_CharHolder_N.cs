using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
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
            Char.Add(new Vorteil() { Bezeichner = "Vorteil1" });

            H1.Wert2.AddConnected(Char.CTRLAttribut.Charisma.Wert2);
            H1.Wert2.AddConnected(Char.CTRLVorteil[0].Wert2);
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
            Char.Add(new Vorteil() { Bezeichner = "Vorteil1" });

            H1.Wert2.AddConnected(Char.CTRLAttribut.Charisma.Wert2);
            H1.Wert2.AddConnected(Char.CTRLVorteil[0].Wert2);

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

            Char.CTRLVorteil.Data[0].Wert2.BaseValue++;

            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert2.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 2);

            Char.CTRLHandlung.Data[0].Wert2.BaseValue++;

            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.BaseValue == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert2.Value == 3);
        }

    }
}
