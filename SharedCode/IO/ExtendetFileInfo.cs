//Author: Tobi van Helsinki

using System;
using System.IO;
using TLIB;

namespace ShadowRunHelper.IO
{
    public class ExtendetFileInfo
    {
        public FileInfo FileInfo;
        public string Name => FileInfo.Name;
        public string Path => FileInfo.Path();
        public string FullName => FileInfo.FullName;
        public DirectoryInfo Directory => FileInfo.Directory;
        public DateTime LastAccessTime { get; set; }
        public long Length { get; set; }

        public ExtendetFileInfo(string fullpath)
        {
            FileInfo = new FileInfo(fullpath);
        }

        public ExtendetFileInfo(string fileInfo, DateTime lastAccessTime, long length) : this(fileInfo)
        {
            LastAccessTime = lastAccessTime;
            Length = length;
        }

        public static implicit operator FileInfo(ExtendetFileInfo efi)
        {
            return efi.FileInfo;
        }

        public override string ToString()
        {
            return FileInfo.ToString();
        }
    }
}