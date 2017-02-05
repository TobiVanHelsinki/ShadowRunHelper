using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.Model
{
    public class Notification
    {
        public string strMessage;
        public bool bIsRead;
        public Exception ThrownExceptiom;

        public Notification(string istrMessage, Exception iExeption = null)
        {
            strMessage = istrMessage;
            ThrownExceptiom = iExeption;
        }

    }
}
