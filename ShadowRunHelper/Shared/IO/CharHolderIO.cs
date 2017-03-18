using System;
using ShadowRunHelper.Model;
using TLIB;
using TLIB.IO;
using ShadowRunHelper.IO;
using Newtonsoft.Json;
using ShadowRunHelper;

namespace Shared.IO
{
    class CharHolderIO : SharedIO<CharHolder>
    {
        static new CharHolder Convert(string strFileVersion, string fileContent)
        {
            CharHolder ReturnCharHolder;
            switch (strFileVersion)
            {
                case Constants.CHARFILE_VERSION_1_3:
                    ShadowRunHelper1_3.Controller.CharHolder CH1_3 = ShadowRunHelper1_3.IO.CharIO.JSON_to_Char(fileContent);
                    ReturnCharHolder = OldConverter.ConvertVersion1_3to1_5(CH1_3);
                    GC.Collect();
                    break;
                case Constants.CHARFILE_VERSION_1_5:
                    JsonSerializerSettings test = new JsonSerializerSettings()
                    {
                        Error = TLIB.IO.SharedIO<CharHolder>.SerializationErrorHandler,
                        PreserveReferencesHandling = PreserveReferencesHandling.All
                    };
                    ReturnCharHolder = JsonConvert.DeserializeObject<CharHolder>(fileContent, test);
                    break;
                default:
                    throw new Exception(ExceptionMessages.IO_DeserializeVersion_Error);
            }
            ReturnCharHolder.RefreshThingList();
            ReturnCharHolder.Repair();
            return ReturnCharHolder;
        }

    }
}
