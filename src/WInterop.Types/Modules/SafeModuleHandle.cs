// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Handles.DataTypes;

namespace WInterop.Modules.DataTypes
{
    public class SafeModuleHandle : SafeHandleZeroIsInvalid
    {
        public SafeModuleHandle() : base(ownsHandle: true)
        {
        }

        public SafeModuleHandle(IntPtr handle, bool ownsHandle) : base(ownsHandle)
        {
            this.handle = handle;
        }

        protected override bool ReleaseHandle()
        {
            Support.Internal.Imports.FreeLibrary(handle);
            handle = IntPtr.Zero;
            return true;
        }

        static public implicit operator ModuleHandle(SafeModuleHandle handle)
        {
            return new ModuleHandle(handle.handle);
        }
    }
}
