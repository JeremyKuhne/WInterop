// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles.DataTypes
{
    /// <summary>
    /// Simple struct to encapsulate a module handle (HMODULE). Use this for APIs that do NOT
    /// increment the reference count when creating a handle. Use SafeModuleHandle for those
    /// that DO increment the ref count.
    /// </summary>
    public struct ModuleHandle
    {
        public IntPtr HMODULE;

        public static ModuleHandle NullModuleHandle = new ModuleHandle(IntPtr.Zero);

        public ModuleHandle(IntPtr hmodule)
        {
            HMODULE = hmodule;
        }

        static public implicit operator IntPtr(ModuleHandle handle)
        {
            return handle.HMODULE;
        }

        static public implicit operator SafeModuleHandle(ModuleHandle handle)
        {
            return new SafeModuleHandle(handle.HMODULE, ownsHandle: false);
        }
    }
}
