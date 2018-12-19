using System.Linq;

namespace ShadowRunHelper.Model
{
    public static class CharHolderTest
    {
        public static CharHolder TestAllCats(int count = 1)
        {
            var CH = new CharHolder();
            var c = 0;
            foreach (var item in TypeHelper.ThingTypeProperties.Where(x => x.Usable))
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        var newThing = CH.Add(item.ThingType);
                        newThing.Bezeichner = "Test";
                        newThing.Notiz = @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.";
                        newThing.Typ = item.Type.Name;
                        newThing.Wert = c++;
                        newThing.Zusatz = TLIB.StaticRandom.Next(0, 2) == 0 ? "-1" : "+1";
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                }
            }
            return CH;
        }
    }
}
