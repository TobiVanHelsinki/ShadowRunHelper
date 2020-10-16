//Author: Tobi van Helsinki

using ShadowRunHelper.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TLIB;
using Xamarin.Essentials;

namespace ShadowRunHelperViewer.Platform.Xamarin
{
    public class IO : StandardIO, IPlatformIO
    {
        public override async Task<string> LoadFileContent(FileInfo Info)
        {
            if (Cache.TryGetValue(Info.FullName, out var retval)) // use cached version
            {
                if (retval.CanRead)
                {
                    var ret = "";
                    using (var r = new StreamReader(retval))
                    {
                        ret = r.ReadToEnd();
                    }
                    return ret;
                }
                else
                {
                    throw new AccessViolationException("cannot read from cache stream");
                }
            }
            else
            {
                return await base.LoadFileContent(Info);
            }
        }

        public async override Task SaveFileContent(string saveChar, FileInfo Info)
        {
            if (Cache.TryGetValue(Info.FullName, out var retval)) // use cached version
            {
                //TODO Test with external files, opened with FileClick
                if (retval.CanRead)
                {
                    using (var r = new StreamWriter(retval))
                    {
                        r.Write(saveChar);
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("now saving: " + Info.FullName);
                await base.SaveFileContent(saveChar, Info);
            }
        }

        public async Task<DirectoryInfo> CreateFolder(DirectoryInfo Info)
        {
            var statusO = Info.Exists;
            Info.Create();
            var statusN = Info.Exists;
            return Info;
        }

        public async Task<string> GetCompleteInternPath(Place place)
        {
            string ret;
            switch (place)
            {
                case Place.Roaming:
                    ret = PCLStorage.FileSystem.Current.RoamingStorage?.Path ?? PCLStorage.FileSystem.Current.LocalStorage.Path;
                    break;
                case Place.Local:
                    ret = PCLStorage.FileSystem.Current.LocalStorage.Path;
                    break;
                case Place.Assets:
                    ret = FileSystem.AppDataDirectory; //TODO Test. Eventuell muss noch ein "assets" dran
                    //TODO aber vielleicht kann man assets auch durch app dir ersetzen, wäre sinnvoller
                    break;
                case Place.Temp:
                    ret = FileSystem.CacheDirectory;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return ret.LastOrDefault() == Path.DirectorySeparatorChar ? ret : ret + Path.DirectorySeparatorChar;
        }

        public Task<bool> OpenFolder(DirectoryInfo Info)
        {
            throw new NotImplementedException();
        }
        static Dictionary<string, Stream> Cache = new Dictionary<string, Stream>();

        private static void AddToCache(string key, Stream data)
        {
            if (Cache.Count > 10)
            {
                Cache.Remove(Cache.Keys.First());
            }
            Cache.Add(key, data);
        }

        /// <summary>
        /// PickFile
        /// </summary>
        /// <param name="lststrFileEndings"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        /// <exception cref="IsOKException"></exception>
        public async Task<FileInfo> PickFile(IEnumerable<string> lststrFileEndings, string Token = null)
        {
            throw new NotImplementedException();
            //try
            //{
            //    var result = await FilePicker.PickAsync(new PickOptions()
            //    {
            //        FileTypes = new FilePickerFileType(
            //            new Dictionary<DevicePlatform, IEnumerable<string>>
            //            {
            //                { DevicePlatform.Android, lststrFileEndings.Select(x=> "text/"+x)},
            //                { DevicePlatform.iOS, lststrFileEndings.Select(x=> "public"+x)},
            //                { DevicePlatform.UWP, lststrFileEndings },
            //            }),
            //        PickerTitle = "Select a file to open"
            //    });
            //    return await result.OpenReadAsync();
            //}
            //catch (Exception ex)
            //{
            //    throw new IsOKException("Error picking File", ex);
            //}
            //var fileData = await CrossFilePicker.Current.PickFile(); //TODO lädt pfad in flaschem format
            //if (fileData != null)
            //{
            //    var file = new FileInfo(Path.Combine(fileData.FilePath, fileData.FileName));
            //    AddToCache(file.FullName, fileData.GetStream());
            //    return file;
            //}
            //else
            //{
            //    throw new IsOKException();
            //}
        }

        public Task<DirectoryInfo> PickFolder(string Token = null)
        {
            //TODO test
            //Android Code
            //Intent intent = new Intent(Intent.ActionOpenDocumentTree);
            //intent.SetFlags(ActivityFlags.NewTask);
            //Application.Context.StartActivity(intent);
            throw new NotImplementedException();
        }

        public async Task<bool> HasAccess(FileInfo Path)
        {
            try
            {
                return Path.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> HasAccess(DirectoryInfo Path)
        {
            try
            {
                return Path.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}