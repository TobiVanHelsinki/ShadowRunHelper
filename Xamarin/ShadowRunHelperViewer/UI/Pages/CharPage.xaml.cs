//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.Model;
using ShadowRunHelperViewer.UI.Pages;
using SharedCode.Ressourcen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TAPPLICATION.IO;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class CharPage : ContentView, IDisposable
    {
        public AppModel AppModel => AppModel.Instance;

        public CharPage()
        {
            InitializeComponent();
            BindingContext = this;
            Thread.Sleep(50); //Enable Waiting INdicator
        }

        public virtual void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool t)
        {
            if (Content is Grid g && g.Children.Count > 0 && g.Children[0] is IDisposable currentchar)
            {
                currentchar.Dispose();
            }
        }

        /// <summary>
        /// Activate
        /// </summary>
        /// <param name="myChar"></param>
        /// <returns></returns>
        public IEnumerable<SubMenuAction> Activate(CharHolder myChar)
        {
            if (Content is Grid G)
            {
                G.Children.Add(new GCharHolder(myChar) { Margin = 0, Padding = 0, BackgroundColor = Color.Transparent });
                return new[] {
                    new SubMenuAction(UiResources.Save,"\xf0c7",new Command(()=> myChar.SetSaveTimerTo(0, true))),
                    new SubMenuAction(UiResources.SaveAtCurrentPlace,"\xf56f",new Command(()=> SaveIntern(myChar))),
                    new SubMenuAction(UiResources.SaveExtern,"\xf56e",new Command(()=> SaveExtern(myChar))),
                    new SubMenuAction(UiResources.OpenFolder,"\xf07c",new Command(()=> SharedIO.CurrentIO.OpenFolder(myChar.FileInfo.Directory))),
                    new SubMenuAction(UiResources.CharSettings,"\xf4fe",new Command(()=> Log.Write("Not yet implemented", true))),
                    new SubMenuAction(UiResources.Repair,"\xf6e3",new Command(()=> myChar.Repair())),
                    new SubMenuAction(UiResources.Unload,"\xf235",new Command(()=>{ AppModel.Instance?.RemoveMainObject(myChar); AppModel.Instance.RequestNavigation(ProjectPages.Administration);})),
                };
            }
            return Array.Empty<SubMenuAction>();
        }

        private static async void SaveExtern(CharHolder myChar)
        {
            try
            {
                SharedIO.Save(myChar, new FileInfo(Path.Combine((await SharedIO.CurrentIO.PickFolder()).FullName, myChar.FileInfo.Name)));
            }
            catch (Exception)
            {
                //TODO Handle
            }
        }

        private static void SaveIntern(CharHolder myChar)
        {
            try
            {
                SharedIO.SaveAtCurrentPlace(myChar);
                //TODO user notification. entweder hier oder gleich in die methode
            }
            catch (Exception)
            {
                //TODO Handle
            }
        }

        /// <summary>
        /// return true if handled
        /// </summary>
        /// <returns></returns>
        internal bool OnBackButtonPressed()
        {
            if (Content is Grid g && g.Children.Count > 0 && g.Children[0] is GCharHolder currentchar)
            {
                return currentchar.OnBackButtonPressed();
            }
            return false;
        }
    }
}