// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Handles.DataTypes;

namespace WInterop.Desktop.Registry.DataTypes
{
    public class RegistryKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public RegistryKeyHandle() : this (ownsHandle: true)
        {
        }

        public RegistryKeyHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        private RegistryKeyHandle(uint predefined) : this(ownsHandle: false)
        {
            handle = (IntPtr)predefined;
        }

        public static RegistryKeyHandle HKEY_CLASSES_ROOT = new RegistryKeyHandle(0x80000000);
        public static RegistryKeyHandle HKEY_CURRENT_USER = new RegistryKeyHandle(0x80000001);
        public static RegistryKeyHandle HKEY_LOCAL_MACHINE = new RegistryKeyHandle(0x80000002);
        public static RegistryKeyHandle HKEY_USERS = new RegistryKeyHandle(0x80000003);
        public static RegistryKeyHandle HKEY_PERFORMANCE_DATA = new RegistryKeyHandle(0x80000004);
        public static RegistryKeyHandle HKEY_PERFORMANCE_TEXT = new RegistryKeyHandle(0x80000050);
        public static RegistryKeyHandle HKEY_PERFORMANCE_NLSTEXT = new RegistryKeyHandle(0x80000060);
        public static RegistryKeyHandle HKEY_CURRENT_CONFIG = new RegistryKeyHandle(0x80000005);
        public static RegistryKeyHandle HKEY_DYN_DATA = new RegistryKeyHandle(0x80000006);
        public static RegistryKeyHandle HKEY_CURRENT_USER_LOCAL_SETTINGS = new RegistryKeyHandle(0x80000007);

        protected override bool ReleaseHandle()
        {
            return RegistryDesktopMethods.Direct.RegCloseKey(handle) != WindowsError.ERROR_SUCCESS;
        }
    }
}
