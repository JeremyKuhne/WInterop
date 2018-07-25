// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Modules;
using WInterop.ProcessAndThreads.Types;
using WInterop.Support.Buffers;

namespace WInterop.Modules.BufferWrappers
{
    public struct ModuleFileNameWrapper : IBufferFunc<StringBuffer, uint>
    {
        public ModuleInstance Module;
        public SafeProcessHandle Process;

        uint IBufferFunc<StringBuffer, uint>.Func(StringBuffer buffer)
        {
            if (Process == null)
                return ModuleMethods.Imports.GetModuleFileNameW(Module, buffer, buffer.CharCapacity);
            else
                return ModuleMethods.Imports.K32GetModuleFileNameExW(Process, Module, buffer, buffer.CharCapacity);
        }
    }
}
