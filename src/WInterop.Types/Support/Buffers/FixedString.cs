// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Contains definitions for various fixed size strings for creating blittable
    /// structs. Provides easy string property access. Set strings are always null
    /// terminated and will truncate if too large.
    /// 
    /// Usage: Instead of "fixed char _buffer[12]" use "FixedBuffer.Size12 _buffer"
    /// </summary>
    public unsafe struct FixedString
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size12
        {
            private const int Size = 12;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size14
        {
            private const int Size = 14;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size16
        {
            private const int Size = 16;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size32
        {
            private const int Size = 32;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size64
        {
            private const int Size = 64;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size128
        {
            private const int Size = 128;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size256
        {
            private const int Size = 256;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size260
        {
            private const int Size = 260;
            private unsafe fixed char _buffer[Size];

            public Span<char> Buffer { get { fixed (char* c = _buffer) return new Span<char>(c, Size); } }
        }
    }
}
