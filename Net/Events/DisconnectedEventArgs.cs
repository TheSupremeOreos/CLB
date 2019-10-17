using System;

namespace PixelCLB.Net.Events
{
    public sealed class DisconnectedEventArgs : EventArgs
    {
        public DisconnectType Reason { get; private set; }
        public string Exception { get; private set; }

        public DisconnectedEventArgs(DisconnectType pDCType)
        {
            Reason = pDCType;
        }

        public DisconnectedEventArgs(string pExcetion)
        {
            Exception = pExcetion;
            Reason = DisconnectType.Exception;
        }
    }
}
