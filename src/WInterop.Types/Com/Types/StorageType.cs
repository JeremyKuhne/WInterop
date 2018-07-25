// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Types
{
    /// <summary>
    /// Type of the storage element. [STGTY]
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/objidl/ne-objidl-tagstgty"/>
    /// </summary>
    public enum StorageType : uint
    {
        /// <summary>
        /// Element is a storage object. [STGTY_STORAGE]
        /// </summary>
        Storage = 1,

        /// <summary>
        /// Element is a stream object. [STGTY_STREAM]
        /// </summary>
        Stream = 2,

        /// <summary>
        /// Element is a byte array object. [STGTY_LOCKBYTES]
        /// </summary>
        LockBytes = 3,

        /// <summary>
        /// Element is a property storage object. [STGTY_PROPERTY]
        /// </summary>
        Property = 4
    }
}
