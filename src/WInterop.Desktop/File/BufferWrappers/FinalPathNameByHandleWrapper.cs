// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.File.Types;
using WInterop.Support.Buffers;

namespace WInterop.File.BufferWrappers
{
    public struct FinalPathNameByHandleWrapper : IBufferFunc<StringBuffer, uint>
    {
        public SafeFileHandle FileHandle;
        public GetFinalPathNameByHandleFlags Flags;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            return FileMethods.Imports.GetFinalPathNameByHandleW(FileHandle, buffer, buffer.CharCapacity, Flags);
        }
    }
}
