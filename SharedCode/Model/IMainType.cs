using Newtonsoft.Json;
using System;
using System.IO;
using TLIB;

namespace ShadowRunHelper.Model
{
    /// <summary>
    /// Interface for the Frameworks Main-Type.
    /// </summary>
    public interface IMainType
    {
        string APP_VERSION_NUMBER { get; } 
        string FILE_VERSION_NUMBER { get; }

        FileInfo FileInfo { get; set; }

        /// <summary>
        /// use this Event if you want to get this object saved
        /// </summary>
        event EventHandler<IMainType> SaveRequest;
        [JsonIgnore]
        bool HasChanges { get; set; }
    }
}
