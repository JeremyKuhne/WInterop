// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Buffers;
using WInterop.Windows;

namespace WInterop.Windows.BufferWrappers
{
    public struct ClassNameWrapper : IBufferFunc<StringBuffer, uint>
    {
        public WindowHandle Window;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            return checked((uint)WindowMethods.Imports.GetClassNameW(Window, buffer, (int)buffer.CharCapacity));
        }
    }
}
