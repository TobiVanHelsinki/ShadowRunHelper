using System.IO;

namespace SharedCode.IO
{
    public class TestIO
    {
        public static void Do(string Path)
        {
            return;
            var Folder = new DirectoryInfo(Path);
            if (Folder.Exists)
            {
                var sub = Folder.CreateSubdirectory("Char_Store");
                if (sub.Exists)
                {
                    var file = new FileInfo(sub.FullName + @"\XXX.SRHChar");
                    using (var w = file.CreateText())
                    {
                        w.WriteLine("Hallo du da");
                    }
                }
            }
            Folder = Folder.Parent;
            if (Folder.Exists)
            {
                var sub = Folder.CreateSubdirectory("Char_Store");
                if (sub.Exists)
                {
                    var file = new FileInfo(sub.FullName + @"\XXX.SRHChar");
                    using (var w = file.CreateText())
                    {
                        w.WriteLine("Hallo du da");
                    }
                }
            }
        }
    }
}
