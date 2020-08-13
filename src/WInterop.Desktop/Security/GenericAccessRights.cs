// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Security
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa446632.aspx">Generic access rights</a>.
    ///  These are rights flags that are internally mapped to standard and specific rights by the relevant object
    ///  manager provider.
    /// </summary>
    [Flags]
    public enum GenericAccessRights : uint
    {
        /// <summary>
        ///  [GENERIC_READ]
        /// </summary>
        Read = 0x80000000,

        /// <summary>
        ///  [GENERIC_WRITE]
        /// </summary>
        Write = 0x40000000,

        /// <summary>
        ///  [GENERIC_EXECUTE]
        /// </summary>
        Execute = 0x20000000,

        /// <summary>
        ///  [GENERIC_ALL]
        /// </summary>
        All = 0x10000000
    }
}
