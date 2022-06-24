// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support.Buffers;

namespace WInterop.SafeString.Native;

// https://docs.microsoft.com/windows/win32/api/subauth/ns-subauth-unicode_string
// https://docs.microsoft.com/windows/win32/api/ntdef/ns-ntdef-_unicode_string
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public unsafe struct UNICODE_STRING
{
    /// <summary>
    ///  Length, in bytes, not including the the null, if any.
    /// </summary>
    public ushort Length;

    /// <summary>
    ///  Max size of the buffer in bytes
    /// </summary>
    public ushort MaximumLength;

    public char* Buffer;

    /// <summary>
    ///  Set to the current values for the given StringBuffer. Any changes to the buffer will NOT be reflected,
    ///  call the method again for any updates.
    /// </summary>
    public void UpdateFromStringBuffer(StringBuffer buffer)
    {
        Length = checked((ushort)(buffer.Length * sizeof(char)));
        MaximumLength = checked((ushort)buffer.ByteCapacity);
        Buffer = buffer.CharPointer;
    }

    /// <summary>
    ///  Set to the current values for the given StringBuffer. Any changes to the buffer will NOT be reflected,
    ///  call the UpdateFromStringBuffer method for any updates.
    /// </summary>
    public UNICODE_STRING(StringBuffer buffer)
    {
        Length = checked((ushort)(buffer.Length * sizeof(char)));
        MaximumLength = checked((ushort)buffer.ByteCapacity);
        Buffer = buffer.CharPointer;
    }

    /// <summary>
    ///  Initialize the content of the string based on a fixed char from the given source string.
    /// </summary>
    public unsafe UNICODE_STRING(char* buffer, int lengthInChars)
    {
        Length = checked((ushort)(lengthInChars * sizeof(char)));
        MaximumLength = Length;
        Buffer = buffer;
    }

    /// <summary>
    ///  Returns the current buffer as a string.
    /// </summary>
    public override unsafe string ToString()
        => Length == 0 ? string.Empty : new string(Buffer, 0, Length / sizeof(char));
}