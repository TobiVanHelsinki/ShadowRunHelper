//Author: Tobi van Helsinki

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TLIB;

namespace ShadowRunHelper.IO
{
    public class StandardIO
    {
        public virtual async Task<IEnumerable<ExtendetFileInfo>> GetFiles(DirectoryInfo Info, IEnumerable<string> FileTypes = null)
        {
            return Info.GetFiles().Where(x => FileTypes != null ? FileTypes.Contains(x.Extension) : true).Select(x => new ExtendetFileInfo(x.FullName, x.LastWriteTime, x.Length));
        }

        #region Basic File Operations

        public virtual async Task RemoveFile(FileInfo Info)
        {
            Info.Delete();
        }

        public virtual async Task<FileInfo> Rename(FileInfo Source, string NewName)
        {
            var fileName = Source.Path() + NewName;
            Source.MoveTo(fileName);
            return new FileInfo(fileName);
        }

        public virtual async Task<FileInfo> CopyTo(FileInfo Source, FileInfo Target)
        {
            return Source.CopyTo(Target.FullName, true);
        }

        public virtual async Task MoveTo(FileInfo Source, FileInfo Target)
        {
            Source.MoveTo(Target.FullName);
        }

        #endregion Basic File Operations

        #region Multiple File Operations

        public async Task CopyAllFiles(DirectoryInfo Source, DirectoryInfo Target, IEnumerable<string> FileTypes = null)
        {
            foreach (var item in await GetFiles(Source, FileTypes))
            {
                try
                {
                    await CopyTo(item, new FileInfo(Target.Path() + item.Name));
                }
                catch (Exception ex)
                {
                    Log.Write("Could not CopyAllFiles from " + Source.ToString() + " to " + Target.ToString(), ex, logType: LogType.Error);
                }
            }
        }

        public async Task MoveAllFiles(DirectoryInfo Source, DirectoryInfo Target, IEnumerable<string> FileTypes = null)
        {
            foreach (var item in await GetFiles(Source, FileTypes))
            {
                try
                {
                    await MoveTo(item, new FileInfo(Target.Path() + item.Name));
                }
                catch (Exception ex)
                {
                    Log.Write("Could not MoveAllFiles from " + Source.ToString() + " to " + Target.ToString(), ex, logType: LogType.Error);
                }
            }
        }

        #endregion Multiple File Operations

        #region File Content

        public virtual async Task SaveFileContent(string saveChar, FileInfo Info)
        {
            if (!Info.Exists)
            {
                Info.Create();
            }
            File.Delete(Info.FullName);
            File.WriteAllText(Info.FullName, saveChar);
        }

        public virtual async Task<string> LoadFileContent(FileInfo Info)
        {
            return File.ReadAllText(Info.FullName);
        }

        #endregion File Content

        #region Helper

        public static string CorrectName(string name, bool ReplaceInsteadOfRemove = true)
        {
            var ReturnValue = "";
            foreach (var item in name)
            {
                if (item == '/' || item == '"' || item == '\\')
                {
                    if (ReplaceInsteadOfRemove)
                    {
                        ReturnValue += '_';
                    }
                }
                else
                {
                    ReturnValue += item;
                }
            }
            return ReturnValue;
        }

        //public virtual async Task<DirectoryInfo> CreateFoldersRecursive(DirectoryInfo Info, DirectoryInfo StartFolder)
        //{
        //    DirectoryInfo Folder;
        //    var path = Info.FullName.Replace(StartFolder.FullName, "");
        //    var folders = path.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
        //    Folder = StartFolder;
        //    foreach (var item in folders)
        //    {
        //        Folder = Folder.CreateSubdirectory(item);
        //    }
        //    return Folder;
        //}
        #endregion Helper
    }
}