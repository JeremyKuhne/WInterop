﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ProcessAndThreads;
using WInterop.Support.Buffers;
using WInterop.Modules.Unsafe;

namespace WInterop.Modules.BufferWrappers
{
    public struct ModuleFileNameWrapper : IBufferFunc<StringBuffer, uint>
    {
        public ModuleInstance Module;
        public SafeProcessHandle Process;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            if (Process == null)
                return Imports.GetModuleFileNameW(Module, buffer, buffer.CharCapacity);
            else
                return Imports.K32GetModuleFileNameExW(Process, Module, buffer, buffer.CharCapacity);
        }
    }
}
