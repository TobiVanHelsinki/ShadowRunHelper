
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper;
using ShadowRunHelper.Model;

namespace SharedCodeTest
{
    [TestClass]
    public class Test_CharHolder
    {
        [TestMethod]
        public void TestOfAddition()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();
            var Char = TestHelper.CreateTestChar();
            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 0);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Gegen == 0);

            Char.CTRLAttribut.Charisma.Wert++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Gegen == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);

            Char.CTRLVorteil.Data[0].Wert++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].GegenCalced == 1);
        }
    }
}