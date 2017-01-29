using ShadowRunHelper.CharModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ShadowRunHelper.CharController
{
    public interface IController<T>
    {
        T AddNewThing();
        void RemoveThing(T tRem);

        /// <summary>
        /// To populate the "All Things" List - will be overridden by singlecontroller
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<Thing, string>> GetElementsForThingList();

    }
}