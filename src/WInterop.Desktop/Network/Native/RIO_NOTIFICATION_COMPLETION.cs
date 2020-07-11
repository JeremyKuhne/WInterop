using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WInterop.Network.Native
{
    public enum RIO_NOTIFICATION_COMPLETION_TYPE
    {
        RIO_EVENT_COMPLETION,
        RIO_IOCP_COMPLETION
    }

    public delegate IntPtr LPFN_RIOCREATECOMPLETIONQUEUE(
        uint QueueSize,
        ref RIO_NOTIFICATION_COMPLETION NotificationCompletion);

    public struct RIO_NOTIFICATION_COMPLETION
    {
        public RIO_NOTIFICATION_COMPLETION_TYPE Type;
        public UnionType Union;

        [StructLayout(LayoutKind.Explicit)]
        public struct UnionType
        {
            [FieldOffset(0)]
            public EventCompletion EventCompletion;

            [FieldOffset(0)]
            public IocpCompletion IocpCompletion;
        }

        public struct EventCompletion
        {
            public IntPtr EventHandle;

            // Should use a BOOL enum like .NET does for clarity
            public int NotifyReset;
        }

        public struct IocpCompletion
        {
            public IntPtr IocpHandle;
            public IntPtr CompletionKey;
            public IntPtr Overlapped;
        }
    }
}
