// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Buffers;
using WInterop.ProcessAndThreads.Native;

namespace WInterop.ProcessAndThreads.BufferWrappers
{
    public struct EnvironmentVariableWrapper : IBufferFunc<StringBuffer, uint>
    {
        public string Name;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            return Imports.GetEnvironmentVariableW(Name, buffer, buffer.CharCapacity);
        }
    }
}
