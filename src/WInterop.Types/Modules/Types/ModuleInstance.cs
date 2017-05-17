// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.Types;

namespace WInterop.Modules.Types
{
    /// <summary>
    /// Module handle. This is synonymous with HINSTANCE and HMODULE.
    /// </summary>
    /// <remarks>
    /// Typically we try to avoid using the safe prefix, in this case
    /// ModuleHandle is a type that lives in System.
    /// </remarks>
    public class ModuleInstance : HandleZeroIsInvalid
    {
        public static ModuleInstance Null = new ModuleInstance(IntPtr.Zero);

        public ModuleInstance() : this(ownsHandle: false) { }

        protected ModuleInstance(bool ownsHandle) : base(ownsHandle) { }

        public ModuleInstance(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

        static public implicit operator ModuleInstance(IntPtr handle) => new ModuleInstance(handle);

        protected override bool ReleaseHandle()
        {
            // Module handles are ref counted with LoadLibrary/FreeLibrary.
            // We use a derived class to return a module handle from LoadLibary
            // that will decrement appropriately.
            return true;
        }
    }
}
