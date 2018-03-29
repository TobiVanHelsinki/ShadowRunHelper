using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.Model;
using System.Linq;

namespace SharedCodeTests
{
    public static class Helper
    {
        public static CharHolder CreatetestChar()
        {
            CharHolder Ret = new CharHolder();
            var H1 = new Handlung() { Bezeichner = "Handlung1" };
            Ret.Add(H1);
            var V1 = new Vorteil() { Bezeichner = "Vorteil1" };
            Ret.Add(V1);

            H1.LinkedThings.Add(Ret.LinkList.First(x => x.Object == Ret.CTRLAttribut.Charisma));
            H1.LinkedThings.Add(Ret.LinkList.First(x => x.Object == Ret.CTRLAttribut.Logik));
            H1.GegenZusammensetzung.Add(Ret.LinkList.First(x => x.Object == V1));

            return Ret;
        }
    }
    [TestClass]
    public class ConnectionTests
    {
        [TestMethod]
        public void TestOfAddition()
        {
            AppModel.Initialize();
            SettingsModel.Initialize();
            var Char = Helper.CreatetestChar();
            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 0);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].WertCalced == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Gegen == 0);

            Char.CTRLAttribut.Charisma.Wert++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Gegen == 0);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].WertCalced == 1);

            Char.CTRLVorteil.Data[0].Wert++;

            Assert.IsTrue(Char.CTRLAttribut.Charisma.Wert == 1);
            Assert.IsTrue(Char.CTRLAttribut.Logik.Wert == 0);
            Assert.IsTrue(Char.CTRLVorteil.Data[0].Wert == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Wert == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].WertCalced == 1);
            Assert.IsTrue(Char.CTRLHandlung.Data[0].Gegen == 1);
        }
    }
}
