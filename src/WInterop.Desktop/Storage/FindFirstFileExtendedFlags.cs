// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx
    [Flags]
    public enum FindFirstFileExtendedFlags : uint
    {
        /// <summary>
        ///  [FIND_FIRST_EX_CASE_SENSITIVE]
        /// </summary>
        CaseSensitive = 1,

        /// <summary>
        ///  [FIND_FIRST_EX_LARGE_FETCH]
        /// </summary>
        LargeFetch = 2
    }
}
