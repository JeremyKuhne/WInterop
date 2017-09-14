// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Support
{
    public static class Pointers
    {
        /// <summary>
        /// Offset the given pointer by the specified number of bytes.
        /// </summary>
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern unsafe void* Offset<T>(void* pointer, T offset) where T : struct;
    }
}
