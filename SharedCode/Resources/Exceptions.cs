//Author: Tobi van Helsinki

using System;
using SharedCode.Resources;

namespace ShadowRunHelper
{
    public class IO_FileVersion : Exception
    {
        internal static string msg = AppResources.Error_IO_FileVersion;

        public IO_FileVersion(Exception ex = null) : base(msg, ex)
        { }
    }

    public class IO_UserDecision : Exception
    {
        internal static string msg = AppResources.Error_IO_UserDecision;

        public IO_UserDecision(Exception ex = null) : base(msg, ex)
        { }
    }

    public class IO_FolderNotFoundOrNotCreated : Exception
    {
        internal static string msg = AppResources.Error_IO_FolderNotFoundOrNotCreated;

        public IO_FolderNotFoundOrNotCreated(Exception ex = null) : base(msg, ex)
        { }
    }

    public class AllListChooserError : Exception
    {
        internal static string msg = AppResources.Error_AllListChooser;

        public AllListChooserError(Exception ex = null) : base(msg, ex)
        { }
    }

    public class IO_Deserialize : Exception
    {
        internal static string msg = AppResources.Error_IO_Deserialize;

        public IO_Deserialize(Exception ex = null) : base(msg, ex)
        { }
    }

    public class Enum : Exception
    {
        internal static string msg = AppResources.Error_Enum;

        public Enum(Exception ex = null) : base(msg, ex)
        { }
    }
}