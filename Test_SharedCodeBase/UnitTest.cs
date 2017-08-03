using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShadowRunHelper.Model;
using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using TLIB.Model;

namespace Test_SharedCodeBase
{
    //[TestClass]
    //public class UnitTestHelper
    //{
    //    public static CharHolder PrepareBigChar()
    //    {
    //        CharHolder Char = new CharHolder();
    //        List<Thing> lstAll = new List<Thing>();
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Char.Add(ShadowRunHelper.ThingDefs.Adeptenkraft_KomplexeForm);
    //            Char.Add(ShadowRunHelper.ThingDefs.Connection);
    //            Char.Add(ShadowRunHelper.ThingDefs.CyberDeck);
    //            Char.Add(ShadowRunHelper.ThingDefs.Eigenschaft);
    //            Char.Add(ShadowRunHelper.ThingDefs.Fertigkeit);
    //            Char.Add(ShadowRunHelper.ThingDefs.Foki_Widgets);
    //            Char.Add(ShadowRunHelper.ThingDefs.Geist_Sprite);
    //            Char.Add(ShadowRunHelper.ThingDefs.Handlung);
    //            Char.Add(ShadowRunHelper.ThingDefs.Implantat);
    //            Char.Add(ShadowRunHelper.ThingDefs.Item);
    //            Char.Add(ShadowRunHelper.ThingDefs.Kommlink);
    //            Char.Add(ShadowRunHelper.ThingDefs.Munition);
    //            Char.Add(ShadowRunHelper.ThingDefs.Nachteil);
    //            Char.Add(ShadowRunHelper.ThingDefs.Nahkampfwaffe);
    //            Char.Add(ShadowRunHelper.ThingDefs.Panzerung);
    //            Char.Add(ShadowRunHelper.ThingDefs.Programm);
    //            Char.Add(ShadowRunHelper.ThingDefs.Sin);
    //            Char.Add(ShadowRunHelper.ThingDefs.Strömung_Wandlung);
    //            Char.Add(ShadowRunHelper.ThingDefs.Tradition_Initiation);
    //            Char.Add(ShadowRunHelper.ThingDefs.Vehikel);
    //            Char.Add(ShadowRunHelper.ThingDefs.Vorteil);
    //            Char.Add(ShadowRunHelper.ThingDefs.Zaubersprüche);
    //        }
    //        return Char;
    //    }
    //}

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Controllers()
        {
            Assert.AreEqual(0, 0);
            CharHolder Char = new CharHolder();
            List<Thing> lstAll = new List<Thing>();
            for (int i = 0; i < 10; i++)
            {
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Adeptenkraft_KomplexeForm));
                Assert.ThrowsException<System.NotSupportedException>(() => Char.Add(ShadowRunHelper.ThingDefs.Attribut));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Connection));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.CyberDeck));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Eigenschaft));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Fertigkeit));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Foki_Widgets));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Geist_Sprite));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Handlung));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Implantat));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Item));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Kommlink));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Munition));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Nachteil));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Nahkampfwaffe));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Panzerung));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Programm));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Sin));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Strömung_Wandlung));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Tradition_Initiation));
                Assert.ThrowsException<System.NotSupportedException>(() => Char.Add(ShadowRunHelper.ThingDefs.Undef));
                Assert.ThrowsException<System.NotSupportedException>(() => Char.Add(ShadowRunHelper.ThingDefs.UndefTemp));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Vehikel));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Vorteil));
                lstAll.Add(Char.Add(ShadowRunHelper.ThingDefs.Zaubersprüche));
            }
        }
    }

    //[TestClass]
    //public class CharIO
    //{
    //    [TestMethod]
    //    public async void CharSaveLoad()
    //    {
    //        CharHolder Char = UnitTestHelper.PrepareBigChar();
    //        CharHolder Char2 = UnitTestHelper.PrepareBigChar();
    //        await Test_SharedIO<CharHolder>.Save(Char, TLIB.IO.UserDecision.ThrowError, new TLIB.IO.FileInfoClass { Filepath = "UnitTest", Fileplace = TLIB.IO.Place.Local, Filename = "test" });
    //        Char2 = await Test_SharedIO<CharHolder>.Load(new TLIB.IO.FileInfoClass { Filepath = "UnitTest", Fileplace = TLIB.IO.Place.Local, Filename = "test" },eUD:TLIB.IO.UserDecision.ThrowError);
    //        Assert.AreEqual(Char, Char2);
    //    }
    //}

    //[TestClass]
    //public class Test_SharedIO<T> : TLIB.IO.SharedIO<T> where T : IMainType, new()
    //{
    //    public void Convert()
    //    {
    //    }
    //}
}
