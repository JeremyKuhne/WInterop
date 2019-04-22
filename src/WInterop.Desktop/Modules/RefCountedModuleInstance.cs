// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Modules.Native;

namespace WInterop.Modules
{
    /// <summary>
    /// Use this ModuleHandle when you want to decrement the module ref count when disposed.
    /// </summary>
    public class RefCountedModuleInstance : ModuleInstance
    {
        public RefCountedModuleInstance() : base(ownsHandle: true) { }

        public RefCountedModuleInstance(IntPtr handle) : base(handle, ownsHandle: true) { }

        protected override bool ReleaseHandle()
        {
            // Supported in store apps
            Imports.FreeLibrary(handle);
            handle = IntPtr.Zero;
            return true;
        }
    }
}
