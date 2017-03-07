using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShadowRunHelper.IO
{
    internal enum UserDecision
    {
        AskUser = 0,
        ThrowError = 1
    }

    interface IGeneralIO
    {
        /// <summary>
        /// Save a string to the specified target file
        /// </summary>
        /// <param name="saveChar"></param>
        /// <param name="ePlace"></param>
        /// <param name="strSaveName"></param>
        /// <param name="strSavePath"></param>
        void Save(string saveChar, Place ePlace, string strSaveName = "", string strSavePath = "");

        /// <summary>
        /// Load a string from the specified target file
        /// </summary>
        /// <param name="delChar"></param>
        /// <param name="ePlace"></param>
        /// <param name="strSaveName"></param>
        /// <param name="strSavePath"></param>
        void Remove(string delChar, Place ePlace, string strSaveName = "", string strSavePath = "");

        /// <summary>
        /// Remove the specified target file
        /// </summary>
        /// <param name="ePlace"></param>
        /// <param name="strSaveName"></param>
        /// <param name="strSavePath"></param>
        /// <param name="FileTypes"></param>
        /// <param name="eUD"></param>
        /// <returns></returns>
        Task<string> Load(Place ePlace, string strSaveName = "", string strSavePath = "", List<string> FileTypes = null, UserDecision eUD = UserDecision.AskUser);
    }
}
