// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Buffers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Checked helpers for reading data from a NativeBuffer. Use StreamBuffer for more complicated read operations.
    /// </summary>
    /// <remarks>
    /// Didn't use extension methods to avoid dirtying the usage of derived classes. We don't want to encourage accidentally
    /// grabbing strings for StringBuffer using these methods, for example.
    /// </remarks>
    public static class NativeBufferReader
    {
        unsafe public static string ReadString(NativeBuffer buffer, ulong byteOffset, int charCount)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (byteOffset > buffer.ByteCapacity) throw new ArgumentOutOfRangeException(nameof(byteOffset));
            if ((uint)charCount * sizeof(char) > buffer.ByteCapacity - byteOffset) throw new ArgumentOutOfRangeException(nameof(charCount));

            if (charCount == 0) return string.Empty;

            return new string((char*)((byte*)buffer.DangerousGetHandle() + byteOffset), startIndex: 0, length: charCount);
        }

        unsafe public static int ReadInt(NativeBuffer buffer, ulong byteOffset)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (buffer.ByteCapacity < sizeof(int) || byteOffset > buffer.ByteCapacity - sizeof(int)) throw new ArgumentOutOfRangeException(nameof(byteOffset));

            byte* address = (byte*)buffer.DangerousGetHandle() + byteOffset;
            if ((unchecked((int)address) & 0x3) == 0)
            {
                // aligned read
                return *((int*)address);
            }
            else
            {
                // unaligned read
                int value;
                byte* valuePointer = (byte*)&value;
                valuePointer[0] = address[0];
                valuePointer[1] = address[1];
                valuePointer[2] = address[2];
                valuePointer[3] = address[3];
                return value;
            }
        }
    }
}
