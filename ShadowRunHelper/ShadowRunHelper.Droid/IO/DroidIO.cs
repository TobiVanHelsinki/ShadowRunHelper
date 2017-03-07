using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShadowRunHelper.IO
{
    internal class DroidIO : IGeneralIO
    {

        public void Save(string saveChar, Place ePlace, string strSaveName = "", string strSavePath = "")
        {
            throw new NotImplementedException();
        }

        public void Remove(string delChar, Place ePlace, string strSaveName = "", string strSavePath = "")
        {
            throw new NotImplementedException();
        }

        public Task<string> Load(Place ePlace, string strSaveName = "", string strSavePath = "", List<string> FileTypes = null, IO.UserDecision eUD = IO.UserDecision.AskUser)
        {
            throw new NotImplementedException();
        }
    }
}