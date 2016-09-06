using System;

namespace UPnP.Models
{
    public class UPnPException : Exception
    {
        internal UPnPException(int error)
        {
            ErrorCode = error;
        }

        public int ErrorCode { get; private set; }

        public override string Message
        {
            get { return "UPnP Error " + ErrorCode; }
        }
    }
}