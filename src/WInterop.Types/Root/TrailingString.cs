// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop
{
    /// <summary>
    /// It is a pretty common pattern to have a trailing string on a native struct
    /// defined like WCHAR FileName[1]; Using these structs will allow easier access
    /// to the string value.
    /// </summary>
    /// <remarks>
    /// Accessing the string values is only safe when you have a pointer to the
    /// containing struct in a buffer. If you have an actual struct (Foo, not Foo*),
    /// the trailing characters will have been truncated as they aren't actually
    /// part of the struct.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TrailingString
    {
        private char _firstChar;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe ReadOnlySpan<char> GetBuffer(uint sizeInBytes)
        {
            fixed (char* c = &_firstChar)
                return new ReadOnlySpan<char>(c, (int)(sizeInBytes / sizeof(char)));
        }
    }
}
