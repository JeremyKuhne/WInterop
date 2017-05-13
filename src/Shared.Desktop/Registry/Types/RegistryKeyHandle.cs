// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling.Types;
using WInterop.Handles.Types;

namespace WInterop.Desktop.Registry.Types
{
    public class RegistryKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private const uint REMOTE_HANDLE_TAG       = 0x00000001;
        private const uint REG_CLASSES_SPECIAL_TAG = 0x00000002;

        public static RegistryKeyHandle HKEY_CLASSES_ROOT = new RegistryKeyHandle(0x80000000);
        public static RegistryKeyHandle HKEY_CURRENT_USER = new RegistryKeyHandle(0x80000001);
        public static RegistryKeyHandle HKEY_LOCAL_MACHINE = new RegistryKeyHandle(0x80000002);
        public static RegistryKeyHandle HKEY_USERS = new RegistryKeyHandle(0x80000003);
        public static RegistryKeyHandle HKEY_PERFORMANCE_DATA = new RegistryKeyHandle(0x80000004, isPerfKey: true);
        public static RegistryKeyHandle HKEY_PERFORMANCE_TEXT = new RegistryKeyHandle(0x80000050, isPerfKey: true);
        public static RegistryKeyHandle HKEY_PERFORMANCE_NLSTEXT = new RegistryKeyHandle(0x80000060, isPerfKey: true);
        public static RegistryKeyHandle HKEY_CURRENT_CONFIG = new RegistryKeyHandle(0x80000005);
        public static RegistryKeyHandle HKEY_DYN_DATA = new RegistryKeyHandle(0x80000006);
        public static RegistryKeyHandle HKEY_CURRENT_USER_LOCAL_SETTINGS = new RegistryKeyHandle(0x80000007);

        public RegistryKeyHandle() : this (ownsHandle: true)
        {
        }

        public RegistryKeyHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        private RegistryKeyHandle(uint predefined, bool isPerfKey = false) : this(ownsHandle: false)
        {
            // Need to cast to int first to avoid Overflow when
            // casting to IntPtr when running on 32 bit.
            handle = (IntPtr)(int)predefined;
            IsPerfKey = isPerfKey;
        }

        public bool IsPerfKey { get; private set; }

        /// <summary>
        /// Returns true if the key is from the local machine.
        /// </summary>
        public bool IsLocalKey => ((uint)handle & REMOTE_HANDLE_TAG) == 0;

        /// <summary>
        /// Returns true if the key is special (notably in HKEY_CLASSES_ROOT, where
        /// it might be redirected to per user settings).
        /// </summary>
        public bool IsSpecialKey => ((uint)handle & REG_CLASSES_SPECIAL_TAG) != 0;


        protected override bool ReleaseHandle()
        {
            return RegistryMethods.Imports.RegCloseKey(handle) != WindowsError.ERROR_SUCCESS;
        }
    }
}
