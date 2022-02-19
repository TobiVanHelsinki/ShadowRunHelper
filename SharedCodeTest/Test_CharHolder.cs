
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
            Assert.IsTrue(Char.CTRLAttribut.Charisma.Value.Value == 0);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Value.Value == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Against.Value == 0);

            Char.CTRLAttribut.Charisma.Value.BaseValue++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Value.Value == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Value.Value == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Value.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Against.Value == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);

            Char.CTRLVorteil.Data[0].Value.BaseValue++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Value.Value == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Value.Value == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Value.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Value.Value == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Against.Value == 1);
        }
    }
}