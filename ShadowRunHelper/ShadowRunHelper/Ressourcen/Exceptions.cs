using System;

namespace ShadowRunHelper
{
    internal static class ExceptionMessages
    {
        internal const string AllListChooser_AllList_Empty = "Error at AllList, List was empty";
        internal const string AllListChooser_AllList_Null = "Error at AllList, List was null";
        internal const string AllListChooser_Data_Null = "Error at AllList, data was null";
        internal const string IO_Deserialize_Error = "Something went wrong with des your char";
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
