
using ShadowRunHelper.CharModel;
using SharedCode.Resources;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using TLIB;

namespace ShadowRunHelper.CharController
{
    /// <summary>
    /// Use this Controller, if you want to maintain your own data collection (MyData)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ShadowRunHelper.CharController.Controller{T}" />
    public class OwnDataController<T> : Controller<T> where T : Thing, new()
    {
        //protected ObservableCollection<T> MyData;

        #region Override Controller
        public override ObservableCollection<T> Data { get => new ObservableCollection<T>(OwnThings.Select(x => x.GetValue(this)).OfType<T>()); protected set { } }
        #endregion Override Controller

        public OwnDataController() : base()
        {
            RefreshIdentifiers();
            //Data = new ObservableCollection<T>();
            //Data.AddRange(OwnThings.Select(x => x.GetValue(this)).OfType<T>());
        }

        private IEnumerable<PropertyInfo> OwnThings => GetType().GetProperties().Where(x => x.PropertyType == typeof(T) && x.CanRead && x.CanWrite);

        /// <summary>
        /// Refreshes the identifiers of the things with the ModelResources
        /// </summary>
        /// <param name="controller">The controller.</param>
        public void RefreshIdentifiers()
        {
            foreach (var item in OwnThings)
            {
                if (item.GetValue(this) is T thing)
                {
                    thing.Bezeichner = ModelResources.ResourceManager.GetStringSafe(typeof(T).Name + "_" + item.Name);
                }
            }
        }
    }
}