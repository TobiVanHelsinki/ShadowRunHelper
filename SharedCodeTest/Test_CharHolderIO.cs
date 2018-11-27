using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.CharModel;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using System;
using System.Linq;
using TAPPLICATION.IO;
using TLIB;

namespace SharedCodeTest
{
    [TestClass]
    public class Test_CharHolderIO
    {
        [TestMethod]
        public void Serialize()
        {
            var Soll = CharHolder.CreateCharWithStandardContent();
            var SollAsString = SharedIO.Serialize(Soll);
            var Ist = CharHolderIO.Deserialize(SollAsString);
            CompareCharHolder(Soll, Ist);
        }

        static void CompareCharHolder(CharHolder soll, CharHolder ist)
        {
            foreach (var (A,B) in soll.CTRLList.Zip(ist.CTRLList, (a, b) => (a, b)))
            {
                Assert.AreEqual(A.eDataTyp, B.eDataTyp);
                Assert.AreEqual(A.GetElements().Count(), B.GetElements().Count());
                foreach (var (E1, E2) in A.GetElements().Zip(B.GetElements(), (a, b) => (a, b)))
                {
                    foreach (var item in Thing.GetProperties(E1))
                    {
                        object O1 = item.GetValue(E1);
                        object O2 = item.GetValue(E2);
                        Assert.IsTrue(O1 == O2 || O1.Equals(O2)); // == does just a reference comp
                    }
                }
            }
        }
    }
}
