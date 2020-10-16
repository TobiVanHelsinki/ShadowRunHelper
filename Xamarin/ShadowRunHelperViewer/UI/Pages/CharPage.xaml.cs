//Author: Tobi van Helsinki

using ShadowRunHelper;
using ShadowRunHelper.IO;
using ShadowRunHelper.Model;
using SharedCode.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ShadowRunHelperViewer.UI;
using ShadowRunHelperViewer.UI.ControlsChar;
using ShadowRunHelperViewer.UI.Pages;
using ShadowRunHelperViewer.UI.Resources;
using TLIB;
using Xamarin.Forms;

namespace ShadowRunHelperViewer
{
    public partial class CharPage : ContentView, IDisposable, IBackButton
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
        public IEnumerable<SubMenuAction> Activate(ProjectPagesOptions pageOptions, CharHolder myChar)
        {
            if (Content is Grid G)
            {
                GCharHolder gChar = new GCharHolder(myChar) { Margin = 0, Padding = 0, BackgroundColor = Color.Transparent };
                G.Children.Add(gChar);
                gChar.AfterLoad(pageOptions);
                return new[] {
                    new SubMenuAction(UiResources.Save,"\xf0c7",new Command(()=> myChar.SetSaveTimerTo(0, true))),
                    new SubMenuAction(UiResources.SaveAtCurrentPlace,"\xf56f",new Command(()=> SaveIntern(myChar))),
                    new SubMenuAction(UiResources.SaveExtern,"\xf56e",new Command(()=> SaveExtern(myChar))),
                    new SubMenuAction(UiResources.OpenFolder,"\xf07c",new Command(()=> SharedIO.CurrentIO.OpenFolder(myChar.FileInfo.Directory))),
                    new SubMenuAction(UiResources.CharSettings,"\xf4fe",new Command(()=> _ = RgPopUp.DisplayDefaultPopUp(new CharSettingsView(myChar)))),
                    new SubMenuAction(UiResources.SubtractLifeStyleCost,"\xf155",new Command(()=>  myChar.SubtractLifeStyleCost())),
                    new SubMenuAction(UiResources.Repair,"\xf6e3",new Command(()=> myChar.Repair())),
                    new SubMenuAction(UiResources.Close,"\xf235",new Command(()=>{ AppModel.Instance?.RemoveMainObject(myChar); AppModel.Instance.RequestNavigation(ProjectPages.Administration);})),
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
                Log.Write(AppResources.Error_FileExportFail, true);
            }
        }

        private static void SaveIntern(CharHolder myChar)
        {
            try
            {
                SharedIO.SaveAtCurrentPlace(myChar);
                Log.Write(AppResources.Info_Success_Import, true);
            }
            catch (Exception)
            {
                Log.Write(AppResources.Error_Import, true);
            }
        }

        /// <summary>
        /// return true if handled
        /// </summary>
        /// <returns></returns>
        public bool OnBackButtonPressed()
        {
            if (Content is Grid g && g.Children.Count > 0 && g.Children[0] is GCharHolder currentchar)
            {
                return currentchar.OnBackButtonPressed();
            }
            return false;
        }
    }
}