// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop.Support
{
    public static class Structs
    {
        /// <summary>
        /// Returns the address of the given target struct.
        /// </summary>
        public static unsafe T* AddressOf<T>(ref T target) where T : unmanaged
        {
            fixed (T* p = &target)
            {
                return p;
            }
        }

        /// <summary>
        /// Get the size (in bytes) of the given struct type.
        /// </summary>
        public static unsafe uint SizeInBytes<T>() where T : unmanaged
        {
            return (uint)sizeof(T);
        }

        /// <summary>
        /// Get the size (in bytes) of the given struct.
        /// </summary>
        public static unsafe uint SizeInBytes<T>(ref T target) where T : unmanaged
        {
            return (uint)sizeof(T);
        }

        /// <summary>
        /// Helper method to skip initialization of a struct.
        /// </summary>
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern unsafe void NoInit<T>(out T target) where T : struct;
    }
}
