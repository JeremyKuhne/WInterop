// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Support
{
    public static class Pointers
    {
        /// <summary>
        ///  Offset the given pointer by the specified number of bytes.
        /// </summary>
        public static unsafe void* Offset(void* pointer, int offset)
        {
            return (void*)(((byte*)pointer) + offset);
        }

        /// <summary>
        ///  Offset the given pointer by the specified number of bytes.
        /// </summary>
        public static unsafe void* Offset(void* pointer, uint offset)
        {
            return (void*)(((byte*)pointer) + offset);
        }

        /// <summary>
        ///  Offset the given pointer by the specified number of bytes.
        /// </summary>
        public static unsafe void* Offset(this IntPtr pointer, uint offset)
        {
            return (void*)(((byte*)pointer) + offset);
        }

        /// <summary>
        ///  Offset the given pointer by the specified number of bytes.
        /// </summary>
        public static unsafe void* Offset(this IntPtr pointer, ulong offset)
        {
            return (void*)(((byte*)pointer) + offset);
        }

        /// <summary>
        ///  Move the pointer forward by the size of <typeparamref name="T"/>.
        /// </summary>
        public static unsafe void* Offset<T>(void* pointer) where T : unmanaged
        {
            return (void*)(((byte*)pointer) + sizeof(T));
        }
    }
}
