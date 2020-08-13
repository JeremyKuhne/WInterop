// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  Storage object formats. [STGFMT]
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380330.aspx"/>
    /// </summary>
    public enum StorageFormat : uint
    {
        /// <summary>
        ///  Compound file. [STGFMT_STORAGE]
        /// </summary>
        Storage = 0,

        // Not documented
        // STGFMT_NATIVE = 1,

        /// <summary>
        ///  NTFS IPropertySetStorage. [STGFMT_FILE]
        /// </summary>
        File = 3,

        /// <summary>
        ///  Allows the system to determine the right type. Not valid for creation.
        ///  [STGFMT_ANY]
        /// </summary>
        Any = 4,

        /// <summary>
        ///  Extended compound file- allows using <see cref="STGOPTIONS"/> when
        ///  creating storage objects. [STGFMT_DOCFILE]
        /// </summary>
        DocFile = 5
    }
}
