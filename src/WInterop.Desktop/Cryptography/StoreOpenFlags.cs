// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Cryptography
{
    /// <summary>
    ///  From wincrypt.h
    /// </summary>
    [Flags]
    public enum StoreOpenFlags : uint
    {
        /// <summary>
        ///  [CERT_STORE_NO_CRYPT_RELEASE_FLAG]
        /// </summary>
        NoCryptRelease = 0x00000001,

        /// <summary>
        ///  [CERT_STORE_SET_LOCALIZED_NAME_FLAG]
        /// </summary>
        SetLocalizedName = 0x00000002,

        /// <summary>
        ///  [CERT_STORE_DEFER_CLOSE_UNTIL_LAST_FREE_FLAG]
        /// </summary>
        DeferCloseUntilLastFree = 0x00000004,

        /// <summary>
        ///  [CERT_STORE_DELETE_FLAG]
        /// </summary>
        Delete = 0x00000010,

        /// <summary>
        ///  [CERT_STORE_UNSAFE_PHYSICAL_FLAG]
        /// </summary>
        UnsafePhysical = 0x00000020,

        /// <summary>
        ///  [CERT_STORE_SHARE_STORE_FLAG]
        /// </summary>
        ShareStore = 0x00000040,

        /// <summary>
        ///  [CERT_STORE_SHARE_CONTEXT_FLAG]
        /// </summary>
        ShareContext = 0x00000080,

        /// <summary>
        ///  [CERT_STORE_MANIFOLD_FLAG]
        /// </summary>
        Manifold = 0x00000100,

        /// <summary>
        ///  [CERT_STORE_ENUM_ARCHIVED_FLAG]
        /// </summary>
        EnumArchived = 0x00000200,

        /// <summary>
        ///  [CERT_STORE_UPDATE_KEYID_FLAG]
        /// </summary>
        UpdateKeyId = 0x00000400,

        /// <summary>
        ///  [CERT_STORE_BACKUP_RESTORE_FLAG]
        /// </summary>
        BackupRestore = 0x00000800,

        /// <summary>
        ///  [CERT_STORE_READONLY_FLAG]
        /// </summary>
        ReadOnly = 0x00008000,

        /// <summary>
        ///  [CERT_STORE_OPEN_EXISTING_FLAG]
        /// </summary>
        OpenExisting = 0x00004000,

        /// <summary>
        ///  [CERT_STORE_CREATE_NEW_FLAG]
        /// </summary>
        CreateNew = 0x00002000,

        /// <summary>
        ///  [CERT_STORE_MAXIMUM_ALLOWED_FLAG]
        /// </summary>
        MaximumAllowed = 0x00001000,
    }
}
