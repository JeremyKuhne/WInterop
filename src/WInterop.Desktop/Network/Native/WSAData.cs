// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Network.Native
{
    /// <summary>
    ///  <see cref="https://docs.microsoft.com/windows/win32/api/winsock/ns-winsock-wsadata"/>
    /// </summary>
    /// <remarks>
    ///  This is an awkward struct to represent as the layout changes depending on the architecture.
    ///  While the union approach isn't technically correct, it give the correct layout and be slightly
    ///  larger on 64 bit as the "32 bit" version won't align it's pointer tightly.
    /// </remarks>
    public unsafe struct WSAData
    {
        private const int WSADESCRIPTION_LEN = 256;
        private const int WSASYS_STATUS_LEN = 128;

        public UnionType Union;

        // It doesn't really matter which union arm we hit for the versions.
        public ushort wVersion => Union.WSAData64.wVersion;
        public ushort wHighVersion => Union.WSAData32.wHighVersion;

        public ReadOnlySpan<char> szDescription
        {
            get
            {
                char* description;
                if (Environment.Is64BitProcess)
                {
                    fixed (char* c = Union.WSAData64.szDescription)
                    {
                        description = c;
                    }
                }
                else
                {
                    fixed (char* c = Union.WSAData32.szDescription)
                    {
                        description = c;
                    }
                }

                return new ReadOnlySpan<char>(description, WSADESCRIPTION_LEN).SliceAtNull();
            }
        }

        public ReadOnlySpan<char> szSystemStatus
        {
            get
            {
                char* description;
                if (Environment.Is64BitProcess)
                {
                    fixed (char* c = Union.WSAData64.szSystemStatus)
                    {
                        description = c;
                    }
                }
                else
                {
                    fixed (char* c = Union.WSAData32.szSystemStatus)
                    {
                        description = c;
                    }
                }

                return new ReadOnlySpan<char>(description, WSASYS_STATUS_LEN).SliceAtNull();
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct UnionType
        {
            [FieldOffset(0)]
            public WSAData64 WSAData64;

            [FieldOffset(0)]
            public WSAData32 WSAData32;
        }

        public struct WSAData64
        {
            public ushort wVersion;
            public ushort wHighVersion;
            public ushort iMaxSockets;
            public ushort iMaxUdpDg;
            public IntPtr lpVendorInfo;
            public fixed char szDescription[WSADESCRIPTION_LEN + 1];
            public fixed char szSystemStatus[WSASYS_STATUS_LEN + 1];
        }

        public struct WSAData32
        {
            public ushort wVersion;
            public ushort wHighVersion;
            public fixed char szDescription[WSADESCRIPTION_LEN + 1];
            public fixed char szSystemStatus[WSASYS_STATUS_LEN + 1];
            public ushort iMaxSockets;
            public ushort iMaxUdpDg;
            public IntPtr lpVendorInfo;
        }
    }
}