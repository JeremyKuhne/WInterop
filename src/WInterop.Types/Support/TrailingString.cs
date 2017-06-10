// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Support
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
    public static class TrailingString
    {
        /// <summary>
        /// Helper wrapper.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct FirstChar
        {
            private char _firstChar;

            public unsafe string GetNullTerminatedString()
            {
                fixed (char* c = &_firstChar)
                    return new string(c);
            }

            public unsafe string GetValue(uint sizeInBytes)
            {
                fixed (char* c = &_firstChar)
                    return new string(c, 0, (int)(sizeInBytes / sizeof(char)));
            }
        }

        /// <summary>
        /// For null-terminated trailing strings that don't have a size.
        /// </summary>
        public struct Unsized
        {
            private FirstChar _firstChar;

            /// <summary>
            /// Get the string value by looking for a null terminator.
            /// </summary>
            public unsafe string Value => _firstChar.GetNullTerminatedString();

            /// <summary>
            /// Gets the string value of the specified byte size.
            /// </summary>
            public unsafe string GetValue(uint sizeInBytes) => _firstChar.GetValue(sizeInBytes);
        }

        /// <summary>
        /// For null-terminated trailing strings that start with a uint of the size in bytes.
        /// </summary>
        public struct SizedInBytes
        {
            public uint SizeInBytes;
            private FirstChar _firstChar;

            /// <summary>
            /// Gets the string value.
            /// </summary>
            public string Value => _firstChar.GetValue(SizeInBytes);
        }
    }
}
