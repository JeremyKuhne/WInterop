// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633532(v=vs.85).aspx
    public enum LockCode : uint
    {
        /// <summary>
        ///  [LSFW_LOCK]
        /// </summary>
        Lock = 1,

        /// <summary>
        ///  [LSFW_UNLOCK]
        /// </summary>
        Unlock = 2
    }
}
