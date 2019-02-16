using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System.Linq;
using TAPPLICATION.IO;

namespace SharedCodeTest
{
    public static class TestHelper
    {
        public static CharHolder CreateTestChar()
        {
            CharHolder Ret = new CharHolder();
            var H1 = new Handlung() { Bezeichner = "Handlung1" };
            Ret.Add(H1);
            Ret.Add(new Vorteil() { Bezeichner = "Vorteil1" });

            H1.LinkedThings.Add(Ret.LinkList.First(x => x.Object == Ret.CTRLAttribut.Charisma));
            H1.LinkedThings.Add(Ret.LinkList.First(x => x.Object == Ret.CTRLAttribut.Logik));
            H1.GegenZusammensetzung.Add(Ret.LinkList.First(x => x.Object == Ret.CTRLVorteil[0]));

            return Ret;
        }
        public static CharHolder SaveLoadChar(CharHolder Char)
        {
            return CharHolderIO.Deserialize(SharedIO.Serialize(Char));
        }

        public static void CompareCharHolder(CharHolder CharA, CharHolder CharB)
        {
            Assert.AreEqual(CharA.HasChanges, CharB.HasChanges);

            Assert.AreEqual(CharA.Settings.CategoryOptions.Count(), CharB.Settings.CategoryOptions.Count());
            foreach (var (CatA, CatB) in CharA.Settings.CategoryOptions.Join(CharB.Settings.CategoryOptions, x => x.ThingType, x => x.ThingType, (x, y) => (x, y)))
            {
                Assert.AreEqual(CatA.Order, CatB.Order);
                Assert.AreEqual(CatA.Pivot, CatB.Pivot);
                Assert.AreEqual(CatA.ThingType, CatB.ThingType);
                Assert.AreEqual(CatA.Visibility, CatB.Visibility);
            }
            Assert.AreEqual(CharA.Favorites.Count(), CharB.Favorites.Count());
            foreach (var (A, B) in CharA.Favorites.Zip(CharB.Favorites, (x, y) => (x, y)))
            {
                CompareThing(A, B);
            }
            foreach (var item in Thing.GetProperties(CharA.Person))
            {
                object O1 = item.GetValue(CharA.Person);
                object O2 = item.GetValue(CharB.Person);
                Assert.IsTrue(O1 == O2 || O1.Equals(O2)); // == does just a reference comp
            }
            foreach (var (CTRLA, CTRLB) in CharA.CTRLList.Zip(CharB.CTRLList, (a, b) => (a, b)))
            {
                Assert.AreEqual(CTRLA.eDataTyp, CTRLB.eDataTyp);
                Assert.AreEqual(CTRLA.GetElements().Count(), CTRLB.GetElements().Count());
                foreach (var (El1, El2) in CTRLA.GetElements().Zip(CTRLB.GetElements(), (a, b) => (a, b)))
                {
                    CompareThing(El1, El2);
                }
            }
        }
        public static void CompareThing(Thing El1, Thing El2)
        {
            if (El1 == null && El2 == null)
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                return;
            }
            foreach (var item in Thing.GetProperties(El1))
            {
                object O1 = item.GetValue(El1);
                object O2 = item.GetValue(El2);
                Assert.IsTrue(O1 == O2 || O1.Equals(O2)); // == does just a reference comp
            }
        }
    }
}
