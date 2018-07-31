// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Buffers;
using WInterop.Windows.Native;

namespace WInterop.Windows.BufferWrappers
{
    public struct KeyNameTextWrapper : IBufferFunc<StringBuffer, uint>
    {
        public LPARAM LParam;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            return checked((uint)Imports.GetKeyNameTextW(LParam, buffer, (int)buffer.CharCapacity));
        }
    }
}
