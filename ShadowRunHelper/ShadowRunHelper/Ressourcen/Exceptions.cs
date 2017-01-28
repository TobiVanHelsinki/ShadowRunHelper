using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowRunHelper.Ressourcen
{
    class Exceptions
    {

    }

    //[System.Serializable]
    public class WrongTypeException : Exception
    {
        public WrongTypeException() { }
        public WrongTypeException(string message) : base(message) { }
        public WrongTypeException(string message, Exception inner) : base(message, inner) { }
        //    protected WrongTypeException(
        //      System.Runtime.Serialization.SerializationInfo info,
        //      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
