// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Buffers;

namespace WInterop.Authentication
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa380518.aspx
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct UNICODE_STRING
    {
        /// <summary>
        /// Length, in bytes, not including the the null, if any.
        /// </summary>
        public ushort Length;

        /// <summary>
        /// Max size of the buffer in bytes
        /// </summary>
        public ushort MaximumLength;

        public char* Buffer;

        /// <summary>
        /// Set to the current values for the given StringBuffer. Any changes to the buffer will NOT be reflected,
        /// call the method again for any updates.
        /// </summary>
        public void UpdateFromStringBuffer(StringBuffer buffer)
        {
            Length = checked((ushort)(buffer.Length * sizeof(char)));
            MaximumLength =checked((ushort)buffer.ByteCapacity);
            Buffer = buffer.CharPointer;
        }

        /// <summary>
        /// Set to the current values for the given StringBuffer. Any changes to the buffer will NOT be reflected,
        /// call the UpdateFromStringBuffer method for any updates.
        /// </summary>
        public UNICODE_STRING(StringBuffer buffer)
        {
            Length = checked((ushort)(buffer.Length * sizeof(char)));
            MaximumLength = checked((ushort)buffer.ByteCapacity);
            Buffer = buffer.CharPointer;
        }
    }
}
