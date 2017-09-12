// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Authorization.Types;

namespace WInterop.Registry.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724878.aspx
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa384129.aspx

    [Flags]
    public enum RegistryAccessRights : uint
    {
        KEY_QUERY_VALUE         = 0x0001,
        KEY_SET_VALUE           = 0x0002,
        KEY_CREATE_SUB_KEY      = 0x0004,
        KEY_ENUMERATE_SUB_KEYS  = 0x0008,
        KEY_NOTIFY              = 0x0010,
        KEY_CREATE_LINK         = 0x0020,

        /// <summary>
        /// Forces accessing the 32 bit view of the registry.
        /// </summary>
        KEY_WOW64_32KEY = 0x0200,

        /// <summary>
        /// Forces accessing the 64 bit view of the registry.
        /// </summary>
        KEY_WOW64_64KEY         = 0x0100,

        /// <summary>
        /// Mask for the WOW_64 options.
        /// </summary>
        KEY_WOW64_RES           = 0x0300,

        KEY_READ =
            StandardAccessRights.Read
            | KEY_QUERY_VALUE
            | KEY_ENUMERATE_SUB_KEYS
            | KEY_NOTIFY,
        KEY_WRITE =
            StandardAccessRights.Write
            | KEY_SET_VALUE
            | KEY_CREATE_SUB_KEY,
        KEY_EXECUTE = KEY_READ,
        KEY_ALL_ACCESS = StandardAccessRights.Required
            | KEY_QUERY_VALUE
            | KEY_SET_VALUE
            | KEY_CREATE_SUB_KEY
            | KEY_ENUMERATE_SUB_KEYS
            | KEY_NOTIFY
            | KEY_CREATE_LINK,

        /// <summary>
        /// The right to delete the object.
        /// </summary>
        DELETE = StandardAccessRights.Delete,

        /// <summary>
        /// The right to read the information in the object's security descriptor.
        /// Doesn't include system access control list info (SACL).
        /// </summary>
        READ_CONTROL = StandardAccessRights.ReadControl,

        /// <summary>
        /// The right to modify the discretionary access control list (DACL) in the
        /// object's security descriptor.
        /// </summary>
        WRITE_DAC = StandardAccessRights.WriteDac,

        /// <summary>
        /// The right to change the owner in the object's security descriptor.
        /// </summary>
        WRITE_OWNER = StandardAccessRights.WriteOwner,

        /// <summary>
        /// Internally maps to KEY_READ.
        /// </summary>
        GENERIC_READ = GenericAccessRights.Read,

        /// <summary>
        /// Internally maps to KEY_WRITE.
        /// </summary>
        GENERIC_WRITE = GenericAccessRights.Write,

        /// <summary>
        /// Internally maps to KEY_EXECUTE | KEY_CREATE_LINK.
        /// </summary>
        GENERIC_EXECUTE = GenericAccessRights.Execute,

        /// <summary>
        /// Internally maps to KEY_ALL_ACCESS.
        /// </summary>
        GENERIC_ALL = GenericAccessRights.All
    }
}
