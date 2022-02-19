
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System.Linq;
using ShadowRunHelper.IO;

namespace SharedCodeTest
{
    public static class TestHelper
    {
        public static CharHolder CreateTestChar()
        {
            CharHolder myChar = new CharHolder();
            var H1 = new Handlung() { Bezeichner = "Handlung1" };
            myChar.Add(H1);
            myChar.Add(new Vorteil() { Bezeichner = "Vorteil1" });
            myChar.RefreshLists();  
            H1.Value.Connected.Add(myChar.Connects.First(x => x.Owner == myChar.CTRLAttribut.Charisma));
            H1.Value.Connected.Add(myChar.Connects.First(x => x.Owner == myChar.CTRLAttribut.Logik));
            H1.Against.Connected.Add(myChar.Connects.First(x => x.Owner == myChar.CTRLVorteil[0]));

            return myChar;
        }

        public static CharHolder SaveOpen(CharHolder Char)
        {
            return CharHolderIO.Deserialize(SharedIO.Serialize(Char));
        }

        public static void CompareCharHolder(CharHolder CharA, CharHolder CharB)
        {
            //Assert.AreEqual(CharA.HasChanges, CharB.HasChanges);

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
            foreach (var item in TLIB.ReflectionHelper.GetProperties(CharA.Person, typeof(Used_UserAttribute)))
            {
                object O1 = item.GetValue(CharA.Person);
                object O2 = item.GetValue(CharB.Person);
                Assert.IsTrue(O1 == O2 || O1.Equals(O2)); // == does just a reference comp
            }
            foreach (var (CTRLA, CTRLB) in CharA.Controlers.Zip(CharB.Controlers, (a, b) => (a, b)))
            {
                Assert.AreEqual(CTRLA.eDataTyp, CTRLB.eDataTyp);
                Assert.AreEqual(CTRLA.GetAllData().Count(), CTRLB.GetAllData().Count());
                foreach (var (El1, El2) in CTRLA.GetAllData().Zip(CTRLB.GetAllData(), (a, b) => (a, b)))
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
            foreach (var item in El1.GetProperties().Where(x => x.PropertyType != typeof(ConnectProperty)))
            {
                object O1 = item.GetValue(El1);
                object O2 = item.GetValue(El2);
                Assert.IsTrue(O1 == O2 || O1.Equals(O2)); // == does just a reference comp
            }
            foreach (var item in El1.GetProperties().Where(x => x.PropertyType == typeof(ConnectProperty)))
            {
                var O1 = (ConnectProperty)item.GetValue(El1);
                var O2 = (ConnectProperty)item.GetValue(El2);
                Assert.AreEqual(O1.BaseValue, O2.BaseValue);
                Assert.AreEqual(O1.Value, O2.Value);
                Assert.AreEqual(O1.Connected.Count, O2.Connected.Count);
                if (O1.Active != O2.Active)
                {
                    if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
                }
                Assert.AreEqual(O1.Active, O2.Active);
            }
        }
    }
}