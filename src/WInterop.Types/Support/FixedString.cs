// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Support
{
    /// <summary>
    /// Contains definitions for various fixed size strings for creating blittable
    /// structs. Provides easy string property access. Set strings are always null
    /// terminated and will truncate if too large.
    /// 
    /// Usage: Instead of "fixed char _buffer[12]" use "FixedBuffer.Size12 _buffer"
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct FixedString
    {
        // We cant derive structs in C#, this is the next best thing. Nested
        // class/struct defines have visibility to the nesting class/struct
        // privates, and given the pointer manipulation we must do we can
        // leverage it for a similar effect. It isn't perfect, but it reduces
        // copying of the riskier blocks of code.

        private const int BaseSize = 1;
        private unsafe fixed char _buffer[BaseSize];

        private string GetString(int maxSize)
        {
            fixed (char* c = _buffer)
            {
                // Go to null or end of buffer
                int end = 0;
                while (end < maxSize && c[end] != (char)0) end++;
                return new string(c, 0, end);
            }
        }

        private void SetString(string value, int maxSize)
        {
            fixed (char* c = _buffer)
                Strings.StringToBuffer(value, c, maxSize - 1, nullTerminate: true);
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size12
        {
            private const int Size = 12;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size16
        {
            private const int Size = 16;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size32
        {
            private const int Size = 32;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size64
        {
            private const int Size = 64;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size128
        {
            private const int Size = 128;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size256
        {
            private const int Size = 256;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct Size260
        {
            private const int Size = 260;
            private FixedString _buffer;
            private unsafe fixed char _bufferExtension[Size - BaseSize];

            public string Value
            {
                get => _buffer.GetString(Size);
                set => _buffer.SetString(value, Size);
            }
        }
    }
}
